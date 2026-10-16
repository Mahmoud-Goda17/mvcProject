using System.Net;
using System.Net.Mail;

namespace Company.G01.PL.Service
{
    public static class EmailSetting
    {
        public static bool SendEmail(Email email)
        {
            try
            {
                var Client = new SmtpClient("smtp.gmail.com", 587);
                Client.EnableSsl = true;
                Client.Credentials = new NetworkCredential("mahmoudgoda322@gmail.com", "hmofevkyntietkkp");
                Client.Send("mahmoudgoda322@gmail.com", email.To, email.Subject, email.Body);
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }
    }
}
