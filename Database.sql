CREATE DATABASE BloodManagementSystem;
GO

USE BloodManagementSystem;
GO

-- Roles table: Stores user roles
CREATE TABLE Roles (
    RoleID INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(20) NOT NULL UNIQUE -- Guest, Member, Staff-Doctor, Staff-BloodManager, Admin
);

-- Department: Stores information about hospital department
CREATE TABLE Departments(
	DepartmentID INT PRIMARY KEY IDENTITY(1,1),
	DepartmentName NVARCHAR(255),
);

-- Components table: Stores blood components
CREATE TABLE Components (
    ComponentID INT PRIMARY KEY IDENTITY(1,1),
    ComponentType NVARCHAR(20) NOT NULL -- Hồng cầu, Huyết tương, Tiểu cầu, Toàn phần
);
-- Users table: Stores user accounts with encrypted data
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
	
    Email NVARCHAR(255), -- AES encrypted
    Password NVARCHAR(255), -- Hash encrypted
    Phone NVARCHAR(11), -- AES encrypted

    IDCardType NVARCHAR(50),-- AES encrypted
    IDCard NVARCHAR(12),-- AES encrypted
    Name NVARCHAR(50),  -- AES encrypted
    DateOfBirth DATETIME,
    Age INT,
    Gender NVARCHAR(10), -- Male, Female, Other
    City NVARCHAR(255), -- AES encrypted
    District NVARCHAR(50), -- AES encrypted
    Ward NVARCHAR(255),   -- AES encrypted
    Address NVARCHAR(255), -- AES encrypted
    Distance FLOAT, -- Distance: user address-hospital
    BloodGroup NVARCHAR(2), -- A, B, AB, O
    RhType NVARCHAR(3), -- Rh+, Rh-
    Weight FLOAT, -- Weight in kg
    Height FLOAT, -- Height in cm

    Status TINYINT DEFAULT 1, -- 0: inactive, 1: active
    RoleID INT NOT NULL,
    DepartmentID INT NULL, -- For Staff-Doctor (e.g., Khoa A)
    CreatedAt DATETIME DEFAULT GETDATE(),
    SelfReportedLastDonationDate DATETIME2 NULL,
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID),
    FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID)
);

-- HospitalInfo table: Stores information about hospital
CREATE TABLE HospitalInfo (
    ID INT PRIMARY KEY CHECK (ID = 1),-- Giới hạn insert
    Name NVARCHAR(255),
    Address NVARCHAR(255),
    Phone NVARCHAR(20),
    Email NVARCHAR(100),
    WorkingHours NVARCHAR(255),
    MapImageUrl NVARCHAR(MAX), -- link gán = ảnh bản đồ tĩnh Google Maps,
    Latitude FLOAT NOT NULL,
    Longitude FLOAT NOT NULL
);

--Tags table: For sorting type of tags
CREATE TABLE Tags(
    TagID INT IDENTITY(1,1) PRIMARY KEY,
    TagName NVARCHAR(50) NOT NULL -- A+, O−, Truyền máu, Khẩn cấp
);

 -- Contents table: For storing hospital content
