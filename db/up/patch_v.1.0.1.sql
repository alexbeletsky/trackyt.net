CREATE TABLE Credentials
(
	Id Integer Not NULL Identity (1, 1) PRIMARY KEY,
	[Account] nvarchar(MAX), 	
	[Email] nvarchar(MAX),
	[Password] nvarchar(MAX),
);

ALTER TABLE [Users]
ADD PasswordHash nvarchar(32)
