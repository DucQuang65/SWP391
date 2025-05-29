USE BloodManagementSystem
Go

INSERT INTO Roles (RoleName)
VALUES 
('Member'),
('Doctor'),
('BloodManager'),
('Admin');
GO

INSERT INTO HospitalInfo (Name, Address, Phone, Email, WorkingHours, MapImageUrl)
VALUES
(N'Bệnh viện Chợ Rẫy', N'201B Nguyễn Chí Thanh, Phường 12, Quận 5, TP.HCM', 'bvchoray@choray.vn', '02838554137', 10.7556, 106.6639),
GO

INSERT INTO Tags (TagName) VALUES ('A'),('B'),('AB'),('O'),('Rh+'),('Rh-'),('Tổng quan nhóm máu')('Truyền máu');GO

INSERT INTO BloodArticles (Title, Content, img_url)
VALUES
-- Nhóm A Rh+
('Giới thiệu nhóm máu A Rh+', 
N'Nhóm máu A Rh+ là một trong những nhóm máu phổ biến ở người.

Đặc điểm:
- Có kháng nguyên A trên bề mặt hồng cầu.
- Có kháng thể anti-B trong huyết tương.
- Có yếu tố Rh (D antigen), tức là Rh dương tính (Rh+).

Người có nhóm máu A Rh+:
- Có thể nhận máu từ: A Rh+, A Rh-, O Rh+, O Rh-
- Có thể cho máu cho: A Rh+, AB Rh+

Lưu ý:
- Trong truyền máu, yếu tố Rh đóng vai trò quan trọng. Người Rh+ có thể nhận máu Rh- nhưng ngược lại thì không an toàn.', 
'article1.jpg'),

-- Nhóm A Rh-
('Giới thiệu nhóm máu A Rh-', 
N'Nhóm máu A Rh- là nhóm máu hiếm hơn A Rh+.

Đặc điểm:
- Có kháng nguyên A trên bề mặt hồng cầu.
- Có kháng thể anti-B trong huyết tương.
- Không có yếu tố Rh (Rh-), nên không mang D antigen.

Người có nhóm máu A Rh-:
- Có thể nhận máu từ: A Rh-, O Rh-
- Có thể cho máu cho: A Rh-, A Rh+, AB Rh-, AB Rh+

Lưu ý:
- Người Rh- **chỉ nên nhận máu Rh-**, vì nếu nhận Rh+ có thể gây phản ứng miễn dịch nghiêm trọng.', 
'article1.jpg'),

-- Nhóm B Rh+
('Giới thiệu nhóm máu B Rh+', 
N'Nhóm máu B Rh+ có những đặc điểm:

- Có kháng nguyên B trên hồng cầu.
- Có kháng thể anti-A trong huyết tương.
- Mang yếu tố Rh (Rh+).

Người có nhóm máu B Rh+:
- Có thể nhận máu từ: B Rh+, B Rh-, O Rh+, O Rh-
- Có thể cho máu cho: B Rh+, AB Rh+

Lưu ý:
- Nhóm máu B Rh+ là phổ biến và có khả năng nhận từ nhiều nhóm khác nếu tương thích Rh.', 
'article1.jpg'),

-- Nhóm B Rh-
('Giới thiệu nhóm máu B Rh-', 
N'Nhóm máu B Rh- là một trong những nhóm máu hiếm.

Đặc điểm:
- Có kháng nguyên B.
- Có kháng thể anti-A.
- Không có yếu tố Rh (Rh-).

Người có nhóm máu B Rh-:
- Có thể nhận máu từ: B Rh-, O Rh-
- Có thể cho máu cho: B Rh-, B Rh+, AB Rh-, AB Rh+

Lưu ý:
- Trong truyền máu, người Rh- cần thận trọng và **chỉ nên nhận từ Rh-** để tránh phản ứng miễn dịch.', 
'article1.jpg'),

-- Nhóm AB Rh+
('Giới thiệu nhóm máu AB Rh+', 
N'Nhóm máu AB Rh+ là nhóm máu **hiếm và đặc biệt**.

Đặc điểm:
- Có cả kháng nguyên A và B trên hồng cầu.
- Không có kháng thể anti-A hay anti-B trong huyết tương.
- Có yếu tố Rh (Rh+).

Người có nhóm máu AB Rh+:
- Có thể nhận máu từ: Tất cả các nhóm (A, B, AB, O - cả Rh+ và Rh-)
- Có thể cho máu cho: AB Rh+

Lưu ý:
- AB Rh+ là **người nhận máu phổ thông**, rất thuận lợi trong cấp cứu.', 
'article1.jpg'),

