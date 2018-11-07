namespace MockingBirdService.Emails
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Net.Mail;

    public class EmailAlets
    {
        public static bool SendEmail(string HTMLBody, string Subject, string EmailAddresses)
        {
            //string MailConfig = @Convert.ToString(@ConfigurationManager.AppSettings["ASMAConfigurationDirectory"]) + @"\OtherConfig\MailServer.asma";
            //string ProgramToRun = "";
            string MailAddressFrom = "test@Computershare.co.uk";

            MailMessage mail = new System.Net.Mail.MailMessage(MailAddressFrom, EmailAddresses);
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "mail.emea.cshare.net";
            mail.Subject = Subject;
            mail.Body = HTMLBody;
            mail.IsBodyHtml = true;
            client.Send(mail);

            return true;
        }
    }
}
