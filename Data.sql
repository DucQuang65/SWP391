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


-- Insert data into Users table with updated Vietnamese full names
INSERT INTO Users (Email, Password, Phone, Name, Age, Gender, Address, BloodGroup, RhType, Status, RoleID, DepartmentID)
VALUES
	-- Role: Member (RoleID = 1)
    ('vinhntqse180354@fpt.edu.vn', 'Ab1234@', '0901234567', N'Trần Quang Vinh', 30, 'Male', N'123 Nguyễn Huệ, Phường Bến Nghé, Quận 1, TP.HCM', 'A', 'Rh+', 1, 1, NULL),
    ('ducquang0565@gmail.com', 'Ab1234@', '0987654321', N'Bùi Quang Đức', 25, 'Male', N'45 Lê Lợi, Phường 1, Quận 3, TP.HCM', 'O', 'Rh+', 1, 1, NULL),
    ('member3@fpt.edu.vn', 'Ab1234@', '0931234567', N'Nguyễn Minh Tuấn', 19, 'Male', N'67 Trần Phú, Phường 4, Quận 5, TP.HCM', 'A', 'Rh+', 1, 1, NULL),

	-- Role: Doctor (RoleID = 2)
    ('kienlvse180681@fpt.edu.vn', 'Ab1234@', '0912345678', N'Lê Văn Kiên', 45, 'Female', N'89 Phạm Văn Đồng, Phường Linh Đông, TP Thủ Đức, TP.HCM', 'O', 'Rh-', 1, 2, 1),
    ('doctor2@fpt.edu.vn', 'Ab1234@', '0971234567', N'Nguyễn Văn Hiếu', 38, 'Male', N'234 Nguyễn Văn Cừ, Phường Cầu Kho, Quận 1, TP.HCM', 'A', 'Rh-', 1, 2, 2),
    ('doctor3@fpt.edu.vn', 'Ab1234@', '0941234567', N'Đặng Thị Thảo', 42, 'Female', N'56 Nguyễn Trãi, Phường 3, Quận 5, TP.HCM', 'O', 'Rh+', 1, 2, 1),

	-- Role: BloodManager (RoleID = 3)
    ('xpnhi023@gmail.com', 'Ab1234@', '0961234567', N'Phạm Thị Yến Nhi', 35, 'Male', N'78 Cách Mạng Tháng Tám, Phường 6, Quận 3, TP.HCM', 'B', 'Rh+', 1, 3, NULL),
    ('bloodmanager2@gmail.com', 'Ab1234@', '0991234567', N'Trần Thị Kim Hoa', 29, 'Female', N'90 Huỳnh Tấn Phát, Phường Tân Thuận Đông, Quận 7, TP.HCM', 'AB', 'Rh+', 1, 3, NULL),
    ('bloodmanager3@gmail.com', 'Ab1234@', '0701234567', N'Lê Quốc Phong', 36, 'Male', N'12 Lê Văn Sỹ, Phường 13, Quận 3, TP.HCM', 'O', 'Rh+', 1, 3, NULL),

	-- Role: Admin (RoleID = 4)
    ('admin1@gmail.com', 'Ab1234@', '0771234567', N'Nguyễn Thị Hồng Linh', 31, 'Female', N'34 Bùi Thị Xuân, Phường 2, Quận Tân Bình, TP.HCM', 'A', 'Rh+', 1, 4, NULL),
    ('admin2@gmail.com', 'Ab1234@', '0881234567', N'Phạm Văn Dũng', 34, 'Male', N'56 Nguyễn Đình Chiểu, Phường Đa Kao, Quận 1, TP.HCM', 'B', 'Rh+', 1, 4, NULL),
    ('vukhanhnhu@gmail.com', 'Ab1234@', '0791234567', N'Phạm Vũ Khánh Như', 28, 'Female', N'78 Trần Hưng Đạo, Phường 2, Quận 5, TP.HCM', 'AB', 'Rh-', 1, 4, NULL),

	-- Khoa Nhi
    ('lan.khoa.nhi@gmail.com', 'Ab1234@', '0911111001', N'Nguyễn Thị Thanh Lan', 40, 'Female', N'12 Pasteur, Quận 1, TP.HCM', 'B', 'Rh+', 1, 2, 5),
    ('hoang.khoa.nhi@gmail.com', 'Ab1234@', '0911111002', N'Trần Văn Hoàng', 35, 'Male', N'23 Nguyễn Đình Chiểu, Quận 3, TP.HCM', 'O', 'Rh+', 1, 2, 5),

    -- Khoa Cấp Cứu
    ('minh.capcuu@gmail.com', 'Ab1234@', '0922222001', N'Nguyễn Văn Minh', 46, 'Male', N'90 Hai Bà Trưng, Quận 3, TP.HCM', 'AB', 'Rh-', 1, 2, 3),
    ('thu.capcuu@gmail.com', 'Ab1234@', '0922222002', N'Lê Thị Thu', 38, 'Female', N'77 Nguyễn Thái Học, Quận 1, TP.HCM', 'A', 'Rh+', 1, 2, 3),

    -- Khoa Giải phẫu
    ('hoa.giaiphau@gmail.com', 'Ab1234@', '0933333001', N'Phạm Thị Hoa', 39, 'Female', N'76 Lý Tự Trọng, Quận 1, TP.HCM', 'A', 'Rh+', 1, 2, 7),
    ('vuong.giaiphau@gmail.com', 'Ab1234@', '0933333002', N'Nguyễn Hữu Vương', 44, 'Male', N'21 Trần Hưng Đạo, Quận 5, TP.HCM', 'B', 'Rh-', 1, 2, 7),

    -- Khoa Tim mạch
    ('tung.timmach@gmail.com', 'Ab1234@', '0944444001', N'Nguyễn Văn Tùng', 50, 'Male', N'55 Võ Thị Sáu, Quận 1, TP.HCM', 'O', 'Rh-', 1, 2, 2),
    ('hien.timmach@gmail.com', 'Ab1234@', '0944444002', N'Trần Thị Hiền', 36, 'Female', N'34 Nguyễn Văn Trỗi, Phú Nhuận, TP.HCM', 'AB', 'Rh+', 1, 2, 2),

    -- Khoa Ngoại
    ('dung.ngoai@gmail.com', 'Ab1234@', '0955555001', N'Nguyễn Thị Duyên', 33, 'Female', N'123 Nguyễn Thị Minh Khai, Quận 3, TP.HCM', 'B', 'Rh+', 1, 2, 8),
    ('khoa.ngoai@gmail.com', 'Ab1234@', '0955555002', N'Lê Văn Khoa', 47, 'Male', N'9 Phạm Văn Đồng, TP Thủ Đức, TP.HCM', 'A', 'Rh-', 1, 2, 8),

    -- Khoa Huyết học
    ('phong.huyethoc@gmail.com', 'Ab1234@', '0966666001', N'Trần Quốc Phong', 41, 'Male', N'88 Trường Chinh, Tân Bình, TP.HCM', 'AB', 'Rh+', 1, 2, 1),
    ('trang.huyethoc@gmail.com', 'Ab1234@', '0966666002', N'Nguyễn Thị Trang', 37, 'Female', N'19 Hoàng Sa, Quận 1, TP.HCM', 'O', 'Rh-', 1, 2, 1);
GO

