Create database VAHRM

CREATE TABLE Users
(
    Id VARCHAR(32) PRIMARY KEY,
    UserName VARCHAR(256),
    Password VARCHAR(256),
    IsAdmin bit,
	Permission VARCHAR(max),
	PermissionGroup VARCHAR(max),
	Name NVARCHAR(255),
	Code VARCHAR(255),
	Gender VARCHAR(255),
	IdentityNumber VARCHAR(255),
	DateOfBirth Datetime,
	PhoneHouseholder VARCHAR(255),
	Email VARCHAR(255),
	Address NVARCHAR(255),
	Salary int,
	Avatar VARCHAR(255),
);



CREATE TABLE PermissionGroup
(
    Id VARCHAR(32) PRIMARY KEY,	
	CodeGroup VARCHAR(256),
	NameGroup VARCHAR(256),
	Permission VARCHAR(max),
);

CREATE TABLE BonusDiscipline
(
    Id VARCHAR(32) PRIMARY KEY,	
	UsersId VARCHAR(256),
	Code VARCHAR(256),
	Name NVARCHAR(256),
	PathAttachFile VARCHAR(256),
	AttachFileName NVARCHAR(256),
	Amount int,
	EffectiveState VARCHAR(256),
	Reason NVARCHAR(256),
	CreateDate DateTime,
	BonusDisciplineType VARCHAR(255),
	Date DateTime,
);

CREATE TABLE TimekeepingType
(
    Id VARCHAR(32) PRIMARY KEY,	
	Code VARCHAR(256),
	Name NVARCHAR(256),
	Note NVARCHAR(256),
	EffectiveState VARCHAR(256),
	CreateDate DateTime,
);

CREATE TABLE OvertimeRate
(
    Id VARCHAR(32) PRIMARY KEY,	
	Day int,
	Night int,
	DayOff int,
	NightOff int,
	DayHoliday int,
	NightHoliday int,
	EffectiveState VARCHAR(256),
	CreateDate DateTime,
	Date DateTime,
);

CREATE TABLE TimekeepingShift
(
    Id VARCHAR(32) PRIMARY KEY,	
	Code VARCHAR(256),
	Name NVARCHAR(256),
	StartTime DateTime,
	EndTime DateTime,
	StartBreaksTime DateTime,
	EndBreaksTime DateTime,
	CreateDate DateTime,
	Duration decimal(8,3),
	TimekeepingTypeFull VARCHAR(256),
	TimekeepingTypeFirst VARCHAR(256),
	TimekeepingTypeSecond VARCHAR(256),
	TimekeepingTypeOff VARCHAR(256),
	EffectiveState VARCHAR(256),
	BreaksTimeType VARCHAR(256),
);


CREATE TABLE TimekeepingFormula
(
    Id VARCHAR(32) PRIMARY KEY,	
	Code VARCHAR(256),
	Name NVARCHAR(256),
	CountCode int,
	Formula NVARCHAR(1000),
	CreateDate DateTime,
);

CREATE TABLE Vacation
(
    Id VARCHAR(32) PRIMARY KEY,	
	UsersId VARCHAR(256),
	StartDate DateTime,
	EndDate DateTime,
	ApprovedDate DateTime,
	NumberOfDays decimal(8,3),
	Reason NVARCHAR(256),
	DisapprovedReason NVARCHAR(256),
	TimekeepingTypeId VARCHAR(256),
	ChooseBreak VARCHAR(256),
	ApprovalStatus VARCHAR(256),
	CreatorObject VARCHAR(256),
	CreateDate DateTime,
);

CREATE TABLE Overtime
(
    Id VARCHAR(32) PRIMARY KEY,	
	UsersId VARCHAR(256),
	RegisterDate DateTime,
	StartTime DateTime2,
	EndTime DateTime2,
	StartBreakTime DateTime2,
	EndBreakTime DateTime2,
	ApprovedDate DateTime,
	TotalHour decimal(8,3),
	DayHourAmount decimal(8,3),
	NightHourAmount decimal(8,3),
	Reason NVARCHAR(256),
	DisapprovedReason NVARCHAR(256),
	BreaksTimeType VARCHAR(256),
	ApprovalStatus VARCHAR(256),
	OvertimeType VARCHAR(256),
	CreatorObject VARCHAR(256),
	CreateDate DateTime,
);

