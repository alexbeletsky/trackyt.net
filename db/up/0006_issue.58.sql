ALTER TABLE [Tasks] 
ADD Notes nvarchar(MAX);

ALTER TABLE [Tasks]
ADD PlannedDate datetime2;

ALTER TABLE [Tasks]
ADD PlannedEffort Integer;