-- Insert data into HospitalInfo table
INSERT INTO HospitalInfo (ID, Name, Address, Phone, Email, WorkingHours, MapImageUrl, Latitude, Longitude)
VALUES
    (1, N'Trung Tâm Hiến Máu Bệnh viện Đa khoa Ánh Dương', N'Đường CMT8, Q.3, TP.HCM, Việt Nam', '02838554137', 'trungtamhienmau.anhduong@gmail.com', 
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
    N'Nhóm máu A Rh+ là một trong những nhóm máu phổ biến ở người.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Đặc điểm:' + CHAR(13) + CHAR(10) +
    N'- Có kháng nguyên A trên bề mặt hồng cầu.' + CHAR(13) + CHAR(10) +
    N'- Có kháng thể anti-B trong huyết tương.' + CHAR(13) + CHAR(10) +
    N'- Có yếu tố Rh (D antigen), tức là Rh dương tính (Rh+).' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Người có nhóm máu A Rh+:' + CHAR(13) + CHAR(10) +
    N'- Có thể nhận máu từ: A Rh+, A Rh-, O Rh+, O Rh-' + CHAR(13) + CHAR(10) +
    N'- Có thể cho máu cho: A Rh+, AB Rh+' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Lưu ý:' + CHAR(13) + CHAR(10) +
    N'- Trong truyền máu, yếu tố Rh đóng vai trò quan trọng. Người Rh+ có thể nhận máu Rh- nhưng ngược lại thì không an toàn.',
    'https://vieclam123.vn/ckfinder/userfiles/images/images/nhom-mau-a-la-gi(1).jpg', 
    2, 
    'Article', 
    '2025-05-01'),

    -- Nhóm A Rh-
    (N'Giới thiệu nhóm máu A Rh-', 
    N'Nhóm máu A Rh- là nhóm máu hiếm hơn A Rh+.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Đặc điểm:' + CHAR(13) + CHAR(10) +
    N'- Có kháng nguyên A trên bề mặt hồng cầu.' + CHAR(13) + CHAR(10) +
    N'- Có kháng thể anti-B trong huyết tương.' + CHAR(13) + CHAR(10) +
    N'- Không có yếu tố Rh (Rh-), nên không mang D antigen.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Người có nhóm máu A Rh-:' + CHAR(13) + CHAR(10) +
    N'- Có thể nhận máu từ: A Rh-, O Rh-' + CHAR(13) + CHAR(10) +
    N'- Có thể cho máu cho: A Rh-, A Rh+, AB Rh-, AB Rh+' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Lưu ý:' + CHAR(13) + CHAR(10) +
    N'- Người Rh- **chỉ nên nhận máu Rh-**, vì nếu nhận Rh+ có thể gây phản ứng miễn dịch nghiêm trọng.',
    
    'https://img.jagranjosh.com/images/2022/July/2672022/blood-group-peronality-test-blood-type-a-personality.jpg',
    2,
    'Article',
    '2025-05-01'),

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
     'https://phongkhambienviet.com/hinhanh/images/nhom-mau.jpg', 2, 'Article','2025-05-01'),

    -- Nhóm B Rh-
    (N'Giới thiệu nhóm máu B Rh+', 
    N'Nhóm máu B Rh+ có những đặc điểm:' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'- Có kháng nguyên B trên hồng cầu.' + CHAR(13) + CHAR(10) +
    N'- Có kháng thể anti-A trong huyết tương.' + CHAR(13) + CHAR(10) +
    N'- Mang yếu tố Rh (Rh+).' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Người có nhóm máu B Rh+:' + CHAR(13) + CHAR(10) +
    N'- Có thể nhận máu từ: B Rh+, B Rh-, O Rh+, O Rh-' + CHAR(13) + CHAR(10) +
    N'- Có thể cho máu cho: B Rh+, AB Rh+' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Lưu ý:' + CHAR(13) + CHAR(10) +
    N'- Nhóm máu B Rh+ là phổ biến và có khả năng nhận từ nhiều nhóm khác nếu tương thích Rh.',
    
    'https://benhvienphuongdong.vn/public/uploads/tin-tuc/bai-viet/phu-nu-nhom-mau-b-1-1.jpg',
    2,
    'Article',
    '2025-05-01'),

    -- Nhóm AB Rh+
    (N'Giới thiệu nhóm máu AB Rh+', 
    N'Nhóm máu AB Rh+ là nhóm máu **hiếm và đặc biệt**.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Đặc điểm:' + CHAR(13) + CHAR(10) +
    N'- Có cả kháng nguyên A và B trên hồng cầu.' + CHAR(13) + CHAR(10) +
    N'- Không có kháng thể anti-A hay anti-B trong huyết tương.' + CHAR(13) + CHAR(10) +
    N'- Có yếu tố Rh (Rh+).' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Người có nhóm máu AB Rh+:' + CHAR(13) + CHAR(10) +
    N'- Có thể nhận máu từ: Tất cả các nhóm (A, B, AB, O - cả Rh+ và Rh-)' + CHAR(13) + CHAR(10) +
    N'- Có thể cho máu cho: AB Rh+' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Lưu ý:' + CHAR(13) + CHAR(10) +
    N'- AB Rh+ là **người nhận máu phổ thông**, rất thuận lợi trong cấp cứu.',
    
    'https://esuhai.vn/upload/news/2019/10/28/157224376892.jpg',
    2,
    'Article',
    '2025-05-01'),

    -- Nhóm AB Rh-
    (N'Giới thiệu nhóm máu AB Rh-', 
    N'Nhóm máu AB Rh- là một trong những nhóm máu **hiếm nhất**.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Đặc điểm:' + CHAR(13) + CHAR(10) +
    N'- Có kháng nguyên A và B.' + CHAR(13) + CHAR(10) +
    N'- Không có kháng thể anti-A hay anti-B.' + CHAR(13) + CHAR(10) +
    N'- Không có yếu tố Rh.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Người có nhóm máu AB Rh-:' + CHAR(13) + CHAR(10) +
    N'- Có thể nhận máu từ: AB Rh-, A Rh-, B Rh-, O Rh-' + CHAR(13) + CHAR(10) +
    N'- Có thể cho máu cho: AB Rh-, AB Rh+' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Lưu ý:' + CHAR(13) + CHAR(10) +
    N'- Dù không có kháng thể, nhưng vì Rh- nên **chỉ nhận được từ người Rh-**.',
    
    'https://tudienbenhhoc.com/wp-content/uploads/2019/08/nhom-mau-1.jpg',
    2,
    'Article',
    '2025-05-01'),

    -- Nhóm O Rh+
    (N'Giới thiệu nhóm máu O Rh+', 
    N'Nhóm máu O Rh+ là nhóm máu phổ biến nhất tại nhiều quốc gia.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Đặc điểm:' + CHAR(13) + CHAR(10) +
    N'- Không có kháng nguyên A hoặc B.' + CHAR(13) + CHAR(10) +
    N'- Có cả kháng thể anti-A và anti-B trong huyết tương.' + CHAR(13) + CHAR(10) +
    N'- Có yếu tố Rh (Rh+).' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Người có nhóm máu O Rh+:' + CHAR(13) + CHAR(10) +
    N'- Có thể nhận máu từ: O Rh+, O Rh-' + CHAR(13) + CHAR(10) +
    N'- Có thể cho máu cho: A Rh+, B Rh+, AB Rh+, O Rh+' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Lưu ý:' + CHAR(13) + CHAR(10) +
    N'- Không thể nhận từ các nhóm A, B, AB vì có kháng thể.',
    
    'https://cdn.vietnammoi.vn/2019/4/24/583749802262586157325502275917074281267200n-1556102897580755669015.png',
    2,
    'Article',
    '2025-05-01'),

    -- Nhóm O Rh-
    (N'Giới thiệu nhóm máu O Rh-', 
    N'Nhóm máu O Rh- được xem là nhóm máu **cho phổ thông** trong truyền máu khẩn cấp.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Đặc điểm:' + CHAR(13) + CHAR(10) +
    N'- Không có kháng nguyên A hoặc B.' + CHAR(13) + CHAR(10) +
    N'- Có cả kháng thể anti-A và anti-B.' + CHAR(13) + CHAR(10) +
    N'- Không có yếu tố Rh (Rh-).' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Người có nhóm máu O Rh-:' + CHAR(13) + CHAR(10) +
    N'- Có thể nhận máu từ: O Rh-' + CHAR(13) + CHAR(10) +
    N'- Có thể cho máu cho: Tất cả nhóm máu (A, B, AB, O - cả Rh+ và Rh-)' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Lưu ý:' + CHAR(13) + CHAR(10) +
    N'- O Rh- là nhóm máu **quan trọng trong cấp cứu**, vì an toàn với hầu hết người nhận.',
    
    'https://ichef.bbci.co.uk/ace/standard/976/cpsprodpb/182FF/production/_107317099_blooddonor976.jpg',
    2,
    'Article',
    '2025-05-01'),
    
    -- Hiến máu lần đầu
	(N'Hiến Máu Lần Đầu: Hành Trình Nhân Ái Bắt Đầu Từ Một Giọt Máu',
    
    N'1. Vì Sao Nên Hiến Máu?' + CHAR(13) + CHAR(10) +
    N'Hiến máu là một hành động cao cả, mang lại cơ hội sống cho hàng triệu người mỗi năm. Mỗi đơn vị máu bạn hiến có thể cứu sống đến ba người nhờ việc tách thành các thành phần như hồng cầu, tiểu cầu và huyết tương.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Ngoài ra, hiến máu còn giúp bạn:' + CHAR(13) + CHAR(10) +
    N'- Kiểm tra sức khỏe miễn phí: Trước khi hiến, bạn sẽ được kiểm tra huyết áp, nhịp tim, và xét nghiệm máu.' + CHAR(13) + CHAR(10) +
    N'- Cải thiện tuần hoàn máu: Việc hiến máu định kỳ giúp kích thích cơ thể sản sinh máu mới.' + CHAR(13) + CHAR(10) +
    N'- Giảm nguy cơ mắc bệnh tim mạch: Một số nghiên cứu cho thấy hiến máu có thể giảm lượng sắt dư thừa, từ đó giảm nguy cơ bệnh tim.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'2. Chuẩn Bị Trước Khi Hiến Máu' + CHAR(13) + CHAR(10) +
    N'Để đảm bảo quá trình hiến máu diễn ra suôn sẻ, bạn nên:' + CHAR(13) + CHAR(10) +
    N'- Ngủ đủ giấc: Ít nhất 7–8 tiếng trước ngày hiến máu.' + CHAR(13) + CHAR(10) +
    N'- Ăn nhẹ: Tránh ăn thực phẩm nhiều dầu mỡ; nên ăn nhẹ trước khi hiến máu khoảng 2 tiếng.' + CHAR(13) + CHAR(10) +
    N'- Uống đủ nước: Giúp duy trì huyết áp ổn định và dễ dàng lấy máu.' + CHAR(13) + CHAR(10) +
    N'- Mang theo giấy tờ tùy thân: CMND/CCCD hoặc giấy tờ hợp lệ khác.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'3. Quy Trình Hiến Máu Diễn Ra Như Thế Nào?' + CHAR(13) + CHAR(10) +
    N'Quy trình hiến máu thường bao gồm các bước sau:' + CHAR(13) + CHAR(10) +
    N'- Đăng ký: Điền thông tin cá nhân và lịch sử y tế.' + CHAR(13) + CHAR(10) +
    N'- Khám sàng lọc: Kiểm tra huyết áp, nhịp tim, và xét nghiệm máu nhanh.' + CHAR(13) + CHAR(10) +
    N'- Hiến máu: Quá trình lấy máu kéo dài khoảng 10–15 phút.' + CHAR(13) + CHAR(10) +
    N'- Nghỉ ngơi: Sau khi hiến, bạn sẽ được nghỉ ngơi và ăn nhẹ để phục hồi.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'4. Lưu Ý Sau Khi Hiến Máu' + CHAR(13) + CHAR(10) +
    N'Sau khi hiến máu, bạn nên:' + CHAR(13) + CHAR(10) +
    N'- Uống nhiều nước: Giúp cơ thể nhanh chóng bù đắp lượng máu đã mất.' + CHAR(13) + CHAR(10) +
    N'- Tránh vận động mạnh: Trong 24 giờ đầu tiên, hạn chế các hoạt động thể chất nặng.' + CHAR(13) + CHAR(10) +
    N'- Ăn uống đầy đủ: Bổ sung thực phẩm giàu sắt như thịt đỏ, rau xanh đậm.' + CHAR(13) + CHAR(10) +
    N'- Theo dõi sức khỏe: Nếu có dấu hiệu bất thường, hãy liên hệ với cơ sở y tế gần nhất.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'5. Kết Luận' + CHAR(13) + CHAR(10) +
    N'Hiến máu không chỉ là hành động cứu người mà còn mang lại nhiều lợi ích cho chính bạn. Nếu bạn đang cân nhắc hiến máu lần đầu, hãy chuẩn bị kỹ lưỡng và đừng ngần ngại tham gia. Một giọt máu cho đi, một cuộc đời ở lại.',

    'https://cdni.iconscout.com/illustration/premium/thumb/boy-donating-blood-6358731-5270679.png',
    6,
    'Article',
    '2025-05-01'),

    -- Lưu ý hiến máu định kỳ
    (N'Người Hiến Máu Thường Xuyên Cần Lưu Ý Điều Gì?', 
    
    N'1. Khoảng Cách Giữa Các Lần Hiến Máu' + CHAR(13) + CHAR(10) +
    N'Để đảm bảo sức khỏe, người hiến máu cần tuân thủ khoảng cách tối thiểu giữa các lần hiến:' + CHAR(13) + CHAR(10) +
    N'- Hiến máu toàn phần: ít nhất 12 tuần (3 tháng) giữa hai lần hiến.' + CHAR(13) + CHAR(10) +
    N'- Hiến tiểu cầu hoặc huyết tương: có thể thực hiện sau mỗi 2 tuần, tùy theo chỉ định của cơ sở y tế.' + CHAR(13) + CHAR(10) +
    N'Việc tuân thủ khoảng cách này giúp cơ thể có đủ thời gian để phục hồi và tái tạo lượng máu đã hiến.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'2. Chế Độ Dinh Dưỡng Hợp Lý' + CHAR(13) + CHAR(10) +
    N'Người hiến máu thường xuyên nên duy trì chế độ ăn uống cân đối, giàu chất sắt và vitamin:' + CHAR(13) + CHAR(10) +
    N'- Thực phẩm giàu sắt: thịt đỏ, gan, rau xanh đậm, đậu, hạt.' + CHAR(13) + CHAR(10) +
    N'- Vitamin C: cam, chanh, dâu tây, giúp tăng cường hấp thu sắt.' + CHAR(13) + CHAR(10) +
    N'- Tránh: các thực phẩm nhiều chất béo và đồ uống có cồn trước khi hiến máu.' + CHAR(13) + CHAR(10) +
    N'Chế độ dinh dưỡng hợp lý giúp duy trì lượng hemoglobin ổn định và hỗ trợ quá trình tái tạo máu.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'3. Lối Sống Lành Mạnh' + CHAR(13) + CHAR(10) +
    N'Để đảm bảo sức khỏe khi hiến máu thường xuyên, bạn nên:' + CHAR(13) + CHAR(10) +
    N'- Ngủ đủ giấc: ngủ từ 7–8 tiếng mỗi đêm.' + CHAR(13) + CHAR(10) +
    N'- Tập thể dục đều đặn: duy trì hoạt động thể chất nhẹ nhàng như đi bộ, yoga.' + CHAR(13) + CHAR(10) +
    N'- Tránh căng thẳng: thực hành thiền, hít thở sâu để giảm stress.' + CHAR(13) + CHAR(10) +
    N'Lối sống lành mạnh giúp cơ thể phục hồi nhanh chóng sau mỗi lần hiến máu.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'4. Theo Dõi Sức Khỏe Định Kỳ' + CHAR(13) + CHAR(10) +
    N'Người hiến máu thường xuyên nên kiểm tra sức khỏe định kỳ:' + CHAR(13) + CHAR(10) +
    N'- Xét nghiệm máu: kiểm tra hemoglobin, sắt huyết thanh.' + CHAR(13) + CHAR(10) +
    N'- Khám tổng quát: đánh giá tổng thể tình trạng sức khỏe.' + CHAR(13) + CHAR(10) +
    N'Việc theo dõi sức khỏe giúp phát hiện sớm các vấn đề và đảm bảo an toàn khi tiếp tục hiến máu.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'5. Lưu Ý Sau Khi Hiến Máu' + CHAR(13) + CHAR(10) +
    N'Sau mỗi lần hiến máu, bạn nên:' + CHAR(13) + CHAR(10) +
    N'- Uống nhiều nước: giúp cơ thể bù đắp lượng dịch đã mất.' + CHAR(13) + CHAR(10) +
    N'- Ăn nhẹ: bổ sung năng lượng bằng bữa ăn nhẹ sau hiến máu.' + CHAR(13) + CHAR(10) +
    N'- Tránh vận động mạnh: trong 24 giờ đầu sau hiến máu.' + CHAR(13) + CHAR(10) +
    N'Những lưu ý này giúp cơ thể bạn phục hồi nhanh chóng và chuẩn bị tốt cho lần hiến máu tiếp theo.',
    
    'https://thumbs.dreamstime.com/b/blood-donation-illustration-concept-can-use-web-banners-infographics-hero-images-flat-illustration-isolated-white-270884972.jpg',
    6,
    'Article',
    '2025-05-01'),

    -- Lợi ích hiến máu hiến máu định kỳ
    (N'Những Lợi Ích Sức Khỏe Khi Hiến Máu Định Kỳ',

    N'Hiến Máu – Không Chỉ Là Cứu Người' + CHAR(13) + CHAR(10) +
    N'Hiến máu từ lâu được biết đến là một hành động nhân đạo cao đẹp, giúp cứu sống hàng triệu người mỗi năm. Tuy nhiên, ít ai biết rằng việc hiến máu định kỳ cũng mang lại nhiều lợi ích thiết thực cho chính người hiến.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Khi bạn hiến máu đều đặn, cơ thể không chỉ được kích thích sản sinh máu mới mà còn tạo điều kiện để bạn:' + CHAR(13) + CHAR(10) +
    N'- Giảm lượng sắt dư thừa: Duy trì mức sắt ổn định giúp hạn chế nguy cơ mắc bệnh tim và gan.' + CHAR(13) + CHAR(10) +
    N'- Cải thiện tuần hoàn máu: Việc hiến máu thúc đẩy quá trình sản sinh tế bào máu mới, giúp máu lưu thông tốt hơn.' + CHAR(13) + CHAR(10) +
    N'- Tầm soát bệnh: Mỗi lần hiến máu đều được kiểm tra miễn phí các chỉ số như huyết áp, nhịp tim, và xét nghiệm máu giúp phát hiện sớm các bệnh lý tiềm ẩn.' + CHAR(13) + CHAR(10) +
    N'- Tăng cường sức khỏe tinh thần: Cảm giác được giúp đỡ người khác mang lại sự hài lòng, giảm stress và nâng cao chất lượng cuộc sống.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Lợi Ích Với Tâm Trạng Và Cuộc Sống' + CHAR(13) + CHAR(10) +
    N'Không chỉ cải thiện thể chất, hiến máu còn giúp cải thiện tinh thần đáng kể:' + CHAR(13) + CHAR(10) +
    N'- Giảm căng thẳng: Khi làm việc tốt, cơ thể tiết ra hormone hạnh phúc giúp bạn cảm thấy tích cực hơn.' + CHAR(13) + CHAR(10) +
    N'- Xây dựng thói quen sống lành mạnh: Người hiến máu thường xuyên sẽ chú ý hơn đến dinh dưỡng, giấc ngủ và luyện tập để đảm bảo đủ điều kiện sức khỏe.' + CHAR(13) + CHAR(10) +
    N'- Gắn kết cộng đồng: Hiến máu là một hoạt động kết nối mọi người, lan tỏa yêu thương và trách nhiệm xã hội.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Hiến Máu Bao Nhiêu Lần Là Đủ?' + CHAR(13) + CHAR(10) +
    N'Tùy vào loại hiến máu, mỗi người có thể hiến với tần suất khác nhau:' + CHAR(13) + CHAR(10) +
    N'- Hiến máu toàn phần: Tối đa 4 lần/năm đối với nam và 3 lần/năm với nữ.' + CHAR(13) + CHAR(10) +
    N'- Hiến tiểu cầu/huyết tương: Có thể lặp lại sau mỗi 2 tuần, nhưng không quá 24 lần/năm.' + CHAR(13) + CHAR(10) +
    N'Điều quan trọng là bạn cần theo dõi sức khỏe và tuân thủ hướng dẫn từ nhân viên y tế để đảm bảo an toàn.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Lưu Ý Khi Hiến Máu Định Kỳ' + CHAR(13) + CHAR(10) +
    N'Để đảm bảo hiến máu hiệu quả và an toàn, hãy:' + CHAR(13) + CHAR(10) +
    N'- Ăn uống đầy đủ trước và sau hiến máu.' + CHAR(13) + CHAR(10) +
    N'- Uống nhiều nước để hỗ trợ quá trình tuần hoàn.' + CHAR(13) + CHAR(10) +
    N'- Tránh vận động mạnh sau hiến máu ít nhất 24 giờ.' + CHAR(13) + CHAR(10) +
    N'- Giữ tâm trạng thoải mái và ngủ đủ giấc trước ngày hiến máu.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Kết Luận' + CHAR(13) + CHAR(10) +
    N'Hiến máu định kỳ không chỉ cứu người mà còn là liệu pháp giúp bạn sống khỏe mạnh, hạnh phúc và có ích hơn cho cộng đồng. Hãy biến việc hiến máu trở thành thói quen đẹp trong cuộc sống của bạn.',

    'https://content.health.harvard.edu/wp-content/uploads/2023/08/b0dc8f35-3126-4eae-b575-38285553c9a4.jpg',
    6,
    'Article',
    '2025-05-01'),

    -- Hiểu về nhóm máu và vai trò
    (N'Hiểu Đúng Về Các Nhóm Máu Và Vai Trò Trong Hiến Máu',

    N'1. Nhóm Máu Là Gì?' + CHAR(13) + CHAR(10) +
    N'Máu của mỗi người được phân loại dựa trên sự hiện diện hay vắng mặt của các kháng nguyên và kháng thể. Hai hệ thống phân loại nhóm máu phổ biến nhất hiện nay là:' + CHAR(13) + CHAR(10) +
    N'- Hệ ABO: Gồm 4 nhóm chính – A, B, AB và O.' + CHAR(13) + CHAR(10) +
    N'- Hệ Rh: Dựa trên sự hiện diện (+) hoặc không có (-) của yếu tố Rh (Rhesus) trên bề mặt hồng cầu.' + CHAR(13) + CHAR(10) +
    N'Khi kết hợp lại, ta có 8 nhóm máu phổ biến: A+, A−, B+, B−, AB+, AB−, O+, O−.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'2. Các Nhóm Máu Phổ Biến Như Thế Nào?' + CHAR(13) + CHAR(10) +
    N'Tỷ lệ các nhóm máu không đồng đều ở từng khu vực và quốc gia. Tại Việt Nam, thống kê y học cho thấy:' + CHAR(13) + CHAR(10) +
    N'- O+: Chiếm khoảng 42% dân số – nhóm máu phổ biến nhất.' + CHAR(13) + CHAR(10) +
    N'- A+: Khoảng 25% – cũng rất phổ biến.' + CHAR(13) + CHAR(10) +
    N'- B+: Chiếm khoảng 23%.' + CHAR(13) + CHAR(10) +
    N'- AB+: Khoảng 7%.' + CHAR(13) + CHAR(10) +
    N'- Nhóm Rh− (âm tính): Rất hiếm, chỉ khoảng 0.04% dân số.' + CHAR(13) + CHAR(10) +
    N'Sự phân bố này ảnh hưởng rất lớn đến nhu cầu và khả năng tiếp nhận máu khi truyền.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'3. Vì Sao Nhóm Máu Quan Trọng Trong Hiến Máu?' + CHAR(13) + CHAR(10) +
    N'Trong truyền máu, việc phù hợp nhóm máu giữa người cho và người nhận là vô cùng quan trọng. Nếu truyền sai nhóm máu, có thể xảy ra phản ứng miễn dịch nghiêm trọng, thậm chí gây tử vong.' + CHAR(13) + CHAR(10) +
    N'Một số quy tắc cơ bản:' + CHAR(13) + CHAR(10) +
    N'- Nhóm O− là nhóm máu "phổ quát", có thể cho tất cả các nhóm máu khác trong trường hợp khẩn cấp.' + CHAR(13) + CHAR(10) +
    N'- Nhóm O+ có thể cho O+, A+, B+ và AB+.' + CHAR(13) + CHAR(10) +
    N'- Nhóm A− có thể cho A− và A+, AB− và AB+.' + CHAR(13) + CHAR(10) +
    N'- Nhóm A+ có thể cho A+ và AB+.' + CHAR(13) + CHAR(10) +
    N'- Nhóm B− có thể cho B− và B+, AB− và AB+.' + CHAR(13) + CHAR(10) +
    N'- Nhóm B+ có thể cho B+ và AB+.' + CHAR(13) + CHAR(10) +
    N'- Nhóm AB− có thể cho AB− và AB+.' + CHAR(13) + CHAR(10) +
    N'- Nhóm AB+ chỉ có thể cho AB+, nhưng lại có thể nhận máu từ tất cả các nhóm – gọi là "người nhận phổ quát".' + CHAR(13) + CHAR(10) +
    N'Tuy nhiên, các nguyên tắc trên chỉ áp dụng cho truyền máu toàn phần hoặc hồng cầu, còn với tiểu cầu hoặc huyết tương, nguyên tắc có thể khác biệt.' + CHAR(13) + CHAR(10) +
    N'- Nhóm máu đặc biệt:' + CHAR(13) + CHAR(10) +
    N'  • O− (người cho máu toàn phần phổ quát): Có thể cho bất kỳ nhóm máu nào.' + CHAR(13) + CHAR(10) +
    N'  • AB+ (người nhận máu toàn phần phổ quát): Có thể nhận máu từ bất kỳ nhóm nào.' + CHAR(13) + CHAR(10) +
    N'Tuy nhiên, điều này chỉ đúng với máu toàn phần hoặc khối hồng cầu. Với tiểu cầu, huyết tương hay các thành phần khác, quy tắc truyền có thể khác.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'4. Ai Là Người Cần Máu?' + CHAR(13) + CHAR(10) +
    N'Máu được sử dụng trong rất nhiều trường hợp y tế:' + CHAR(13) + CHAR(10) +
    N'- Người gặp tai nạn, mất máu cấp.' + CHAR(13) + CHAR(10) +
    N'- Bệnh nhân phẫu thuật (đặc biệt là các ca mổ lớn).' + CHAR(13) + CHAR(10) +
    N'- Người bệnh tan máu bẩm sinh, ung thư máu, suy tủy.' + CHAR(13) + CHAR(10) +
    N'- Phụ nữ mang thai bị băng huyết hoặc biến chứng thai sản.' + CHAR(13) + CHAR(10) +
    N'- Trẻ sinh non hoặc sơ sinh thiếu máu.' + CHAR(13) + CHAR(10) +
    N'Do đó, nhu cầu về máu luôn cao và diễn ra liên tục. Việc hiểu rõ nhóm máu giúp người dân chủ động hơn trong việc hiến máu phù hợp.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'5. Vai Trò Của Các Nhóm Máu Trong Hiến Máu' + CHAR(13) + CHAR(10) +
    N'Mỗi nhóm máu có vai trò riêng biệt trong công tác hiến máu cứu người:' + CHAR(13) + CHAR(10) +
    N'- Nhóm O−: Rất quý hiếm. Dù chỉ chiếm tỷ lệ nhỏ, nhưng cực kỳ quan trọng trong cấp cứu khẩn cấp vì có thể truyền cho tất cả các nhóm máu.' + CHAR(13) + CHAR(10) +
    N'- Nhóm O+ và A+: Do số lượng lớn người sở hữu nhóm máu này nên lượng máu dự trữ cần duy trì đều đặn.' + CHAR(13) + CHAR(10) +
    N'- Nhóm AB: Dù ít người mang nhóm máu này, nhưng huyết tương AB lại là loại phổ quát – có thể truyền cho mọi nhóm máu.' + CHAR(13) + CHAR(10) +
    N'- Rh−: Do tỉ lệ cực thấp, nên người nhóm máu Rh− được xem là "kho máu hiếm".' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'6. Bạn Đã Biết Nhóm Máu Của Mình Chưa?' + CHAR(13) + CHAR(10) +
    N'Rất nhiều người chưa biết nhóm máu của mình – điều này tiềm ẩn rủi ro trong các tình huống cấp cứu. Khi đi hiến máu, bạn sẽ được xét nghiệm miễn phí nhóm máu và biết được thông tin quan trọng này. Ngoài ra, bạn có thể yêu cầu cấp thẻ người hiến máu để lưu trữ và sử dụng trong các trường hợp khẩn cấp.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'7. Kết Luận' + CHAR(13) + CHAR(10) +
    N'Việc hiểu đúng về các nhóm máu không chỉ giúp bạn bảo vệ sức khỏe cá nhân mà còn giúp bạn chủ động hơn trong việc tham gia hiến máu phù hợp, đúng thời điểm, đúng nhu cầu. Dù bạn thuộc nhóm máu nào, mỗi giọt máu bạn cho đi đều mang lại cơ hội sống cho người khác.' + CHAR(13) + CHAR(10) +
    N'Hiến máu không chỉ là một hành động tốt – đó còn là sự kết nối kỳ diệu giữa những trái tim.',

    'https://images.ctfassets.net/pxcfulgsd9e2/articleImage90323/86f549d15651b745eab20e1e20c5cc84/Blood-donation-myths-HN1221-Stock-844661710-Sized.png',
    2,
    'Article',
    '2025-05-01'),

    -- Lưu và sử dụng máu hiến
    (N'Máu Hiến Sẽ Đi Đâu Và Được Sử Dụng Như Thế Nào?',

    N'Hành Trình Của Máu' + CHAR(13) + CHAR(10) +
    N'Bạn có bao giờ tự hỏi: Sau khi hiến máu, đơn vị máu ấy sẽ được xử lý và sử dụng ra sao? Hành trình của máu không dừng lại tại điểm hiến – mà nó bắt đầu một chuỗi quy trình y tế nghiêm ngặt để đảm bảo an toàn và hiệu quả trong cứu chữa.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'1. Tiếp Nhận Và Bảo Quản' + CHAR(13) + CHAR(10) +
    N'Ngay sau khi bạn hiến máu, túi máu được:' + CHAR(13) + CHAR(10) +
    N'- Gắn mã số định danh duy nhất.' + CHAR(13) + CHAR(10) +
    N'- Đặt vào hộp chuyên dụng bảo quản lạnh (khoảng 2–6°C).' + CHAR(13) + CHAR(10) +
    N'- Vận chuyển về trung tâm huyết học hoặc ngân hàng máu trong thời gian ngắn nhất.' + CHAR(13) + CHAR(10) +
    N'Tại đây, máu sẽ được lưu trữ tạm thời trong điều kiện tiêu chuẩn trước khi phân tách và xét nghiệm.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'2. Xét Nghiệm Và Kiểm Tra' + CHAR(13) + CHAR(10) +
    N'Mỗi đơn vị máu đều phải trải qua các bước xét nghiệm nghiêm ngặt:' + CHAR(13) + CHAR(10) +
    N'- Kiểm tra nhóm máu: ABO và Rh.' + CHAR(13) + CHAR(10) +
    N'- Sàng lọc bệnh truyền nhiễm: HIV, viêm gan B và C, giang mai, sốt rét, v.v.' + CHAR(13) + CHAR(10) +
    N'- Kiểm tra kháng thể bất thường để đảm bảo tương thích khi truyền.' + CHAR(13) + CHAR(10) +
    N'Chỉ những đơn vị máu đạt tiêu chuẩn an toàn tuyệt đối mới được đưa vào sử dụng.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'3. Phân Tách Các Thành Phần Máu' + CHAR(13) + CHAR(10) +
    N'Một đơn vị máu toàn phần có thể được tách ra thành nhiều thành phần khác nhau, phục vụ cho các mục đích điều trị cụ thể:' + CHAR(13) + CHAR(10) +
    N'- Hồng cầu: Truyền cho bệnh nhân thiếu máu, mất máu cấp.' + CHAR(13) + CHAR(10) +
    N'- Tiểu cầu: Dành cho người bị xuất huyết giảm tiểu cầu, bệnh ung thư.' + CHAR(13) + CHAR(10) +
    N'- Huyết tương tươi đông lạnh: Dùng trong cấp cứu, bệnh rối loạn đông máu.' + CHAR(13) + CHAR(10) +
    N'Nhờ quá trình này, một đơn vị máu có thể cứu được từ 2 đến 3 bệnh nhân, giúp tối ưu hóa giá trị của máu hiến.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'4. Cung Ứng Cho Bệnh Viện Và Trung Tâm Y Tế' + CHAR(13) + CHAR(10) +
    N'Sau khi phân tích và xử lý, máu sẽ được phân phối đến:' + CHAR(13) + CHAR(10) +
    N'- Bệnh viện đa khoa, trung tâm cấp cứu.' + CHAR(13) + CHAR(10) +
    N'- Trung tâm điều trị ung thư, sản khoa.' + CHAR(13) + CHAR(10) +
    N'- Cơ sở phẫu thuật, hồi sức tích cực, v.v.' + CHAR(13) + CHAR(10) +
    N'Tại đây, máu được truyền trực tiếp cho bệnh nhân theo chỉ định của bác sĩ, góp phần quan trọng trong việc cứu sống hàng triệu người mỗi năm.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'5. Những Trường Hợp Cần Máu Cấp Bách' + CHAR(13) + CHAR(10) +
    N'Nhu cầu máu luôn ở mức cao, nhất là trong:' + CHAR(13) + CHAR(10) +
    N'- Tai nạn giao thông, chấn thương nặng.' + CHAR(13) + CHAR(10) +
    N'- Phẫu thuật lớn (tim mạch, ghép tạng).' + CHAR(13) + CHAR(10) +
    N'- Bệnh lý huyết học như tan máu bẩm sinh, suy tủy.' + CHAR(13) + CHAR(10) +
    N'- Sản phụ mất máu sau sinh.' + CHAR(13) + CHAR(10) +
    N'- Bệnh nhân ung thư cần truyền tiểu cầu.' + CHAR(13) + CHAR(10) +
    N'Việc hiến máu đều đặn và liên tục chính là nguồn lực quý giá giúp ngành y tế ứng phó với những tình huống khẩn cấp này.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Kết Luận' + CHAR(13) + CHAR(10) +
    N'Máu bạn hiến ra không hề lãng phí – nó trải qua một quá trình kiểm định chặt chẽ và được sử dụng để mang lại sự sống cho những người đang chiến đấu với bệnh tật hoặc tai nạn. Mỗi giọt máu là một tia hy vọng. Hãy tiếp tục hành trình nhân đạo này, vì bạn có thể đang giữ trong mình “chìa khóa sống” của ai đó.',

    'https://www.trentondaily.com/wp-content/uploads/2024/06/Baldwin-1-10-Reasons-to-Donate-Blood.jpg',
    6,
    'Article',
    '2025-05-01'),

    -- Hiến toàn phần và hiến tiểu cầu
    (N'Sự Khác Biệt Giữa Hiến Máu Toàn Phần Và Hiến Tiểu Cầu',
    
    N'Không Chỉ Là “Hiến Máu” – Hãy Hiểu Đúng Hơn' + CHAR(13) + CHAR(10) +
    N'Khi nghe đến “hiến máu”, nhiều người chỉ nghĩ đơn giản là lấy máu từ cơ thể người hiến để truyền cho người cần. Tuy nhiên, trong y học hiện đại, máu có thể được phân loại và hiến tách biệt theo nhu cầu điều trị. Hai hình thức phổ biến nhất là hiến máu toàn phần và hiến tiểu cầu – mỗi loại đều có quy trình, mục đích và lợi ích riêng biệt.' + CHAR(13) + CHAR(10) +
    N'Hãy cùng khám phá sự khác biệt để lựa chọn hình thức hiến máu phù hợp nhất với bạn.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'1. Hiến Máu Toàn Phần – Đơn Giản Và Phổ Biến' + CHAR(13) + CHAR(10) +
    N'Hiến máu toàn phần là hình thức hiến máu truyền thống, trong đó khoảng 350–450ml máu được lấy ra từ tĩnh mạch người hiến, bao gồm tất cả các thành phần máu như: hồng cầu, bạch cầu, tiểu cầu và huyết tương.' + CHAR(13) + CHAR(10) +
    N' Thời gian thực hiện: Khoảng 10–15 phút.' + CHAR(13) + CHAR(10) +
    N' Thời gian phục hồi: Từ 1–2 tháng, do cơ thể cần tái tạo đủ hồng cầu.' + CHAR(13) + CHAR(10) +
    N' Tần suất hiến máu:' + CHAR(13) + CHAR(10) +
    N'Nam giới: Tối đa 4 lần/năm' + CHAR(13) + CHAR(10) +
    N'Nữ giới: Tối đa 3 lần/năm' + CHAR(13) + CHAR(10) +
    N' Ứng dụng lâm sàng: Máu toàn phần có thể được truyền trực tiếp hoặc phân tách thành nhiều thành phần để phục vụ nhiều bệnh nhân khác nhau.' + CHAR(13) + CHAR(10) +
    N' Ưu điểm: Nhanh chóng, dễ thực hiện, phù hợp với hầu hết người lần đầu hiến máu.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'2. Hiến Tiểu Cầu – Chính Xác Và Chuyên Biệt' + CHAR(13) + CHAR(10) +
    N'Hiến tiểu cầu là phương pháp trong đó máy tách tiểu cầu tự động sẽ thu lấy tiểu cầu từ máu người hiến, sau đó trả lại hồng cầu và huyết tương về cơ thể.' + CHAR(13) + CHAR(10) +
    N' Thời gian thực hiện: 60–90 phút do cần quá trình lọc máu liên tục.' + CHAR(13) + CHAR(10) +
    N' Thời gian phục hồi: Nhanh hơn, thường chỉ sau vài ngày.' + CHAR(13) + CHAR(10) +
    N' Tần suất hiến máu: Có thể 2 tuần/lần, tối đa 24 lần/năm.' + CHAR(13) + CHAR(10) +
    N' Ứng dụng lâm sàng: Tiểu cầu rất cần thiết cho bệnh nhân ung thư, người điều trị hóa chất, xuất huyết nặng hoặc rối loạn đông máu.' + CHAR(13) + CHAR(10) +
    N' Ưu điểm: Cung cấp tiểu cầu chất lượng cao, hỗ trợ điều trị cho các ca bệnh đặc biệt cần truyền gấp.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'3. So Sánh Nhanh: Toàn Phần vs. Tiểu Cầu' + CHAR(13) + CHAR(10) +
    N'Tiêu chí	Hiến máu toàn phần	Hiến tiểu cầu' + CHAR(13) + CHAR(10) +
    N'Thành phần lấy	Tất cả thành phần máu	Chỉ tiểu cầu' + CHAR(13) + CHAR(10) +
    N'Thời gian	10–15 phút	60–90 phút' + CHAR(13) + CHAR(10) +
    N'Tần suất	3–4 lần/năm	24 lần/năm (cách 2 tuần/lần)' + CHAR(13) + CHAR(10) +
    N'Ứng dụng	Đa dạng	Điều trị đặc biệt' + CHAR(13) + CHAR(10) +
    N'Phục hồi	4–8 tuần	3–5 ngày' + CHAR(13) + CHAR(10) +
    N'Yêu cầu thiết bị	Không	Máy tách tiểu cầu chuyên dụng' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'4. Nên Chọn Hình Thức Nào?' + CHAR(13) + CHAR(10) +
    N'Nếu bạn mới bắt đầu hiến máu: Hiến máu toàn phần là lựa chọn an toàn và dễ thực hiện.' + CHAR(13) + CHAR(10) +
    N'Nếu bạn có thời gian, sức khỏe ổn định và muốn hỗ trợ những ca bệnh đặc biệt: Hãy thử hiến tiểu cầu.' + CHAR(13) + CHAR(10) +
    N'Tùy thuộc vào nhu cầu thực tế tại bệnh viện, bạn có thể được khuyến khích chọn hình thức phù hợp nhất.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Kết Luận' + CHAR(13) + CHAR(10) +
    N'Dù là hiến máu toàn phần hay hiến tiểu cầu, mỗi giọt máu bạn trao đi đều mang trong mình giá trị sống vô giá. Việc hiểu rõ từng hình thức không chỉ giúp bạn chuẩn bị tốt hơn mà còn đảm bảo đóng góp hiệu quả nhất cho cộng đồng.' + CHAR(13) + CHAR(10) +
    N' Hãy chọn cách hiến máu phù hợp với bạn, và cùng nhau lan tỏa sự sống đến mọi người!',

    'https://www.singlecare.com/blog/wp-content/uploads/2019/12/Blog_010620_Who_Can_Cant_Donate_Blood.png',
    2,
    'Article',
    '2025-05-01'),

    -- Câu chuyện thật
    (N'Câu Chuyện Thật: Một Đơn Vị Máu, Một Cuộc Đời Được Cứu',
    
    N'Đằng Sau Một Túi Máu – Là Một Cuộc Đời' + CHAR(13) + CHAR(10) +
    N'Hiến máu là hành động giản dị nhưng mang ý nghĩa sâu sắc. Mỗi đơn vị máu bạn hiến tặng không chỉ là những giọt chất lỏng đỏ tươi – mà là niềm hy vọng sống còn của một con người, một gia đình, thậm chí cả một thế hệ.' + CHAR(13) + CHAR(10) +
    N'Câu chuyện dưới đây là minh chứng rõ ràng nhất cho giá trị của một hành động nhỏ nhưng cứu cả một cuộc đời.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'1. Em Bé 5 Tuổi Và Cuộc Chiến Với Căn Bệnh Hiếm' + CHAR(13) + CHAR(10) +
    N'Tại khoa Huyết học – Bệnh viện Nhi Trung Ương, bé Linh (5 tuổi) mắc căn bệnh thiếu máu bất sản – một rối loạn hiếm gặp khiến cơ thể không thể tự sản sinh máu. Mỗi tháng, em cần truyền máu để duy trì sự sống.' + CHAR(13) + CHAR(10) +
    N'Một lần, lượng máu trong kho bệnh viện sụt giảm nghiêm trọng, đặc biệt là nhóm máu O – nhóm máu của Linh. Các bác sĩ chỉ còn 12 giờ để tìm máu nếu không, em sẽ rơi vào hôn mê do thiếu oxy nghiêm trọng.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'2. Người Lạ Trong Danh Sách Khẩn Cấp' + CHAR(13) + CHAR(10) +
    N'Khi thông tin được đăng tải lên mạng xã hội, anh Minh – một nhân viên văn phòng 32 tuổi sống gần đó – ngay lập tức đến trung tâm hiến máu.' + CHAR(13) + CHAR(10) +
    N'Là người từng hiến máu nhiều lần, anh Minh không ngần ngại khi biết ca bệnh khẩn cấp cần nhóm máu O. Sau khi xét nghiệm, anh đủ điều kiện và lập tức hiến tiểu cầu trực tiếp cho bé Linh.' + CHAR(13) + CHAR(10) +
    N'Chỉ vài giờ sau, máu được truyền đến bệnh viện, và kịp thời cứu sống em bé khỏi biến chứng nguy hiểm.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'3. Gặp Lại Sau Một Năm – Khoảnh Khắc Không Bao Giờ Quên' + CHAR(13) + CHAR(10) +
    N'Một năm sau, trong chương trình “Lễ hội Xuân Hồng”, bé Linh – giờ đã khỏe mạnh và đi học bình thường – được mời lên sân khấu. Em mang bó hoa nhỏ để cảm ơn những người đã từng hiến máu cứu mình. Trùng hợp, anh Minh cũng có mặt trong sự kiện với tư cách người hiến tiêu biểu.' + CHAR(13) + CHAR(10) +
    N'Khoảnh khắc hai người gặp nhau, cả hội trường vỡ òa trong xúc động. Em bé ôm lấy anh – người từng không quen biết – và nói:' + CHAR(13) + CHAR(10) +
    N'“Cháu cảm ơn chú, nếu không có chú, cháu sẽ không được đến lớp học với các bạn...”' + CHAR(13) + CHAR(10) +
    N'Không có món quà nào ý nghĩa hơn giây phút ấy.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'4. Một Đơn Vị Máu – Nhiều Hơn Bạn Nghĩ' + CHAR(13) + CHAR(10) +
    N'Một đơn vị máu có thể:' + CHAR(13) + CHAR(10) +
    N'Cứu sống người gặp tai nạn giao thông.' + CHAR(13) + CHAR(10) +
    N'Hồi sinh bệnh nhân xuất huyết nội.' + CHAR(13) + CHAR(10) +
    N'Duy trì sự sống cho những người bị bệnh máu mãn tính như tan máu bẩm sinh, ung thư máu...' + CHAR(13) + CHAR(10) +
    N'Mang đến cơ hội sống cho trẻ sơ sinh thiếu máu, người cần phẫu thuật tim mạch, ghép tạng...' + CHAR(13) + CHAR(10) +
    N' Một người hiến máu, có thể cứu tới 3 người bệnh khi máu được tách thành các thành phần khác nhau.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Kết Luận' + CHAR(13) + CHAR(10) +
    N'Câu chuyện của bé Linh chỉ là một trong hàng ngàn ca được cứu sống nhờ sự sẻ chia của cộng đồng hiến máu. Hôm nay, bạn có thể là người hiến; ngày mai, người thân bạn có thể là người nhận.' + CHAR(13) + CHAR(10) +
    N'Hiến máu không mất đi – chỉ là đang trao đi sự sống.' + CHAR(13) + CHAR(10) +
    N'Hãy tham gia – để mỗi giọt máu bạn cho đi là một câu chuyện hồi sinh diệu kỳ.',
    
    'https://static.vecteezy.com/system/resources/previews/017/012/867/non_2x/man-donating-his-blood-blood-donation-illustration-free-vector.jpg',
    6,
    'Article',
    '2025-05-01'),

   -- Sự kiến hiến máu
