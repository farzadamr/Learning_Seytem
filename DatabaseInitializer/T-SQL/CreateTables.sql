USE leaning_SystemDB;


CREATE TABLE Person (
    Id INT PRIMARY KEY IDENTITY,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    PhoneNumber NVARCHAR(11),
    Email NVARCHAR(100),
    AvatarPath NVARCHAR(255),
    Password NVARCHAR(255)
);

CREATE TABLE Student (
    Id INT PRIMARY KEY IDENTITY,
    Lockout BIT,
    Major NVARCHAR(50) NOT NULL,
    LinkedId NVARCHAR(128),
    ActivityArea NVARCHAR(50),
    PersonId INT,
    FOREIGN KEY (PersonId) REFERENCES Person(Id)
);

CREATE TABLE Teacher (
    Id INT PRIMARY KEY IDENTITY,
    Resume NVARCHAR(MAX),
    PersonId INT,
    FOREIGN KEY (PersonId) REFERENCES Person(Id)
);

CREATE TABLE Admin (
    Id INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(50),
    PersonId INT,
    FOREIGN KEY (PersonId) REFERENCES Person(Id));
CREATE TABLE Category (
    Id INT PRIMARY KEY IDENTITY,
    Type NVARCHAR(50),
    ParentTypeId INT,
    FOREIGN KEY (ParentTypeId) REFERENCES Category(Id)
);
CREATE TABLE Courses (
    Id INT PRIMARY KEY IDENTITY,
    TeacherId INT,
    CategoryId INT,
    Title NVARCHAR(255),
    Description NVARCHAR(MAX),
    CreationDate DATETIME,
    UpdateTime DATETIME,
    Capacity INT,
    ThumbnailPath NVARCHAR(255),
    DemoVideoPath NVARCHAR(255),
    Time INT,
    Rate INT,
    Status NVARCHAR(50),
    FOREIGN KEY (TeacherId) REFERENCES Teacher(Id),
    FOREIGN KEY (CategoryId) REFERENCES Category(Id)
);

CREATE TABLE StudentCourses (
    StudentId INT,
    CourseId INT,
    PRIMARY KEY (StudentId, CourseId),
    FOREIGN KEY (StudentId) REFERENCES Student(Id),
    FOREIGN KEY (CourseId) REFERENCES Courses(Id)
);

CREATE TABLE Section (
    Id INT PRIMARY KEY IDENTITY,
    CourseId INT,
    Title NVARCHAR(255),
    FOREIGN KEY (CourseId) REFERENCES Courses(Id)
);

CREATE TABLE Episodes (
    Number INT,
    CourseId INT,
    SectionId INT,
    Time INT,
    FilePath NVARCHAR(255),
    FileSize INT,
    Visit INT,
    PRIMARY KEY (Number,SectionId),
    FOREIGN KEY (CourseId) REFERENCES Courses(Id),
    FOREIGN KEY (SectionId) REFERENCES Section(Id)
);
CREATE TABLE Wallet (
    Id INT PRIMARY KEY IDENTITY,
    Credit BIGINT,
    ChargeTime DATETIME,
    StudentId INT,
    FOREIGN KEY (StudentId) REFERENCES Student(Id),
    );
CREATE TABLE Charge(
    RefID BIGINT PRIMARY KEY,
    Authority INT,
    ispay BIT,
    amount INT,
    datepay DATETIME,
    StudentId INT,
    WalletId INT,
    FOREIGN KEY (StudentId) REFERENCES Student(Id),
    FOREIGN KEY (WalletId) REFERENCES Wallet(Id)
    );
