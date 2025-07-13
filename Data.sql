USE BloodManagementSystem;
GO

-- Insert data into Roles table
INSERT INTO Roles (RoleName)
VALUES 
    ('Member'),			-- ID = 1
    ('Doctor'),			-- ID = 2 
    ('BloodManager'),	-- ID = 3
    ('Admin'),			-- ID = 4 
    ('System');			-- ID = -1

INSERT INTO Departments (DepartmentName)
VALUES 
(N'Khoa Huyết học'),        -- ID = 1
(N'Khoa Tim mạch'),         -- ID = 2 
(N'Khoa Cấp cứu'),          -- ID = 3
(N'Khoa Nội tổng hợp'),     -- ID = 4 
(N'Khoa Nhi'),				-- ID = 5
(N'Khoa Truyền máu'),		-- ID = 6
(N'Khoa Giải phẫu'),		-- ID = 7
(N'Khoa Ngoại');			-- ID = 8

INSERT INTO Components (ComponentType)
VALUES 
(N'Hồng cầu'),        -- ID = 1
(N'Huyết tương'),     -- ID = 2
(N'Tiểu cầu'),        -- ID = 3
(N'Toàn phần');       -- ID = 4


-- Insert data into Users table
INSERT INTO Users (Email, Password, Phone, Name, Age, Gender, Address, BloodGroup, RhType, Status, RoleID, DepartmentID)
VALUES
	-- Role: Member (RoleID = 1)
    ('vinhntqse180354@fpt.edu.vn','Ab1234@', '0901234567', 'Vinh', 30, 'Male', N'123 Nguyễn Huệ, Phường Bến Nghé, Quận 1, TP.HCM', 'A', 'Rh+', 1, 1, NULL),
	('ducquang0565@gmail.com','Ab1234@', '0987654321', 'Duc', 25, 'Male', N'45 Lê Lợi, Phường 1, Quận 3, TP.HCM', 'O', 'Rh+', 1, 1, NULL),
	('member3@fpt.edu.vn', 'Ab1234@', '0931234567', 'Tuan', 19, 'Male', N'67 Trần Phú, Phường 4, Quận 5, TP.HCM', 'A', 'Rh+', 1, 1, NULL),

	-- Role: Doctor (RoleID = 2)
    ('kienlvse180681@fpt.edu.vn','Ab1234@', '0912345678', 'Kien', 45, 'Female', N'89 Phạm Văn Đồng, Phường Linh Đông, TP Thủ Đức, TP.HCM', 'O', 'Rh-', 1, 2, 1),
	('doctor2@fpt.edu.vn', 'Ab1234@', '0971234567', 'Hieu', 38, 'Male', N'234 Nguyễn Văn Cừ, Phường Cầu Kho, Quận 1, TP.HCM', 'A', 'Rh-', 1, 2, 2),
    ('doctor3@fpt.edu.vn', 'Ab1234@', '0941234567', 'Thao', 42, 'Female', N'56 Nguyễn Trãi, Phường 3, Quận 5, TP.HCM', 'O', 'Rh+', 1, 2, 1),

	-- Role: BloodManager (RoleID = 3)
    ('xpnhi023@gmail.com','Ab1234@', '0961234567', 'Nhi', 35, 'Male', N'78 Cách Mạng Tháng Tám, Phường 6, Quận 3, TP.HCM', 'B', 'Rh+', 1, 3, NULL),
	('bloodmanager2@gmail.com', 'Ab1234@', '0991234567', 'Hoa', 29, 'Female', N'90 Huỳnh Tấn Phát, Phường Tân Thuận Đông, Quận 7, TP.HCM', 'AB', 'Rh+', 1, 3, NULL),
    ('bloodmanager3@gmail.com', 'Ab1234@', '0701234567', 'Phong', 36, 'Male', N'12 Lê Văn Sỹ, Phường 13, Quận 3, TP.HCM', 'O', 'Rh+', 1, 3, NULL),

	-- Role: Admin (RoleID = 4)
	('admin1@gmail.com', 'Ab1234@', '0771234567', 'Linh', 31, 'Female', N'34 Bùi Thị Xuân, Phường 2, Quận Tân Bình, TP.HCM', 'A', 'Rh+', 1, 4, NULL),
    ('admin2@gmail.com', 'Ab1234@', '0881234567', 'Dung', 34, 'Male', N'56 Nguyễn Đình Chiểu, Phường Đa Kao, Quận 1, TP.HCM', 'B', 'Rh+', 1, 4, NULL),
    ('vukhanhnhu@gmail.com','Ab1234@', '0791234567', 'Nhu', 28, 'Female', N'78 Trần Hưng Đạo, Phường 2, Quận 5, TP.HCM', 'AB', 'Rh-', 1, 4, NULL),

	-- Khoa Nhi
	('lan.khoa.nhi@gmail.com', 'Ab1234@', '0911111001', 'Lan', 40, 'Female', N'12 Pasteur, Quận 1, TP.HCM', 'B', 'Rh+', 1, 2, 5),
    ('hoang.khoa.nhi@gmail.com', 'Ab1234@', '0911111002', 'Hoàng', 35, 'Male', N'23 Nguyễn Đình Chiểu, Quận 3, TP.HCM', 'O', 'Rh+', 1, 2, 5),

    -- Khoa Cấp Cứu
    ('minh.capcuu@gmail.com', 'Ab1234@', '0922222001', 'Minh', 46, 'Male', N'90 Hai Bà Trưng, Quận 3, TP.HCM', 'AB', 'Rh-', 1, 2, 3),
    ('thu.capcuu@gmail.com', 'Ab1234@', '0922222002', 'Thu', 38, 'Female', N'77 Nguyễn Thái Học, Quận 1, TP.HCM', 'A', 'Rh+', 1, 2, 3),

    -- Khoa Giải phẫu
    ('hoa.giaiphau@gmail.com', 'Ab1234@', '0933333001', 'Hoa', 39, 'Female', N'76 Lý Tự Trọng, Quận 1, TP.HCM', 'A', 'Rh+', 1, 2, 7),
    ('vuong.giaiphau@gmail.com', 'Ab1234@', '0933333002', 'Vương', 44, 'Male', N'21 Trần Hưng Đạo, Quận 5, TP.HCM', 'B', 'Rh-', 1, 2, 7),

    -- Khoa Tim mạch
    ('tung.timmach@gmail.com', 'Ab1234@', '0944444001', 'Tùng', 50, 'Male', N'55 Võ Thị Sáu, Quận 1, TP.HCM', 'O', 'Rh-', 1, 2, 2),
    ('hien.timmach@gmail.com', 'Ab1234@', '0944444002', 'Hiền', 36, 'Female', N'34 Nguyễn Văn Trỗi, Phú Nhuận, TP.HCM', 'AB', 'Rh+', 1, 2, 2),

    -- Khoa Ngoại
    ('dung.ngoai@gmail.com', 'Ab1234@', '0955555001', 'Dung', 33, 'Female', N'123 Nguyễn Thị Minh Khai, Quận 3, TP.HCM', 'B', 'Rh+', 1, 2, 8),
    ('khoa.ngoai@gmail.com', 'Ab1234@', '0955555002', 'Khoa', 47, 'Male', N'9 Phạm Văn Đồng, TP Thủ Đức, TP.HCM', 'A', 'Rh-', 1, 2, 8),
    -- Khoa Huyết học
    ('phong.huyethoc@gmail.com', 'Ab1234@', '0966666001', 'Phong', 41, 'Male', N'88 Trường Chinh, Tân Bình, TP.HCM', 'AB', 'Rh+', 1, 2, 1),
    ('trang.huyethoc@gmail.com', 'Ab1234@', '0966666002', 'Trang', 37, 'Female', N'19 Hoàng Sa, Quận 1, TP.HCM', 'O', 'Rh-', 1, 2, 1);


GO

-- Insert data into HospitalInfo table
INSERT INTO HospitalInfo (ID, Name, Address, Phone, Email, WorkingHours, MapImageUrl, Latitude, Longitude)
VALUES
    (1, N'Trung Tâm Hiến Máu', N'đường CMT8, Q.3, TP.HCM, Vietnam', '02838554137', 'trungtamhienmau@gmail.com', 
     N'Thứ 2 - Thứ 6: 7:00 - 17:00', 
     'https://www.google.com/maps/place/10%C2%B046''30.5%22N+106%C2%B041''10.4%22E/@10.7751389,106.6862222,17z/data=!3m1!4b1!4m4!3m3!8m2!3d10.7751389!4d106.6862222?entry=ttu&g_ep=EgoyMDI1MDUyOC4wIKXMDSoASAFQAw%3D%3D', 
     '10.7751237', '106.6862143');
GO

-- Thêm user System
SET IDENTITY_INSERT Users ON;
INSERT INTO Users (UserID, Name, Status, RoleID)
VALUES (-1, 'System', 1, 5);
SET IDENTITY_INSERT Users OFF;
GO

