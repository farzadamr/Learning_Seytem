USE leaning_SystemDB;


CREATE TABLE Person (
    Id INT PRIMARY KEY,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    PhoneNumber NVARCHAR(11),
    Email NVARCHAR(100),
    AvatarPath NVARCHAR(255),
    Password NVARCHAR(255)
);

CREATE TABLE Student (
    Id INT PRIMARY KEY,
    Lockout BIT,
    FOREIGN KEY (Id) REFERENCES Person(Id)
);

CREATE TABLE Teacher (
    Id INT PRIMARY KEY,
    Resume NVARCHAR(MAX),
    FOREIGN KEY (Id) REFERENCES Person(Id)
);

CREATE TABLE Admin (
    Id INT PRIMARY KEY,
    Username NVARCHAR(50),
    FOREIGN KEY (Id) REFERENCES Person(Id)
);

CREATE TABLE Courses (
    Id INT PRIMARY KEY,
    TeacherId INT,
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
    FOREIGN KEY (TeacherId) REFERENCES Teacher(Id)
);

CREATE TABLE StudentCourses (
    StudentId INT,
    CourseId INT,
    PRIMARY KEY (StudentId, CourseId),
    FOREIGN KEY (StudentId) REFERENCES Student(Id),
    FOREIGN KEY (CourseId) REFERENCES Courses(Id)
);

CREATE TABLE Category (
    Id INT PRIMARY KEY,
    CourseId INT,
    Type NVARCHAR(50),
    ParentTypeId INT,
    FOREIGN KEY (CourseId) REFERENCES Courses(Id),
    FOREIGN KEY (ParentTypeId) REFERENCES Category(Id)
);

CREATE TABLE Section (
    Id INT PRIMARY KEY,
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
    PRIMARY KEY (Number),
    FOREIGN KEY (CourseId) REFERENCES Courses(Id),
    FOREIGN KEY (SectionId) REFERENCES Section(Id)
);