(N' CHIẾN DỊCH HIẾN MÁU CỘNG ĐỒNG 2025 - KẾT NỐI YÊU THƯƠNG ',
    
    N' Một giọt máu cho đi – Một cuộc đời ở lại' + CHAR(13) + CHAR(10) +
    N'Chiến Dịch Hiến Máu Cộng Đồng 2025, hợp tác với các doanh nghiệp tại TP.HCM, là sự kiện ý nghĩa nhằm khắc phục tình trạng thiếu máu tại Trung Tâm Hiến Máu.' + CHAR(13) + CHAR(10) +
    N'Hiến máu không chỉ cứu sống bệnh nhân mà còn mang lại lợi ích sức khỏe như cải thiện tuần hoàn máu và giảm nguy cơ bệnh tim.' + CHAR(13) + CHAR(10) +
    N'Sự kiện này lan tỏa tinh thần tương thân tương ái, xây dựng cộng đồng đoàn kết, nhân văn.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Đây là bản hòa ca của lòng nhân ái, nơi những giọt máu hồng kết nối những trái tim, thắp sáng hy vọng cho những người cần máu khẩn cấp.' + CHAR(13) + CHAR(10) +
    N'Sự tham gia của bạn là món quà vô giá, mang lại cơ hội sống và niềm tin.' + CHAR(13) + CHAR(10) +
    N'Hãy cùng Trung Tâm Hiến Máu và các doanh nghiệp lan tỏa yêu thương! Nhanh tay đăng ký để chung tay cứu sống nhé.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Kết Nối Doanh Nghiệp, Sẻ Chia Sự Sống' + CHAR(13) + CHAR(10) +
    N'_______________________________' + CHAR(13) + CHAR(10) +

    N'THÔNG TIN CHI TIẾT SỰ KIỆN:' + CHAR(13) + CHAR(10) +
    N' Thời gian: 15/07/2025, 8:00 - 16:00' + CHAR(13) + CHAR(10) +
    N' Địa điểm: Trung Tâm Hiến Máu, đường CMT8, Q.3, TP.HCM' + CHAR(13) + CHAR(10) +
    N' Đối tượng tham gia: Nhân viên doanh nghiệp, cộng đồng địa phương, và tất cả những ai muốn sẻ chia',
    
    'https://c8.alamy.com/comp/2D6N38T/blood-donation-transfusion-vector-flat-cartoon-illustration-volunteer-female-donor-donating-blood-in-medical-hospital-laboratory-world-blood-donor-2D6N38T.jpg',
    2,
    'News',
    '2025-07-01'),

    -- Ngày hiến máu thế giới
