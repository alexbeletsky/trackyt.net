ALTER TABLE [Users]
DROP COLUMN Password
ALTER TABLE [Users]
ADD ApiToken nvarchar(32)