-- Nhóm AB Rh-
('Giới thiệu nhóm máu AB Rh-', 
N'Nhóm máu AB Rh- là một trong những nhóm máu **hiếm nhất**.

Đặc điểm:
- Có kháng nguyên A và B.
- Không có kháng thể anti-A hay anti-B.
- Không có yếu tố Rh.

Người có nhóm máu AB Rh-:
- Có thể nhận máu từ: AB Rh-, A Rh-, B Rh-, O Rh-
- Có thể cho máu cho: AB Rh-, AB Rh+

Lưu ý:
- Dù không có kháng thể, nhưng vì Rh- nên **chỉ nhận được từ người Rh-**.', 
'article1.jpg'),

-- Nhóm O Rh+
('Giới thiệu nhóm máu O Rh+', 
N'Nhóm máu O Rh+ là nhóm máu phổ biến nhất tại nhiều quốc gia.

Đặc điểm:
- Không có kháng nguyên A hoặc B.
- Có cả kháng thể anti-A và anti-B trong huyết tương.
- Có yếu tố Rh (Rh+).

Người có nhóm máu O Rh+:
- Có thể nhận máu từ: O Rh+, O Rh-
- Có thể cho máu cho: A Rh+, B Rh+, AB Rh+, O Rh+

Lưu ý:
- Không thể nhận từ các nhóm A, B, AB vì có kháng thể.', 
'article1.jpg'),

-- Nhóm O Rh-
('Giới thiệu nhóm máu O Rh-', 
N'Nhóm máu O Rh- được xem là nhóm máu **cho phổ thông** trong truyền máu khẩn cấp.

Đặc điểm:
- Không có kháng nguyên A hoặc B.
- Có cả kháng thể anti-A và anti-B.
- Không có yếu tố Rh (Rh-).

Người có nhóm máu O Rh-:
- Có thể nhận máu từ: O Rh-
- Có thể cho máu cho: Tất cả nhóm máu (A, B, AB, O - cả Rh+ và Rh-)

Lưu ý:
- O Rh- là nhóm máu **quan trọng trong cấp cứu**, vì an toàn với hầu hết người nhận.', 
'article1.jpg');
GO

INSERT INTO ArticleTags (ArticleID, TagID) VALUES
-- Bài 1: A Rh+
(1, 1), (1, 5), (1, 7), (1, 8),
-- Bài 2: A Rh−
(2, 1), (2, 6), (2, 7), (2, 8),
-- Bài 3: B Rh+
(3, 2), (3, 5), (3, 7), (3, 8),
-- Bài 4: B Rh−
(4, 2), (4, 6), (4, 7), (4, 8),
-- Bài 5: AB Rh+
(5, 3), (5, 5), (5, 7), (5, 8),
-- Bài 6: AB Rh−
(6, 3), (6, 6), (6, 7), (6, 8),
-- Bài 7: O Rh+
(7, 4), (7, 5), (7, 7), (7, 8),
-- Bài 8: O Rh−
(8, 4), (8, 6), (8, 7); (8, 8),
GO

