using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyt.Core.DAL.Repositories.Impl;
using Trackyt.Core.Services;
using Trackyt.Core.DAL.DataModel;

// v.1.0.2
// This application is not valid any more, since data model has been changed (no Password column in User any more)

namespace PasswordClosed.NotifyUsers.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Trackyt.net - upgrade for v.1.0.1.\nNotify all users about password remove.\n");

            try
            {
                // repositories
                var usersRepository = new UsersRepository();
                var credentialsRepository = new CredentialsRepository();

                // services
                var credentialService = new CredentialsService(credentialsRepository);
                var emailService = new EmailService(credentialService);

                SendEmailNotificationsToUsers(usersRepository, emailService);
            }
            catch (Exception e)
            {
                Console.WriteLine("User notification failed!");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        private static void SendEmailNotificationsToUsers(UsersRepository usersRepository, EmailService emailService)
        {
            var notTempUsers = usersRepository.Users.Where(u => u.Temp == false);

            foreach (var user in notTempUsers)
            {
                if (user.Email != "Admin")
                {
                    var message = CreateEmailMessageForUser(user);

                    try
                    {
                        emailService.SendEmail(message, "support");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Failed to send email for {0}", user.Email);
                        Console.WriteLine("\tDetails: {0}", e.Message);
                        continue;
                    }


                    Console.WriteLine(String.Format("Sent email for {0}", user.Email));
                }
            }
        }

        private static EmailMessage CreateEmailMessageForUser(User user)
        {
            var message = String.Format(
                    "<p>Dear {0},</p>" +
                    "<p>" +
                        "Thank you very much for your registration on <a href=\"http://trackyt.net\">Trackyt.net</a>." + 
                    "</p>" +
                    "<p>" +
                        "In nearest hour we are going to apply patch that would improve security and privacy of our site." +
                        "All passwords that was previously stored open-text will be removed. We would kindly remind " +
                        "password that you used during registration." +
                    "</p>" + 
                    "<p>" +
                        "Password: {1}" +
                    "</p>" +
                    "<p>" +
                        "Please keep this email, because we will not be able to remind you password if you lost it." +
                    "</p>"
                , user.Email, user.Password);

            var email = new EmailMessage { 
                From = "support@trackyt.net", 
                To = user.Email, 
                Message = message, 
                Subject = "Trackyt.net password reminder", 
                IsHtml = true };

            return email;
        }
    }
}
