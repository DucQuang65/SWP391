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
(16, N'Kiến Thức Y Khoa'),
(17, N'Quy Trình Xử Lý Máu'),
(18, N'Tổ Chức Hiến Máu'),
(19, N'Cứu Người'),
(20, N'Hiến Máu Toàn Phần'),
(21, N'Hiến Tiểu Cầu'),
(22, N'Kỹ Thuật Hiến Máu'),
(23, N'Câu Chuyện Hiến Máu'),
(24, N'Truyền Cảm Hứng'),
(25, N'Người Hiến Ẩn Danh'),
(26, N'Phân Loại Máu'),
(27, N'Hiến Máu'),
(28, N'Kêu Gọi Hiến Máu'),
(29, N'Sự Kiện Hiến Máu'),
(30, N'Ngày Hội Hiến Máu'),
(31, N'Đăng Ký Hiến Máu'),
(32, N'FAQ Hiến Máu'),
(33, N'Giải Đáp Thắc Mắc'),
(34, N'Hiểu Đúng Hiến Máu'),
(35, N'Hiến Máu Thường Xuyên'),
(36, N'Chăm Sóc Sức Khoẻ'),
(37, N'Lưu Ý Trước Hiến Máu');
(38, N'Lưu Ý Sau Hiến Máu');
(39, N'Hướng Dẫn Hiến Máu'),
(40, N'Dinh Dưỡng Cho Người Hiến Máu'),
(41, N'Theo Dõi Sức Khỏe'),
(42, N'Sức Khỏe Tinh Thần'),
(43, N'Phòng Bệnh');
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
🔹Hiến máu là một hành động cao cả, mang lại cơ hội sống cho hàng triệu người mỗi năm. Mỗi đơn vị máu bạn hiến có thể cứu sống đến ba người nhờ việc tách thành các thành phần như hồng cầu, tiểu cầu và huyết tương.

Ngoài ra, hiến máu còn giúp bạn:

🔹Kiểm tra sức khỏe miễn phí: Trước khi hiến, bạn sẽ được kiểm tra huyết áp, nhịp tim, và xét nghiệm máu.

🔹Cải thiện tuần hoàn máu: Việc hiến máu định kỳ giúp kích thích cơ thể sản sinh máu mới.

🔹Giảm nguy cơ mắc bệnh tim mạch: Một số nghiên cứu cho thấy hiến máu có thể giảm lượng sắt dư thừa, từ đó giảm nguy cơ bệnh tim.

2. Chuẩn Bị Trước Khi Hiến Máu
Để đảm bảo quá trình hiến máu diễn ra suôn sẻ, bạn nên:

🔹Ngủ đủ giấc: Ít nhất 7–8 tiếng trước ngày hiến máu.

🔹Ăn nhẹ: Tránh ăn thực phẩm nhiều dầu mỡ; nên ăn nhẹ trước khi hiến máu khoảng 2 tiếng.

🔹Uống đủ nước: Giúp duy trì huyết áp ổn định và dễ dàng lấy máu.

🔹Mang theo giấy tờ tùy thân: CMND/CCCD hoặc giấy tờ hợp lệ khác.

3. Quy Trình Hiến Máu Diễn Ra Như Thế Nào?
Quy trình hiến máu thường bao gồm các bước sau:

🔹Đăng ký: Điền thông tin cá nhân và lịch sử y tế.

🔹Khám sàng lọc: Kiểm tra huyết áp, nhịp tim, và xét nghiệm máu nhanh.

🔹Hiến máu: Quá trình lấy máu kéo dài khoảng 10–15 phút.

🔹Nghỉ ngơi: Sau khi hiến, bạn sẽ được nghỉ ngơi và ăn nhẹ để phục hồi.

4. Lưu Ý Sau Khi Hiến Máu
Sau khi hiến máu, bạn nên:

🔹Uống nhiều nước: Giúp cơ thể nhanh chóng bù đắp lượng máu đã mất.

🔹Tránh vận động mạnh: Trong 24 giờ đầu tiên, hạn chế các hoạt động thể chất nặng.

🔹Ăn uống đầy đủ: Bổ sung thực phẩm giàu sắt như thịt đỏ, rau xanh đậm.

🔹Theo dõi sức khỏe: Nếu có dấu hiệu bất thường, hãy liên hệ với cơ sở y tế gần nhất.

