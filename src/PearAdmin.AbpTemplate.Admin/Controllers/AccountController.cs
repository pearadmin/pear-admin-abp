using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.MultiTenancy;
using Abp.Notifications;
using Abp.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PearAdmin.AbpTemplate.Admin.Models.Account;
using PearAdmin.AbpTemplate.Authorization;
using PearAdmin.AbpTemplate.Authorization.Users;
using PearAdmin.AbpTemplate.ExternalAuth;
using PearAdmin.AbpTemplate.Identity;
using PearAdmin.AbpTemplate.MultiTenancy;
using PearAdmin.AbpTemplate.Sessions;

namespace PearAdmin.AbpTemplate.Admin.Controllers
{
    public class AccountController : AbpTemplateControllerBase
    {
        private readonly UserManager _userManager;
        private readonly TenantManager _tenantManager;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly AbpLoginResultTypeHelper _abpLoginResultTypeHelper;
        private readonly LogInManager _logInManager;
        private readonly SignInManager _signInManager;
        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly ISessionAppService _sessionAppService;
        private readonly ITenantCache _tenantCache;
        private readonly INotificationPublisher _notificationPublisher;
        private readonly IExternalAuthManager _externalAuthManager;

        public AccountController(
            UserManager userManager,
            IMultiTenancyConfig multiTenancyConfig,
            TenantManager tenantManager,
            IUnitOfWorkManager unitOfWorkManager,
            AbpLoginResultTypeHelper abpLoginResultTypeHelper,
            LogInManager logInManager,
            SignInManager signInManager,
            UserRegistrationManager userRegistrationManager,
            ISessionAppService sessionAppService,
            ITenantCache tenantCache,
            INotificationPublisher notificationPublisher,
            IExternalAuthManager externalAuthManager)
        {
            _userManager = userManager;
            _multiTenancyConfig = multiTenancyConfig;
            _tenantManager = tenantManager;
            _unitOfWorkManager = unitOfWorkManager;
            _abpLoginResultTypeHelper = abpLoginResultTypeHelper;
            _logInManager = logInManager;
            _signInManager = signInManager;
            _userRegistrationManager = userRegistrationManager;
            _sessionAppService = sessionAppService;
            _tenantCache = tenantCache;
            _notificationPublisher = notificationPublisher;
            _externalAuthManager = externalAuthManager;
        }

        #region Login / Logout

        /// <summary>
        /// 宿主登录
        /// </summary>
        /// <param name="userNameOrEmailAddress"></param>
        /// <param name="returnUrl"></param>
        /// <param name="successMessage"></param>
        /// <returns></returns>
        public ActionResult HostLogin(string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = GetAppHomeUrl();
            }

            ViewBag.TenantId = null;

            return View("Login", new LoginFormViewModel
            {
                ReturnUrl = returnUrl,
                IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled,
                IsSelfRegistrationAllowed = IsSelfRegistrationEnabled(),
                MultiTenancySide = AbpSession.MultiTenancySide
            });
        }

        /// <summary>
        /// 租户登录
        /// </summary>
        /// <param name="userNameOrEmailAddress"></param>
        /// <param name="returnUrl"></param>
        /// <param name="successMessage"></param>
        /// <returns></returns>
        public ActionResult Login(string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = GetAppHomeUrl();
            }

            ViewBag.TenantId = AbpTemplateApplicationConsts.DefaultTenantId;

