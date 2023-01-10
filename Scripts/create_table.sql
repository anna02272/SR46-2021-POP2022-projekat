select * from addresses

CREATE TABLE dbo.Addresses (
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Street VARCHAR(50),
	StreetNumber VARCHAR(10),
	City VARCHAR(50),
	Country VARCHAR (50), 
	IsDeleted BIT NOT NULL
)


CREATE TABLE dbo.Administrators (
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	UserName VARCHAR(50) NOT NULL,
	Password VARCHAR(50) NOT NULL
)

SET IDENTITY_INSERT Administrators ON
Insert into Administrators(id, username, Password) values (1, 'Admin', 'Admin123')



CREATE TABLE dbo.Languages
(
 Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
 NameOfLanguage VARCHAR(50) NOT NULL,
 IsDeleted BIT NOT NULL,
)



CREATE TABLE dbo.Users
(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	FirstName VARCHAR(50) NOT NULL,
	LastName VARCHAR(50) NOT NULL,
	Jmbg VARCHAR(13) UNIQUE NOT NULL,
	Email VARCHAR(50) UNIQUE NOT NULL,
	Password VARCHAR(50) NOT NULL,
	Gender VARCHAR(10) NOT NULL,
	UserType VARCHAR(10) NOT NULL,
	IsActive BIT NOT NULL,
	AddressId INT,
	CONSTRAINT FK_ADDRESSES_USERS
	FOREIGN KEY (AddressId) REFERENCES dbo.Addresses (Id)
)

CREATE TABLE dbo.Professors
(
 Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
 UserId INT NOT NULL UNIQUE,
 CONSTRAINT FK_USERS_PROFESSORS
 FOREIGN KEY (UserId) REFERENCES dbo.Users (Id)
)

CREATE TABLE dbo.Students
(
 Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
 UserId INT NOT NULL UNIQUE,
 CONSTRAINT FK_USERS_STUDENTS
 FOREIGN KEY (UserId) REFERENCES dbo.Users (Id)
)


CREATE TABLE dbo.Lessons
(
 Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
 Name VARCHAR(50) NOT NULL, 
 ProfessorId INT, 
 Date smalldatetime NOT NULL,
 Duration TIME NOT NULL ,
 Status BIT NOT NULL,
 StudentId INT,
 IsDeleted BIT NOT NULL,
 CONSTRAINT FK_PROFESSORS_LESSONS
 FOREIGN KEY(ProfessorId) REFERENCES dbo.Professors (Id),
 CONSTRAINT FK_STUDENTS_LESSONS
 FOREIGN KEY(StudentId) REFERENCES dbo.Students (Id)
)

CREATE TABLE dbo.Schools
(
 Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
 Name VARCHAR(50) NOT NULL, 
 AddressId INT, 
 LanguageId INT, 
 IsDeleted BIT NOT NULL,
 CONSTRAINT FK_ADDRESSES_SCHOOLS
 FOREIGN KEY(AddressId) REFERENCES dbo.Addresses (Id),
 CONSTRAINT FK_LANGUAGES_SCHOOLS
 FOREIGN KEY(LanguageId) REFERENCES dbo.Languages (Id)
)

select * from users
select * from professors
select * from students
select * from Addresses
select * from schools
select * from lessons
select * from languages