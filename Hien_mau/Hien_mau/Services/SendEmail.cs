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
        public async Task SendThankYouEmail(Appointments appointment)
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
    
            <div style='text-align:center; color:#555; font-size:22px; font-weight:bold; margin-bottom:10px;'>Thư Tri Ân Hiến Máu</div>
            <div style='text-align:center; font-size:17px; color:#B71C1C; margin-bottom:20px; font-weight:bold;'>Trung tâm Hiến máu Bệnh viện Đa khoa Ánh Dương</div>
    
            <p style='color:#000000'>Kính gửi <strong>{donorName}</strong>,</p>
    
            <p style='color:#000000'>
                Trung tâm Hiến máu <strong>Bệnh viện Đa khoa Ánh Dương</strong> xin gửi lời tri ân sâu sắc đến bạn vì đã tham gia hiến máu tình nguyện vào ngày 
                <strong>{donationDate}</strong>, với <strong>{donationCapacity}ml</strong> máu nhóm <strong>{fullBloodType}</strong>.
            </p>
    
            <p style='color:#000000'>
                Đây không chỉ là một hành động cao đẹp, mà còn là món quà vô giá mà bạn đã trao tặng cho những bệnh nhân đang chiến đấu với bệnh tật từng ngày. 
                Sự sẻ chia quý báu này đã góp phần giúp duy trì sự sống và mang lại hy vọng cho những người đang cần máu khẩn cấp.
            </p>
    
            <div style='font-style:italic; color:#B71C1C; text-align:center; font-size:16px; margin:20px 0; font-weight:bold;'>
                “Mỗi giọt máu cho đi – Một cuộc đời ở lại.”
            </div>
    
            <p style='color:#000000'>Bạn có thể tiếp tục hiến máu sau ít nhất 84 ngày kể từ lần hiến gần nhất.</p>
    
            <p style='color:#000000'>Chúng tôi rất mong tiếp tục nhận được sự đồng hành quý báu từ bạn trong những lần hiến máu sắp tới, để cùng nhau lan tỏa tinh thần nhân ái và sẻ chia đến với cộng đồng.</p>
    
            <p style='margin-top:10px; color:#000000'>Một lần nữa, xin chân thành cảm ơn và chúc bạn luôn mạnh khỏe, hạnh phúc!</p>
    
            <p style='color:#000000'><strong>Trân trọng,</strong><br>
            Trung tâm Hiến máu Bệnh viện Đa khoa Ánh Dương<br>

            <div style='background-color:#fce4ec; padding:15px; border-radius:8px; margin-top:20px; font-size:14px;'>
                <p style='color:#000000'><strong>Thông tin sức khỏe và lịch sử hiến máu của bạn đã được cập nhật trên hệ thống. Vui lòng đăng nhập vào website để theo dõi chi tiết.  <br>
                Nếu bạn cần thêm thông tin hoặc có bất kỳ vấn đề gì sau khi hiến máu, vui lòng liên hệ với chúng tôi qua:</strong></p>
                <table style=""border:none; border-collapse:collapse; font-family:Arial, sans-serif;"">
            <tr>
            <!-- Logo -->
            <td style=""padding-right:15px; vertical-align:top;"">
              <img src=""https://i.postimg.cc/WzxX9QR4/logo.png""
                    alt=""Logo"" width=""90"" style=""display:block; border-radius:6px;"">
            </td>
            <!-- Thông tin -->
            <td style=""vertical-align:top; font-size:14px; line-height:20px; color:#333; padding-left:10px; border-left:3px solid #b4004e;"">
              <!-- Tiêu đề -->
              <div style=""font-size:16px; font-weight:bold; color:#b4004e; margin-bottom:4px;"">
                Trung tâm Hiến máu<br>Bệnh viện Đa khoa Ánh Dương
              </div>
              <!-- Địa chỉ -->
              <div style=""margin:6px 0;"">
                <span style=""style='color:#000000';""></span> 
                <strong>Địa chỉ:</strong> Đường CMT8, Q.3, TP.HCM
              </div>
              <!-- Liên hệ -->
              <div style=""margin:6px 0;"">
                <span style=""style='color:#000000';""></span> 
                <strong>Liên hệ:</strong> <a href=""tel:+842838554137"" style=""text-decoration:none; color:#333;"">028 3855 4137</a>
              </div>
              <!-- Email -->
              <div style=""margin:6px 0;"">
                <span style=""style='color:#000000';""></span> 
                <strong>Email:</strong> 
                <a href=""mailto:trungtamhienmau.anhduong@gmail.com"" style=""text-decoration:none; color:#1a73e8;"">
                  trungtamhienmau.anhduong@gmail.com
                </a>
              </div>
     
            </td>
          </tr>
        </table>

            </div>

          </div>
        </div>";

            var mail = new MailMessage
            {
                From = new MailAddress(senderEmail, "Trung tâm Hiến máu"),
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

        public async Task SendAppointmentReminders()
        {
            var vnTime = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var nowVN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vnTime);

            var tomorrow = nowVN.Date.AddDays(1);

            var appointments = await _context.Appointments
                .Include(a => a.User)
                .Where(a => a.AppointmentDate.HasValue &&
                            a.AppointmentDate.Value.Date == tomorrow &&
                            a.Cancel == false)
                .ToListAsync();

            foreach (var appointment in appointments)
            {
                if (string.IsNullOrWhiteSpace(appointment.User?.Email))
                    continue;

                var donorName = appointment.User.Name ?? "Bạn";
                var dateStr = appointment.AppointmentDate.Value.ToString("dd/MM/yyyy");
                var slot = appointment.TimeSlot ?? "";

                var smtpServer = _config["EmailSettings:SmtpServer"];
                var smtpPort = int.Parse(_config["EmailSettings:SmtpPort"]);
                var senderEmail = _config["EmailSettings:SenderEmail"];
                var senderPassword = _config["EmailSettings:SenderPassword"];

                var body = $@"
                <div style=""font-family: Arial, Helvetica, sans-serif; background-color:#f8f9fa; padding:20px;"">
                <div style=""max-width:600px; background-color:#ffffff; margin:0 auto; padding:30px; border-radius:10px; box-shadow:0 4px 12px rgba(0,0,0,0.1); color:#333333;"">
           
                <div style=""text-align:center; color:#555; font-size:22px; font-weight:bold; margin-bottom:10px;"">
                    Thông Tin Buổi Hiến Máu
                </div>

                <div style='text-align:center; font-size:17px; color:#B71C1C; margin-bottom:20px; font-weight:bold;'>Trung tâm Hiến máu Bệnh viện Đa khoa Ánh Dương</div>

                <p  style=""color:#000000;"" >Kính gửi  <strong> {donorName},</strong></p>

                <p style=""color:#000000;"">
                    Cảm ơn bạn đã đăng ký tham gia chương trình hiến máu tình nguyện do 
                    <strong>Trung tâm Hiến máu Bệnh viện Đa khoa Ánh Dương</strong> tổ chức. 
                    Chúng tôi xin gửi đến bạn thông tin chi tiết về buổi hiến máu sắp tới:
                </p>

                <div style=""background-color:#fce4ec; padding:15px; border-radius:8px; margin:15px 0;"">
                    <p style=""margin:8px 0; font-size:15px; color:#000000;""><strong>Thời gian: </strong>{dateStr}</p>
                    <p style=""margin:8px 0; font-size:15px; color:#000000;""><strong>Giờ: </strong> {slot}</p>
                    <p style=""margin:8px 0; font-size:15px; color:#000000;""><strong>Địa điểm: </strong> Trung tâm Hiến máu Ánh Dương - Đường CMT8, Q.3, TP.HCM</p>
                </div>

                <p style=""color:#000000;"">Hiến máu là một nghĩa cử cao đẹp, và sự tham gia của bạn sẽ góp phần mang lại cơ hội sống cho nhiều bệnh nhân đang cần máu.</p

                <p style=""font-weight:bold;color:#B71C1C; margin:20px 0;font-size:16px;"">Để buổi hiến máu diễn ra an toàn và thuận lợi, bạn vui lòng lưu ý:</p>
                <ul>
                    <li style=""color:#000000;"" >Ăn nhẹ và uống đủ nước trước khi hiến máu.</li>
                    <li style=""color:#000000;"" >Nghỉ ngơi đầy đủ vào đêm trước đó.</li>
                    <li style=""color:#000000;"" >Tránh sử dụng rượu bia trong vòng 24 giờ trước khi hiến máu.</li>
                    <li style=""color:#000000;"" >Mang theo giấy tờ tùy thân (CMND/CCCD) khi đến địa điểm hiến máu.</li>
                </ul>

             
                <p style=""margin-top:20px; ""color:#000000;"">Một lần nữa, cảm ơn bạn đã đồng hành cùng chúng tôi trong hành trình lan tỏa sự sống.</p>
                <p style=""color:#000000; font-weight:bold;"">Rất mong được gặp bạn tại buổi hiến máu.</p>

                <p style=""color:#000000;""><strong>Trân trọng,</strong><br>
                    Trung tâm Hiến máu Bệnh viện Đa khoa Ánh Dương
                </p>

                <div style='background-color:#fce4ec; padding:15px; border-radius:8px; margin-top:20px; font-size:14px;'>
                <p style='color:#000000'><strong>
                    Nếu bạn cần thêm thông tin hoặc có bất kỳ vấn đề gì sau khi hiến máu, vui lòng liên hệ với chúng tôi qua:</strong>
                </p>
                <table style=""border:none; border-collapse:collapse; font-family:Arial, sans-serif;"">
                    <tr>
                    <!-- Logo -->
                        <td style=""padding-right:15px; vertical-align:top;"">
                <img src=""https://i.postimg.cc/WzxX9QR4/logo.png""
                            alt=""Logo"" width=""90"" style=""display:block; border-radius:6px;"">
                        </td>
                    <!-- Thông tin -->
                        <td style=""vertical-align:top; font-size:14px; line-height:20px; color:#333; padding-left:10px; border-left:3px solid #b4004e;"">
                  <!-- Tiêu đề -->
                  <div style=""font-size:16px; font-weight:bold; color:#b4004e; margin-bottom:4px;"">
                    Trung tâm Hiến máu<br>Bệnh viện Đa khoa Ánh Dương
                  </div>
                  <!-- Địa chỉ -->
                  <div style=""margin:6px 0;"">
                    <span style=""style='color:#000000';""></span> 
                    <strong>Địa chỉ:</strong> Đường CMT8, Q.3, TP.HCM
                  </div>
                    <!-- Liên hệ -->
                    <div style=""margin:6px 0;"">
                        <span style=""style='color:#000000';""></span> 
                        <strong>Liên hệ:</strong> <a href=""""tel:+842838554137"" style=""text-decoration:none; color:#333;"">028 3855 4137</a>
                    </div>
                    <!-- Email -->
                    <div style=""margin:6px 0;"">
                        <span style=""style='color:#000000';""></span> 
                        <strong>Email:</strong> 
                        <a href=""mailto:trungtamhienmau.anhduong@gmail.com"" style=""text-decoration:none; color:#1a73e8;"">
                          trungtamhienmau.anhduong@gmail.com
                        </a>
                  </div>
                        </td>
                      </tr>
                    </table>
                </div>";

                var mail = new MailMessage
                {
                    From = new MailAddress(senderEmail, "Trung tâm Hiến máu"),
                    Subject = "Nhắc nhở lịch hiến máu",
                    Body = body,
                    IsBodyHtml = true
                };

                mail.To.Add(appointment.User.Email);

                using var smtp = new SmtpClient(smtpServer)
                {
                    Port = smtpPort,
                    Credentials = new NetworkCredential(senderEmail, senderPassword),
                    EnableSsl = true
                };

                await smtp.SendMailAsync(mail);
            }
        }

        public async Task SendDonationBloodCall()
        {
            var vnTime = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var nowVN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vnTime);

            var eligibleUsers = await _context.Appointments
                .Include(a => a.User)
                .Where(a => a.DonationDate.HasValue &&
                            EF.Functions.DateDiffDay(a.DonationDate.Value, nowVN.Date) >= 84 &&
                            a.Cancel == false)
                .GroupBy(a => a.UserID)
                .Select(g => g.OrderByDescending(a => a.DonationDate).First()) 
                .ToListAsync();

            foreach (var appointment in eligibleUsers)
            {
                if (string.IsNullOrWhiteSpace(appointment.User?.Email))
                    continue;

                var donorName = appointment.User.Name ?? "Bạn";
                var bloodGroup = appointment.BloodGroup ?? "";
                var rhSymbol = (appointment.RhType ?? "").Replace("Rh", "").Trim();
                var fullBloodType = $"{bloodGroup}{rhSymbol}";

                var smtpServer = _config["EmailSettings:SmtpServer"];
                var smtpPort = int.Parse(_config["EmailSettings:SmtpPort"]);
                var senderEmail = _config["EmailSettings:SenderEmail"];
                var senderPassword = _config["EmailSettings:SenderPassword"];

                var body = $@"
            <div style='font-family: Arial, Helvetica, sans-serif; background-color:#f8f9fa; padding:20px;'> 
<div style='max-width:600px; background-color:#ffffff; margin:0 auto; padding:30px; border-radius:10px; box-shadow:0 4px 12px rgba(0,0,0,0.1); color:#333333;'>
    
    <!-- Tiêu đề -->
    <h2 style=""color: #B71C1C; font-size: 20px; margin-bottom: 20px; text-align:center;"">[Khẩn] Cần gấp máu <span style=""font-weight:bold; tẽ""></span> <br> Rất mong nhận được sự hỗ trợ từ bạn</h2>
    
    <!-- Lời chào -->
    <p style=""color:#000000;""><strong>Kính gửi {donorName},</strong></p>
    
    <!-- Nội dung cảm ơn -->
    <p style=""color:#000000;"" >Trước tiên, Trung tâm Hiến máu <strong>Bệnh viện Đa khoa Ánh Dương</strong> xin gửi lời cảm ơn sâu sắc đến bạn vì đã tin tưởng và đồng hành cùng chương trình hiến máu trong suốt thời gian qua. Nghĩa cử cao đẹp của bạn chính là nguồn cảm hứng và sức mạnh để cộng đồng tiếp tục lan tỏa yêu thương.</p>
    
    <!-- Nội dung chính --> 
    <p style=""color: #B71C1C;""><strong>Hiện tại, ngân hàng máu của bệnh viện đang rất cần nhóm máu {fullBloodType} của bạn</strong> để kịp thời hỗ trợ những ca bệnh cấp cứu. Theo thông tin bạn đã đăng ký, bạn thuộc nhóm máu phù hợp và đủ điều kiện tham gia hiến máu trong thời điểm này.</p>
    
    <p style=""color:#000000;"">Chúng tôi rất mong bạn dành chút thời gian tham gia hiến máu hoặc chia sẽ cho mọi người biết đến, bởi mỗi đơn vị máu bạn trao đi không chỉ cứu sống một người mà còn là món quà vô giá, thắp sáng niềm hy vọng cho nhiều gia đình.</p>
    
    <!-- Thông tin buổi hiến máu -->
    <div style=""border-left: 4px solid #B71C1C; padding-left: 15px; margin: 20px 0;"">
      <p style=""color:#000000;""><strong>Thông tin buổi hiến máu:</strong></p>
      <p style=""color:#000000;""><strong>Thời gian: </strong>7:00 - 17:00</p>
      <p style=""color:#000000;""><strong>Địa điểm:</strong>  Đường CMT8, Q.3, TP.HCM</p>
    </div>
    
<p style=""color:#000000;""><em>Một hành động nhỏ hôm nay có thể thay đổi cuộc sống của ai đó vào ngày mai.</em><br>
    Chúng tôi rất mong sớm nhận được sự chung tay quý báu từ bạn.</p>

    <p style='color:#000000'>Trân trọng,<br>
      <strong>Trung tâm Hiến máu Bệnh viện Đa khoa Ánh Dương</strong>
    </p>
      
        <div style='background-color:#fce4ec; padding:15px; border-radius:8px; margin-top:20px; font-size:14px;'>
        <p style='color:#000000'><strong>
        Nếu bạn cần thêm thông tin hoặc có bất kỳ vấn đề gì sau khi hiến máu, vui lòng liên hệ với chúng tôi qua:</strong></p>
        <table style=""border:none; border-collapse:collapse; font-family:Arial, sans-serif;"">
    <tr>
    <!-- Logo -->
    <td style=""padding-right:15px; vertical-align:top;"">
      <img src=""https://i.postimg.cc/WzxX9QR4/logo.png""
            alt=""Logo"" width=""90"" style=""display:block; border-radius:6px;"">
    </td>
    <!-- Thông tin -->
    <td style=""vertical-align:top; font-size:14px; line-height:20px; color:#333; padding-left:10px; border-left:3px solid #b4004e;"">
      <!-- Tiêu đề -->
      <div style=""font-size:16px; font-weight:bold; color:#b4004e; margin-bottom:4px;"">
        Trung tâm Hiến máu<br>Bệnh viện Đa khoa Ánh Dương
      </div>
      <!-- Địa chỉ -->
      <div style=""margin:6px 0;"">
        <span style=""style='color:#000000';""></span> 
        <strong>Địa chỉ:</strong> Đường CMT8, Q.3, TP.HCM
      </div>
      <!-- Liên hệ -->
      <div style=""margin:6px 0;"">
        <span style=""style='color:#000000';""></span> 
        <strong>Liên hệ:</strong> <a href=""tel:+842838554137"" style=""text-decoration:none; color:#333;"">028 3855 4137</a>
      </div>
      <!-- Email -->
      <div style=""margin:6px 0;"">
        <span style=""style='color:#000000';""></span> 
        <strong>Email:</strong> 
        <a href=""mailto:trungtamhienmau.anhduong@gmail.com"" style=""text-decoration:none; color:#1a73e8;"">
          trungtamhienmau.anhduong@gmail.com
        </a>
      </div>
     
    </td>
  </tr>
</table>
    </div>
  </div>
</div>
";
                var mail = new MailMessage
                {
                    From = new MailAddress(senderEmail, "Trung tâm Hiến máu"),
                    Subject = "Lời kêu gọi hiến máu",
                    Body = body,
                    IsBodyHtml = true
                };

                mail.To.Add(appointment.User.Email);

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
}
