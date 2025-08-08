// Services/NullEmailSender.cs
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace RuedaYPata.Services
{
    public class NullEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // No hace nada, solo para evitar errores en desarrollo
            return Task.CompletedTask;
        }
    }
}