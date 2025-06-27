CREATE DATABASE BloodManagementSystem;
GO

USE BloodManagementSystem;
GO

-- Roles table: Stores user roles
CREATE TABLE Roles (
    RoleID INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(20) NOT NULL UNIQUE -- Guest, Member, Staff-Doctor, Staff-BloodManager, Admin
);

-- Users table: Stores user accounts with encrypted data
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    --FirebaseUID NVARCHAR(128) NOT NULL UNIQUE, -- Firebase user ID
	
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
    Department NVARCHAR(50), -- For Staff-Doctor (e.g., Khoa A)
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);

CREATE TRIGGER trg_CalculateAge
ON Users
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE u
    SET Age = DATEDIFF(YEAR, i.DateOfBirth, GETDATE())
    FROM Users u
    INNER JOIN inserted i ON u.UserID = i.UserID
    WHERE i.DateOfBirth IS NOT NULL;
END;

-- HospitalInfo table: Stores information about hospital
CREATE TABLE HospitalInfo (
    ID INT PRIMARY KEY CHECK (ID = 1),-- Giới hạn insert
    Name NVARCHAR(255),
    Address NVARCHAR(255),
    Phone NVARCHAR(20),
    Email NVARCHAR(100),
    WorkingHours NVARCHAR(255),
    MapImageUrl NVARCHAR(255), -- link gán = ảnh bản đồ tĩnh Google Maps,
    Latitude FLOAT NOT NULL,
    Longitude FLOAT NOT NULL
);

