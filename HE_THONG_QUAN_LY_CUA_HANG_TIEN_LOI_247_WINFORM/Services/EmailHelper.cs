using System;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Helper
{
    public class EmailHelper
    {
        // Cấu hình email của mày (người gửi)
        private static string _fromEmail = "khoi22092021@gmail.com";
        private static string _password = "voyz vznk xzvq hnxd";

        public static bool SendMail(string toEmail, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(_fromEmail);
                mail.To.Add(toEmail);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(_fromEmail, _password);

                smtp.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}