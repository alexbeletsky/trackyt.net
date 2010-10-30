CREATE TABLE BlogPosts
(
	Id Integer not NULL IDENTITY (1, 1) PRIMARY KEY,
	[Url] Nvarchar(MAX) not NULL,
	[Title] Nvarchar(MAX) not NULL,
	[Body] Nvarchar(MAX) not NULL,
	[CreatedDate] DATETIME not NULL,
	[CreatedBy] Nvarchar(MAX) not NULL,
	[Timestamp] Timestamp 
		
)