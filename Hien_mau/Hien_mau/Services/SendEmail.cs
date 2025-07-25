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
            <div style='font-family: Arial, Helvetica, sans-serif; background-color:#f8f9fa; padding:20px;'>
      <div style='max-width:600px; background-color:#ffffff; margin:0 auto; padding:30px; border-radius:10px; box-shadow:0 4px 12px rgba(0,0,0,0.1); color:#333333;'>
        
        <div style='text-align:center; color:#B71C1C; font-size:22px; font-weight:bold; margin-bottom:10px;'>Thư Tri Ân Hiến Máu</div>
        <div style='text-align:center; font-size:16px; color:#555; margin-bottom:20px;'>Trung tâm Hiến máu Bệnh viện Đa khoa Ánh Dương</div>
        
        <p>Kính gửi <strong>{donorName}</strong>,</p>
        
        <p>
            Trung tâm Hiến máu <strong>Bệnh viện Đa khoa Ánh Dương</strong> xin gửi lời tri ân sâu sắc đến bạn vì đã tham gia hiến máu tình nguyện vào ngày 
            <strong>{donationDate}</strong>, với <strong>{donationCapacity}ml</strong> máu nhóm <strong>{fullBloodType}</strong>.
        </p>
        
        <p>
            Đây không chỉ là một hành động cao đẹp, mà còn là món quà vô giá mà bạn đã trao tặng cho những bệnh nhân đang chiến đấu với bệnh tật từng ngày. 
            Sự sẻ chia quý báu này đã góp phần giúp duy trì sự sống và mang lại hy vọng cho những người đang cần máu khẩn cấp.
        </p>
        
        <div style='font-style:italic; color:#B71C1C; text-align:center; font-size:16px; margin:20px 0; font-weight:bold;'>
            “Mỗi giọt máu cho đi – Một cuộc đời ở lại.”
        </div>
        
        <p>Chúng tôi hy vọng bạn sẽ tiếp tục đồng hành cùng chương trình hiến máu trong những lần tới để lan tỏa nghĩa cử nhân văn này đến cộng đồng.</p>
        
        <div style='background-color:#fce4ec; padding:15px; border-radius:8px; margin-top:20px; font-size:14px;'>
            <p><strong>Nếu bạn cần thêm thông tin hoặc có bất kỳ vấn đề gì sau khi hiến máu, vui lòng liên hệ với chúng tôi qua:</strong></p>
            <p>📞 028 3855 4137<br>
            📧 <a href='mailto:trungtamhienmau.anhduong@gmail.com' style='color:#B71C1C; text-decoration:none;'>trungtamhienmau.anhduong@gmail.com</a></p>
        </div>
        
        <p style='margin-top:10px'>Một lần nữa, xin chân thành cảm ơn và chúc bạn luôn mạnh khỏe, hạnh phúc!</p>
        
        <p><strong>Trân trọng,</strong><br>
        Trung tâm Hiến máu Bệnh viện Đa khoa Ánh Dương<br>
        <span style='color:#800080;'>Địa chỉ: Đường CMT8, Q.3, TP.HCM, Việt Nam</span></p>
        
        <p style='font-size:13px; color:#666;'><em>Lưu ý: Bạn có thể hiến máu lần tiếp theo sau ít nhất 84 ngày.</em>
        </p>
        <p style='font-size:13px; color:#666;'><em> Thông tin sức khoẻ và quy trình hiến máu của bạn đã được chúng tôi cập nhật trên website. Bạn có thể đăng nhập vào website để xem lại chi tiết.</em>
        </p>

      </div>
    </div>";


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