-- Insert data into Tags table
INSERT INTO Tags (TagName)
VALUES
    -- Nhóm máu
    (N'A+'),	 -- ID = 1
    (N'A-'),	 -- ID = 2
    (N'B+'),	 -- ID = 3
    (N'B-'),	 -- ID = 4
    (N'AB+'),	 -- ID = 5
    (N'AB-'),	 -- ID = 6
    (N'O+'),	 -- ID = 7
    (N'O-'),	 -- ID = 8
    -- Tag bài viết
    (N'Tổng quan nhóm máu'),	 -- ID = 9
    (N'Truyền máu'),			 -- ID = 10
    (N'Hiến Máu Lần Đầu'),		 -- ID = 11
    (N'Chuẩn Bị Trước Hiến Máu'),-- ID = 12
    (N'Quy Trình Hiến Máu'),	 -- ID = 13
    (N'Lợi Ích Hiến Máu'),		 -- ID = 14
    (N'Hiến Máu Định Kỳ'),		 -- ID = 15
    (N'Sức Khoẻ'),				 -- ID = 16
    (N'Nhóm Máu'),				 -- ID = 17
    (N'Kiến Thức Y Khoa'),		 -- ID = 18
    (N'Quy Trình Xử Lý Máu'),	 -- ID = 19
    (N'Cứu Người'),				 -- ID = 20
    (N'Hiến Máu Toàn Phần'),	 -- ID = 21
    (N'Hiến Tiểu Cầu'),			 -- ID = 22
    (N'Kỹ Thuật Hiến Máu'),		 -- ID = 23
    (N'Câu Chuyện Hiến Máu'),	 -- ID = 24
    (N'Truyền Cảm Hứng'),		 -- ID = 25
    (N'Phân Loại Máu'),			 -- ID = 26
    (N'Hiến Máu'),				 -- ID = 27
    (N'Kêu Gọi Hiến Máu'),		 -- ID = 28
    (N'Sự Kiện Hiến Máu'),		 -- ID = 29
    (N'Phòng Bệnh'),			 -- ID = 30
    (N'Tin tức');				 -- ID = 31
GO