-- BloodArticles table: Stores public blood group articles
CREATE TABLE BloodArticles (
    ArticleID INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(255) NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    ImgUrl NVARCHAR(255),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UserID INT NOT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- BloodTypeTags table: For sorting blood type tags
CREATE TABLE Tags(
    TagID INT PRIMARY KEY,
    TagName NVARCHAR(50) NOT NULL -- A+, O−, Truyền máu, Khẩn cấp
);

-- ArticleTags table: For sorting article tags
CREATE TABLE ArticleTags (
    ArticleID INT,
    TagID INT,
    PRIMARY KEY (ArticleID, TagID),
    FOREIGN KEY (ArticleID) REFERENCES BloodArticles(ArticleID),
    FOREIGN KEY (TagID) REFERENCES Tags(TagID)
);

-- News table: Stores blog posts
CREATE TABLE News (
    PostID INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(255) NOT NULL,
    Content NVARCHAR(MAX)NOT NULL,
    ImgUrl NVARCHAR(255),
    UserID INT NOT NULL,
    PostedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- NewsTags table: For sorting blogs
CREATE TABLE NewsTags (
    PostID INT,
    TagID INT,
    PRIMARY KEY (PostID, TagID),
    FOREIGN KEY (PostID) REFERENCES News(PostID),
    FOREIGN KEY (TagID) REFERENCES Tags(TagID)
);

-- ActivityLog table: Tracks News and Article activities for Admin
CREATE TABLE ActivityLog (
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
CREATE TABLE BloodInventory (
    InventoryID INT PRIMARY KEY IDENTITY(1,1),
    BloodGroup NVARCHAR(2) NOT NULL,
    RhType NVARCHAR(3) NOT NULL,
    ComponentType NVARCHAR(20) NOT NULL, -- Hồng cầu, Huyết tương, Tiểu cầu, Toàn phần
    Quantity INT NOT NULL CHECK (Quantity >= 0),
    IsRare BIT DEFAULT 0, -- 1 for rare blood (e.g., AB-)
    Status TINYINT NOT NULL, -- 0: Khẩn cấp, 1: Thiếu máu, 2: Trung bình, 3: An toàn
    LastUpdated DATETIME DEFAULT GETDATE(),
    BagType NVARCHAR(5), -- 250ml, 350ml, 450ml
    ReceivedDate DATETIME NOT NULL DEFAULT GETDATE(), -- Date received
    ExpirationDate DATETIME -- Expiration date
);


-- Create BloodInventoryHistory table after BloodInventory
CREATE TABLE BloodInventoryHistory (
    HistoryID INT PRIMARY KEY IDENTITY(1,1),
    InventoryID INT NULL,
    BloodGroup NVARCHAR(2) NULL,
    RhType NVARCHAR(3) NULL,
    ComponentType NVARCHAR(20) NULL,
    ActionType NVARCHAR(10) NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    Reason NVARCHAR(255) NULL,
    Notes NVARCHAR(255) NULL,
    PerformedBy INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    PerformedAt DATETIME DEFAULT GETDATE(),
    BagType NVARCHAR(5),
    ReceivedDate DATETIME, -- Date received
    ExpirationDate DATETIME, -- Expiration date
	FOREIGN KEY (InventoryID) REFERENCES BloodInventory(InventoryID)
);

-- BloodRequests table: Stores blood requests
CREATE TABLE BloodRequests (
    RequestID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL, -- Requester (Member or Staff-Doctor)
    BloodGroup NVARCHAR(2) NOT NULL,
    RhType NVARCHAR(3) NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    UrgencyLevel TINYINT NOT NULL, -- 0: Normal, 1: Urgent, 2: Critical
    NeededTime DATETIME NOT NULL,
    Reason NVARCHAR(1000),
    IsAutoApproved BIT DEFAULT 0, -- 1 for Staff-Doctor requests
    Status TINYINT NOT NULL, -- 0: Pending, 1: Accepted, 2: Completed, 3: Rejected
    CreatedTime DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- RequestComponents table: Stores blood components for requests
CREATE TABLE RequestComponents (
    ComponentID INT PRIMARY KEY IDENTITY(1,1),
    RequestID INT NOT NULL,
    ComponentType NVARCHAR(20) NOT NULL, -- RedCells, Plasma, Platelets, Whole
    FOREIGN KEY (RequestID) REFERENCES BloodRequests(RequestID)
);

-- BloodRequestHistory table: Tracks request status changes
CREATE TABLE BloodRequestHistory (
    HistoryID INT PRIMARY KEY IDENTITY(1,1),
    RequestID INT NOT NULL,
    UserID INT NOT NULL, -- Staff-Doctor or Staff-BloodManager
    Status TINYINT NOT NULL, --0-- Không thành công, 1-- Khám sức khỏe(đạt/ 0 đạt), 2-- Hiến máu, 3-- Xét nghiệm máu(đạt/0 đạt), 4-- Nhập kho
    Notes NVARCHAR(255),
    TimeStamp DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (RequestID) REFERENCES BloodRequests(RequestID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- BloodDonationHistory table: Stores donation records
CREATE TABLE BloodDonationHistory (
    DonationID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    DonationDate DATETIME NOT NULL,
    BloodGroup NVARCHAR(2) NOT NULL,
    RhType NVARCHAR(3) NOT NULL,
    ComponentType NVARCHAR(20) NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
	IsSuccess BIT DEFAULT 1,
    Notes NVARCHAR(255),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Appointments table: Stores appointment created
CREATE TABLE Appointments (
    AppointmentID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL, -- Người đăng ký khám (thường là người hiến máu)
    AppointmentDate DATE NOT NULL, -- Ngày giờ hẹn khám
    Status TINYINT DEFAULT 0, --0-- Không thành công, 1-- Khám sức khỏe(đạt/ 0 đạt), 2-- Hiến máu, 3-- Xét nghiệm máu(đạt/0 đạt), 4--Nhập kho
    Notes NVARCHAR(255),
	TimeSlot NVARCHAR(50),
	Cancel BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Notifications table: Stores user notifications
CREATE TABLE Notifications (
    NotificationID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    Title NVARCHAR(255) NOT NULL,
    Message NVARCHAR(255)NOT NULL,
    Type NVARCHAR(50) NOT NULL, -- Reminder, Alert, Report
    Priority TINYINT DEFAULT 0, -- 0: Normal, 1: High (for rare blood)
    IsRead BIT DEFAULT 0,
    SentAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- DonationReminders table: Stores donation reminders
CREATE TABLE DonationReminders (
    ReminderID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    SuggestedDate DATETIME NOT NULL,
    IsSent BIT DEFAULT 0,
    SentAt DATETIME,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
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
 GO
