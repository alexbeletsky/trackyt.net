UPDATE [Tasks] SET [Status]=0 WHERE [Status] IS NULL;

ALTER TABLE [Tasks]
ALTER COLUMN [Status] Integer NOT NULL;