-- Insert data into BloodArticles table
INSERT INTO Contents (Title, Content, ImgUrl, UserID, ContentType, CreatedAt)
VALUES
    -- Nhóm A Rh+
    (N'Giới thiệu nhóm máu A Rh+', 
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
     'article1.jpg', 2, 'Article',01/05/2025),

    -- Nhóm A Rh-
    (N'Giới thiệu nhóm máu A Rh-', 
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
     'article2.jpg', 2, 'Article',01/05/2025),

    -- Nhóm B Rh+
    (N'Giới thiệu nhóm máu B Rh+', 
     N'Nhóm máu B Rh+ có những đặc điểm:

- Có kháng nguyên B trên hồng cầu.
- Có kháng thể anti-A trong huyết tương.
- Mang yếu tố Rh (Rh+).

Người có nhóm máu B Rh+:
- Có thể nhận máu từ: B Rh+, B Rh-, O Rh+, O Rh-
- Có thể cho máu cho: B Rh+, AB Rh+

Lưu ý:
- Nhóm máu B Rh+ là phổ biến và có khả năng nhận từ nhiều nhóm khác nếu tương thích Rh.', 
     'article3.jpg', 2, 'Article',01/05/2025),

    -- Nhóm B Rh-
    (N'Giới thiệu nhóm máu B Rh-', 
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
     'article4.jpg', 2, 'Article',01/05/2025),

    -- Nhóm AB Rh+
    (N'Giới thiệu nhóm máu AB Rh+', 
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
     'article5.jpg', 2, 'Article',01/05/2025),

    -- Nhóm AB Rh-
    (N'Giới thiệu nhóm máu AB Rh-', 
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
     'article6.jpg', 2, 'Article',01/05/2025),

    -- Nhóm O Rh+
    (N'Giới thiệu nhóm máu O Rh+', 
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
     'article7.jpg', 2, 'Article',01/05/2025),

    -- Nhóm O Rh-
    (N'Giới thiệu nhóm máu O Rh-', 
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
     'article8.jpg', 2, 'Article',01/05/2025),
    
    -- Hiến máu lần đầu
 (N' Hiến Máu Lần Đầu: Hành Trình Nhân Ái Bắt Đầu Từ Một Giọt Máu', 
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
Hiến máu không chỉ là hành động cứu người mà còn mang lại nhiều lợi ích cho chính bạn. Nếu bạn đang cân nhắc hiến máu lần đầu, hãy chuẩn bị kỹ lưỡng và đừng ngần ngại tham gia. Một giọt máu cho đi, một cuộc đời ở lại.', 
'article9.jpg', 6, 'Article',01/05/2025),

    -- Lưu ý hiến máu định kỳ
    (N' Người Hiến Máu Thường Xuyên Cần Lưu Ý Điều Gì?', 
     N'1. Khoảng Cách Giữa Các Lần Hiến Máu
Để đảm bảo sức khỏe, người hiến máu cần tuân thủ khoảng cách tối thiểu giữa các lần hiến:

Hiến máu toàn phần: ít nhất 12 tuần (3 tháng) giữa hai lần hiến.

Hiến tiểu cầu hoặc huyết tương: có thể thực hiện sau mỗi 2 tuần, tùy theo chỉ định của cơ sở y tế.

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

Những lưu ý này giúp cơ thể bạn phục hồi nhanh chóng và chuẩn bị tốt cho lần hiến máu tiếp theo.', 
'article10.jpg', 6, 'Article',01/05/2025),

    -- Lợi ích hiến máu hiến máu định kỳ
    (N' Những Lợi Ích Sức Khỏe Khi Hiến Máu Định Kỳ', 
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

Hiến máu định kỳ không chỉ cứu người mà còn là liệu pháp giúp bạn sống khỏe mạnh, hạnh phúc và có ích hơn cho cộng đồng. Hãy biến việc hiến máu trở thành thói quen đẹp trong cuộc sống của bạn.', 
'article11.jpg', 6, 'Article',01/05/2025),

    -- Hiểu về nhóm máu và vai trò
    (N' Hiểu Đúng Về Các Nhóm Máu Và Vai Trò Trong Hiến Máu', 
     N'1. Nhóm Máu Là Gì?
Máu của mỗi người được phân loại dựa trên sự hiện diện hay vắng mặt của các kháng nguyên và kháng thể. Hai hệ thống phân loại nhóm máu phổ biến nhất hiện nay là:

Hệ ABO: Gồm 4 nhóm chính – A, B, AB và O.

Hệ Rh: Dựa trên sự hiện diện (+) hoặc không có (-) của yếu tố Rh (Rhesus) trên bề mặt hồng cầu.

Khi kết hợp lại, ta có 8 nhóm máu phổ biến: A+, A−, B+, B−, AB+, AB−, O+, O−.

2. Các Nhóm Máu Phổ Biến Như Thế Nào?
Tỷ lệ các nhóm máu không đồng đều ở từng khu vực và quốc gia. Tại Việt Nam, thống kê y học cho thấy:

O+: Chiếm khoảng 42% dân số – nhóm máu phổ biến nhất.

A+: Khoảng 25% – cũng rất phổ biến.

B+: Chiếm khoảng 23%.

AB+: Khoảng 7%.

Nhóm Rh− (âm tính): Rất hiếm, chỉ khoảng 0.04% dân số.

Sự phân bố này ảnh hưởng rất lớn đến nhu cầu và khả năng tiếp nhận máu khi truyền.

3. Vì Sao Nhóm Máu Quan Trọng Trong Hiến Máu?
Trong truyền máu, việc phù hợp nhóm máu giữa người cho và người nhận là vô cùng quan trọng. Nếu truyền sai nhóm máu, có thể xảy ra phản ứng miễn dịch nghiêm trọng, thậm chí gây tử vong.

Một số quy tắc cơ bản:

Nhóm O− là nhóm máu "phổ quát", có thể cho tất cả các nhóm máu khác trong trường hợp khẩn cấp.

Nhóm O+ có thể cho O+, A+, B+ và AB+.

Nhóm A− có thể cho A− và A+, AB− và AB+.

Nhóm A+ có thể cho A+ và AB+.

Nhóm B− có thể cho B− và B+, AB− và AB+.

Nhóm B+ có thể cho B+ và AB+.

Nhóm AB− có thể cho AB− và AB+.

Nhóm AB+ chỉ có thể cho AB+, nhưng lại có thể nhận máu từ tất cả các nhóm – gọi là "người nhận phổ quát".

Tuy nhiên, các nguyên tắc trên chỉ áp dụng cho truyền máu toàn phần hoặc hồng cầu, còn với tiểu cầu hoặc huyết tương, nguyên tắc có thể khác biệt.

Nhóm máu đặc biệt:
O− (người cho máu toàn phần phổ quát): Có thể cho bất kỳ nhóm máu nào.

AB+ (người nhận máu toàn phần phổ quát): Có thể nhận máu từ bất kỳ nhóm nào.

Tuy nhiên, điều này chỉ đúng với máu toàn phần hoặc khối hồng cầu. Với tiểu cầu, huyết tương hay các thành phần khác, quy tắc truyền có thể khác.

4. Ai Là Người Cần Máu?
Máu được sử dụng trong rất nhiều trường hợp y tế:

Người gặp tai nạn, mất máu cấp.

Bệnh nhân phẫu thuật (đặc biệt là các ca mổ lớn).

Người bệnh tan máu bẩm sinh, ung thư máu, suy tủy.

Phụ nữ mang thai bị băng huyết hoặc biến chứng thai sản.

Trẻ sinh non hoặc sơ sinh thiếu máu.

Do đó, nhu cầu về máu luôn cao và diễn ra liên tục. Việc hiểu rõ nhóm máu giúp người dân chủ động hơn trong việc hiến máu phù hợp.

5. Vai Trò Của Các Nhóm Máu Trong Hiến Máu
Mỗi nhóm máu có vai trò riêng biệt trong công tác hiến máu cứu người:

Nhóm O−: Rất quý hiếm. Dù chỉ chiếm tỷ lệ nhỏ, nhưng cực kỳ quan trọng trong cấp cứu khẩn cấp vì có thể truyền cho tất cả các nhóm máu.

Nhóm O+ và A+: Do số lượng lớn người sở hữu nhóm máu này nên lượng máu dự trữ cần duy trì đều đặn. Các bệnh viện thường xuyên cần máu từ nhóm này.

Nhóm AB: Dù ít người mang nhóm máu này, nhưng huyết tương AB lại là loại phổ quát – có thể truyền cho mọi nhóm máu. Do đó, người nhóm AB được khuyến khích hiến huyết tương định kỳ.

Rh−: Do tỉ lệ cực thấp, nên người nhóm máu Rh− được xem là "kho máu hiếm". Họ nên tham gia ngân hàng máu hiếm để hỗ trợ khi có ca bệnh cần khẩn cấp.

6. Bạn Đã Biết Nhóm Máu Của Mình Chưa?
Rất nhiều người chưa biết nhóm máu của mình – điều này tiềm ẩn rủi ro trong các tình huống cấp cứu. Khi đi hiến máu, bạn sẽ được xét nghiệm miễn phí nhóm máu và biết được thông tin quan trọng này. Ngoài ra, bạn có thể yêu cầu cấp thẻ người hiến máu để lưu trữ và sử dụng trong các trường hợp khẩn cấp.

7. Kết Luận
Việc hiểu đúng về các nhóm máu không chỉ giúp bạn bảo vệ sức khỏe cá nhân mà còn giúp bạn chủ động hơn trong việc tham gia hiến máu phù hợp, đúng thời điểm, đúng nhu cầu. Dù bạn thuộc nhóm máu nào, mỗi giọt máu bạn cho đi đều mang lại cơ hội sống cho người khác.

Hiến máu không chỉ là một hành động tốt – đó còn là sự kết nối kỳ diệu giữa những trái tim.', 
'article12.jpg', 2, 'Article',01/05/2025),

    -- Lưu và sử dụng máu hiến
    (N' Máu Hiến Sẽ Đi Đâu Và Được Sử Dụng Như Thế Nào?', 
     N'Hành Trình Của Máu
Bạn có bao giờ tự hỏi: Sau khi hiến máu, đơn vị máu ấy sẽ được xử lý và sử dụng ra sao? Hành trình của máu không dừng lại tại điểm hiến – mà nó bắt đầu một chuỗi quy trình y tế nghiêm ngặt để đảm bảo an toàn và hiệu quả trong cứu chữa.

1. Tiếp Nhận Và Bảo Quản
Ngay sau khi bạn hiến máu, túi máu được:

Gắn mã số định danh duy nhất.

Đặt vào hộp chuyên dụng bảo quản lạnh (khoảng 2–6°C).

Vận chuyển về trung tâm huyết học hoặc ngân hàng máu trong thời gian ngắn nhất.

Tại đây, máu sẽ được lưu trữ tạm thời trong điều kiện tiêu chuẩn trước khi phân tách và xét nghiệm.

2. Xét Nghiệm Và Kiểm Tra
Mỗi đơn vị máu đều phải trải qua các bước xét nghiệm nghiêm ngặt:

Kiểm tra nhóm máu: ABO và Rh.

Sàng lọc bệnh truyền nhiễm: HIV, viêm gan B và C, giang mai, sốt rét, v.v.

Kiểm tra kháng thể bất thường để đảm bảo tương thích khi truyền.

Chỉ những đơn vị máu đạt tiêu chuẩn an toàn tuyệt đối mới được đưa vào sử dụng.

3. Phân Tách Các Thành Phần Máu
Một đơn vị máu toàn phần có thể được tách ra thành nhiều thành phần khác nhau, phục vụ cho các mục đích điều trị cụ thể:

Hồng cầu: Truyền cho bệnh nhân thiếu máu, mất máu cấp.

Tiểu cầu: Dành cho người bị xuất huyết giảm tiểu cầu, bệnh ung thư.

Huyết tương tươi đông lạnh: Dùng trong cấp cứu, bệnh rối loạn đông máu.

Nhờ quá trình này, một đơn vị máu có thể cứu được từ 2 đến 3 bệnh nhân, giúp tối ưu hóa giá trị của máu hiến.

4. Cung Ứng Cho Bệnh Viện Và Trung Tâm Y Tế
Sau khi phân tích và xử lý, máu sẽ được phân phối đến:

Bệnh viện đa khoa, trung tâm cấp cứu.

Trung tâm điều trị ung thư, sản khoa.

Cơ sở phẫu thuật, hồi sức tích cực, v.v.

Tại đây, máu được truyền trực tiếp cho bệnh nhân theo chỉ định của bác sĩ, góp phần quan trọng trong việc cứu sống hàng triệu người mỗi năm.

5. Những Trường Hợp Cần Máu Cấp Bách
Nhu cầu máu luôn ở mức cao, nhất là trong:

Tai nạn giao thông, chấn thương nặng.

Phẫu thuật lớn (tim mạch, ghép tạng).

Bệnh lý huyết học như tan máu bẩm sinh, suy tủy.

Sản phụ mất máu sau sinh.

Bệnh nhân ung thư cần truyền tiểu cầu.

Việc hiến máu đều đặn và liên tục chính là nguồn lực quý giá giúp ngành y tế ứng phó với những tình huống khẩn cấp này.

Kết Luận
Máu bạn hiến ra không hề lãng phí – nó trải qua một quá trình kiểm định chặt chẽ và được sử dụng để mang lại sự sống cho những người đang chiến đấu với bệnh tật hoặc tai nạn. Mỗi giọt máu là một tia hy vọng. Hãy tiếp tục hành trình nhân đạo này, vì bạn có thể đang giữ trong mình “chìa khóa sống” của ai đó.', 
'article13.jpg', 6, 'Article',01/05/2025),

    -- Hiến toàn phần và hiến tiểu cầu
    (N'Sự Khác Biệt Giữa Hiến Máu Toàn Phần Và Hiến Tiểu Cầu', 
     N'Không Chỉ Là “Hiến Máu” – Hãy Hiểu Đúng Hơn
Khi nghe đến “hiến máu”, nhiều người chỉ nghĩ đơn giản là lấy máu từ cơ thể người hiến để truyền cho người cần. Tuy nhiên, trong y học hiện đại, máu có thể được phân loại và hiến tách biệt theo nhu cầu điều trị. Hai hình thức phổ biến nhất là hiến máu toàn phần và hiến tiểu cầu – mỗi loại đều có quy trình, mục đích và lợi ích riêng biệt.

Hãy cùng khám phá sự khác biệt để lựa chọn hình thức hiến máu phù hợp nhất với bạn.

1. Hiến Máu Toàn Phần – Đơn Giản Và Phổ Biến
Hiến máu toàn phần là hình thức hiến máu truyền thống, trong đó khoảng 350–450ml máu được lấy ra từ tĩnh mạch người hiến, bao gồm tất cả các thành phần máu như: hồng cầu, bạch cầu, tiểu cầu và huyết tương.

 Thời gian thực hiện: Khoảng 10–15 phút.
 Thời gian phục hồi: Từ 1–2 tháng, do cơ thể cần tái tạo đủ hồng cầu.
 Tần suất hiến máu:

Nam giới: Tối đa 4 lần/năm

Nữ giới: Tối đa 3 lần/năm

 Ứng dụng lâm sàng: Máu toàn phần có thể được truyền trực tiếp hoặc phân tách thành nhiều thành phần để phục vụ nhiều bệnh nhân khác nhau.

 Ưu điểm: Nhanh chóng, dễ thực hiện, phù hợp với hầu hết người lần đầu hiến máu.

2. Hiến Tiểu Cầu – Chính Xác Và Chuyên Biệt
Hiến tiểu cầu là phương pháp trong đó máy tách tiểu cầu tự động sẽ thu lấy tiểu cầu từ máu người hiến, sau đó trả lại hồng cầu và huyết tương về cơ thể.

 Thời gian thực hiện: 60–90 phút do cần quá trình lọc máu liên tục.
 Thời gian phục hồi: Nhanh hơn, thường chỉ sau vài ngày.
 Tần suất hiến máu: Có thể 2 tuần/lần, tối đa 24 lần/năm.

 Ứng dụng lâm sàng: Tiểu cầu rất cần thiết cho bệnh nhân ung thư, người điều trị hóa chất, xuất huyết nặng hoặc rối loạn đông máu.

 Ưu điểm: Cung cấp tiểu cầu chất lượng cao, hỗ trợ điều trị cho các ca bệnh đặc biệt cần truyền gấp.

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

 Hãy chọn cách hiến máu phù hợp với bạn, và cùng nhau lan tỏa sự sống đến mọi người!',
 'article14.jpg', 2, 'Article',01/05/2025),

    -- Câu chuyện thật
    (N'Câu Chuyện Thật: Một Đơn Vị Máu, Một Cuộc Đời Được Cứu', 
     N'Đằng Sau Một Túi Máu – Là Một Cuộc Đời
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

 Một người hiến máu, có thể cứu tới 3 người bệnh khi máu được tách thành các thành phần khác nhau.

Kết Luận
Câu chuyện của bé Linh chỉ là một trong hàng ngàn ca được cứu sống nhờ sự sẻ chia của cộng đồng hiến máu. Hôm nay, bạn có thể là người hiến; ngày mai, người thân bạn có thể là người nhận.

Hiến máu không mất đi – chỉ là đang trao đi sự sống.
Hãy tham gia – để mỗi giọt máu bạn cho đi là một câu chuyện hồi sinh diệu kỳ.', 
'article15.jpg', 6, 'Article',01/05/2025),

   -- Sự kiến hiến máu
