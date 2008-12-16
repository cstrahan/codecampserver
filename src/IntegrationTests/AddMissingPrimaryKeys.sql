SET NOCOUNT ON

DECLARE @SQL NVARCHAR(4000)
DECLARE @TableName NVARCHAR(4000)

CREATE TABLE #Tables(TableName NVARCHAR(128))

DECLARE TTableNames CURSOR FOR
  select so.name from sysobjects so where so.id not in (select parent_object_id from sys.key_constraints) and so.type = 'U'

OPEN TTableNames FETCH NEXT FROM TTableNames INTO @TableName WHILE @@FETCH_STATUS = 0 BEGIN
  SELECT @SQL = 'alter table ' + @TableName + ' ADD [' + replace(@TableName, '$', '') + ' Identifier] int identity not null PRIMARY KEY'

        exec sp_executesql @SQL
                FETCH next
        FROM TTableNames INTO @TableName

	END
CLOSE TTableNames
DEALLOCATE TTableNames

drop table #Tables

SET NOCOUNT OFF