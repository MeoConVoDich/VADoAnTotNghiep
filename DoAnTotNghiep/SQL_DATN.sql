Create database VAHRM

CREATE TABLE Users
(
    Id VARCHAR(32) PRIMARY KEY,
    UserName VARCHAR(256),
    Password VARCHAR(256),
    IsAdmin bit,
	Permission VARCHAR(max),
	PermissionGroup VARCHAR(max),
);
ALTER TABLE Users ADD Name NVARCHAR(255);
ALTER TABLE Users ADD Code VARCHAR(255);
ALTER TABLE Users ADD Gender VARCHAR(255);
ALTER TABLE Users ADD IdentityNumber VARCHAR(255);
ALTER TABLE Users ADD DateOfBirth Datetime;
ALTER TABLE Users ADD PhoneHouseholder VARCHAR(255);
ALTER TABLE Users ADD Email VARCHAR(255);
ALTER TABLE Users ADD Address NVARCHAR(255);
ALTER TABLE Users ADD Salary int;
ALTER TABLE Users ADD Avatar VARCHAR(255);


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