(N' CHIẾN DỊCH HIẾN MÁU CỘNG ĐỒNG 2025 - KẾT NỐI YÊU THƯƠNG ',
N' Một giọt máu cho đi – Một cuộc đời ở lại 
 Chiến Dịch Hiến Máu Cộng Đồng 2025, hợp tác với các doanh nghiệp tại TP.HCM, là sự kiện ý nghĩa nhằm khắc phục tình trạng thiếu máu tại Trung Tâm Hiến Máu. Hiến máu không chỉ cứu sống bệnh nhân mà còn mang lại lợi ích sức khỏe như cải thiện tuần hoàn máu và giảm nguy cơ bệnh tim. Sự kiện này lan tỏa tinh thần tương thân tương ái, xây dựng cộng đồng đoàn kết, nhân văn.
 Đây là bản hòa ca của lòng nhân ái, nơi những giọt máu hồng kết nối những trái tim, thắp sáng hy vọng cho những người cần máu khẩn cấp. Sự tham gia của bạn là món quà vô giá, mang lại cơ hội sống và niềm tin.
 Hãy cùng Trung Tâm Hiến Máu và các doanh nghiệp lan tỏa yêu thương! Nhanh tay đăng ký để chung tay cứu sống nhé.
 Kết Nối Doanh Nghiệp, Sẻ Chia Sự Sống
_______________________________
THÔNG TIN CHI TIẾT SỰ KIỆN:
 Thời gian: 15/07/2025, 8:00 - 16:00
 Địa điểm: Trung Tâm Hiến Máu, đường CMT8, Q.3, TP.HCM
 Đối tượng tham gia: Nhân viên doanh nghiệp, cộng đồng địa phương, và tất cả những ai muốn sẻ chia', 
 'event1.jpg', 2, 'News', 01/07/2025),

    -- Ngày hiến máu thế giới
(N' NGÀY HIẾN MÁU THẾ GIỚI 2025 - LAN TỎA TÌNH NHÂN ÁI ',
N' Một giọt máu cho đi – Một cuộc đời ở lại 
 Ngày Hiến Máu Thế Giới 14/06/2025 là dịp để toàn cầu tôn vinh những người hiến máu và nâng cao nhận thức về tầm quan trọng của hiến máu nhân đạo. Với chủ đề **“Cảm ơn bạn, người hiến máu!”**, sự kiện nhấn mạnh vai trò của mỗi giọt máu trong việc cứu sống hàng triệu người. Tại Trung Tâm Hiến Máu, chúng tôi kêu gọi cộng đồng tham gia, đặc biệt những người có nhóm máu hiếm như O- và AB-, để cùng giải quyết tình trạng thiếu máu và mang lại lợi ích sức khỏe như cải thiện tuần hoàn máu và tinh thần tích cực.
 Đây không chỉ là một sự kiện, mà là bản hòa ca của lòng nhân ái, nơi những trái tim toàn cầu chung nhịp đập vì sự sống. Mỗi giọt máu bạn trao đi là sợi dây gắn kết yêu thương, lan tỏa niềm tin và hy vọng đến mọi người.
 Sự chung tay của bạn là món quà ý nghĩa cho những bệnh nhân đang cần máu. Hãy nhanh tay đăng ký để cùng Trung Tâm Hiến Máu lan tỏa tình nhân ái nhé!
 Cảm Ơn Bạn – Người Hiến Máu Toàn Cầu
_______________________________
THÔNG TIN CHI TIẾT SỰ KIỆN:
 Thời gian: 14/06/2025, 7:00 - 17:00
 Địa điểm: Trung Tâm Hiến Máu, đường CMT8, Q.3, TP.HCM
 Đối tượng tham gia: Tất cả những ai đủ điều kiện sức khỏe, đặc biệt người có nhóm máu hiếm
 Hoạt động: Kiểm tra nhóm máu miễn phí, chia sẻ câu chuyện hiến máu, vinh danh các nhà hiến máu tiêu biểu', 
 'event2.jpg', 6, 'News', 01/06/2025),

    -- Sinh viên hiến máu
(N'HIẾN MÁU NHÂN ĐẠO 2025 - TIẾP NGUỒN SINH KHÍ',
N'Một giọt máu cho đi – Một cuộc đời ở lại
 Hiến Máu Nhân Đạo 2025 - Tiếp Nguồn Sinh Khí là dự án hiến máu được thực hiện hằng năm nhằm góp phần khắc phục tình trạng thiếu máu tại các ngân hàng máu trên địa bàn TP.HCM, đồng thời nâng cao nhận thức về hoạt động Hiến Máu Nhân Đạo – một nghĩa cử cao đẹp không chỉ giúp các bệnh nhân và các hoạt động y tế mà còn mang lại lợi ích sức khỏe cho chính người hiến. Qua đó, dự án mong muốn lan tỏa tinh thần tương thân tương ái, xây dựng lối sống tích cực, nhân văn trong cộng đồng sinh viên FPT nói riêng và xã hội nói chung.
 Đây không chỉ là một sự kiện, mà còn là bản hòa ca của lòng nhân ái, nơi những trái tim cùng chung nhịp đập vì sự sẻ chia và tình yêu thương cuộc sống. Mỗi giọt máu được trao đi không chỉ mang lại cơ hội sống, mà còn là sợi dây gắn kết yêu thương, lan tỏa niềm tin và hy vọng đến mọi người xung quanh.
 Sự chung tay của bạn sẽ là một niềm hy vọng, là món quà ý nghĩa dành tặng những người đang cần được tiếp sức. Và để thực hiện hóa được niềm hy vọng đó, hãy nhanh tay đăng ký tham gia để cùng SiTiGroup lan tỏa yêu thương nhé.
_______________________________
THÔNG TIN CHI TIẾT SỰ KIỆN:
 Thời gian : 28/04/2025.
 Địa điểm: Đại học FPT HCM.
 Đối tượng tham gia: dành cho toàn thể cán bộ, giảng viên, nhân viên và sinh viên Đại học FPT HCM.', 
 'event3.jpg', 6, 'News', 16/04/2025),

    -- Chương trình máu hiếm
