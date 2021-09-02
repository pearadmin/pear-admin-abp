namespace PearAdmin.AbpTemplate.AppProvider.Configuration.MailboxTemplates
{
    public class InviteMailboxTemplate
    {
        public static string DefaultTemplate() =>
            "<!DOCTYPE html>" +
            "<html lang='en'>" +
            "<head>" +
            "   <meta charset='UTF-8'>" +
            "   <title>{{email.Title}}</title>" +
            "   <meta name='viewport' content='width=device-width, initial-scale=1.0' />" +
            "</head>" +
            "<body style='margin: 0; padding: 0;'>" +
            "   <table align='center' border='0' cellpadding='0' cellspacing='0' width='600' style='border-collapse: collapse;'>" +
            "       <tr>" +
            "           <td>" +
            "               <div style='margin: 20px;text-align: center;margin-top: 50px;'>" +
            "                   <img src='{{email.Logo}}' border='0' style='display:block;width: 100%;height: 100 %; '>" +
            "               </div>" +
            "           </td>" +
            "       </tr>" +
            "       <tr>" +
            "           <td>" +
            "               <div style='border: #36649d 1px dashed;margin: 30px;padding: 20px'>" +
            "                   <label style='font-size: 22px;color: #36649d;font-weight: bold'>{{email.Subject}}</label>" +
            "                   <p style='font-size: 16px'>&nbsp;<label style='font-weight: bold'>{{email.UserName}}先生/女士</label>&nbsp; 您好！欢迎加入{{email.CompanyName}}。</p>" +
            "               </div>" +
            "           </td>" +
            "       </tr>" +
            "       <tr>" +
            "           <td>" +
            "               <div style='margin: 40px'>" +
            "                   <p style='color:red;font-size: 14px;'>（这是一封自动发送的邮件，请勿回复。）</p>" +
            "               </div>" +
            "           </td>" +
            "       </tr>" +
            "       <tr>" +
            "           <td>" +
            "               <div align='right' style='margin: 40px;border-top: solid 1px gray' id='bottomTime'>" +
            "                   <p style='margin -right: 20px;'>{{email.CompanyName}}</p> <label style='margin-right: 20px;'>{{email.SendTime}}</label>" +
            "               </div>" +
            "           </td>" +
            "       </tr>" +
            "   </table>" +
            "</body>" +
            "</html>";
    }
}