(N' NGÀY HIẾN MÁU THẾ GIỚI 2025 - LAN TỎA TÌNH NHÂN ÁI ',

    N' Một giọt máu cho đi – Một cuộc đời ở lại' + CHAR(13) + CHAR(10) +
    N'Ngày Hiến Máu Thế Giới 14/06/2025 là dịp để toàn cầu tôn vinh những người hiến máu và nâng cao nhận thức về tầm quan trọng của hiến máu nhân đạo.' + CHAR(13) + CHAR(10) +
    N'Với chủ đề **“Cảm ơn bạn, người hiến máu!”**, sự kiện nhấn mạnh vai trò của mỗi giọt máu trong việc cứu sống hàng triệu người.' + CHAR(13) + CHAR(10) +
    N'Tại Trung Tâm Hiến Máu, chúng tôi kêu gọi cộng đồng tham gia, đặc biệt những người có nhóm máu hiếm như O- và AB-, để cùng giải quyết tình trạng thiếu máu và mang lại lợi ích sức khỏe như cải thiện tuần hoàn máu và tinh thần tích cực.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Đây không chỉ là một sự kiện, mà là bản hòa ca của lòng nhân ái, nơi những trái tim toàn cầu chung nhịp đập vì sự sống.' + CHAR(13) + CHAR(10) +
    N'Mỗi giọt máu bạn trao đi là sợi dây gắn kết yêu thương, lan tỏa niềm tin và hy vọng đến mọi người.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Sự chung tay của bạn là món quà ý nghĩa cho những bệnh nhân đang cần máu.' + CHAR(13) + CHAR(10) +
    N'Hãy nhanh tay đăng ký để cùng Trung Tâm Hiến Máu lan tỏa tình nhân ái nhé!' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Cảm Ơn Bạn – Người Hiến Máu Toàn Cầu' + CHAR(13) + CHAR(10) +
    N'_______________________________' + CHAR(13) + CHAR(10) +

    N'THÔNG TIN CHI TIẾT SỰ KIỆN:' + CHAR(13) + CHAR(10) +
    N' Thời gian: 14/06/2025, 7:00 - 17:00' + CHAR(13) + CHAR(10) +
    N' Địa điểm: Trung Tâm Hiến Máu, đường CMT8, Q.3, TP.HCM' + CHAR(13) + CHAR(10) +
    N' Đối tượng tham gia: Tất cả những ai đủ điều kiện sức khỏe, đặc biệt người có nhóm máu hiếm' + CHAR(13) + CHAR(10) +
    N' Hoạt động: Kiểm tra nhóm máu miễn phí, chia sẻ câu chuyện hiến máu, vinh danh các nhà hiến máu tiêu biểu',

    'https://static.vecteezy.com/system/resources/previews/007/926/239/non_2x/blood-donation-illustration-concept-with-blood-bag-world-blood-donor-day-vector.jpg',
    6,
    'News',
    '2025-06-01'),

    -- Sinh viên hiến máu
