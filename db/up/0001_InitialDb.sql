CREATE TABLE Users
(
	Id Integer Not NULL Identity (1, 1) PRIMARY KEY,
	[Email] nvarchar(MAX),
	[Password] nvarchar(MAX),
	[Temp] Bit Not Null DEFAULT ('0'),
	[Timestamp] timestamp 
);
CREATE TABLE Tasks
(
	Id Integer Not NULL Identity (1, 1) PRIMARY KEY,
	[UserId] Integer Not NULL References Users(Id),
	[Number] Integer,
	[Description] Nvarchar(MAX),
	[Status] Integer,
	[ActualWork] Integer,
	[Timestamp] timestamp 
);
CREATE TABLE BlogPosts
(
	Id Integer not NULL IDENTITY (1, 1) PRIMARY KEY,
	[Url] Nvarchar(MAX) not NULL,
	[Title] Nvarchar(MAX) not NULL,
	[Body] Nvarchar(MAX) not NULL,
	[CreatedDate] DATETIME not NULL,
	[CreatedBy] Nvarchar(MAX) not NULL,
	[Timestamp] Timestamp 
		
);