(N' CHƯƠNG TRÌNH NGƯỜI HIẾN MÁU HIẾM 2025 - ÁNH SÁNG HY VỌNG ',
N' Một giọt máu cho đi – Một cuộc đời ở lại 
 Chương Trình Đăng Ký Người Hiến Máu Hiếm 2025, khởi động từ 01/07/2025, là sáng kiến của Trung Tâm Hiến Máu nhằm xây dựng ngân hàng máu hiếm (O-, AB-). Máu hiếm là nguồn lực quý giá, cứu sống những bệnh nhân khó tìm máu tương thích. Tham gia chương trình mang lại lợi ích sức khỏe như kiểm tra định kỳ và nâng cao nhận thức về nhóm máu.
 Đây là hành trình của những trái tim dũng cảm, nơi mỗi giọt máu hiếm là ngọn đèn soi sáng cho những ca bệnh hiểm nghèo. Sự tham gia của bạn là món quà vô giá, kết nối yêu thương.
 Nếu bạn có nhóm máu O- hoặc AB-, hãy đăng ký ngay để cùng Trung Tâm Hiến Máu cứu người! Liên hệ qua hệ thống hoặc số 02838554137.
 Máu Hiếm – Món Quà Vô Giá
_______________________________
THÔNG TIN CHI TIẾT CHƯƠNG TRÌNH:
 Thời gian khởi động: 01/07/2025
 Địa điểm đăng ký: Trung Tâm Hiến Máu, đường CMT8, Q.3, TP.HCM
 Đối tượng tham gia: Người có nhóm máu hiếm (O-, AB-) hoặc chưa biết nhóm máu',
 'event4.jpg', 2, 'News', 15/06/2025),

    -- Vinh danh người hiến máu
(N' LỄ VINH DANH NGƯỜI HIẾN MÁU XUẤT SẮC 2025 - NHỮNG NGỌN LỬA NHÂN ÁI ',
N' Một giọt máu cho đi – Một cuộc đời ở lại 
 Lễ Vinh Danh Người Hiến Máu Xuất Sắc 2025, diễn ra vào 30/08/2025, là dịp để Trung Tâm Hiến Máu tri ân những người đã hiến máu nhiều lần, góp phần cứu sống hàng trăm bệnh nhân. Sự kiện lan tỏa tinh thần hiến máu, khuyến khích cộng đồng tham gia hành động nhân đạo, mang lại lợi ích sức khỏe và niềm vui sẻ chia.
 Đây là bản giao hưởng của lòng biết ơn, nơi những trái tim nhân ái được tôn vinh, truyền cảm hứng cho mọi người. Sự hiện diện của bạn sẽ làm rực rỡ ý nghĩa của sự kiện.
 Hãy đến để cùng Trung Tâm Hiến Máu vinh danh những người hùng thầm lặng! Đăng ký tham dự qua hệ thống hoặc email trungtamhienmau@gmail.vn.
 Tri Ân Những Người Hùng Thầm Lặng
_______________________________
THÔNG TIN CHI TIẾT SỰ KIỆN:
 Thời gian: 30/08/2025, 18:00 - 20:00
 Địa điểm: Trung Tâm Hiến Máu, đường CMT8, Q.3, TP.HCM
 Đối tượng tham gia: Người hiến máu, cộng đồng, và những ai yêu mến hành động nhân đạo',
 'event5.jpg', 6, 'News', 01/08/2025),

    -- Hiến máu tiểu cầu
(N' TẦM QUAN TRỌNG CỦA HIẾN TIỂU CẦU 2025 - HỖ TRỢ BỆNH NHÂN UNG THƯ ',
N' Một giọt máu cho đi – Một cuộc đời ở lại 
 Trung Tâm Hiến Máu kêu gọi cộng đồng tham gia **Chiến Dịch Hiến Tiểu Cầu 2025**, khởi động từ 01/11/2025, để hỗ trợ bệnh nhân ung thư, những người cần truyền tiểu cầu để ngăn ngừa xuất huyết trong quá trình hóa trị. Tiểu cầu, một thành phần quan trọng của máu, giúp đông máu và duy trì sự sống. Hiến tiểu cầu không chỉ cứu sống mà còn mang lại lợi ích sức khỏe như kích thích sản sinh tế bào mới và nâng cao tinh thần sẻ chia. Chiến dịch này nhằm nâng cao nhận thức về nhu cầu tiểu cầu và lan tỏa tinh thần nhân ái.
 Đây không chỉ là một hành động hiến máu, mà là bản hòa ca của lòng nhân ái, nơi mỗi túi tiểu cầu là ngọn lửa hy vọng, thắp sáng cuộc sống cho những bệnh nhân ung thư đang chiến đấu từng ngày. Sự tham gia của bạn là món quà vô giá, mang lại cơ hội sống và niềm tin.
 Hãy đến Trung Tâm Hiến Máu để hiến tiểu cầu và hỗ trợ bệnh nhân ung thư! Nhanh tay đăng ký qua hệ thống hoặc liên hệ số 02838554137 để biết thêm chi tiết.
 Tiểu Cầu – Ngọn Lửa Hy Vọng Cho Ung Thư
_______________________________
THÔNG TIN CHI TIẾT CHƯƠNG TRÌNH:
 Thời gian khởi động: 01/11/2025
 Địa điểm: Trung Tâm Hiến Máu, đường CMT8, Q.3, TP.HCM
 Đối tượng tham gia: Tất cả những ai đủ điều kiện sức khỏe, đặc biệt người có nhóm máu A+, B+, AB+, O+
 Hoạt động: Hiến tiểu cầu, kiểm tra sức khỏe miễn phí, tư vấn y tế về ung thư và hiến máu',
 'event6.jpg', 2, 'News', 15/10/2025);
GO

-- Insert data into ArticleTags table
INSERT INTO ContentTags (ContentID, TagID)
VALUES
    -- Bài 1: A Rh+
    (1, 1), (1, 5), (1, 7), (1, 8), (1, 15), (1, 26),
    -- Bài 2: A Rh−
    (2, 1), (2, 6), (2, 7), (2, 8), (2, 15), (2, 26),
    -- Bài 3: B Rh+
    (3, 2), (3, 5), (3, 7), (3, 8), (3, 15), (3, 26),
    -- Bài 4: B Rh−
    (4, 2), (4, 6), (4, 7), (4, 8), (4, 15), (4, 26),
    -- Bài 5: AB Rh+
    (5, 3), (5, 5), (5, 7), (5, 8), (5, 15), (5, 26),
    -- Bài 6: AB Rh−
    (6, 3), (6, 6), (6, 7), (6, 8), (6, 15), (6,26),
    -- Bài 7: O Rh+
    (7, 4), (7, 5), (7, 7), (7, 8), (7, 15), (7, 26),
    -- Bài 8: O Rh−
    (8, 4), (8, 6), (8, 7), (8, 8), (8, 15), (8, 26),
    -- Bài 9: Hiến máu lần đầu
    (9, 12), (9, 14), (9, 9), (9, 10), (9, 11), (9, 26),
    -- Bài 10: Lưu ý hiến máu định kỳ
    (10, 13), (10, 14), (10, 10), (10, 12), (10, 26),
    -- Bài 11: Lợi ích hiến máu hiến máu định kỳ
    (11, 12), (11, 13), (11, 14), (11, 30), (11, 26), (11, 29),
    -- Bài 12: Hiểu về nhóm máu và vai trò
    (12, 7), (12, 8), (12, 15), (12, 16), (12, 26),
    -- Bài 13: Lưu và sử dụng máu hiến
    (13, 16), (13, 17), (13, 25), (13, 26),
    -- Bài 14: Hiến toàn phần và hiến tiểu cầu
    (14, 16), (14, 20), (14, 21), (14, 22), (14, 26),
    -- Bài 15: Câu chuyện thật
    (15, 12), (15, 19), (15, 23), (15, 24), (15, 26), (15, 27),
    -- Tin tức 1: Sự kiến hiến máu
    (16, 19), (16, 26), (16, 27), (16, 28), (16, 30),
    -- Tin tức 2: Ngày hiến máu thế giới
    (17, 19), (17, 24), (17, 26), (17, 27), (17, 28), (17, 30),
    -- Tin tức 3: Sinh viên hiến máu
    (18, 19), (18, 24), (18, 26), (18, 27), (18, 28), (18, 30),
    -- Tin tức 4: Chương trình máu hiếm 
    (19, 16), (19, 19), (19, 24), (19, 26), (19, 27), (19, 28), (19, 30),
    -- Tin tức 5: Vinh danh người hiến máu
    (20, 19), (20, 24), (20, 26), (20, 27), (20, 28), (20, 30),
    -- Tin tức 6: Hiến máu tiểu cầu
    (21, 19), (21, 21), (21, 24), (21, 26), (21, 27), (21, 28), (21, 30);
GO