(N'HIẾN MÁU NHÂN ĐẠO 2025 - TIẾP NGUỒN SINH KHÍ',

    N'Một giọt máu cho đi – Một cuộc đời ở lại' + CHAR(13) + CHAR(10) +
    N'Hiến Máu Nhân Đạo 2025 - Tiếp Nguồn Sinh Khí là dự án hiến máu được thực hiện hằng năm nhằm góp phần khắc phục tình trạng thiếu máu tại các ngân hàng máu trên địa bàn TP.HCM, đồng thời nâng cao nhận thức về hoạt động Hiến Máu Nhân Đạo – một nghĩa cử cao đẹp không chỉ giúp các bệnh nhân và các hoạt động y tế mà còn mang lại lợi ích sức khỏe cho chính người hiến.' + CHAR(13) + CHAR(10) +
    N'Qua đó, dự án mong muốn lan tỏa tinh thần tương thân tương ái, xây dựng lối sống tích cực, nhân văn trong cộng đồng sinh viên FPT nói riêng và xã hội nói chung.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Đây không chỉ là một sự kiện, mà còn là bản hòa ca của lòng nhân ái, nơi những trái tim cùng chung nhịp đập vì sự sẻ chia và tình yêu thương cuộc sống.' + CHAR(13) + CHAR(10) +
    N'Mỗi giọt máu được trao đi không chỉ mang lại cơ hội sống, mà còn là sợi dây gắn kết yêu thương, lan tỏa niềm tin và hy vọng đến mọi người xung quanh.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Sự chung tay của bạn sẽ là một niềm hy vọng, là món quà ý nghĩa dành tặng những người đang cần được tiếp sức.' + CHAR(13) + CHAR(10) +
    N'Và để thực hiện hóa được niềm hy vọng đó, hãy nhanh tay đăng ký tham gia để cùng SiTiGroup lan tỏa yêu thương nhé.' + CHAR(13) + CHAR(10) +
    N'_______________________________' + CHAR(13) + CHAR(10) +

    N'THÔNG TIN CHI TIẾT SỰ KIỆN:' + CHAR(13) + CHAR(10) +
    N' Thời gian : 28/04/2025.' + CHAR(13) + CHAR(10) +
    N' Địa điểm: Đại học FPT HCM.' + CHAR(13) + CHAR(10) +
    N' Đối tượng tham gia: dành cho toàn thể cán bộ, giảng viên, nhân viên và sinh viên Đại học FPT HCM.',

    'https://static.vecteezy.com/system/resources/previews/013/927/153/original/blood-donation-illustration-concept-on-white-background-vector.jpg',
    6,
    'News',
    '2025-04-16'),

    -- Chương trình máu hiếm
