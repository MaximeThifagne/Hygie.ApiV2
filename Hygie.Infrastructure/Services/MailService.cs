using Azure.Core;
using Hygie.Infrastructure.Common.Interfaces;
using Hygie.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

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

            var smtpSettings = _configuration.GetSection("SmtpSettings");
            var smtpServer = smtpSettings["SmtpServer"];
            var smtpPort = int.Parse(smtpSettings["SmtpPort"]);
            var smtpUsername = smtpSettings["SmtpUsername"];
            var smtpPassword = smtpSettings["SmtpPassword"];

            if (user == null)
                throw new NullReferenceException(nameof(user));

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = $"http://localhost:8080/reset-password?userId={user.Id}&token={token}";

            var subject = "Réinitialisation de mot de passe";
            var body = $"Cliquez sur le lien suivant pour réinitialiser votre mot de passe : {link}";
            var from = new MailAddress("thifagne.maxime@gmail.com", "Votre application");
            var to = new MailAddress(user.Email!);

            using (var smtpClient = new SmtpClient(smtpServer, smtpPort))
            {
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

                var mailMessage = new MailMessage(from, to)
                {
                    Subject = subject,
                    Body = body
                };

                try
                {
                    await smtpClient.SendMailAsync(mailMessage);
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
        }
    }
}
