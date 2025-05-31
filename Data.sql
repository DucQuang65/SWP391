USE BloodManagementSystem
Go

INSERT INTO Roles (RoleName)
VALUES 
('Member'),
('Doctor'),
('BloodManager'),
('Admin');
GO

INSERT INTO HospitalInfo (ID, Name, Address, Phone, Email, WorkingHours, MapImageUrl, Latitude, Longitude)
VALUES
(1, N'Trung Tâm Hiến Máu', N'đường CMT8, Q.3, TP.HCM, Vietnam', '02838554137', 'trungtamhienmau@gmail.vn', N'Thứ 2 - Thứ 6: 7:00 - 17:00','https://maps.app.goo.gl/NhCZ66UD3kdH2bPM6', '10.7751237', '106.6862143'),
GO

INSERT INTO Tags (TagID, TagName) VALUES
-- Nhóm máu
(1, 'A'),
(2, 'B'),
(3, 'AB'),
(4, 'O'),
(5, 'Rh+'),
(6, 'Rh-'),

-- Tag bài viết về hiến máu
(7, N'Tổng quan nhóm máu'),
(8, N'Truyền máu'),
(9,  N'Hiến Máu Lần Đầu'),
(10, N'Chuẩn Bị Trước Hiến Máu'),
(11, N'Quy Trình Hiến Máu'),
(12, N'Lợi Ích Hiến Máu'),
(13, N'Hiến Máu Định Kỳ'),
(14, N'Sức Khoẻ'),
(15, N'Nhóm Máu'),
(16, N'Truyền Máu'),
(17, N'Kiến Thức Y Khoa'),
(18, N'Quy Trình Xử Lý Máu'),
(19, N'Tổ Chức Hiến Máu'),
(20, N'Cứu Người'),
(21, N'Hiến Máu Toàn Phần'),
(22, N'Hiến Tiểu Cầu'),
(23, N'Kỹ Thuật Hiến Máu'),
(24, N'Câu Chuyện Hiến Máu'),
(25, N'Truyền Cảm Hứng'),
(26, N'Người Hiến Ẩn Danh'),
(27, N'Nhóm Máu O'),
(28, N'Hiếm Máu'),
(29, N'Kêu Gọi Hiến Máu'),
(30, N'Sự Kiện Hiến Máu'),
(31, N'Ngày Hội Hiến Máu'),
(32, N'Đăng Ký Hiến Máu'),
(33, N'FAQ Hiến Máu'),
(34, N'Giải Đáp Thắc Mắc'),
(35, N'Hiểu Đúng Hiến Máu'),
(36, N'Hiến Máu Thường Xuyên'),
(37, N'Chăm Sóc Sức Khoẻ'),
(38, N'Lưu Ý Sau Hiến Máu');
GO

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