(N'CHƯƠNG TRÌNH NGƯỜI HIẾN MÁU HIẾM 2025 - ÁNH SÁNG HY VỌNG',

    N'Một giọt máu cho đi – Một cuộc đời ở lại' + CHAR(13) + CHAR(10) +
    N'Chương Trình Đăng Ký Người Hiến Máu Hiếm 2025, khởi động từ 01/07/2025, là sáng kiến của Trung Tâm Hiến Máu nhằm xây dựng ngân hàng máu hiếm (O-, AB-).' + CHAR(13) + CHAR(10) +
    N'Máu hiếm là nguồn lực quý giá, cứu sống những bệnh nhân khó tìm máu tương thích.' + CHAR(13) + CHAR(10) +
    N'Tham gia chương trình mang lại lợi ích sức khỏe như kiểm tra định kỳ và nâng cao nhận thức về nhóm máu.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Đây là hành trình của những trái tim dũng cảm, nơi mỗi giọt máu hiếm là ngọn đèn soi sáng cho những ca bệnh hiểm nghèo.' + CHAR(13) + CHAR(10) +
    N'Sự tham gia của bạn là món quà vô giá, kết nối yêu thương.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Nếu bạn có nhóm máu O- hoặc AB-, hãy đăng ký ngay để cùng Trung Tâm Hiến Máu cứu người!' + CHAR(13) + CHAR(10) +
    N'Liên hệ qua hệ thống hoặc số 02838554137.' + CHAR(13) + CHAR(10) +
    N'Máu Hiếm – Món Quà Vô Giá' + CHAR(13) + CHAR(10) +
    N'_______________________________' + CHAR(13) + CHAR(10) +

    N'THÔNG TIN CHI TIẾT CHƯƠNG TRÌNH:' + CHAR(13) + CHAR(10) +
    N' Thời gian khởi động: 01/07/2025' + CHAR(13) + CHAR(10) +
    N' Địa điểm đăng ký: Trung Tâm Hiến Máu, đường CMT8, Q.3, TP.HCM' + CHAR(13) + CHAR(10) +
    N' Đối tượng tham gia: Người có nhóm máu hiếm (O-, AB-) hoặc chưa biết nhóm máu',

    'https://static.vecteezy.com/system/resources/previews/004/449/815/original/blood-donation-2d-isolated-illustration-man-in-chair-on-blood-transfusion-donor-with-smiling-nurse-flat-characters-on-cartoon-background-charity-work-and-volunteering-colourful-scene-vector.jpg',
    2,
    'News',
    '2025-06-15'),

    -- Vinh danh người hiến máu
