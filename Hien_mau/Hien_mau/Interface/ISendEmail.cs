using Hien_mau.Models;

namespace Hien_mau.Interface
{
    public interface ISendEmail
    {
        Task SendThankYouEmail(Appointments appointment);

        Task SendAppointmentReminders();
        Task SendDonationBloodCall();
    }
}