CREATE TABLE Contents (
    ContentID INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(MAX) NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    ImgUrl NVARCHAR(MAX),
    ContentType NVARCHAR(20) NOT NULL, -- 'Article', 'News'
    UserID INT NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE() NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

CREATE TABLE ContentTags (
    ContentID INT,
    TagID INT,
    PRIMARY KEY (ContentID, TagID),
    FOREIGN KEY (ContentID) REFERENCES Contents(ContentID),
    FOREIGN KEY (TagID) REFERENCES Tags(TagID)
);

-- ActivityLog table: Tracks News and Article activities for Admin
CREATE TABLE ActivityLogs (
    LogID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    ActivityType NVARCHAR(50) NOT NULL, -- e.g., CreateArticle, UpdateArticle, DeleteArticle, CreateNews, UpdateNews, DeleteNews
    EntityID INT NOT NULL, -- ArticleID or PostID
    EntityType NVARCHAR(20) NOT NULL, -- Article or News
    Description NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Create BloodInventory table first
CREATE TABLE BloodInventories (
    InventoryID INT PRIMARY KEY IDENTITY(1,1),
    BloodGroup NVARCHAR(2) NOT NULL,               -- A, B, AB, O
    RhType NVARCHAR(3) NOT NULL,                   -- Rh+, Rh-
    BagType NVARCHAR(5),                           -- 250ml, 350ml, 450ml
    Quantity INT NOT NULL CHECK (Quantity >= 0),   -- Tổng số túi
    IsRare BIT NOT NULL DEFAULT 0,                 -- Rh- là máu hiếm
    Status INT NOT NULL,                           -- 0=Khẩn cấp, 1=Thiếu, 2=TB, 3=An toàn
    LastUpdated DATETIME NOT NULL DEFAULT GETDATE(), -- Ngày cập nhật cuối
    ComponentId INT NOT NULL,                      -- FK loại máu
    FOREIGN KEY (ComponentId) REFERENCES Components(ComponentID)
);



-- Create BloodInventoryHistory table after BloodInventory
CREATE TABLE BloodInventoryHistories (
    HistoryId INT PRIMARY KEY IDENTITY(1,1),
    InventoryId INT NOT NULL,                        -- Liên kết đến bảng chính
    ActionType NVARCHAR(10) NOT NULL,                -- CheckIn, CheckOut, Hủy
    Quantity INT NOT NULL CHECK (Quantity > 0),      -- Số lượng thay đổi
    Notes NVARCHAR(255),                             -- Ghi chú thao tác
    PerformedBy INT NOT NULL,                        -- Người thao tác (-1 nếu System)
    PerformedAt DATETIME NOT NULL DEFAULT GETDATE(), -- Thời điểm
    BagType NVARCHAR(5),                             -- Dung tích
    ReceivedDate DATETIME,                           -- Ngày nhận (nếu nhập kho)
    ExpirationDate DATETIME,                         -- Ngày hết hạn
    FOREIGN KEY (InventoryId) REFERENCES BloodInventories(InventoryID),
    FOREIGN KEY (PerformedBy) REFERENCES Users(UserID)
);


 CREATE TABLE Patients (
	PatientID INT PRIMARY KEY IDENTITY(1,1),
	FullName NVARCHAR(50),
	Gender NVARCHAR(10),
	DateOfBirth DATETIME,
	Age INT,
	Phone NVARCHAR(11),
	Address NVARCHAR(255),
	Email NVARCHAR(255),
);

-- BloodRequests table: Stores blood requests
CREATE TABLE BloodRequests (
    RequestID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL, -- Requester (Member or Staff-Doctor)
    PatientID INT,
    PatientName NVARCHAR(100),
    Age INT,
    Gender NVARCHAR(10),
    Relationship NVARCHAR(20),
    FacilityName NVARCHAR(255),
    DoctorName NVARCHAR(100), -- external doctor
    DoctorPhone NVARCHAR(11),
    BloodGroup NVARCHAR(2) NOT NULL,
    RhType NVARCHAR(3) NOT NULL,
    ComponentID INT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    Reason NVARCHAR(1000),
    Status TINYINT NOT NULL, -- 0: Pending, 1: Accepted, 2: Completed, 3: Rejected, 4: Delete
    Note NVARCHAR(1000) NULL,
    CreatedTime DATETIME DEFAULT GETDATE(),
    MedicalReport NVARCHAR(500) NULL,
    FOREIGN KEY (ComponentID) REFERENCES Components(ComponentID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (PatientID) REFERENCES Patients(PatientID)
);

-- Appointments table: Stores appointment created
CREATE TABLE Appointments (
    AppointmentID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    DoctorID1 INT NULL, -- Bác sĩ khám sức khỏe (update lần 1)
    DoctorID2 INT NULL, -- Bác sĩ xét nghiệm (update lần 2)
    AppointmentDate DATE NOT NULL,
    TimeSlot NVARCHAR(50) NOT NULL,
    Status BIT NULL, -- 0: từ chối, 1: chấp nhận
    Process TINYINT NOT NULL DEFAULT 0, -- 0: đăng ký, 1: khám sức khỏe, 2: lấy máu, 3: xét nghiệm, 4: nhập kho
    Cancel BIT NOT NULL DEFAULT 0,
    Notes NVARCHAR(255) NULL,
    BloodPressure NVARCHAR(20) NULL,
    HeartRate INT NULL,
    Hemoglobin FLOAT NULL,
    Temperature FLOAT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    WeightAppointment FLOAT NULL,
    HeightAppointment FLOAT NULL,
    DonationCapacity FLOAT NULL,
    DonationDate DATETIME NULL, -- Ngày hiến máu thực tế (do bác sĩ nhập sau, có thể NULL)
    BloodGroup NVARCHAR(2) NULL, -- Nhóm máu từ xét nghiệm
    RhType NVARCHAR(3) NULL, -- Rh từ xét nghiệm
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (DoctorID1) REFERENCES Users(UserID),
    FOREIGN KEY (DoctorID2) REFERENCES Users(UserID)
);


-- Notifications table: Stores user notifications
CREATE TABLE Notifications (
    NotificationID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    Title NVARCHAR(255) NOT NULL,
    Message NVARCHAR(255)NOT NULL,
    Type NVARCHAR(50) NOT NULL, -- Reminder, Alert, Report
    Priority BIT DEFAULT 0, -- 0: Normal, 1: High (for rare blood)
    IsRead BIT DEFAULT 0,
    SentAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- DonationReminders table: Stores donation reminders
CREATE TABLE Reminders (
    ReminderId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    Type NVARCHAR(50) NOT NULL,          -- BloodDonation, Recovery,...
    Message NVARCHAR(255) NOT NULL,
    RemindAt DATETIME NOT NULL,
    IsDisabled BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE(),
    IsSent BIT DEFAULT 0,
    SentAt DATETIME NULL,
    FOREIGN KEY (UserId) REFERENCES Users(UserID)
);

CREATE TABLE UploadedFiles (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FileName NVARCHAR(500),
    FileUrl NVARCHAR(MAX),
    UploadDate DATETIME,
    UploadedBy INT -- hoặc tên người upload
)
GO