(N'LỄ VINH DANH NGƯỜI HIẾN MÁU XUẤT SẮC 2025 - NHỮNG NGỌN LỬA NHÂN ÁI',

    N'Một giọt máu cho đi – Một cuộc đời ở lại' + CHAR(13) + CHAR(10) +
    N'Lễ Vinh Danh Người Hiến Máu Xuất Sắc 2025, diễn ra vào 30/08/2025, là dịp để Trung Tâm Hiến Máu tri ân những người đã hiến máu nhiều lần, góp phần cứu sống hàng trăm bệnh nhân.' + CHAR(13) + CHAR(10) +
    N'Sự kiện lan tỏa tinh thần hiến máu, khuyến khích cộng đồng tham gia hành động nhân đạo, mang lại lợi ích sức khỏe và niềm vui sẻ chia.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Đây là bản giao hưởng của lòng biết ơn, nơi những trái tim nhân ái được tôn vinh, truyền cảm hứng cho mọi người.' + CHAR(13) + CHAR(10) +
    N'Sự hiện diện của bạn sẽ làm rực rỡ ý nghĩa của sự kiện.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Hãy đến để cùng Trung Tâm Hiến Máu vinh danh những người hùng thầm lặng!' + CHAR(13) + CHAR(10) +
    N'Đăng ký tham dự qua hệ thống hoặc email trungtamhienmau@gmail.vn.' + CHAR(13) + CHAR(10) +
    N'Tri Ân Những Người Hùng Thầm Lặng' + CHAR(13) + CHAR(10) +
    N'_______________________________' + CHAR(13) + CHAR(10) +

    N'THÔNG TIN CHI TIẾT SỰ KIỆN:' + CHAR(13) + CHAR(10) +
    N' Thời gian: 30/08/2025, 18:00 - 20:00' + CHAR(13) + CHAR(10) +
    N' Địa điểm: Trung Tâm Hiến Máu, đường CMT8, Q.3, TP.HCM' + CHAR(13) + CHAR(10) +
    N' Đối tượng tham gia: Người hiến máu, cộng đồng, và những ai yêu mến hành động nhân đạo',

    'https://www.mindray.com/content/dam/xpace/en/media-center/press-center/blog/transfusion-safety-of-donated-blood/blood-donor-day-23-kv-pc.jpg',
    6,
    'News',
    '2025-08-01'),

    -- Hiến máu tiểu cầu
