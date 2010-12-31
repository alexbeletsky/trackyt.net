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


UPDATE [Tasks] SET [ActualWork]=0 WHERE [ActualWork] IS NULL;

ALTER TABLE [Tasks]
ALTER COLUMN [ActualWork] Integer NOT NULL;

UPDATE [Tasks] SET [Status]=0 WHERE [Status] IS NULL;

ALTER TABLE [Tasks]
ALTER COLUMN [Status] Integer NOT NULL;

DELETE t from [Tasks] as t
INNER JOIN Users as u on u.Temp=1
WHERE t.UserId = u.Id;
DELETE from [Users] where Temp=1;