INSERT INTO BlogPosts (PostID, Title, Content, CreatedAt) VALUES
(1, N'🩸 Hiến Máu Lần Đầu: Hành Trình Nhân Ái Bắt Đầu Từ Một Giọt Máu', 
N'1. Vì Sao Nên Hiến Máu?
Hiến máu là một hành động cao cả, mang lại cơ hội sống cho hàng triệu người mỗi năm. Mỗi đơn vị máu bạn hiến có thể cứu sống đến ba người nhờ việc tách thành các thành phần như hồng cầu, tiểu cầu và huyết tương.

Ngoài ra, hiến máu còn giúp bạn:

Kiểm tra sức khỏe miễn phí: Trước khi hiến, bạn sẽ được kiểm tra huyết áp, nhịp tim, và xét nghiệm máu.

Cải thiện tuần hoàn máu: Việc hiến máu định kỳ giúp kích thích cơ thể sản sinh máu mới.

Giảm nguy cơ mắc bệnh tim mạch: Một số nghiên cứu cho thấy hiến máu có thể giảm lượng sắt dư thừa, từ đó giảm nguy cơ bệnh tim.

2. Chuẩn Bị Trước Khi Hiến Máu
Để đảm bảo quá trình hiến máu diễn ra suôn sẻ, bạn nên:

Ngủ đủ giấc: Ít nhất 7–8 tiếng trước ngày hiến máu.

Ăn nhẹ: Tránh ăn thực phẩm nhiều dầu mỡ; nên ăn nhẹ trước khi hiến máu khoảng 2 tiếng.

Uống đủ nước: Giúp duy trì huyết áp ổn định và dễ dàng lấy máu.

Mang theo giấy tờ tùy thân: CMND/CCCD hoặc giấy tờ hợp lệ khác.

3. Quy Trình Hiến Máu Diễn Ra Như Thế Nào?
Quy trình hiến máu thường bao gồm các bước sau:

Đăng ký: Điền thông tin cá nhân và lịch sử y tế.

Khám sàng lọc: Kiểm tra huyết áp, nhịp tim, và xét nghiệm máu nhanh.

Hiến máu: Quá trình lấy máu kéo dài khoảng 10–15 phút.

Nghỉ ngơi: Sau khi hiến, bạn sẽ được nghỉ ngơi và ăn nhẹ để phục hồi.

4. Lưu Ý Sau Khi Hiến Máu
Sau khi hiến máu, bạn nên:

Uống nhiều nước: Giúp cơ thể nhanh chóng bù đắp lượng máu đã mất.

Tránh vận động mạnh: Trong 24 giờ đầu tiên, hạn chế các hoạt động thể chất nặng.

Ăn uống đầy đủ: Bổ sung thực phẩm giàu sắt như thịt đỏ, rau xanh đậm.

Theo dõi sức khỏe: Nếu có dấu hiệu bất thường, hãy liên hệ với cơ sở y tế gần nhất.

5. Kết Luận
Hiến máu không chỉ là hành động cứu người mà còn mang lại nhiều lợi ích cho chính bạn. Nếu bạn đang cân nhắc hiến máu lần đầu, hãy chuẩn bị kỹ lưỡng và đừng ngần ngại tham gia. Một giọt máu cho đi, một cuộc đời ở lại.

', '2025-05-19 16:03:52'),
(2, N'🩸 Người Hiến Máu Thường Xuyên Cần Lưu Ý Điều Gì?', 
N'
1. Khoảng Cách Giữa Các Lần Hiến Máu
Để đảm bảo sức khỏe, người hiến máu cần tuân thủ khoảng cách tối thiểu giữa các lần hiến:

Hiến máu toàn phần: ít nhất 12 tuần (3 tháng) giữa hai lần hiến.

Hiến tiểu cầu hoặc huyết tương: có thể thực hiện sau mỗi 2 tuần, tùy theo chỉ định của cơ sở y tế.
Vinmec International Hospital

Việc tuân thủ khoảng cách này giúp cơ thể có đủ thời gian để phục hồi và tái tạo lượng máu đã hiến.

2. Chế Độ Dinh Dưỡng Hợp Lý
Người hiến máu thường xuyên nên duy trì chế độ ăn uống cân đối, giàu chất sắt và vitamin:

Thực phẩm giàu sắt: thịt đỏ, gan, rau xanh đậm, đậu, hạt.

Vitamin C: cam, chanh, dâu tây, giúp tăng cường hấp thu sắt.

Tránh: các thực phẩm nhiều chất béo và đồ uống có cồn trước khi hiến máu.

Chế độ dinh dưỡng hợp lý giúp duy trì lượng hemoglobin ổn định và hỗ trợ quá trình tái tạo máu.

3. Lối Sống Lành Mạnh
Để đảm bảo sức khỏe khi hiến máu thường xuyên, bạn nên:

Ngủ đủ giấc: ngủ từ 7–8 tiếng mỗi đêm.

Tập thể dục đều đặn: duy trì hoạt động thể chất nhẹ nhàng như đi bộ, yoga.

Tránh căng thẳng: thực hành thiền, hít thở sâu để giảm stress.

Lối sống lành mạnh giúp cơ thể phục hồi nhanh chóng sau mỗi lần hiến máu.

4. Theo Dõi Sức Khỏe Định Kỳ
Người hiến máu thường xuyên nên kiểm tra sức khỏe định kỳ:

Xét nghiệm máu: kiểm tra hemoglobin, sắt huyết thanh.

Khám tổng quát: đánh giá tổng thể tình trạng sức khỏe.

Việc theo dõi sức khỏe giúp phát hiện sớm các vấn đề và đảm bảo an toàn khi tiếp tục hiến máu.

5. Lưu Ý Sau Khi Hiến Máu
Sau mỗi lần hiến máu, bạn nên:

Uống nhiều nước: giúp cơ thể bù đắp lượng dịch đã mất.

Ăn nhẹ: bổ sung năng lượng bằng bữa ăn nhẹ sau hiến máu.

Tránh vận động mạnh: trong 24 giờ đầu sau hiến máu.

Những lưu ý này giúp cơ thể bạn phục hồi nhanh chóng và chuẩn bị tốt cho lần hiến máu tiếp theo.', '2025-05-21 16:03:52'),
(3, N'🩸 Những Lợi Ích Sức Khỏe Khi Hiến Máu Định Kỳ',
N'Hiến Máu – Không Chỉ Là Cứu Người

Hiến máu từ lâu được biết đến là một hành động nhân đạo cao đẹp, giúp cứu sống hàng triệu người mỗi năm. Tuy nhiên, ít ai biết rằng việc hiến máu định kỳ cũng mang lại nhiều lợi ích thiết thực cho chính người hiến.

Khi bạn hiến máu đều đặn, cơ thể không chỉ được kích thích sản sinh máu mới mà còn tạo điều kiện để bạn:

Giảm lượng sắt dư thừa: Duy trì mức sắt ổn định giúp hạn chế nguy cơ mắc bệnh tim và gan.

Cải thiện tuần hoàn máu: Việc hiến máu thúc đẩy quá trình sản sinh tế bào máu mới, giúp máu lưu thông tốt hơn.

Tầm soát bệnh: Mỗi lần hiến máu đều được kiểm tra miễn phí các chỉ số như huyết áp, nhịp tim, và xét nghiệm máu giúp phát hiện sớm các bệnh lý tiềm ẩn.

Tăng cường sức khỏe tinh thần: Cảm giác được giúp đỡ người khác mang lại sự hài lòng, giảm stress và nâng cao chất lượng cuộc sống.

Lợi Ích Với Tâm Trạng Và Cuộc Sống

Không chỉ cải thiện thể chất, hiến máu còn giúp cải thiện tinh thần đáng kể:

Giảm căng thẳng: Khi làm việc tốt, cơ thể tiết ra hormone hạnh phúc giúp bạn cảm thấy tích cực hơn.

Xây dựng thói quen sống lành mạnh: Người hiến máu thường xuyên sẽ chú ý hơn đến dinh dưỡng, giấc ngủ và luyện tập để đảm bảo đủ điều kiện sức khỏe.

Gắn kết cộng đồng: Hiến máu là một hoạt động kết nối mọi người, lan tỏa yêu thương và trách nhiệm xã hội.

Hiến Máu Bao Nhiêu Lần Là Đủ?

Tùy vào loại hiến máu, mỗi người có thể hiến với tần suất khác nhau:

Hiến máu toàn phần: Tối đa 4 lần/năm đối với nam và 3 lần/năm với nữ.

Hiến tiểu cầu/huyết tương: Có thể lặp lại sau mỗi 2 tuần, nhưng không quá 24 lần/năm.

Điều quan trọng là bạn cần theo dõi sức khỏe và tuân thủ hướng dẫn từ nhân viên y tế để đảm bảo an toàn.

Lưu Ý Khi Hiến Máu Định Kỳ

Để đảm bảo hiến máu hiệu quả và an toàn, hãy:

Ăn uống đầy đủ trước và sau hiến máu.

Uống nhiều nước để hỗ trợ quá trình tuần hoàn.

Tránh vận động mạnh sau hiến máu ít nhất 24 giờ.

Giữ tâm trạng thoải mái và ngủ đủ giấc trước ngày hiến máu.

Kết Luận

Hiến máu định kỳ không chỉ cứu người mà còn là liệu pháp giúp bạn sống khỏe mạnh, hạnh phúc và có ích hơn cho cộng đồng. Hãy biến việc hiến máu trở thành thói quen đẹp trong cuộc sống của bạn.', '2025-05-23 16:03:52'),
(4, N'Hiến Máu Định Kỳ – Một Lối Sống Đẹp', N'Hiến máu định kỳ không chỉ giúp cộng đồng duy trì nguồn máu dự trữ ổn định mà còn là một cách để theo dõi sức khỏe định kỳ. Mỗi người trưởng thành khỏe mạnh có thể hiến máu 3–4 lần mỗi năm và vẫn duy trì được sức khỏe bình thường.', '2025-05-25 16:03:52'),
(5, N'Câu Chuyện Của Một Người Hiến Máu Ẩn Danh', N'Anh Nguyễn Văn A là một người hiến máu thường xuyên nhưng luôn giữ kín danh tính. Đối với anh, việc hiến máu là cách thể hiện lòng biết ơn cuộc sống và chia sẻ với những người cần. Câu chuyện của anh đã truyền cảm hứng cho rất nhiều người trẻ tham gia hiến máu.', '2025-05-28 16:03:52');
GO

INSERT INTO PostTags (PostID, TagID) VALUES
(1, 12),
(1, 20),
(1, 25),
(2, 10),
(2, 14),
(2, 37),
(3, 11),
(3, 35),
(3, 18),
(4, 13),
(4, 36),
(4, 14),
(5, 24),
(5, 26),
(5, 25);
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
