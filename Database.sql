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
	
    Email NVARCHAR(MAX), -- AES encrypted
    Password NVARCHAR(MAX), -- Hash encrypted
    Phone NVARCHAR(11), -- AES encrypted

    Name NVARCHAR(50), -- AES encrypted
    Age INT,
    Gender NVARCHAR(10), -- Male, Female, Other
    Address NVARCHAR(MAX), -- AES encrypted
    BloodGroup NVARCHAR(2), -- A, B, AB, O
    RhType NVARCHAR(3), -- Rh+, Rh-


    Status TINYINT NOT NULL, -- 0: inactive, 1: active, 2: banned
    RoleID INT NOT NULL,
    Department NVARCHAR(50), -- For Staff-Doctor (e.g., Khoa A)
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);


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
    ImgUrl NVARCHAR(255)
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

-- BlogPosts table: Stores blog posts
CREATE TABLE BlogPosts (
    PostID INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(255) NOT NULL,
    Content NVARCHAR(MAX)NOT NULL,
    ImgUrl NVARCHAR(255),
    UserID INT NOT NULL,
    PostedAt DATETIME DEFAULT GETDATE(),
    Status TINYINT DEFAULT 0, -- 0: Pending, 1: Approved, 2: Rejected
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);


-- BlogPostTags table: For sorting blogs
CREATE TABLE BlogPostTags (
    PostID INT,
    TagID INT,
    PRIMARY KEY (PostID, TagID),
    FOREIGN KEY (PostID) REFERENCES BlogPosts(PostID),
    FOREIGN KEY (TagID) REFERENCES Tags(TagID)
);


-- BloodInventory table: Stores blood stock
CREATE TABLE BloodInventory (
    InventoryID INT PRIMARY KEY IDENTITY(1,1),
    BloodGroup NVARCHAR(2) NOT NULL,
    RhType NVARCHAR(3) NOT NULL,
    ComponentType NVARCHAR(20) NOT NULL, -- Whole, RedCells, Plasma, Platelets
    Quantity INT NOT NULL CHECK (Quantity >= 0),
    IsRare BIT DEFAULT 0, -- 1 for rare blood (e.g., AB-)
    LastUpdated DATETIME DEFAULT GETDATE()
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
    ComponentType NVARCHAR(20) NOT NULL, -- RedCells, Plasma, Platelets
    FOREIGN KEY (RequestID) REFERENCES BloodRequests(RequestID)
);


-- BloodRequestHistory table: Tracks request status changes
CREATE TABLE BloodRequestHistory (
    HistoryID INT PRIMARY KEY IDENTITY(1,1),
    RequestID INT NOT NULL,
    UserID INT NOT NULL, -- Staff-Doctor or Staff-BloodManager
    Status TINYINT NOT NULL, -- 0: Pending, 1: Verified, 2: Processing, 3: Connected, 4: Completed, 5: Rejected
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
    IsSuccessful BIT DEFAULT 1,
    Notes NVARCHAR(255),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);


-- PublicBloodRequests table: Stores public urgent requests
CREATE TABLE PublicBloodRequests (
    PublicRequestID INT PRIMARY KEY IDENTITY(1,1),
    RequestID INT NOT NULL,
    BloodGroup NVARCHAR(2) NOT NULL,
    RhType NVARCHAR(3) NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    Deadline DATETIME NOT NULL,
    IsRare BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (RequestID) REFERENCES BloodRequests(RequestID)
);


-- UserLocations table: Stores user locations
CREATE TABLE UserLocations (
    LocationID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    Latitude FLOAT NOT NULL,
    Longitude FLOAT NOT NULL,
    UpdatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);


-- Appointments table: Stores appointment created
CREATE TABLE Appointments (
    AppointmentID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL, -- Người đăng ký khám (thường là người hiến máu)
    AppointmentDate DATETIME  NOT NULL, -- Ngày giờ hẹn khám
    Status TINYINT DEFAULT 0, -- 0: Đang chờ, 1: Đã xác nhận, 2: Hủy      
    Notes NVARCHAR(255),
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
