using Hien_mau.Data;
using Hien_mau.Interface;
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
        public async Task SendThankYouEmailAsync(Appointments appointment)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == appointment.UserID);

            if (user == null || string.IsNullOrEmpty(user.Email))
                return;

            var recipientEmail = appointment.User.Email;
            var donorName = appointment.User.Name ?? "Bạn";

            var bloodGroup = appointment.BloodGroup ?? "";
            var rhSymbol = (appointment.RhType ?? "").Replace("Rh", "").Trim();
            var fullBloodType = $"{bloodGroup}{rhSymbol}";
            var donationCapacity = appointment.DonationCapacity;
            var donationDate = appointment.DonationDate.HasValue ? appointment.DonationDate.Value.ToString("dd/MM/yyyy") : "N/A";

            var smtpServer = _config["EmailSettings:SmtpServer"];
            var smtpPort = int.Parse(_config["EmailSettings:SmtpPort"]);
            var senderEmail = _config["EmailSettings:SenderEmail"];
            var senderPassword = _config["EmailSettings:SenderPassword"];


            var body = $@"
            <p>Xin chào {donorName},</p>
            <p>Chúng tôi xin gửi lời cảm ơn sâu sắc đến bạn vì đã tham gia hiến máu tình nguyện vào ngày <strong>{donationDate}</strong>. Hành động của bạn thật cao đẹp và đáng trân trọng.</p>    
            <p>Thông tin buổi hiến máu:</p>
            <ul>
                <li><strong>Nhóm máu:</strong> {fullBloodType}</li>
                <li><strong>Lượng máu đã hiến:</strong> {donationCapacity}ml</li>
                <li><strong>Ghi chú:</strong> {appointment.Notes ?? "Không có"}</li>
            </ul>
            <p>Bạn đã góp phần cứu sống những người đang cần. Một lần nữa, xin chân thành cảm ơn!</p>
            <p>Trân trọng,</p>
            <p><strong>Hệ thống Hiến máu</strong></p>";

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
