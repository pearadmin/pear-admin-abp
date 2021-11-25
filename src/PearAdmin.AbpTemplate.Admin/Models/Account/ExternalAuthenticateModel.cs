namespace PearAdmin.AbpTemplate.Admin.Models.Account
{
    public class ExternalAuthenticateModel
    {
        public string AuthProvider { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderAccessCode { get; set; }
        public string ReturnUrl { get; set; }
    }
}