-- Insert data into BloodInventory
INSERT INTO BloodInventories (BloodGroup, RhType, BagType, Quantity, IsRare, Status, ComponentId, LastUpdated)
VALUES
('A', 'Rh+', '450ml', 10, 0, 2, 4, GETDATE()),
('A', 'Rh+', '350ml', 15, 0, 3, 4, GETDATE()),
('A', 'Rh+', '250ml', 8, 0, 1, 4, GETDATE()),
('A', 'Rh+', '450ml', 12, 0, 2, 1, GETDATE()),
('A', 'Rh+', '350ml', 9, 0, 3, 1, GETDATE()),
('A', 'Rh+', '250ml', 11, 0, 0, 1, GETDATE()),
('A', 'Rh+', '450ml', 14, 0, 2, 2, GETDATE()),
('A', 'Rh+', '350ml', 16, 0, 3, 2, GETDATE()),
('A', 'Rh+', '250ml', 7, 0, 1, 2, GETDATE()),
('A', 'Rh+', '450ml', 5, 0, 2, 3, GETDATE()),
('A', 'Rh+', '350ml', 6, 0, 3, 3, GETDATE()),
('A', 'Rh+', '250ml', 4, 0, 1, 3, GETDATE()),
('A', 'Rh-', '450ml', 8, 1, 1, 4, GETDATE()),
('A', 'Rh-', '350ml', 10, 1, 2, 4, GETDATE()),
('A', 'Rh-', '250ml', 7, 1, 0, 4, GETDATE()),
('A', 'Rh-', '450ml', 9, 1, 1, 1, GETDATE()),
('A', 'Rh-', '350ml', 11, 1, 2, 1, GETDATE()),
('A', 'Rh-', '250ml', 8, 1, 0, 1, GETDATE()),
('A', 'Rh-', '450ml', 12, 1, 1, 2, GETDATE()),
('A', 'Rh-', '350ml', 14, 1, 2, 2, GETDATE()),
('A', 'Rh-', '250ml', 10, 1, 0, 2, GETDATE()),
('A', 'Rh-', '450ml', 3, 1, 1, 3, GETDATE()),
('A', 'Rh-', '350ml', 4, 1, 2, 3, GETDATE()),
('A', 'Rh-', '250ml', 2, 1, 0, 3, GETDATE()),
('B', 'Rh+', '450ml', 11, 0, 2, 4, GETDATE()),
('B', 'Rh+', '350ml', 13, 0, 3, 4, GETDATE()),
('B', 'Rh+', '250ml', 9, 0, 1, 4, GETDATE()),
('B', 'Rh+', '450ml', 10, 0, 2, 1, GETDATE()),
('B', 'Rh+', '350ml', 12, 0, 3, 1, GETDATE()),
('B', 'Rh+', '250ml', 8, 0, 1, 1, GETDATE()),
('B', 'Rh+', '450ml', 14, 0, 2, 2, GETDATE()),
('B', 'Rh+', '350ml', 16, 0, 3, 2, GETDATE()),
('B', 'Rh+', '250ml', 12, 0, 1, 2, GETDATE()),
('B', 'Rh+', '450ml', 4, 0, 2, 3, GETDATE()),
('B', 'Rh+', '350ml', 5, 0, 3, 3, GETDATE()),
('B', 'Rh+', '250ml', 3, 0, 1, 3, GETDATE()),
('B', 'Rh-', '450ml', 7, 1, 0, 4, GETDATE()),
('B', 'Rh-', '350ml', 9, 1, 1, 4, GETDATE()),
('B', 'Rh-', '250ml', 6, 1, 2, 4, GETDATE()),
('B', 'Rh-', '450ml', 8, 1, 0, 1, GETDATE()),
('B', 'Rh-', '350ml', 10, 1, 1, 1, GETDATE()),
('B', 'Rh-', '250ml', 7, 1, 2, 1, GETDATE()),
('B', 'Rh-', '450ml', 11, 1, 0, 2, GETDATE()),
('B', 'Rh-', '350ml', 13, 1, 1, 2, GETDATE()),
('B', 'Rh-', '250ml', 9, 1, 2, 2, GETDATE()),
('B', 'Rh-', '450ml', 2, 1, 0, 3, GETDATE()),
('B', 'Rh-', '350ml', 3, 1, 1, 3, GETDATE()),
('B', 'Rh-', '250ml', 2, 1, 2, 3, GETDATE()),
('AB', 'Rh+', '450ml', 12, 0, 3, 4, GETDATE()),
('AB', 'Rh+', '350ml', 14, 0, 2, 4, GETDATE()),
('AB', 'Rh+', '250ml', 10, 0, 1, 4, GETDATE()),
('AB', 'Rh+', '450ml', 11, 0, 3, 1, GETDATE()),
('AB', 'Rh+', '350ml', 13, 0, 2, 1, GETDATE()),
('AB', 'Rh+', '250ml', 9, 0, 1, 1, GETDATE()),
('AB', 'Rh+', '450ml', 15, 0, 3, 2, GETDATE()),
('AB', 'Rh+', '350ml', 17, 0, 2, 2, GETDATE()),
('AB', 'Rh+', '250ml', 13, 0, 1, 2, GETDATE()),
('AB', 'Rh+', '450ml', 5, 0, 3, 3, GETDATE()),
('AB', 'Rh+', '350ml', 6, 0, 2, 3, GETDATE()),
('AB', 'Rh+', '250ml', 4, 0, 1, 3, GETDATE()),
('AB', 'Rh-', '450ml', 6, 1, 0, 4, GETDATE()),
('AB', 'Rh-', '350ml', 8, 1, 1, 4, GETDATE()),
('AB', 'Rh-', '250ml', 5, 1, 2, 4, GETDATE()),
('AB', 'Rh-', '450ml', 7, 1, 0, 1, GETDATE()),
('AB', 'Rh-', '350ml', 9, 1, 1, 1, GETDATE()),
('AB', 'Rh-', '250ml', 6, 1, 2, 1, GETDATE()),
('AB', 'Rh-', '450ml', 10, 1, 0, 2, GETDATE()),
('AB', 'Rh-', '350ml', 12, 1, 1, 2, GETDATE()),
('AB', 'Rh-', '250ml', 8, 1, 2, 2, GETDATE()),
('AB', 'Rh-', '450ml', 2, 1, 0, 3, GETDATE()),
('AB', 'Rh-', '350ml', 3, 1, 1, 3, GETDATE()),
('AB', 'Rh-', '250ml', 2, 1, 2, 3, GETDATE()),
('O', 'Rh+', '450ml', 13, 0, 3, 4, GETDATE()),
('O', 'Rh+', '350ml', 15, 0, 2, 4, GETDATE()),
('O', 'Rh+', '250ml', 11, 0, 1, 4, GETDATE()),
('O', 'Rh+', '450ml', 12, 0, 3, 1, GETDATE()),
('O', 'Rh+', '350ml', 14, 0, 2, 1, GETDATE()),
('O', 'Rh+', '250ml', 10, 0, 1, 1, GETDATE()),
('O', 'Rh+', '450ml', 16, 0, 3, 2, GETDATE()),
('O', 'Rh+', '350ml', 18, 0, 2, 2, GETDATE()),
('O', 'Rh+', '250ml', 14, 0, 1, 2, GETDATE()),
('O', 'Rh+', '450ml', 6, 0, 3, 3, GETDATE()),
('O', 'Rh+', '350ml', 7, 0, 2, 3, GETDATE()),
('O', 'Rh+', '250ml', 5, 0, 1, 3, GETDATE()),
('O', 'Rh-', '450ml', 7, 1, 0, 4, GETDATE()),
('O', 'Rh-', '350ml', 9, 1, 1, 4, GETDATE()),
('O', 'Rh-', '250ml', 6, 1, 2, 4, GETDATE()),
('O', 'Rh-', '450ml', 8, 1, 0, 1, GETDATE()),
('O', 'Rh-', '350ml', 10, 1, 1, 1, GETDATE()),
('O', 'Rh-', '250ml', 7, 1, 2, 1, GETDATE()),
('O', 'Rh-', '450ml', 11, 1, 0, 2, GETDATE()),
('O', 'Rh-', '350ml', 13, 1, 1, 2, GETDATE()),
('O', 'Rh-', '250ml', 9, 1, 2, 2, GETDATE()),
('O', 'Rh-', '450ml', 2, 1, 0, 3, GETDATE()),
('O', 'Rh-', '350ml', 3, 1, 1, 3, GETDATE()),
('O', 'Rh-', '250ml', 2, 1, 2, 3, GETDATE());
GO