CREATE TABLE TimekeepingExplanation
(
    Id VARCHAR(32) PRIMARY KEY,	
	UsersId VARCHAR(256),
	RegisterDate DateTime,
	StartTime DateTime2,
	EndTime DateTime2,
	ViolationType VARCHAR(256),
	ApprovedDate DateTime,
	Reason NVARCHAR(256),
	DisapprovedReason NVARCHAR(256),
	TimekeepingTypeId VARCHAR(256),
	ApprovalStatus VARCHAR(256),
	CreatorObject VARCHAR(256),
	CreateDate DateTime,
);

CREATE TABLE WorkShiftTable
(
    Id VARCHAR(32) PRIMARY KEY,	
	UsersId VARCHAR(256),
	Year int,
	Month int,
	Day01 VARCHAR(256),
	Day02 VARCHAR(256),
	Day03 VARCHAR(256),
	Day04 VARCHAR(256),
	Day05 VARCHAR(256),
	Day06 VARCHAR(256),
	Day07 VARCHAR(256),
	Day08 VARCHAR(256),
	Day09 VARCHAR(256),
	Day10 VARCHAR(256),
	Day11 VARCHAR(256),
	Day12 VARCHAR(256),
	Day13 VARCHAR(256),
	Day14 VARCHAR(256),
	Day15 VARCHAR(256),
	Day16 VARCHAR(256),
	Day17 VARCHAR(256),
	Day18 VARCHAR(256),
	Day19 VARCHAR(256),
	Day20 VARCHAR(256),
	Day21 VARCHAR(256),
	Day22 VARCHAR(256),
	Day23 VARCHAR(256),
	Day24 VARCHAR(256),
	Day25 VARCHAR(256),
	Day26 VARCHAR(256),
	Day27 VARCHAR(256),
	Day28 VARCHAR(256),
	Day29 VARCHAR(256),
	Day30 VARCHAR(256),
	Day31 VARCHAR(256),
);

CREATE TABLE FingerprintManagement
(
    Id VARCHAR(32) PRIMARY KEY,	
	UsersId VARCHAR(256),
	ScanDate DateTime,
	CreateDate DateTime,
	Year int,
	Month int,
);

CREATE TABLE OvertimeAggregate
(
    Id VARCHAR(32) PRIMARY KEY,	
	UsersId VARCHAR(256),
	RegisterDate DateTime,
	StartTime DateTime2,
	EndTime DateTime2,
	StartBreakTime DateTime2,
	EndBreakTime DateTime2,
	DayHourAmount decimal(8,3),
	DayHourCoefficientAmount decimal(8,3),
	NightHourAmount decimal(8,3),
	NightHourCoefficientAmount decimal(8,3),
	Total decimal(8,3),
	Year int, 
	Month int,
	Reason NVARCHAR(256),
	OvertimeType VARCHAR(256),
);

CREATE TABLE TimekeepingProcessing
(
    Id VARCHAR(32) PRIMARY KEY,	
	UsersId VARCHAR(256),
	Year int,
	Month int,
	Day01 VARCHAR(256),
	Day02 VARCHAR(256),
	Day03 VARCHAR(256),
	Day04 VARCHAR(256),
	Day05 VARCHAR(256),
	Day06 VARCHAR(256),
	Day07 VARCHAR(256),
	Day08 VARCHAR(256),
	Day09 VARCHAR(256),
	Day10 VARCHAR(256),
	Day11 VARCHAR(256),
	Day12 VARCHAR(256),
	Day13 VARCHAR(256),
	Day14 VARCHAR(256),
	Day15 VARCHAR(256),
	Day16 VARCHAR(256),
	Day17 VARCHAR(256),
	Day18 VARCHAR(256),
	Day19 VARCHAR(256),
	Day20 VARCHAR(256),
	Day21 VARCHAR(256),
	Day22 VARCHAR(256),
	Day23 VARCHAR(256),
	Day24 VARCHAR(256),
	Day25 VARCHAR(256),
	Day26 VARCHAR(256),
	Day27 VARCHAR(256),
	Day28 VARCHAR(256),
	Day29 VARCHAR(256),
	Day30 VARCHAR(256),
	Day31 VARCHAR(256),
);