(N'TẦM QUAN TRỌNG CỦA HIẾN TIỂU CẦU 2025 - HỖ TRỢ BỆNH NHÂN UNG THƯ',

    N'Một giọt máu cho đi – Một cuộc đời ở lại' + CHAR(13) + CHAR(10) +
    N'Trung Tâm Hiến Máu kêu gọi cộng đồng tham gia **Chiến Dịch Hiến Tiểu Cầu 2025**, khởi động từ 01/11/2025, để hỗ trợ bệnh nhân ung thư, những người cần truyền tiểu cầu để ngăn ngừa xuất huyết trong quá trình hóa trị.' + CHAR(13) + CHAR(10) +
    N'Tiểu cầu, một thành phần quan trọng của máu, giúp đông máu và duy trì sự sống.' + CHAR(13) + CHAR(10) +
    N'Hiến tiểu cầu không chỉ cứu sống mà còn mang lại lợi ích sức khỏe như kích thích sản sinh tế bào mới và nâng cao tinh thần sẻ chia.' + CHAR(13) + CHAR(10) +
    N'Chiến dịch này nhằm nâng cao nhận thức về nhu cầu tiểu cầu và lan tỏa tinh thần nhân ái.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Đây không chỉ là một hành động hiến máu, mà là bản hòa ca của lòng nhân ái, nơi mỗi túi tiểu cầu là ngọn lửa hy vọng, thắp sáng cuộc sống cho những bệnh nhân ung thư đang chiến đấu từng ngày.' + CHAR(13) + CHAR(10) +
    N'Sự tham gia của bạn là món quà vô giá, mang lại cơ hội sống và niềm tin.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +

    N'Hãy đến Trung Tâm Hiến Máu để hiến tiểu cầu và hỗ trợ bệnh nhân ung thư!' + CHAR(13) + CHAR(10) +
    N'Nhanh tay đăng ký qua hệ thống hoặc liên hệ số 02838554137 để biết thêm chi tiết.' + CHAR(13) + CHAR(10) +
    N'Tiểu Cầu – Ngọn Lửa Hy Vọng Cho Ung Thư' + CHAR(13) + CHAR(10) +
    N'_______________________________' + CHAR(13) + CHAR(10) +

    N'THÔNG TIN CHI TIẾT CHƯƠNG TRÌNH:' + CHAR(13) + CHAR(10) +
    N' Thời gian khởi động: 01/11/2025' + CHAR(13) + CHAR(10) +
    N' Địa điểm: Trung Tâm Hiến Máu, đường CMT8, Q.3, TP.HCM' + CHAR(13) + CHAR(10) +
    N' Đối tượng tham gia: Tất cả những ai đủ điều kiện sức khỏe, đặc biệt người có nhóm máu A+, B+, AB+, O+' + CHAR(13) + CHAR(10) +
    N' Hoạt động: Hiến tiểu cầu, kiểm tra sức khỏe miễn phí, tư vấn y tế về ung thư và hiến máu',

    'https://stanfordbloodcenter.org/wp-content/uploads/2020/06/Blood-facts_10-illustration-graphics__canteen.png',
    2,
    'News',
    '2025-10-15');
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
(1, N'CheckIn', 2, N'Nhập kho từ hiến máu', 3, '2025-06-01', '250ml', '2025-06-01', '2025-07-01'),
(2, N'Cancel', 1, N'Rách túi máu', 2, '2025-06-02', '350ml', '2025-06-02', '2025-07-02'),
(3, N'CheckOut', 1, N'Cấp cứu BV tỉnh', 4, '2025-06-03', '450ml', '2025-06-03', '2025-07-10'),
(4, N'CheckIn', 3, N'Tài trợ từ tổ chức', 1, '2025-06-04', '250ml', '2025-06-04', '2025-07-20'),
(5, N'CheckOut', 2, N'Sử dụng cho ghép tạng', 3, '2025-06-05', '450ml', '2025-06-05', '2025-07-25'),
(6, N'Cancel', 1, N'Không đạt tiêu chuẩn', 2, '2025-06-06', '350ml', '2025-06-06', '2025-07-06'),
(7, N'CheckIn', 4, N'Hiến máu cộng đồng', 4, '2025-06-07', '250ml', '2025-06-07', '2025-08-01'),
(8, N'CheckOut', 2, N'Chuyển viện', 1, '2025-06-08', '450ml', '2025-06-08', '2025-08-10'),
(1, N'Cancel', 1, N'Máu bị đông', 3, '2025-06-09', '350ml', '2025-06-09', '2025-07-09'),
(2, N'CheckIn', 3, N'Hiến máu nội viện', 2, '2025-06-10', '250ml', '2025-06-10', '2025-07-25'),
(3, N'CheckOut', 1, N'Dùng cho phẫu thuật', 5, '2025-06-11', '450ml', '2025-06-11', '2025-07-20'),
(4, N'Cancel', 2, N'Không bảo quản đúng nhiệt độ', 2, '2025-06-12', '350ml', '2025-06-12', '2025-07-12'),
(5, N'CheckIn', 2, N'Hiến máu tình nguyện', 4, '2025-06-13', '250ml', '2025-06-13', '2025-07-28'),
(6, N'CheckOut', 4, N'Cấp cứu tuyến huyện', 3, '2025-06-14', '450ml', '2025-06-14', '2025-08-10'),
(7, N'Cancel', 3, N'Túi máu vỡ', 1, '2025-06-15', '350ml', '2025-06-15', '2025-07-15'),
(8, N'CheckIn', 1, N'Hiến máu định kỳ', 5, '2025-06-16', '250ml', '2025-06-16', '2025-07-31'),
(1, N'CheckOut', 2, N'Bệnh viện sử dụng cho sản phụ', 4, '2025-06-17', '450ml', '2025-06-17', '2025-08-01'),
(2, N'Cancel', 4, N'Nhiễm khuẩn', 2, '2025-06-18', '350ml', '2025-06-18', '2025-07-20'),
(3, N'CheckIn', 2, N'Nhập kho nội viện', 3, '2025-06-19', '250ml', '2025-06-19', '2025-07-29'),
(4, N'CheckOut', 1, N'Ca phẫu thuật tim', 4, '2025-06-20', '450ml', '2025-06-20', '2025-07-25'),
(5, N'Cancel', 2, N'Máu không đạt chất lượng', 1, '2025-06-21', '350ml', '2025-06-21', '2025-07-21'),
(6, N'CheckIn', 3, N'Hiến máu tại sự kiện trường học', 2, '2025-06-22', '250ml', '2025-06-22', '2025-08-01'),
(7, N'CheckOut', 1, N'Truyền máu ngoại viện', 5, '2025-06-23', '450ml', '2025-06-23', '2025-07-30'),
(8, N'Cancel', 3, N'Rách rỗ', 3, '2025-06-24', '350ml', '2025-06-24', '2025-07-26'),
(1, N'CheckIn', 1, N'Hiến máu nhân đạo', 4, '2025-06-25', '250ml', '2025-06-25', '2025-07-31'),
(2, N'CheckOut', 3, N'Dùng cho trẻ em thiếu máu', 3, '2025-06-26', '450ml', '2025-06-26', '2025-08-02'),
(3, N'Cancel', 2, N'Máu quá hạn', 2, '2025-06-27', '350ml', '2025-06-27', '2025-07-27'),
(4, N'CheckIn', 4, N'Tiếp nhận từ ngân hàng máu', 1, '2025-06-28', '250ml', '2025-06-28', '2025-08-05'),
(5, N'CheckOut', 1, N'Cấp cứu ngoại viện', 4, '2025-06-29', '450ml', '2025-06-29', '2025-08-01'),
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
    (2, N'Yêu cầu khẩn cấp', N'Chúng tôi cần máu của bạn cho trường hợp khẩn cấp', 'Alert', 0, GETDATE()),
    (4, N'Yêu cầu máu đã chấp nhận', N'Yêu cầu máu của bạn đã được chấp nhận', 'Alert', 1, GETDATE()),
    (1, N'Lịch Hẹn Đã Hủy', N'Bạn đã hủy lịch hẹn hiến máu ngày 24/06/2025.', 'Report', 1, '2025-06-24 09:00:00'),
    (2, N'Nhắc Hồi Phục Hiến Máu', N'Bạn đã đủ 12 tuần kể từ lần hiến máu trước (01/04/2025)! Bạn có thể hiến lại.', 'Reminder', 1, '2025-06-24 07:01:00'),
    (2, N'Cảm ơn', N'Cảm ơn bạn đã hiến máu!', 'Report', 1, '2025-04-01 12:00:00');
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

INSERT INTO Appointments (UserID, AppointmentDate, TimeSlot, Status, Process, Cancel, Notes, CreatedAt, DonationDate)
VALUES
-- UserID 1: Lịch đã hủy
(1, '2025-06-24', N'Chiều (13:00-17:00)', NULL, 0, 1, N'Người dùng hủy lịch hẹn', DATEADD(DAY, -10, '2025-06-24'), NULL),

-- UserID 1: Lịch hoàn chỉnh với ngày cách >=84 ngày (đổi từ 2025-06-30 sang 2025-04-01)
(1, '2025-04-01', N'Sáng (7:00-12:00)', 1, 4, 0, N'Đã hiến máu thành công ngày 01/04/2025', DATEADD(DAY, -10, '2025-04-01'), '2025-04-01'),

-- UserID 2: Chỉ lịch hoàn chỉnh (ngày xa quá khứ để đủ điều kiện tạo lịch mới)
(2, '2025-04-01', N'Sáng (7:00-12:00)', 1, 4, 0, N'Đã hiến máu thành công', DATEADD(DAY, -10, '2025-04-01'), '2025-04-01');

GO


INSERT INTO Reminders (UserId, Type, Message, RemindAt, IsDisabled, CreatedAt, IsSent, SentAt)
VALUES
-- UserID 1: BloodDonation trước 1 ngày cho lịch 30/7/2025 (chưa gửi vì tương lai)
(1, 'BloodDonation', N'Nhắc bạn chuẩn bị hiến máu vào ngày 30/07/2025 (Sáng 7:00-12:00)', '2025-07-29 07:00:00', 0, '2025-07-20 12:00:00', 0, NULL),
-- UserID 2: Recovery sau 84 ngày từ DonationDate 01/4/2025 (đã gửi vì quá khứ)
(2, 'Recovery', N'Bạn đã đủ 12 tuần kể từ lần hiến máu trước (01/04/2025)! Bạn có thể hiến lại.', '2025-06-24 07:00:00', 0, '2025-04-01 12:00:00', 1, '2025-06-24 07:01:00');
GO