-- Insert data into BloodInventoryHistories
INSERT INTO BloodInventoryHistories (InventoryID, ActionType, Quantity, Notes, PerformedBy, PerformedAt, BagType, ReceivedDate, ExpirationDate)
VALUES
(1, N'Check in', 2, N'Nhập kho từ hiến máu', 3, '2025-06-01', '250ml', '2025-06-01', '2025-07-01'),
(2, N'Cancel', 1, N'Rách túi máu', 2, '2025-06-02', '350ml', '2025-06-02', '2025-07-02'),
(3, N'Check out', 1, N'Cấp cứu BV tỉnh', 4, '2025-06-03', '450ml', '2025-06-03', '2025-07-10'),
(4, N'Check in', 3, N'Tài trợ từ tổ chức', 1, '2025-06-04', '250ml', '2025-06-04', '2025-07-20'),
(5, N'Check out', 2, N'Sử dụng cho ghép tạng', 3, '2025-06-05', '450ml', '2025-06-05', '2025-07-25'),
(6, N'Cancel', 1, N'Không đạt tiêu chuẩn', 2, '2025-06-06', '350ml', '2025-06-06', '2025-07-06'),
(7, N'Check in', 4, N'Hiến máu cộng đồng', 4, '2025-06-07', '250ml', '2025-06-07', '2025-08-01'),
(8, N'Check out', 2, N'Chuyển viện', 1, '2025-06-08', '450ml', '2025-06-08', '2025-08-10'),
(1, N'Cancel', 1, N'Máu bị đông', 3, '2025-06-09', '350ml', '2025-06-09', '2025-07-09'),
(2, N'Check in', 3, N'Hiến máu nội viện', 2, '2025-06-10', '250ml', '2025-06-10', '2025-07-25'),
(3, N'Check out', 1, N'Dùng cho phẫu thuật', 5, '2025-06-11', '450ml', '2025-06-11', '2025-07-20'),
(4, N'Cancel', 2, N'Không bảo quản đúng nhiệt độ', 2, '2025-06-12', '350ml', '2025-06-12', '2025-07-12'),
(5, N'Check in', 2, N'Hiến máu tình nguyện', 4, '2025-06-13', '250ml', '2025-06-13', '2025-07-28'),
(6, N'Check out', 4, N'Cấp cứu tuyến huyện', 3, '2025-06-14', '450ml', '2025-06-14', '2025-08-10'),
(7, N'Cancel', 3, N'Túi máu vỡ', 1, '2025-06-15', '350ml', '2025-06-15', '2025-07-15'),
(8, N'Check in', 1, N'Hiến máu định kỳ', 5, '2025-06-16', '250ml', '2025-06-16', '2025-07-31'),
(1, N'Check out', 2, N'Bệnh viện sử dụng cho sản phụ', 4, '2025-06-17', '450ml', '2025-06-17', '2025-08-01'),
(2, N'Cancel', 4, N'Nhiễm khuẩn', 2, '2025-06-18', '350ml', '2025-06-18', '2025-07-20'),
(3, N'Check in', 2, N'Nhập kho nội viện', 3, '2025-06-19', '250ml', '2025-06-19', '2025-07-29'),
(4, N'Check out', 1, N'Ca phẫu thuật tim', 4, '2025-06-20', '450ml', '2025-06-20', '2025-07-25'),
(5, N'Cancel', 2, N'Máu không đạt chất lượng', 1, '2025-06-21', '350ml', '2025-06-21', '2025-07-21'),
(6, N'Check in', 3, N'Hiến máu tại sự kiện trường học', 2, '2025-06-22', '250ml', '2025-06-22', '2025-08-01'),
(7, N'Check out', 1, N'Truyền máu ngoại viện', 5, '2025-06-23', '450ml', '2025-06-23', '2025-07-30'),
(8, N'Cancel', 3, N'Rách rỗ', 3, '2025-06-24', '350ml', '2025-06-24', '2025-07-26'),
(1, N'Check in', 1, N'Hiến máu nhân đạo', 4, '2025-06-25', '250ml', '2025-06-25', '2025-07-31'),
(2, N'Check out', 3, N'Dùng cho trẻ em thiếu máu', 3, '2025-06-26', '450ml', '2025-06-26', '2025-08-02'),
(3, N'Cancel', 2, N'Máu quá hạn', 2, '2025-06-27', '350ml', '2025-06-27', '2025-07-27'),
(4, N'Check in', 4, N'Tiếp nhận từ ngân hàng máu', 1, '2025-06-28', '250ml', '2025-06-28', '2025-08-05'),
(5, N'Check out', 1, N'Cấp cứu ngoại viện', 4, '2025-06-29', '450ml', '2025-06-29', '2025-08-01'),
(6, N'Cancel', 2, N'Máu bị vón cục', 1, '2025-06-30', '350ml', '2025-06-30', '2025-07-30');
GO

INSERT INTO Patients (FullName, Gender, DateOfBirth, Age, Phone, Address, Email)
VALUES
    (N'Nguyễn Văn An', N'Nam', '1990-05-15', 35, '0912345678', N'123 Đường Lê Lợi, Quận 1, TP.HCM', 'nguyenvanan@gmail.com'),
    (N'Trần Thị Bình', N'Nữ', '1985-08-22', 39, '0987654321', N'45 Đường Nguyễn Huệ, Quận 3, TP.HCM', 'tranbinh85@yahoo.com'),
    (N'Phạm Minh Châu', N'Nữ', '2000-03-10', 25, '0908765432', N'78 Đường Trần Hưng Đạo, Quận 5, TP.HCM', 'phamchau2000@gmail.com'),
    (N'Lê Quốc Dũng', N'Nam', '1975-11-30', 49, '0933456789', N'12 Đường Phạm Văn Đồng, Thủ Đức, TP.HCM', 'lequocdung75@gmail.com'),
    (N'Hoàng Thị Mai', N'Nữ', '1995-07-20', 29, '0971234567', N'56 Đường Lý Thường Kiệt, Quận 10, TP.HCM', 'hoangmai95@gmail.com'),
    (N'Võ Văn Hùng', N'Nam', '1988-02-14', 37, '0945678901', N'89 Đường Nguyễn Trãi, Quận 7, TP.HCM', 'vohung88@gmail.com'),
    (N'Đỗ Thị Lan', N'Nữ', '2005-09-05', 19, '0967890123', N'34 Đường Cách Mạng Tháng Tám, Quận 3, TP.HCM', 'dolan2005@gmail.com'),
    (N'Ngô Minh Tuấn', N'Nam', '1992-12-25', 32, '0922345678', N'67 Đường Võ Thị Sáu, Quận 1, TP.HCM', 'ngominhtuan@gmail.com');
GO

INSERT INTO BloodRequests (UserID, PatientID, PatientName, Age, Gender, Relationship, FacilityName, DoctorName, DoctorPhone, BloodGroup, RhType, ComponentID, Quantity, Reason, Status, CreatedTime)
VALUES
    (1, 1, N'Nguyễn Văn An', 35, N'Nam', N'Bản thân', N'Bệnh viện Chợ Rẫy', N'Trần Văn Minh', '0912345678', 'A', 'Rh+', 1, 2, N'Phẫu thuật tim', 2, '2025-06-23 09:00:00'),
    (2, 2, N'Trần Thị Bình', 39, N'Nữ', N'Bạn bè', N'Bệnh viện Bạch Mai', N'Lê Quốc Hùng', '0987654321', 'O', 'Rh+', 3, 1, N'Cấp cứu tai nạn', 1, '2025-06-24 10:30:00'),
    (3, 8, N'Ngô Minh Tuấn', 32, N'Nam', N'Bạn bè', N'Bệnh viện Nhân Dân Gia Định', N'Phạm Văn Hòa', '0922345678', 'O', 'Rh-', 4, 2, N'Hỗ trợ cấp cứu', 2, '2025-06-30 16:00:00'),	
    (7, 3, N'Phạm Minh Châu', 25, N'Nữ', N'Gia đình', N'Bệnh viện Từ Dũ', N'Nguyễn Thị Lan', '0908765432', 'A', 'Rh-', 2, 2, N'Sinh mổ', 0, '2025-06-25 11:15:00'),
    (8, 4, N'Lê Quốc Dũng', 49, N'Nam', N'Bản thân', N'Bệnh viện 115', N'Hoàng Văn Tuấn', '0933456789', 'B', 'Rh+', 4, 3, N'Hóa trị ung thư', 3, '2025-06-26 12:00:00'),
    (9, 5, N'Hoàng Thị Mai', 29, N'Nữ', N'Bạn bè', N'Bệnh viện Nhi Đồng', N'Võ Thị Thanh', '0971234567', 'AB', 'Rh+', NULL, 1, N'Chảy máu cấp', 2, '2025-06-27 13:45:00'),
    (11, 6, N'Võ Văn Hùng', 37, N'Nam', N'Gia đình', N'Bệnh viện Đại học Y Dược', N'Đỗ Minh Khang', '0945678901', 'AB', 'Rh-', 3, 2, N'Ghép tạng', 1, '2025-06-28 14:20:00'),
    (12, 7, N'Đỗ Thị Lan', 19, N'Nữ', N'Bản thân', N'Bệnh viện Chấn thương Chỉnh hình', N'Ngô Văn Phát', '0967890123', 'B', 'Rh-', 2, 1, N'Phẫu thuật xương', 0, '2025-06-29 15:10:00');
GO		
	

