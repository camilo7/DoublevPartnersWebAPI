
CREATE DATABASE DbDoubleVPartners;
go

Use DbDoubleVPartners;

create table [People]
(
	[Id] int identity(1,1) not null primary key,
	[Names] varchar(30),
	[LastNames] varchar(30),
	[IdentificationNumber] int,
	[Email] varchar(30),
	[IdentificationType] varchar(10),
	[CreationDate] datetime,
	[Identification] AS (CONVERT(VARCHAR(20), [IdentificationNumber]) + ' ' + [IdentificationType]),
    [FullName] AS ([Names] + ' ' + [LastNames])
);

create table [User]
(
	[Id] int identity(1,1) not null primary key,
	[UserName] varchar(30),
	[Pass] varchar(64),
	[CreationDate] datetime
);

USE DbDoubleVPartners;
GO

CREATE PROCEDURE GetPeople
AS
BEGIN
	SELECT [Id],[Names], [LastNames], [IdentificationNumber], [Email], [IdentificationType], [CreationDate], [Identification], [FullName]  FROM [People]
END;
