SET NOCOUNT ON
DECLARE @FKName NVARCHAR(128)
DECLARE @FKColumnName NVARCHAR(128)
DECLARE @PKColumnName NVARCHAR(128)
DECLARE @fTableName NVARCHAR(128)
DECLARE @fUpdateRule INT
DECLARE @fDeleteRule INT
DECLARE @FieldNames NVARCHAR(500)
DECLARE @SQL NVARCHAR(4000)
CREATE TABLE #Temp(
                PKTABLE_QUALIFIER NVARCHAR(128),
                PKTABLE_OWNER NVARCHAR(128),
                PKTABLE_NAME NVARCHAR(128),
                PKCOLUMN_NAME NVARCHAR(128),
                FKTABLE_QUALIFIER NVARCHAR(128),
                FKTABLE_OWNER NVARCHAR(128),
                FKTABLE_NAME NVARCHAR(128),
                FKCOLUMN_NAME NVARCHAR(128),
                KEY_SEQ INT,
                UPDATE_RULE INT,
                DELETE_RULE INT,
                FK_NAME NVARCHAR(128),
                PK_NAME NVARCHAR(128),
                DEFERRABILITY INT)
DECLARE TTableNames CURSOR FOR
SELECT name
    FROM sysobjects
    WHERE xtype = 'U'
OPEN TTableNames
            FETCH NEXT
    FROM TTableNames
            INTO @fTableName
WHILE @@FETCH_STATUS = 0
    BEGIN
    INSERT #Temp
        EXEC dbo.sp_fkeys @fTableName
                FETCH NEXT
        FROM TTableNames
                INTO @fTableName
    END
            CLOSE TTableNames
            DEALLOCATE TTableNames
SET @FieldNames = ''
SET @fTableName = ''
SELECT DISTINCT FK_NAME AS FKName,FKTABLE_NAME AS FTName,
    @FieldNames AS FTFields,PKTABLE_NAME AS STName,
    @FieldNames AS STFields,@FieldNames AS FKType
            INTO #Temp1
    FROM #Temp
    ORDER BY FK_NAME,FKTABLE_NAME,PKTABLE_NAME
DECLARE FK_CUSROR CURSOR FOR
SELECT FKName
    FROM #Temp1
OPEN FK_CUSROR
            FETCH
    FROM FK_CUSROR INTO @FKName
WHILE @@FETCH_STATUS = 0
    BEGIN
    DECLARE FK_FIELDS_CUSROR CURSOR FOR
    SELECT FKCOLUMN_NAME,PKCOLUMN_NAME,UPDATE_RULE,DELETE_RULE
        FROM #TEMP
        WHERE FK_NAME = @FKName
        ORDER BY KEY_SEQ
    OPEN FK_FIELDS_CUSROR
                FETCH
        FROM FK_FIELDS_CUSROR INTO @FKColumnName,@PKColumnName,
        @fUpdateRule,@fDeleteRule
    WHILE @@FETCH_STATUS = 0
        BEGIN
        UPDATE #Temp1 SET FTFields =  CASE WHEN  LEN(FTFields)
                = 0 THEN   '['+@FKColumnName+']'
            ELSE FTFields
                        +',['+@FKColumnName+']' END
            WHERE FKName = @FKName
        UPDATE #Temp1 SET STFields =  CASE WHEN  LEN(STFields)
                = 0 THEN   '['+@PKColumnName+']'
            ELSE STFields
                        +',['+@PKColumnName+']' END
            WHERE FKName = @FKName
                    FETCH NEXT
            FROM FK_FIELDS_CUSROR INTO @FKColumnName,@PKColumnName,
            @fUpdateRule,@fDeleteRule
        END
    UPDATE #Temp1 SET FKType = CASE WHEN  @fUpdateRule = 0
                    THEN   FKType + ' ON UPDATE CASCADE'
        ELSE FKType END
        WHERE FKName = @FKName
    UPDATE #Temp1 SET FKType = CASE WHEN  @fDeleteRule = 0
                    THEN   FKType + ' ON DELETE CASCADE'
        ELSE FKType END
        WHERE FKName = @FKName
                CLOSE FK_FIELDS_CUSROR
                DEALLOCATE FK_FIELDS_CUSROR
                FETCH next
        FROM FK_CUSROR INTO @FKName
    END
            CLOSE FK_CUSROR
            DEALLOCATE FK_CUSROR

    DECLARE FK_SQL CURSOR FOR
		SELECT 'EXEC sp_rename ''' + FKName + ''', ''FK_' + replace(replace(replace(replace(FTName, '$', ''), ' ', ''), '[', ''), ']', '') + '_' + replace(replace(replace(replace(FTFields, '$', ''), ' ', ''), '[', ''), ']', '') + '_' + replace(replace(replace(replace(STName, '$', ''), ' ', ''), '[', ''), ']', '') + '_' + replace(replace(replace(replace(STFields, '$', ''), ' ', ''), '[', ''), ']', '') + ''', ''OBJECT'''
		FROM #Temp1
	OPEN FK_SQL
            FETCH
    FROM FK_SQL INTO @SQL
WHILE @@FETCH_STATUS = 0
    BEGIN
		exec sp_executesql @SQL
                FETCH next
        FROM FK_SQL INTO @SQL

	END
CLOSE FK_SQL
DEALLOCATE FK_SQL

drop table #Temp
drop table #Temp1





DECLARE @PKName NVARCHAR(128)
DECLARE @PKTableName NVARCHAR(128)
CREATE TABLE #Temp2(
                TABLE_QUALIFIER NVARCHAR(255),
                TABLE_OWNER NVARCHAR(255),
	              PK_TABLE_NAME NVARCHAR(128),
                PK_COLUMN_NAME NVARCHAR(128),
                KEY_SEQ INT,
                PK_NAME NVARCHAR(128))

DECLARE PKTableNames CURSOR FOR
SELECT name
    FROM sysobjects
    WHERE xtype = 'U'
OPEN PKTableNames
            FETCH NEXT
    FROM PKTableNames
            INTO @PKTableName
WHILE @@FETCH_STATUS = 0
    BEGIN
    INSERT #Temp2
        EXEC dbo.sp_pkeys @PKTableName
                FETCH NEXT
        FROM PKTableNames
                INTO @PKTableName
    END
            CLOSE PKTableNames
            DEALLOCATE PKTableNames

    DECLARE PK_SQL CURSOR FOR
		SELECT 'EXEC sp_rename ''' + PK_Name + ''', ''PK_' + replace(replace(replace(replace(PK_TABLE_NAME, '$', ''), ' ', ''), '[', ''), ']', '') + ''', ''OBJECT'''
		FROM #Temp2
	OPEN PK_SQL
            FETCH
    FROM PK_SQL INTO @SQL
WHILE @@FETCH_STATUS = 0
    BEGIN
		exec sp_executesql @SQL
                FETCH next
        FROM PK_SQL INTO @SQL

	END
CLOSE PK_SQL
DEALLOCATE PK_SQL

drop table #Temp2

SET NOCOUNT OFF