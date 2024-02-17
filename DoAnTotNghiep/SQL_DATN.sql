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