-- Insert data into Notifications table
INSERT INTO Notifications (UserID, Title, Message, Type, IsRead, SentAt)
VALUES
    (1, N'Nhắc Hiến Máu', N'Bạn có thể hiến lại sau 2 tuần', 'Reminder', 0, GETDATE()),
    (2, N'Yêu cầu khẩn cấp', N'Chúng tôi cần máu của bạn cho trường hợp khẩn cấp', 'Alert', 0, GETDATE()),
    (3, N'Cảm ơn', N'Cảm ơn bạn đã hiến máu!', 'Report', 1, GETDATE()),
    (4, N'Yêu cầu máu đã chấp nhận', N'Yêu cầu máu của bạn đã được chấp nhận', 'Alert', 1, GETDATE()),
    (5, N'Có 1 bài viết mới', N'Hãy xem bài viết hướng dẫn về máu mới nhất của chúng tôi', 'Report', 0, GETDATE());
GO

INSERT INTO ActivityLogs (UserID, ActivityType, EntityType, EntityId, Description, CreatedAt)
VALUES
-- Logs cho BloodArticles của UserID 2 (Doctor)
(2, 'Create', 'Article', 1, N'Tạo bài viết: Giới thiệu nhóm máu A Rh+', '2025-06-08'),
(2, 'Update', 'Article', 3, N'Cập nhật bài viết: Giới thiệu nhóm máu B Rh+', '2025-06-10'),
(2, 'Delete', 'Article', 6, N'Xoá bài viết: Giới thiệu nhóm máu AB Rh-', '2025-06-15'),

-- Logs cho BloodArticles của UserID 6 (Doctor)
(6, 'Create', 'Article', 9, N'Tạo bài viết: Hiến Máu Lần Đầu', '2025-06-09'),
(6, 'Update', 'Article', 10, N'Cập nhật bài viết: Lưu Ý Hiến Máu Định Kỳ', '2025-06-13'),
(6, 'Delete', 'Article', 11, N'Xoá bài viết: Những Lợi Ích Sức Khỏe Khi Hiến Máu Định Kỳ', '2025-06-17'),

-- Logs cho News của UserID 2
(2, 'Create', 'News', 1, N'Tạo tin tức: CHIẾN DỊCH HIẾN MÁU CỘNG ĐỒNG 2025', '2025-06-10'),
(2, 'Update', 'News', 4, N'Cập nhật tin tức: CHƯƠNG TRÌNH NGƯỜI HIẾN MÁU HIẾM 2025', '2025-06-18'),

-- Logs cho News của UserID 6
(6, 'Create', 'News', 2, N'Tạo tin tức: NGÀY HIẾN MÁU THẾ GIỚI 2025', '2025-06-07'),
(6, 'Update', 'News', 5, N'Cập nhật tin tức: LỄ VINH DANH NGƯỜI HIẾN MÁU XUẤT SẮC 2025', '2025-06-12'),
(6, 'Delete', 'News', 6, N'Xoá tin tức: TẦM QUAN TRỌNG CỦA HIẾN TIỂU CẦU 2025', '2025-06-19');
GO

INSERT INTO Appointments (UserID, AppointmentDate, TimeSlot, Status, Process, Cancel, Notes, CreatedAt)
VALUES
(1, '2025-06-20', N'Sáng (7:00-12:00)', NULL, 0, 0, N'Đặt lịch hiến máu', GETDATE()),
(2, '2025-06-21', N'Chiều (13:00-17:00)', NULL, 0, 0, N'Đặt lịch hiến máu', GETDATE()),
(3, '2025-06-22', N'Sáng (7:00-12:00)', NULL, 0, 0, N'Đặt lịch hiến máu', GETDATE()),

(1, '2025-06-23', N'Chiều (13:00-17:00)', 0, 0, 0,N'Đặt lịch bị từ chối', GETDATE()),
(2, '2025-06-24', N'Sáng (7:00-12:00)', 1, 0, 1, N'Người dùng hủy đặt lịch', GETDATE()), 
(3, '2025-06-25', N'Chiều (13:00-17:00)', 0, 0, 0, N'Đặt lịch bị từ chối', GETDATE()),

(1, '2025-06-26', N'Sáng (7:00-12:00)', 1, 0, 0, N'Đặt lịch bị từ chối', GETDATE()),
(2, '2025-06-27', N'Chiều (13:00-17:00)', 1, 0, 0, N'Đặt lịch bị từ chối', GETDATE()),
(3, '2025-06-28', N'Sáng (7:00-12:00)', 1, 0, 0, N'Đặt lịch bị từ chối', GETDATE()),

(1, '2025-06-29', N'Chiều (13:00-17:00)', 1, 0, 0, N'Đặt lịch bị từ chối', GETDATE()),
(2, '2025-06-30', N'Sáng (7:00-12:00)', 1 0, 0, N'Đặt lịch bị từ chối', GETDATE()),
(3, '2025-07-01', N'Chiều (13:00-17:00)', 1, 0, 1, N'Người dùng hủy đặt lịch', GETDATE()), 

(1, '2025-07-02', N'Sáng (7:00-12:00)', 1, 0, 0, N'Đặt lịch bị từ chối', GETDATE()),
(2, '2025-07-03', N'Chiều (13:00-17:00)', 1, 0, 0, N'Đặt lịch bị từ chối', GETDATE()),
(3, '2025-07-04', N'Sáng (7:00-12:00)', 1, 0, 0, N'Đặt lịch bị từ chối', GETDATE());

-- Insert data into BloodDonationHistory table
INSERT INTO BloodDonationHistories (AppointmentID, DonationDate, BloodGroup, RhType, DoctorID, Notes, CreatedAt, IsSuccess)
VALUES
-- Thành công
(1, '2025-06-20 08:00:00', 'A', 'Rh+', 4, N'Máu đã được chấp nhận', GETDATE(), 1),

-- Thất bại do lý do liên quan đến máu
(2, '2025-06-21 14:00:00', 'B', 'Rh-', 6, N'Phát hiện kháng thể bất thường trong máu', GETDATE(), 0),

-- Thành công
(3, '2025-06-22 08:00:00', 'O', 'Rh+', 23, N'Máu đã được chấp nhận', GETDATE(), 1);

INSERT INTO Reminders (UserId, Type, Message, RemindAt, IsDisabled, CreatedAt, IsSent, SentAt)
VALUES
(1, 'BloodDonation', N'Bạn có lịch hẹn hiến máu hôm nay!', '2025-07-10 08:00:00', 0, GETDATE(), 0, NULL),
(2, 'Recovery', N'Bạn đã đủ 12 tuần kể từ lần hiến máu trước!', '2025-07-09 08:00:00', 0, GETDATE(), 0, NULL),
(3, 'BloodDonation', N'Bạn có lịch hẹn hiến máu hôm nay!', '2025-07-11 07:00:00', 0, GETDATE(), 0, NULL),
(4, 'Recovery', N'Bạn đã đủ điều kiện hiến máu lại!', '2025-07-12 08:00:00', 0, GETDATE(), 0, NULL),
(5, 'BloodDonation', N'Bạn có lịch hiến máu vào ngày mai!', '2025-07-13 08:00:00', 0, GETDATE(), 0, NULL),
(6, 'Recovery', N'Bạn sẽ đủ điều kiện hiến máu lại vào tuần sau.', '2025-07-18 08:00:00', 0, GETDATE(), 0, NULL),
(7, 'BloodDonation', N'Nhắc nhở hiến máu sắp tới!', '2025-07-20 08:00:00', 0, GETDATE(), 0, NULL),
(8, 'Recovery', N'Bạn đã đủ 84 ngày sau hiến máu.', '2025-06-01 08:00:00', 0, GETDATE(), 1, '2025-06-01 08:01:00'),
(9, 'BloodDonation', N'Bạn có lịch hôm nay.', '2025-07-01 08:00:00', 0, GETDATE(), 1, '2025-07-01 08:05:00'),
(10, 'Recovery', N'Bạn đã đủ điều kiện hiến máu lại.', '2025-07-05 08:00:00', 1, GETDATE(), 0, NULL),
(11, 'BloodDonation', N'Nhắc nhở bị tắt.', '2025-07-12 08:00:00', 1, GETDATE(), 0, NULL),
(12, 'BloodDonation', N'Đừng quên lịch hiến máu sáng nay!', '2025-07-12 07:30:00', 0, GETDATE(), 0, NULL),
(13, 'Recovery', N'Hôm nay là ngày bạn đủ điều kiện hiến máu lại!', '2025-07-12 09:00:00', 0, GETDATE(), 0, NULL),
(14, 'BloodDonation', N'Lịch hiến máu vào ngày mai.', '2025-07-13 07:00:00', 0, GETDATE(), 0, NULL),
(15, 'Recovery', N'Bạn đã đủ 12 tuần kể từ lần hiến máu trước.', '2025-07-10 08:30:00', 0, GETDATE(), 0, NULL);
