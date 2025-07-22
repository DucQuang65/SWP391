using Hien_mau.Models;

namespace Hien_mau.Services
{
    public interface ISendEmail
    {
        Task SendThankYouEmailAsync(BloodDonationHistories donation);
    }
}
