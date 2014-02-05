using BeerTech.DataObjects;
using BeerTech.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace BeerTech.Utility
{
    public static class PasswordHelper
    {
        private static int _minExpires = 60;

        public static int MinutesToExpiration
        {
            get { return MinutesToExpiration = _minExpires; }
            set { _minExpires = MinutesToExpiration; }
        }

        public static bool SendResetEmail(User user, string baseUrl)
        {
            string encrypted = Encryption.Encrypt(String.Format("{0}&{1}", user.Email, DateTime.Now.AddMinutes(_minExpires).Ticks),
                ConfigurationManager.AppSettings["EncryptionKey"]);
            Uri uri = new Uri(baseUrl);
            var passwordLink = uri.GetLeftPart(UriPartial.Authority) + "/User/ResetPassword?digest=" + HttpUtility.UrlEncode(encrypted);

            var email = new MailMessage();

            email.From = new MailAddress("cbbeertech@gmail.com");
            email.To.Add(new MailAddress(user.Email));

            email.Subject = "Beer App Password Reset Request";
            email.IsBodyHtml = true;

            email.Body = "<p>A request has been recieved to reset your password. If you did not initiate the request, then please ignore this email.</p>" +
            "<p>Please click the following link to reset your password: <a href='" + passwordLink + "'>" + passwordLink + "</a></p>" +
            "<p>This link will expire within " + _minExpires + " minutes of your original request.</p>";

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
            smtpClient.Credentials = new System.Net.NetworkCredential
                 ("cbbeertech@gmail.com", "b33rt1M3!");
            //Or your Smtp Email ID and Password
            smtpClient.EnableSsl = true;

            try
            {
                smtpClient.Send(email);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static ResetPasswordParts ValidateResetCode(string encryptedParam)
        {
            string decrypted = "";
            var results = new ResetPasswordParts();

            try
            {
                decrypted = Encryption.Decrypt(encryptedParam, ConfigurationManager.AppSettings["EncryptionKey"]);
            }
            catch (Exception ex)
            {
                return results;
            }

            var parts = decrypted.Split('&');

            if(parts.Length != 2) return results;

            var expires = DateTime.Now.AddHours(-1);


            results.User = new UserRepository().LoadByEmail(parts[0]);
            if (results.User == null) return results;

            long ticks = 0;
            if(!long.TryParse(parts[1], out ticks)) return results;
            expires = new DateTime(ticks);
            results.Expires = expires;

            if (expires < DateTime.Now) return results;
            results.IsValid = true;

            return results;
        }
    }
}