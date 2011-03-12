USE $(Database);
GO
BACKUP DATABASE $(Database)
TO DISK = 'C:\backup\$(Database).bak'
   WITH FORMAT,
      MEDIANAME = 'C_SQLServerBackups',
      NAME = 'Full Backup of $(Database)';
GO