UPDATE [Tasks] SET [ActualWork]=0 WHERE [ActualWork] IS NULL;

ALTER TABLE [Tasks]
ALTER COLUMN [ActualWork] Integer NOT NULL;