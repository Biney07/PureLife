using Pure_Life.ViewModel.Email;

namespace Pure_Life.Services
{
    public interface IEmailService
    {
        Task<string> SendEmailAsync(EmailViewModel email);
    }
}
