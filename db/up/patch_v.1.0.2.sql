ALTER TABLE [Users]
DROP COLUMN Password;

ALTER TABLE [Users]
ADD ApiToken nvarchar(32);

ALTER Table [Tasks]
ADD CreatedDate datetime2;

ALTER Table [Tasks]
ADD StartedDate datetime2;

ALTER Table [Tasks]
ADD StoppedDate datetime2;