            return View(new LoginFormViewModel
            {
                ReturnUrl = returnUrl,
                IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled,
                IsSelfRegistrationAllowed = IsSelfRegistrationEnabled(),
                MultiTenancySide = AbpSession.MultiTenancySide
            });
        }

        [HttpPost]
        [UnitOfWork]
        public virtual async Task<JsonResult> Login([FromBody] LoginViewModel loginModel)
        {
            loginModel.ReturnUrl = NormalizeReturnUrl(loginModel.ReturnUrl);
            if (!string.IsNullOrWhiteSpace(loginModel.ReturnUrlHash))
            {
                loginModel.ReturnUrl += loginModel.ReturnUrlHash;
            }

            var loginResult = await GetLoginResultAsync(loginModel.UsernameOrEmailAddress, loginModel.Password, GetTenancyNameOrNull());

            await _signInManager.SignInAsync(loginResult.Identity, loginModel.RememberMe);
            await UnitOfWorkManager.Current.SaveChangesAsync();

            return Json(new AjaxResponse { TargetUrl = GetAppHomeUrl() });
        }

        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync(string usernameOrEmailAddress, string password, string tenancyName)
        {
            var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
            }
        }

        private bool IsSelfRegistrationEnabled()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return false; // No registration enabled for host users!
            }

            return true;
        }
        #endregion

        #region External Login

        [HttpPost]
        [UnitOfWork]
        public async Task<JsonResult> ExternalLogin([FromBody] ExternalAuthenticateModel model)
        {
            using (AbpSession.Use(AbpTemplateApplicationConsts.DefaultTenantId, null))
            {
                var externalUser = await GetExternalUserInfo(model);
                var loginResult = await _logInManager.LoginAsync(new UserLoginInfo(model.AuthProvider, externalUser.ProviderKey, model.AuthProvider), GetTenancyNameOrNull());
                switch (loginResult.Result)
                {
                    case AbpLoginResultType.Success:
                        {
                            await _signInManager.SignInAsync(loginResult.Identity, true);
                            await UnitOfWorkManager.Current.SaveChangesAsync();
                            return Json(new AjaxResponse { TargetUrl = model.ReturnUrl });
                        }
                    case AbpLoginResultType.UnknownExternalLogin:
                        {
                            User user = null;
                            try
                            {

                                user = await _userRegistrationManager.RegisterAsync(
                                    externalUser.Name,
                                    externalUser.Surname,
                                    externalUser.EmailAddress,
                                    externalUser.EmailAddress.ToMd5(),
                                    Authorization.Users.User.CreateRandomPassword(),
                                    true
                                );
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }

                            user.Logins = new List<UserLogin>
                        {
                            new UserLogin
                            {
                                LoginProvider = externalUser.Provider,
                                ProviderKey = externalUser.ProviderKey,
                                TenantId = user.TenantId
                            }
                        };

                            await CurrentUnitOfWork.SaveChangesAsync();

                            // 为新用户执行登录
                            var tryLoginResult = await _logInManager.LoginAsync(new UserLoginInfo(model.AuthProvider, model.ProviderKey, model.AuthProvider), GetTenancyNameOrNull());

                            if (tryLoginResult.Result == AbpLoginResultType.Success)
                            {
                                await _signInManager.SignInAsync(loginResult.Identity, false);

                                return Json(new AjaxResponse { TargetUrl = model.ReturnUrl });
                            }

                            return Json(new AjaxResponse { TargetUrl = model.ReturnUrl });
                        }
                    default:
                        {
                            throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(
                                loginResult.Result,
                                model.ProviderKey,
                                GetTenancyNameOrNull()
                            );
                        }
                }
            }
        }

        private async Task<ExternalAuthUserInfo> GetExternalUserInfo(ExternalAuthenticateModel model)
        {
            var userInfo = await _externalAuthManager.GetUserInfo(model.AuthProvider, model.ProviderAccessCode);
            return userInfo;
        }
        #endregion

        #region Common

        public string GetAppHomeUrl()
        {
            return Url.Action("Index", "Home");
        }

        private string GetTenancyNameOrNull()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return null;
            }

            return _tenantCache.GetOrNull(AbpSession.TenantId.Value)?.TenancyName;
        }

        private string NormalizeReturnUrl(string returnUrl, Func<string> defaultValueBuilder = null)
        {
            if (defaultValueBuilder == null)
            {
                defaultValueBuilder = GetAppHomeUrl;
            }

            if (returnUrl.IsNullOrEmpty())
            {
                return defaultValueBuilder();
            }

            if (Url.IsLocalUrl(returnUrl))
            {
                return returnUrl;
            }

            return defaultValueBuilder();
        }

        #endregion
    }
}
