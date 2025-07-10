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
    SelfReportedLastDonationDate DATETIME NULL,
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
    BloodGroup NVARCHAR(2) NOT NULL,
    RhType NVARCHAR(3) NOT NULL,
    ComponentID INT NOT NULL, -- Hồng cầu, Huyết tương, Tiểu cầu, Toàn phần
    Quantity INT NOT NULL CHECK (Quantity >= 0),
    IsRare BIT DEFAULT 0, -- 1 for rare blood (e.g., AB-)
    Status TINYINT NOT NULL, -- 0: Khẩn cấp, 1: Thiếu máu, 2: Trung bình, 3: An toàn
    LastUpdated DATETIME DEFAULT GETDATE(),
    BagType NVARCHAR(5), -- 250ml, 350ml, 450ml
    ReceivedDate DATETIME NOT NULL DEFAULT GETDATE(), -- Date received
    ExpirationDate DATETIME, -- Expiration date
    FOREIGN KEY (ComponentID) REFERENCES Components(ComponentID)
);


-- Create BloodInventoryHistory table after BloodInventory
CREATE TABLE BloodInventoryHistories (
    HistoryID INT PRIMARY KEY IDENTITY(1,1),
    InventoryID INT NULL,
    BloodGroup NVARCHAR(2) NULL,
    RhType NVARCHAR(3) NULL,
    ComponentID INT NOT NULL, -- Hồng cầu, Huyết tương, Tiểu cầu, Toàn phần
    ActionType NVARCHAR(10) NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    Notes NVARCHAR(255) NULL,
    PerformedBy INT NOT NULL,
    PerformedAt DATETIME DEFAULT GETDATE(),
    BagType NVARCHAR(5),
    ReceivedDate DATETIME, -- Date received
    ExpirationDate DATETIME, -- Expiration date
    FOREIGN KEY (ComponentID) REFERENCES Components(ComponentID),
    FOREIGN KEY (PerformedBy) REFERENCES Users(UserID),
    FOREIGN KEY (InventoryID) REFERENCES BloodInventories(InventoryID)
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
    CreatedTime DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ComponentID) REFERENCES Components(ComponentID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (PatientID) REFERENCES Patients(PatientID)
);

-- BloodDonationHistory table: Stores donation records
CREATE TABLE BloodDonationHistories (
    DonationID INT PRIMARY KEY IDENTITY(1,1),
    AppointmentID INT NOT NULL,
    DonationDate DATETIME NOT NULL,
    BloodGroup NVARCHAR(2) NOT NULL,
    RhType NVARCHAR(3) NOT NULL,
    DoctorID INT NULL,
    Notes NVARCHAR(255) NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    IsSuccess BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (AppointmentID) REFERENCES Appointments(AppointmentID) ON DELETE SET NULL,
    FOREIGN KEY (DoctorID) REFERENCES Users(UserID) ON DELETE SET NULL
);

-- Appointments table: Stores appointment created
CREATE TABLE Appointments (
    AppointmentID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    DoctorID INT NULL,
    AppointmentDate DATE NOT NULL,
    TimeSlot NVARCHAR(50) NOT NULL,
    Status TINYINT NOT NULL DEFAULT 0, -- 0: chờ duyệt, 1: từ chối, 2: chấp nhận
    Cancel BIT NOT NULL DEFAULT 0,
    Notes NVARCHAR(255) NULL,
    BloodPressure NVARCHAR(20) NULL,
    HeartRate INT NULL,
    Hemoglobin FLOAT NULL,
    Temperature FLOAT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE SET NULL,
    FOREIGN KEY (DoctorID) REFERENCES Users(UserID) ON DELETE SET NULL
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
CREATE TABLE DonationReminders (
    ReminderID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    SuggestedDate DATETIME NOT NULL,
    IsSent BIT DEFAULT 0,
    SentAt DATETIME,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
GO