CREATE TABLE SummaryOfTimekeeping
(
    Id VARCHAR(32) PRIMARY KEY,	
	UsersId VARCHAR(256),
	Year int, 
	Month int,
	OvertimeHour decimal(8,3),
	StandradDay int,
	WorkLateMinutes int,
	LeaveEarlyMinutes int,
	DataType VARCHAR(Max),
	DataFormula VARCHAR(Max),
);


INSERT INTO Users (Id, UserName, Password, IsAdmin, Name, Code, Gender, IdentityNumber, DateOfBirth, PhoneHouseholder, Email, Address, Salary)
VALUES 
('1', 'a', 'a', 1, N'Phạm Đình Chí Kiên', 'NV01', '1', '0352569877', '2000-01-01', '123456789', 'NV01@gmail.com', N'Hà Nội', 5000000);

INSERT INTO Users (Id, UserName, Password, IsAdmin, Name, Code, Gender, IdentityNumber, DateOfBirth, PhoneHouseholder, Email, Address, Salary)
VALUES 
('2', 'a2', 'a', 1, N'Phạm Quốc Nhật Anh', 'NV02', '1', '0352321231', '2001-02-02', '123456789', 'NV02@gmail.com', N'Hà Nội', 6000000);

INSERT INTO Users (Id, UserName, Password, IsAdmin, Name, Code, Gender, IdentityNumber, DateOfBirth, PhoneHouseholder, Email, Address, Salary)
VALUES 
('3', 'a3', 'a', 1, N'Nguyễn Quang Mạnh', 'NV03', '1', '0987654321', '2000-01-01', '123456789', 'NV03@gmail.com', N'Hà Nội', 7000000);

INSERT INTO Users (Id, UserName, Password, IsAdmin, Name, Code, Gender, IdentityNumber, DateOfBirth, PhoneHouseholder, Email, Address, Salary)
VALUES 
('4', 'a4', 'a', 1, N'Nguyễn Minh Triết', 'NV04', '1', '0987632112', '2000-01-01', '123456789', 'NV04@gmail.com', N'Hà Nội', 8000000);

INSERT INTO Users (Id, UserName, Password, IsAdmin, Name, Code, Gender, IdentityNumber, DateOfBirth, PhoneHouseholder, Email, Address, Salary)
VALUES 
('5', 'a5', 'a', 1, N'Lê Trần Trung Đức', 'NV05', '1', '5544818754', '2000-01-01', '123456789', 'NV05@gmail.com', N'Hà Nội', 9000000);

INSERT INTO Users (Id, UserName, Password, IsAdmin, Name, Code, Gender, IdentityNumber, DateOfBirth, PhoneHouseholder, Email, Address, Salary)
VALUES 
('6', 'a5', 'a', 1, N'Lê Trần Chí Kiên', 'NV06', '1', '5544881754', '2000-01-01', '123456789', 'NV06@gmail.com', N'Hà Nội', 10000000);

INSERT INTO Users (Id, UserName, Password, IsAdmin, Name, Code, Gender, IdentityNumber, DateOfBirth, PhoneHouseholder, Email, Address, Salary)
VALUES 
('7', 'a7', 'a', 1, N'Lê Đình Phúc Hưng', 'NV07', '1', '5544288754', '2000-01-01', '123456789', 'NV07@gmail.com', N'Hà Nội', 1000000);

INSERT INTO Users (Id, UserName, Password, IsAdmin, Name, Code, Gender, IdentityNumber, DateOfBirth, PhoneHouseholder, Email, Address, Salary)
VALUES 
('8', 'a8', 'a', 1, N'Trần Hưng Thịnh', 'NV08', '1', '5544488754', '2000-01-01', '123456789', 'NV08@gmail.com', N'Hà Nội', 2000000);

INSERT INTO Users (Id, UserName, Password, IsAdmin, Name, Code, Gender, IdentityNumber, DateOfBirth, PhoneHouseholder, Email, Address, Salary)
VALUES 
('9', 'a9', 'a', 1, N'Trần Thành Đạt', 'NV09', '1', '5544588754', '2000-01-01', '123456789', 'NV09@gmail.com', N'Hà Nội', 3000000);

