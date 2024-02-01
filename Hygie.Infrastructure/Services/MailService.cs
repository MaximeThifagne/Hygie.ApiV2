using Hygie.Infrastructure.Common.Interfaces;
using Hygie.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Hygie.Infrastructure.Services
{
    public class MailService : IMailService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public MailService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task SendResetPasswordLink(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var link = $"http://localhost:8080/reset-password?userId={user.Id}&token={token}";

                var subject = "Réinitialisation de mot de passe";
                var body = $"Cliquez sur le lien suivant pour réinitialiser votre mot de passe : {link}";
                var from = new MailAddress("thifagne.maxime@gmail.com", "Votre application");
                var to = new MailAddress(user.Email!);

                await SendMail(subject, body, from, to);
            }
        }

        public async Task SendConfirmEmailLink(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);            

            if (user != null)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var link = $"http://localhost:8080/confirm-email?userId={user.Id}&token={token}";

                var subject = "Confirmer votre adresse email";
                var body = $"Cliquez sur le lien suivant pour valider votre adresse email : {link}";
                var from = new MailAddress("thifagne.maxime@gmail.com", "Votre application");
                var to = new MailAddress(user.Email!);

                await SendMail(subject, body, from, to);
            }
        }

        private async Task SendMail(string subject, string body, MailAddress from, MailAddress to)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings");
            var smtpServer = smtpSettings["SmtpServer"];
            var smtpPort = int.Parse(smtpSettings["SmtpPort"]);
            var smtpUsername = smtpSettings["SmtpUsername"];
            var smtpPassword = smtpSettings["SmtpPassword"];

            using (var smtpClient = new SmtpClient(smtpServer, smtpPort))
            {
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                smtpClient.EnableSsl = true;
                var mailMessage = new MailMessage(from, to)
                {
                    Subject = subject,
                    Body = body
                };

                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}
