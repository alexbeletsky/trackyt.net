CREATE TABLE Users
(
	Id Integer Not NULL Identity (1, 1) PRIMARY KEY,
	[Email] nvarchar(MAX),
	[Password] nvarchar(MAX),
	[Temp] Bit Not Null DEFAULT ('0'),
	[Timestamp] timestamp 
)