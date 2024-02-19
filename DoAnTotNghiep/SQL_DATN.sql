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
	Name VARCHAR(256),
	PathAttachFile VARCHAR(256),
	AttachFileName VARCHAR(256),
	Amount int,
	EffectiveState VARCHAR(256),
	Reason NVARCHAR(256),
	CreateDate DateTime,
	BonusDisciplineType VARCHAR(255),
	Date DateTime,
);