5. Kết Luận
👉Hiến máu không chỉ là hành động cứu người mà còn mang lại nhiều lợi ích cho chính bạn. Nếu bạn đang cân nhắc hiến máu lần đầu, hãy chuẩn bị kỹ lưỡng và đừng ngần ngại tham gia. Một giọt máu cho đi, một cuộc đời ở lại.

', '2025-05-19 16:03:52'),
(2, N'🩸 Người Hiến Máu Thường Xuyên Cần Lưu Ý Điều Gì?', 
N'
1. Khoảng Cách Giữa Các Lần Hiến Máu
Để đảm bảo sức khỏe, người hiến máu cần tuân thủ khoảng cách tối thiểu giữa các lần hiến:

🔹Hiến máu toàn phần: ít nhất 12 tuần (3 tháng) giữa hai lần hiến.

🔹Hiến tiểu cầu hoặc huyết tương: có thể thực hiện sau mỗi 2 tuần, tùy theo chỉ định của cơ sở y tế.

🔹Việc tuân thủ khoảng cách này giúp cơ thể có đủ thời gian để phục hồi và tái tạo lượng máu đã hiến.

2. Chế Độ Dinh Dưỡng Hợp Lý
Người hiến máu thường xuyên nên duy trì chế độ ăn uống cân đối, giàu chất sắt và vitamin:

🔹Thực phẩm giàu sắt: thịt đỏ, gan, rau xanh đậm, đậu, hạt.

🔹Vitamin C: cam, chanh, dâu tây, giúp tăng cường hấp thu sắt.

🔹Tránh: các thực phẩm nhiều chất béo và đồ uống có cồn trước khi hiến máu.

🔹Chế độ dinh dưỡng hợp lý giúp duy trì lượng hemoglobin ổn định và hỗ trợ quá trình tái tạo máu.

3. Lối Sống Lành Mạnh
Để đảm bảo sức khỏe khi hiến máu thường xuyên, bạn nên:

🔹Ngủ đủ giấc: ngủ từ 7–8 tiếng mỗi đêm.

🔹Tập thể dục đều đặn: duy trì hoạt động thể chất nhẹ nhàng như đi bộ, yoga.

🔹Tránh căng thẳng: thực hành thiền, hít thở sâu để giảm stress.

🔹Lối sống lành mạnh giúp cơ thể phục hồi nhanh chóng sau mỗi lần hiến máu.

4. Theo Dõi Sức Khỏe Định Kỳ
🔹Người hiến máu thường xuyên nên kiểm tra sức khỏe định kỳ:

🔹Xét nghiệm máu: kiểm tra hemoglobin, sắt huyết thanh.

🔹Khám tổng quát: đánh giá tổng thể tình trạng sức khỏe.

🔹Việc theo dõi sức khỏe giúp phát hiện sớm các vấn đề và đảm bảo an toàn khi tiếp tục hiến máu.

5. Lưu Ý Sau Khi Hiến Máu
Sau mỗi lần hiến máu, bạn nên:

🔹Uống nhiều nước: giúp cơ thể bù đắp lượng dịch đã mất.

🔹Ăn nhẹ: bổ sung năng lượng bằng bữa ăn nhẹ sau hiến máu.

🔹Tránh vận động mạnh: trong 24 giờ đầu sau hiến máu.

👉Những lưu ý này giúp cơ thể bạn phục hồi nhanh chóng và chuẩn bị tốt cho lần hiến máu tiếp theo.', '2025-05-21 16:03:52'),
(3, N'🩸 Những Lợi Ích Sức Khỏe Khi Hiến Máu Định Kỳ',
N'Hiến Máu – Không Chỉ Là Cứu Người

Hiến máu từ lâu được biết đến là một hành động nhân đạo cao đẹp, giúp cứu sống hàng triệu người mỗi năm. Tuy nhiên, ít ai biết rằng việc hiến máu định kỳ cũng mang lại nhiều lợi ích thiết thực cho chính người hiến.

Khi bạn hiến máu đều đặn, cơ thể không chỉ được kích thích sản sinh máu mới mà còn tạo điều kiện để bạn:

🔹Giảm lượng sắt dư thừa: Duy trì mức sắt ổn định giúp hạn chế nguy cơ mắc bệnh tim và gan.

🔹Cải thiện tuần hoàn máu: Việc hiến máu thúc đẩy quá trình sản sinh tế bào máu mới, giúp máu lưu thông tốt hơn.

🔹Tầm soát bệnh: Mỗi lần hiến máu đều được kiểm tra miễn phí các chỉ số như huyết áp, nhịp tim, và xét nghiệm máu giúp phát hiện sớm các bệnh lý tiềm ẩn.

🔹Tăng cường sức khỏe tinh thần: Cảm giác được giúp đỡ người khác mang lại sự hài lòng, giảm stress và nâng cao chất lượng cuộc sống.

Lợi Ích Với Tâm Trạng Và Cuộc Sống

Không chỉ cải thiện thể chất, hiến máu còn giúp cải thiện tinh thần đáng kể:

🔹Giảm căng thẳng: Khi làm việc tốt, cơ thể tiết ra hormone hạnh phúc giúp bạn cảm thấy tích cực hơn.

🔹Xây dựng thói quen sống lành mạnh: Người hiến máu thường xuyên sẽ chú ý hơn đến dinh dưỡng, giấc ngủ và luyện tập để đảm bảo đủ điều kiện sức khỏe.

🔹Gắn kết cộng đồng: Hiến máu là một hoạt động kết nối mọi người, lan tỏa yêu thương và trách nhiệm xã hội.

Hiến Máu Bao Nhiêu Lần Là Đủ?

🔹Tùy vào loại hiến máu, mỗi người có thể hiến với tần suất khác nhau:

🔹Hiến máu toàn phần: Tối đa 4 lần/năm đối với nam và 3 lần/năm với nữ.

🔹Hiến tiểu cầu/huyết tương: Có thể lặp lại sau mỗi 2 tuần, nhưng không quá 24 lần/năm.

🔹Điều quan trọng là bạn cần theo dõi sức khỏe và tuân thủ hướng dẫn từ nhân viên y tế để đảm bảo an toàn.

Lưu Ý Khi Hiến Máu Định Kỳ

Để đảm bảo hiến máu hiệu quả và an toàn, hãy:

🔹Ăn uống đầy đủ trước và sau hiến máu.

🔹Uống nhiều nước để hỗ trợ quá trình tuần hoàn.

🔹Tránh vận động mạnh sau hiến máu ít nhất 24 giờ.

🔹Giữ tâm trạng thoải mái và ngủ đủ giấc trước ngày hiến máu.

Kết Luận

👉Hiến máu định kỳ không chỉ cứu người mà còn là liệu pháp giúp bạn sống khỏe mạnh, hạnh phúc và có ích hơn cho cộng đồng. Hãy biến việc hiến máu trở thành thói quen đẹp trong cuộc sống của bạn.', '2025-05-23 16:03:52'),
(4, N'🩸 Hiểu Đúng Về Các Nhóm Máu Và Vai Trò Trong Hiến Máu', 
N'1. Nhóm Máu Là Gì?
Máu của mỗi người được phân loại dựa trên sự hiện diện hay vắng mặt của các kháng nguyên và kháng thể. Hai hệ thống phân loại nhóm máu phổ biến nhất hiện nay là:

🔹Hệ ABO: Gồm 4 nhóm chính – A, B, AB và O.

🔹Hệ Rh: Dựa trên sự hiện diện (+) hoặc không có (-) của yếu tố Rh (Rhesus) trên bề mặt hồng cầu.

Khi kết hợp lại, ta có 8 nhóm máu phổ biến: A+, A−, B+, B−, AB+, AB−, O+, O−.

2. Các Nhóm Máu Phổ Biến Như Thế Nào?
Tỷ lệ các nhóm máu không đồng đều ở từng khu vực và quốc gia. Tại Việt Nam, thống kê y học cho thấy:

🔹O+: Chiếm khoảng 42% dân số – nhóm máu phổ biến nhất.

🔹A+: Khoảng 25% – cũng rất phổ biến.

🔹B+: Chiếm khoảng 23%.

🔹AB+: Khoảng 7%.

🔹Nhóm Rh− (âm tính): Rất hiếm, chỉ khoảng 0.04% dân số.

👉Sự phân bố này ảnh hưởng rất lớn đến nhu cầu và khả năng tiếp nhận máu khi truyền.

3. Vì Sao Nhóm Máu Quan Trọng Trong Hiến Máu?
Trong truyền máu, việc phù hợp nhóm máu giữa người cho và người nhận là vô cùng quan trọng. Nếu truyền sai nhóm máu, có thể xảy ra phản ứng miễn dịch nghiêm trọng, thậm chí gây tử vong.

Một số quy tắc cơ bản:

🔹Nhóm O− là nhóm máu "phổ quát", có thể cho tất cả các nhóm máu khác trong trường hợp khẩn cấp.

🔹Nhóm O+ có thể cho O+, A+, B+ và AB+.

🔹Nhóm A− có thể cho A− và A+, AB− và AB+.

🔹Nhóm A+ có thể cho A+ và AB+.

🔹Nhóm B− có thể cho B− và B+, AB− và AB+.

🔹Nhóm B+ có thể cho B+ và AB+.

🔹Nhóm AB− có thể cho AB− và AB+.

🔹Nhóm AB+ chỉ có thể cho AB+, nhưng lại có thể nhận máu từ tất cả các nhóm – gọi là "người nhận phổ quát".

👉Tuy nhiên, các nguyên tắc trên chỉ áp dụng cho truyền máu toàn phần hoặc hồng cầu, còn với tiểu cầu hoặc huyết tương, nguyên tắc có thể khác biệt.

Nhóm máu đặc biệt:
🔹O− (người cho máu toàn phần phổ quát): Có thể cho bất kỳ nhóm máu nào.

🔹AB+ (người nhận máu toàn phần phổ quát): Có thể nhận máu từ bất kỳ nhóm nào.

👉Tuy nhiên, điều này chỉ đúng với máu toàn phần hoặc khối hồng cầu. Với tiểu cầu, huyết tương hay các thành phần khác, quy tắc truyền có thể khác.

4. Ai Là Người Cần Máu?
Máu được sử dụng trong rất nhiều trường hợp y tế:

🔹Người gặp tai nạn, mất máu cấp.

🔹Bệnh nhân phẫu thuật (đặc biệt là các ca mổ lớn).

🔹Người bệnh tan máu bẩm sinh, ung thư máu, suy tủy.

🔹Phụ nữ mang thai bị băng huyết hoặc biến chứng thai sản.

🔹Trẻ sinh non hoặc sơ sinh thiếu máu.

👉Do đó, nhu cầu về máu luôn cao và diễn ra liên tục. Việc hiểu rõ nhóm máu giúp người dân chủ động hơn trong việc hiến máu phù hợp.

5. Vai Trò Của Các Nhóm Máu Trong Hiến Máu
Mỗi nhóm máu có vai trò riêng biệt trong công tác hiến máu cứu người:

🔹Nhóm O−: Rất quý hiếm. Dù chỉ chiếm tỷ lệ nhỏ, nhưng cực kỳ quan trọng trong cấp cứu khẩn cấp vì có thể truyền cho tất cả các nhóm máu.

🔹Nhóm O+ và A+: Do số lượng lớn người sở hữu nhóm máu này nên lượng máu dự trữ cần duy trì đều đặn. Các bệnh viện thường xuyên cần máu từ nhóm này.

🔹Nhóm AB: Dù ít người mang nhóm máu này, nhưng huyết tương AB lại là loại phổ quát – có thể truyền cho mọi nhóm máu. Do đó, người nhóm AB được khuyến khích hiến huyết tương định kỳ.

🔹Rh−: Do tỉ lệ cực thấp, nên người nhóm máu Rh− được xem là "kho máu hiếm". Họ nên tham gia ngân hàng máu hiếm để hỗ trợ khi có ca bệnh cần khẩn cấp.

6. Bạn Đã Biết Nhóm Máu Của Mình Chưa?
🔹Rất nhiều người chưa biết nhóm máu của mình – điều này tiềm ẩn rủi ro trong các tình huống cấp cứu. Khi đi hiến máu, bạn sẽ được xét nghiệm miễn phí nhóm máu và biết được thông tin quan trọng này. Ngoài ra, bạn có thể yêu cầu cấp thẻ người hiến máu để lưu trữ và sử dụng trong các trường hợp khẩn cấp.

7. Kết Luận
🔹Việc hiểu đúng về các nhóm máu không chỉ giúp bạn bảo vệ sức khỏe cá nhân mà còn giúp bạn chủ động hơn trong việc tham gia hiến máu phù hợp, đúng thời điểm, đúng nhu cầu. Dù bạn thuộc nhóm máu nào, mỗi giọt máu bạn cho đi đều mang lại cơ hội sống cho người khác.

👉Hiến máu không chỉ là một hành động tốt – đó còn là sự kết nối kỳ diệu giữa những trái tim.', '2025-05-25 16:03:52'),
(5, N'🩸 Máu Hiến Sẽ Đi Đâu Và Được Sử Dụng Như Thế Nào?', 
N'Hành Trình Của Máu
Bạn có bao giờ tự hỏi: Sau khi hiến máu, đơn vị máu ấy sẽ được xử lý và sử dụng ra sao? Hành trình của máu không dừng lại tại điểm hiến – mà nó bắt đầu một chuỗi quy trình y tế nghiêm ngặt để đảm bảo an toàn và hiệu quả trong cứu chữa.

1. Tiếp Nhận Và Bảo Quản
Ngay sau khi bạn hiến máu, túi máu được:

🔹Gắn mã số định danh duy nhất.

🔹Đặt vào hộp chuyên dụng bảo quản lạnh (khoảng 2–6°C).

🔹Vận chuyển về trung tâm huyết học hoặc ngân hàng máu trong thời gian ngắn nhất.

👉Tại đây, máu sẽ được lưu trữ tạm thời trong điều kiện tiêu chuẩn trước khi phân tách và xét nghiệm.

2. Xét Nghiệm Và Kiểm Tra
Mỗi đơn vị máu đều phải trải qua các bước xét nghiệm nghiêm ngặt:

🔹Kiểm tra nhóm máu: ABO và Rh.

🔹Sàng lọc bệnh truyền nhiễm: HIV, viêm gan B và C, giang mai, sốt rét, v.v.

🔹Kiểm tra kháng thể bất thường để đảm bảo tương thích khi truyền.

🔹Chỉ những đơn vị máu đạt tiêu chuẩn an toàn tuyệt đối mới được đưa vào sử dụng.

3. Phân Tách Các Thành Phần Máu
Một đơn vị máu toàn phần có thể được tách ra thành nhiều thành phần khác nhau, phục vụ cho các mục đích điều trị cụ thể:

🔹Hồng cầu: Truyền cho bệnh nhân thiếu máu, mất máu cấp.

🔹Tiểu cầu: Dành cho người bị xuất huyết giảm tiểu cầu, bệnh ung thư.

🔹Huyết tương tươi đông lạnh: Dùng trong cấp cứu, bệnh rối loạn đông máu.

👉Nhờ quá trình này, một đơn vị máu có thể cứu được từ 2 đến 3 bệnh nhân, giúp tối ưu hóa giá trị của máu hiến.

4. Cung Ứng Cho Bệnh Viện Và Trung Tâm Y Tế
Sau khi phân tích và xử lý, máu sẽ được phân phối đến:

🔹Bệnh viện đa khoa, trung tâm cấp cứu.

🔹Trung tâm điều trị ung thư, sản khoa.

🔹Cơ sở phẫu thuật, hồi sức tích cực, v.v.

👉Tại đây, máu được truyền trực tiếp cho bệnh nhân theo chỉ định của bác sĩ, góp phần quan trọng trong việc cứu sống hàng triệu người mỗi năm.

5. Những Trường Hợp Cần Máu Cấp Bách
Nhu cầu máu luôn ở mức cao, nhất là trong:

🔹Tai nạn giao thông, chấn thương nặng.

🔹Phẫu thuật lớn (tim mạch, ghép tạng).

🔹Bệnh lý huyết học như tan máu bẩm sinh, suy tủy.

🔹Sản phụ mất máu sau sinh.

🔹Bệnh nhân ung thư cần truyền tiểu cầu.

👉Việc hiến máu đều đặn và liên tục chính là nguồn lực quý giá giúp ngành y tế ứng phó với những tình huống khẩn cấp này.

Kết Luận
👉Máu bạn hiến ra không hề lãng phí – nó trải qua một quá trình kiểm định chặt chẽ và được sử dụng để mang lại sự sống cho những người đang chiến đấu với bệnh tật hoặc tai nạn. Mỗi giọt máu là một tia hy vọng. Hãy tiếp tục hành trình nhân đạo này, vì bạn có thể đang giữ trong mình “chìa khóa sống” của ai đó.', '2025-05-28 16:03:52'),
(6, N'🩸Sự Khác Biệt Giữa Hiến Máu Toàn Phần Và Hiến Tiểu Cầu

Không Chỉ Là “Hiến Máu” – Hãy Hiểu Đúng Hơn
Khi nghe đến “hiến máu”, nhiều người chỉ nghĩ đơn giản là lấy máu từ cơ thể người hiến để truyền cho người cần. Tuy nhiên, trong y học hiện đại, máu có thể được phân loại và hiến tách biệt theo nhu cầu điều trị. Hai hình thức phổ biến nhất là hiến máu toàn phần và hiến tiểu cầu – mỗi loại đều có quy trình, mục đích và lợi ích riêng biệt.

Hãy cùng khám phá sự khác biệt để lựa chọn hình thức hiến máu phù hợp nhất với bạn.

1. Hiến Máu Toàn Phần – Đơn Giản Và Phổ Biến
Hiến máu toàn phần là hình thức hiến máu truyền thống, trong đó khoảng 350–450ml máu được lấy ra từ tĩnh mạch người hiến, bao gồm tất cả các thành phần máu như: hồng cầu, bạch cầu, tiểu cầu và huyết tương.

🔹 Thời gian thực hiện: Khoảng 10–15 phút.
🔹 Thời gian phục hồi: Từ 1–2 tháng, do cơ thể cần tái tạo đủ hồng cầu.
🔹 Tần suất hiến máu:

Nam giới: Tối đa 4 lần/năm

Nữ giới: Tối đa 3 lần/năm

🔹 Ứng dụng lâm sàng: Máu toàn phần có thể được truyền trực tiếp hoặc phân tách thành nhiều thành phần để phục vụ nhiều bệnh nhân khác nhau.

👉 Ưu điểm: Nhanh chóng, dễ thực hiện, phù hợp với hầu hết người lần đầu hiến máu.

2. Hiến Tiểu Cầu – Chính Xác Và Chuyên Biệt
Hiến tiểu cầu là phương pháp trong đó máy tách tiểu cầu tự động sẽ thu lấy tiểu cầu từ máu người hiến, sau đó trả lại hồng cầu và huyết tương về cơ thể.

🔹 Thời gian thực hiện: 60–90 phút do cần quá trình lọc máu liên tục.
🔹 Thời gian phục hồi: Nhanh hơn, thường chỉ sau vài ngày.
🔹 Tần suất hiến máu: Có thể 2 tuần/lần, tối đa 24 lần/năm.

🔹 Ứng dụng lâm sàng: Tiểu cầu rất cần thiết cho bệnh nhân ung thư, người điều trị hóa chất, xuất huyết nặng hoặc rối loạn đông máu.

👉 Ưu điểm: Cung cấp tiểu cầu chất lượng cao, hỗ trợ điều trị cho các ca bệnh đặc biệt cần truyền gấp.

3. So Sánh Nhanh: Toàn Phần vs. Tiểu Cầu
Tiêu chí	Hiến máu toàn phần	Hiến tiểu cầu
Thành phần lấy	Tất cả thành phần máu	Chỉ tiểu cầu
Thời gian	10–15 phút	60–90 phút
Tần suất	3–4 lần/năm	24 lần/năm (cách 2 tuần/lần)
Ứng dụng	Đa dạng	Điều trị đặc biệt
Phục hồi	4–8 tuần	3–5 ngày
Yêu cầu thiết bị	Không	Máy tách tiểu cầu chuyên dụng

4. Nên Chọn Hình Thức Nào?
Nếu bạn mới bắt đầu hiến máu: Hiến máu toàn phần là lựa chọn an toàn và dễ thực hiện.

Nếu bạn có thời gian, sức khỏe ổn định và muốn hỗ trợ những ca bệnh đặc biệt: Hãy thử hiến tiểu cầu.

Tùy thuộc vào nhu cầu thực tế tại bệnh viện, bạn có thể được khuyến khích chọn hình thức phù hợp nhất.

Kết Luận
Dù là hiến máu toàn phần hay hiến tiểu cầu, mỗi giọt máu bạn trao đi đều mang trong mình giá trị sống vô giá. Việc hiểu rõ từng hình thức không chỉ giúp bạn chuẩn bị tốt hơn mà còn đảm bảo đóng góp hiệu quả nhất cho cộng đồng.

👉 Hãy chọn cách hiến máu phù hợp với bạn, và cùng nhau lan tỏa sự sống đến mọi người!','2025-04-28 10:23:52'),
(7, '🩸Câu Chuyện Thật: Một Đơn Vị Máu, Một Cuộc Đời Được Cứu',
'Đằng Sau Một Túi Máu – Là Một Cuộc Đời
Hiến máu là hành động giản dị nhưng mang ý nghĩa sâu sắc. Mỗi đơn vị máu bạn hiến tặng không chỉ là những giọt chất lỏng đỏ tươi – mà là niềm hy vọng sống còn của một con người, một gia đình, thậm chí cả một thế hệ.

Câu chuyện dưới đây là minh chứng rõ ràng nhất cho giá trị của một hành động nhỏ nhưng cứu cả một cuộc đời.

1. Em Bé 5 Tuổi Và Cuộc Chiến Với Căn Bệnh Hiếm
Tại khoa Huyết học – Bệnh viện Nhi Trung Ương, bé Linh (5 tuổi) mắc căn bệnh thiếu máu bất sản – một rối loạn hiếm gặp khiến cơ thể không thể tự sản sinh máu. Mỗi tháng, em cần truyền máu để duy trì sự sống.

Một lần, lượng máu trong kho bệnh viện sụt giảm nghiêm trọng, đặc biệt là nhóm máu O – nhóm máu của Linh. Các bác sĩ chỉ còn 12 giờ để tìm máu nếu không, em sẽ rơi vào hôn mê do thiếu oxy nghiêm trọng.

2. Người Lạ Trong Danh Sách Khẩn Cấp
Khi thông tin được đăng tải lên mạng xã hội, anh Minh – một nhân viên văn phòng 32 tuổi sống gần đó – ngay lập tức đến trung tâm hiến máu.

Là người từng hiến máu nhiều lần, anh Minh không ngần ngại khi biết ca bệnh khẩn cấp cần nhóm máu O. Sau khi xét nghiệm, anh đủ điều kiện và lập tức hiến tiểu cầu trực tiếp cho bé Linh.

Chỉ vài giờ sau, máu được truyền đến bệnh viện, và kịp thời cứu sống em bé khỏi biến chứng nguy hiểm.

3. Gặp Lại Sau Một Năm – Khoảnh Khắc Không Bao Giờ Quên
Một năm sau, trong chương trình “Lễ hội Xuân Hồng”, bé Linh – giờ đã khỏe mạnh và đi học bình thường – được mời lên sân khấu. Em mang bó hoa nhỏ để cảm ơn những người đã từng hiến máu cứu mình. Trùng hợp, anh Minh cũng có mặt trong sự kiện với tư cách người hiến tiêu biểu.

Khoảnh khắc hai người gặp nhau, cả hội trường vỡ òa trong xúc động. Em bé ôm lấy anh – người từng không quen biết – và nói:

“Cháu cảm ơn chú, nếu không có chú, cháu sẽ không được đến lớp học với các bạn...”

Không có món quà nào ý nghĩa hơn giây phút ấy.

4. Một Đơn Vị Máu – Nhiều Hơn Bạn Nghĩ
Một đơn vị máu có thể:

Cứu sống người gặp tai nạn giao thông.

Hồi sinh bệnh nhân xuất huyết nội.

Duy trì sự sống cho những người bị bệnh máu mãn tính như tan máu bẩm sinh, ung thư máu...

Mang đến cơ hội sống cho trẻ sơ sinh thiếu máu, người cần phẫu thuật tim mạch, ghép tạng...

👉 Một người hiến máu, có thể cứu tới 3 người bệnh khi máu được tách thành các thành phần khác nhau.

Kết Luận
Câu chuyện của bé Linh chỉ là một trong hàng ngàn ca được cứu sống nhờ sự sẻ chia của cộng đồng hiến máu. Hôm nay, bạn có thể là người hiến; ngày mai, người thân bạn có thể là người nhận.

Hiến máu không mất đi – chỉ là đang trao đi sự sống.
Hãy tham gia – để mỗi giọt máu bạn cho đi là một câu chuyện hồi sinh diệu kỳ.','2025-05-30 10:13:52'),
GO

INSERT INTO PostTags (PostID, TagID) VALUES
  -- Post 1:
(1, 9),(1, 12),(1, 37),(1,38),(1,39),
  -- Post 2:
(2, 13),(2, 14),(2, 38),1,40),(1,41),
  -- Post 3:
(3, 12),(3, 13),(3, 42),(3, 43),
  -- Post 4:
(4, 8),(4, 15),(4, 26),(4, 27),
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