INSERT INTO BlogPosts (BlogPostID, Title, Content, CreatedAt, UpdatedAt)
VALUES
(1, N'Thông tin về nhóm máu A (Rh+)', N'
Nhóm máu A (Rh+) là một trong những nhóm máu phổ biến, đóng vai trò quan trọng trong truyền máu và chăm sóc sức khỏe.

Đặc điểm:
- Kháng nguyên: A
- Kháng thể: anti-B
- Yếu tố Rh: Dương tính (Rh+)

Khả năng truyền máu:
- Có thể nhận máu từ: A (Rh+), A (Rh−), O (Rh+), O (Rh−)
- Có thể cho máu cho: A (Rh+), AB (Rh+)

Ý nghĩa lâm sàng:
- Phụ nữ mang thai Rh− cần lưu ý khi mang thai con Rh+ để tránh hiện tượng bất đồng Rh.

Lưu ý:
- Trước khi truyền máu, cần xác định chính xác nhóm máu và yếu tố Rh để đảm bảo an toàn.
', GETDATE(), GETDATE()),

(2, N'Thông tin về nhóm máu A (Rh−)', N'
Nhóm máu A (Rh−) là một nhóm máu hiếm, chiếm tỷ lệ nhỏ trong dân số, nhưng có ý nghĩa quan trọng trong y học.

Đặc điểm:
- Kháng nguyên: A
- Kháng thể: anti-B
- Yếu tố Rh: Âm tính (Rh−)

Khả năng truyền máu:
- Có thể nhận máu từ: A (Rh−), O (Rh−)
- Có thể cho máu cho: A (Rh−), A (Rh+), AB (Rh−), AB (Rh+)

Ý nghĩa lâm sàng:
- Người Rh− cần cẩn trọng khi nhận máu để tránh phản ứng miễn dịch.

Lưu ý:
- Phụ nữ Rh− mang thai con Rh+ cần theo dõi và điều trị để tránh bệnh tan máu ở trẻ sơ sinh.
', GETDATE(), GETDATE()),

(3, N'Thông tin về nhóm máu B (Rh+)', N'
Nhóm máu B (Rh+) là một nhóm máu phổ biến, có vai trò quan trọng trong truyền máu và chăm sóc sức khỏe.

Đặc điểm:
- Kháng nguyên: B
- Kháng thể: anti-A
- Yếu tố Rh: Dương tính (Rh+)

Khả năng truyền máu:
- Có thể nhận máu từ: B (Rh+), B (Rh−), O (Rh+), O (Rh−)
- Có thể cho máu cho: B (Rh+), AB (Rh+)

Ý nghĩa lâm sàng:
- Người nhóm máu B (Rh+) cần lưu ý khi nhận máu để tránh phản ứng miễn dịch.

Lưu ý:
- Trước khi truyền máu, cần xác định chính xác nhóm máu và yếu tố Rh.
', GETDATE(), GETDATE()),

(4, N'Thông tin về nhóm máu B (Rh−)', N'
Nhóm máu B (Rh−) là một nhóm máu hiếm, có ý nghĩa quan trọng trong truyền máu và chăm sóc sức khỏe.

Đặc điểm:
- Kháng nguyên: B
- Kháng thể: anti-A
- Yếu tố Rh: Âm tính (Rh−)

Khả năng truyền máu:
- Có thể nhận máu từ: B (Rh−), O (Rh−)
- Có thể cho máu cho: B (Rh−), B (Rh+), AB (Rh−), AB (Rh+)

Ý nghĩa lâm sàng:
- Người Rh− cần cẩn trọng khi nhận máu để tránh phản ứng miễn dịch.

Lưu ý:
- Phụ nữ Rh− mang thai con Rh+ cần theo dõi và điều trị để tránh bệnh tan máu ở trẻ sơ sinh.
', GETDATE(), GETDATE()),

(5, N'Thông tin về nhóm máu AB (Rh+)', N'
Nhóm máu AB (Rh+) là nhóm máu hiếm, có khả năng nhận máu từ tất cả các nhóm máu khác, được gọi là "người nhận phổ thông".

Đặc điểm:
- Kháng nguyên: A và B
- Kháng thể: Không có
- Yếu tố Rh: Dương tính (Rh+)

Khả năng truyền máu:
- Có thể nhận máu từ: Tất cả các nhóm máu (A, B, AB, O) bất kể Rh
- Có thể cho máu cho: AB (Rh+)

Ý nghĩa lâm sàng:
- Người nhóm máu AB (Rh+) có thể nhận máu từ bất kỳ nhóm máu nào, giúp thuận lợi trong cấp cứu.

Lưu ý:
- Mặc dù có thể nhận máu từ nhiều nhóm, việc truyền máu vẫn cần kiểm tra kỹ lưỡng để đảm bảo an toàn.
', GETDATE(), GETDATE()),

(6, N'Thông tin về nhóm máu AB (Rh−)', N'
Nhóm máu AB (Rh−) là nhóm máu hiếm nhất, chiếm tỷ lệ rất nhỏ trong dân số, có ý nghĩa đặc biệt trong truyền máu.

Đặc điểm:
- Kháng nguyên: A và B
- Kháng thể: Không có
- Yếu tố Rh: Âm tính (Rh−)

Khả năng truyền máu:
- Có thể nhận máu từ: AB (Rh−), A (Rh−), B (Rh−), O (Rh−)
- Có thể cho máu cho: AB (Rh−), AB (Rh+)

Ý nghĩa lâm sàng:
- Người nhóm máu AB (Rh−) cần cẩn trọng khi nhận máu do tính hiếm của nhóm máu này.

Lưu ý:
- Việc tìm nguồn máu phù hợp cho người AB (Rh−) có thể gặp khó khăn, cần có kế hoạch trước.
', GETDATE(), GETDATE()),

(7, N'Thông tin về nhóm máu O (Rh+)', N'
Nhóm máu O (Rh+) là nhóm máu phổ biến nhất, có khả năng cho máu cho nhiều nhóm khác, được gọi là "người cho phổ thông".

Đặc điểm:
- Kháng nguyên: Không có
- Kháng thể: anti-A và anti-B
- Yếu tố Rh: Dương tính (Rh+)

Khả năng truyền máu:
- Có thể nhận máu từ: O (Rh+), O (Rh−)
- Có thể cho máu cho: O (Rh+), A (Rh+), B (Rh+), AB (Rh+)

Ý nghĩa lâm sàng:
- Người nhóm máu O (Rh+) có thể cho máu cho nhiều nhóm khác, rất quan trọng trong cấp cứu.

Lưu ý:
- Trước khi truyền máu, cần xác định chính xác nhóm máu và yếu tố Rh để đảm bảo an toàn.
', GETDATE(), GETDATE()),

(8, N'Thông tin về nhóm máu O (Rh−)', N'
Nhóm máu O (Rh−) là nhóm máu hiếm, có khả năng cho máu cho tất cả các nhóm máu khác, được gọi là "người cho phổ thông".

Đặc điểm:
- Kháng nguyên: Không có
- Kháng thể: anti-A và anti-B
- Yếu tố Rh: Âm tính (Rh−)

Khả năng truyền máu:
- Có thể nhận máu từ: O (Rh−)
- Có thể cho máu cho: Tất cả các nhóm máu (A, B, AB, O) bất kể Rh

Ý nghĩa lâm sàng:
- Người nhóm máu O (Rh−) rất quan trọng trong cấp cứu, có thể cho máu cho bất kỳ ai.

Lưu ý:
- Do tính hiếm, cần bảo tồn nguồn máu O (Rh−) cho các trường hợp khẩn cấp.
', GETDATE(), GETDATE());
GO

INSERT INTO BlogPostTags (BlogPostID, TagID)
VALUES
-- Bài 1: A Rh+
(1, 1), (1, 5), (1, 7),
-- Bài 2: A Rh−
(2, 1), (2, 6), (2, 7),
-- Bài 3: B Rh+
(3, 2), (3, 5), (3, 7),
-- Bài 4: B Rh−
(4, 2), (4, 6), (4, 7),
-- Bài 5: AB Rh+
(5, 3), (5, 5), (5, 7),
-- Bài 6: AB Rh−
(6, 3), (6, 6), (6, 7),
-- Bài 7: O Rh+
(7, 4), (7, 5), (7, 7),
-- Bài 8: O Rh−
(8, 4), (8, 6), (8, 7);
GO

INSERT INTO BloodInventory (BloodGroup, RhType, ComponentType, Quantity, IsRare) VALUES
('A', 'Rh+', 'Whole', 10, 0),
('B', 'Rh-', 'RedCells', 5, 0),
('AB', 'Rh-', 'Plasma', 2, 1),
('O', 'Rh+', 'Platelets', 8, 0),
('A', 'Rh-', 'Whole', 3, 0);
GO

INSERT INTO BloodRequests (UserID, BloodGroup, RhType, BloodComponent, Quantity, UrgencyLevel, NeededTime, Reason, Status, CreatedTime)
VALUES
(1, 'A', 'Rh+', 'Plasma', 2, 'High', DATEADD(DAY, 1, GETDATE()), 'Surgery', 0, GETDATE()),
(2, 'O', 'Rh-', 'Whole Blood', 1, 'Medium', DATEADD(DAY, 2, GETDATE()), 'Accident', 0, GETDATE()),
(3, 'B', 'Rh+', 'Platelets', 3, 'Low', DATEADD(DAY, 3, GETDATE()), 'Cancer treatment', 1, GETDATE()),
(4, 'AB', 'Rh-', 'Red Cells', 2, 'High', DATEADD(DAY, 1, GETDATE()), 'Emergency', 2, GETDATE()),
(5, 'O', 'Rh+', 'Whole Blood', 1, 'Medium', DATEADD(DAY, 2, GETDATE()), 'Anemia', 0, GETDATE());
GO

INSERT INTO BloodRequestHistory (UserID, RequestID, Status, TimeStamp)
VALUES
(1, 1, 'Requested', GETDATE()),
(2, 2, 'Requested', GETDATE()),
(3, 3, 'Accepted', GETDATE()),
(4, 4, 'Completed', GETDATE()),
(5, 5, 'Requested', GETDATE());
GO

INSERT INTO BloodDonationHistory (UserID, FacilityID, DonationDate, BloodGroup, RhType, ComponentType, Quantity, Notes)
VALUES
(1, 1, GETDATE(), 'A', 'Rh+', 'Plasma', 1, 'Routine donation'),
(2, 2, GETDATE(), 'O', 'Rh-', 'Whole Blood', 1, 'After 6 months'),
(3, 1, GETDATE(), 'B', 'Rh+', 'Platelets', 1, 'First time donor'),
(4, 3, GETDATE(), 'AB', 'Rh-', 'Red Cells', 1, 'Voluntary'),
(5, 2, GETDATE(), 'O', 'Rh+', 'Whole Blood', 1, 'No issues');
GO

INSERT INTO Notifications (UserID, Title, Message, IsRead, SENT)
VALUES
(1, 'Donation Reminder', 'You can donate again in 2 weeks', 0, GETDATE()),
(2, 'Urgent Request', 'We need your blood type urgently', 0, GETDATE()),
(3, 'Thank You', 'Thanks for donating!', 1, GETDATE()),
(4, 'Blood Request Approved', 'Your request has been approved', 1, GETDATE()),
(5, 'New Article', 'Check out our latest blood donation guide', 0, GETDATE());
GO
