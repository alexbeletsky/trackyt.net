CREATE TABLE Tasks
(
	Id Integer Not NULL Identity (1, 1) PRIMARY KEY,
	[UserId] Integer Not NULL References Users(Id),
	[Number] Integer,
	[Description] Nvarchar(MAX),
	[Status] Integer,
	[ActualWork] Integer,
	[Timestamp] timestamp 
)