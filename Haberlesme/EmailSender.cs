using Microsoft.AspNetCore.Identity.UI.Services;

namespace udemyWeb1.Haberlesme
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //Buraya email gonderme islemlerimizi yapabiliriz ekleyebiliriz
            return Task.CompletedTask;
        }
    }
}