INSERT INTO Users (Id, UserName, Password, IsAdmin, Name, Code, Gender, IdentityNumber, DateOfBirth, PhoneHouseholder, Email, Address, Salary)
VALUES 
('10', 'a10', 'a', 1, N'Bùi Tuấn Tú', 'NV10', '1', '5544188754', '2000-01-01', '123456789', 'NV10@gmail.com', N'Hà Nội', 4000000);

INSERT INTO Users (Id, UserName, Password, IsAdmin, Name, Code, Gender, IdentityNumber, DateOfBirth, PhoneHouseholder, Email, Address, Salary)
VALUES 
('11', 'a11', 'a', 1, N'Bùi Phúc Hưng', 'NV11', '1', '5544228754', '2000-01-01', '123456789', 'NV11@gmail.com', N'Hà Nội', 11000000);

INSERT INTO Users (Id, UserName, Password, IsAdmin, Name, Code, Gender, IdentityNumber, DateOfBirth, PhoneHouseholder, Email, Address, Salary)
VALUES 
('12', 'a12', 'a', 1, N'Đặng Anh Dũng', 'NV12', '1', '55448328754', '2000-01-01', '123456789', 'NV12@gmail.com', N'Hà Nội', 12000000);

INSERT INTO Users (Id, UserName, Password, IsAdmin, Name, Code, Gender, IdentityNumber, DateOfBirth, PhoneHouseholder, Email, Address, Salary)
VALUES 
('13', 'a13', 'a', 1, N'Huỳnh Trọng Nghĩa', 'NV13', '1', '5544123754', '2000-01-01', '123456789', 'NV13@gmail.com', N'Hà Nội', 13000000);

INSERT INTO Users (Id, UserName, Password, IsAdmin, Name, Code, Gender, IdentityNumber, DateOfBirth, PhoneHouseholder, Email, Address, Salary)
VALUES 
('14', 'a14', 'a', 1, N'Vũ Thái Sơn', 'NV14', '1', '5544123454', '2000-01-01', '123456789', 'NV14@gmail.com', N'Hà Nội', 14000000);

INSERT INTO Users (Id, UserName, Password, IsAdmin, Name, Code, Gender, IdentityNumber, DateOfBirth, PhoneHouseholder, Email, Address, Salary)
VALUES 
('15', 'a15', 'a', 1, N'Vũ Thiên Phú', 'NV15', '1', '5544432154', '2000-01-01', '123456789', 'NV15@gmail.com', N'Hà Nội', 15000000);

INSERT INTO OvertimeRate (Id, Day, Night, DayOff, NightOff, DayHoliday, NightHoliday, EffectiveState, CreateDate, Date)
VALUES
('1', 100, 150, 200, 250, 300, 350, '2', '2024-03-06 12:00:00', '2024-03-06');

INSERT INTO TimekeepingShift (Id, Code, Name, StartTime, EndTime, StartBreaksTime, EndBreaksTime, CreateDate, Duration, TimekeepingTypeFull, TimekeepingTypeFirst, TimekeepingTypeSecond, TimekeepingTypeOff, EffectiveState, BreaksTimeType)
VALUES
('1', 'HC', N'Hành chính', '2024-03-06 08:00:00', '2024-03-06 17:30:00', '2024-03-06 12:00:00', '2024-03-06 13:30:00', '2024-03-06 10:30:00', 8, 'X', 'X/KL', 'X/KL', 'KL', '2', '2');

INSERT INTO TimekeepingType (Id, Code, Name, Note, EffectiveState, CreateDate)
VALUES
('1', 'X', N'Làm cả ngày', 'SQL tạo', '2', '2024-03-06 12:00:00');

INSERT INTO TimekeepingType (Id, Code, Name, Note, EffectiveState, CreateDate)
VALUES
('2', 'KL', N'Nghỉ không lương', 'SQL tạo', '2', '2024-03-06 12:00:00');

INSERT INTO TimekeepingType (Id, Code, Name, Note, EffectiveState, CreateDate)
VALUES
('3', 'X/KL', N'Nửa ngày làm, nửa ngày nghỉ không lương', 'SQL tạo', '2', '2024-03-06 12:00:00');

INSERT INTO TimekeepingType (Id, Code, Name, Note, EffectiveState, CreateDate)
VALUES
('4', 'X/P', N'Nửa ngày làm, nửa ngày nghỉ phép', 'SQL tạo', '2', '2024-03-06 12:00:00');