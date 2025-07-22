using Hien_mau.Data;
using Hien_mau.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

namespace Hien_mau.Services
{
    public class SendEmail : ISendEmail
    {
        private readonly Hien_mauContext _context;
        private readonly IConfiguration _config;

        public SendEmail(Hien_mauContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public async Task SendThankYouEmailAsync(BloodDonationHistories donation)
        {
            var appointment = await _context.Appointments
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.AppointmentId == donation.AppointmentId);

            if (appointment?.User == null || string.IsNullOrEmpty(appointment.User.Email))
                return;

            var recipientEmail = appointment.User.Email;
            var donorName = appointment.User.Name ?? "Bạn";

            var bloodGroup = donation.BloodGroup ?? "";
            var rhSymbol = (donation.RhType ?? "").Replace("Rh", "").Trim();
            var fullBloodType = $"{bloodGroup}{rhSymbol}"; 
            var donationDate = donation.DonationDate.ToString("dd/MM/yyyy");

            // Lấy tên bác sĩ từ chính donation.DoctorId (nếu có)
            var doctorName = "Bác sĩ";
            if (donation.DoctorId != null)
            {
                var doctor = await _context.Users.FirstOrDefaultAsync(d => d.UserId == donation.DoctorId);
                if (doctor != null)
                    doctorName = doctor.Name ?? "Bác sĩ";
            }

            var smtpServer = _config["EmailSettings:SmtpServer"];
            var smtpPort = int.Parse(_config["EmailSettings:SmtpPort"]);
            var senderEmail = _config["EmailSettings:SenderEmail"];
            var senderPassword = _config["EmailSettings:SenderPassword"];

            var body = $@"
                <p>Xin chào {donorName},</p>
                <p>Chúng tôi chân thành cảm ơn bạn đã tham gia hiến máu vào ngày <strong>{donationDate}</strong>.</p>
                <p>Nhóm máu của bạn: <strong>{fullBloodType}</strong>.</p>
                <p>Bác sĩ phụ trách: <strong>{doctorName}</strong>.</p>
                <p>Ghi chú: {donation.Notes ?? "Không có"}</p>
                <br />
                <p>Bạn đã góp phần cứu sống những người đang cần. Một lần nữa, xin chân thành cảm ơn!</p>
                <p>Trân trọng,</p>
                <p><strong>Hệ thống Hiến máu</strong></p>
            ";

            var mail = new MailMessage
            {
                From = new MailAddress(senderEmail, "Hệ thống hiến máu"),
                Subject = "Cảm ơn bạn đã hiến máu",
                Body = body,
                IsBodyHtml = true
            };

            mail.To.Add(recipientEmail);

            using var smtp = new SmtpClient(smtpServer)
            {
                Port = smtpPort,
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true
            };

            await smtp.SendMailAsync(mail);
        }

    }
}
