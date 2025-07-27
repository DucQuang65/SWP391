using Hien_mau.Models;

namespace Hien_mau.Interface
{
    public interface ISendEmail
    {
        Task SendThankYouEmailAsync(Appointments appointment);

        Task SendAppointmentRemindersAsync();
    }
}
