/*
Скрипт развертывания для master

Этот код был создан программным средством.
Изменения, внесенные в этот файл, могут привести к неверному выполнению кода и будут потеряны
в случае его повторного формирования.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "master"
:setvar DefaultFilePrefix "master"
:setvar DefaultDataPath "C:\Users\tomka\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"
:setvar DefaultLogPath "C:\Users\tomka\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"

GO
:on error exit
GO
/*
Проверьте режим SQLCMD и отключите выполнение скрипта, если режим SQLCMD не поддерживается.
Чтобы повторно включить скрипт после включения режима SQLCMD выполните следующую инструкцию:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'Для успешного выполнения этого скрипта должен быть включен режим SQLCMD.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Выполняется создание [dbo].[Employees]...';


GO
CREATE TABLE [dbo].[Employees] (
    [Id]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [FirstName]    NVARCHAR (50)  NOT NULL,
    [LastName]     NVARCHAR (50)  NOT NULL,
    [Email]        NVARCHAR (MAX) NOT NULL,
    [IsActive]     BIT            NOT NULL,
    [CreatedBy]    NVARCHAR (50)  NULL,
    [DateCreated]  DATETIME       NOT NULL,
    [ModifiedBy]   NVARCHAR (50)  NULL,
    [DateModified] DATETIME       NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Выполняется создание [dbo].[Employees_Skills]...';


GO
CREATE TABLE [dbo].[Employees_Skills] (
    [Id]          BIGINT NOT NULL,
    [Employee_Id] BIGINT NOT NULL,
    [Skill_Id]    INT    NOT NULL,
    [Score_Id]    INT    NOT NULL,
    CONSTRAINT [PK_Employees_Skills] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Выполняется создание [dbo].[Roles]...';


GO
CREATE TABLE [dbo].[Roles] (
    [Id]           INT            NOT NULL,
    [Name]         NVARCHAR (MAX) NOT NULL,
    [CreatedBy]    NVARCHAR (50)  NULL,
    [DateCreated]  DATETIME       NOT NULL,
    [ModifiedBy]   NVARCHAR (50)  NULL,
    [DateModified] DATETIME       NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Выполняется создание [dbo].[Scores]...';


GO
CREATE TABLE [dbo].[Scores] (
    [Id]    INT           NOT NULL,
    [Name]  NVARCHAR (50) NOT NULL,
    [score] INT           NOT NULL,
    CONSTRAINT [PK_Scores] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Выполняется создание [dbo].[Skills]...';


GO
CREATE TABLE [dbo].[Skills] (
    [Id]                     INT            NOT NULL,
    [Name]                   NVARCHAR (MAX) NOT NULL,
    [SkillParentCategory_Id] INT            NULL,
    CONSTRAINT [PK_Skills] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Выполняется создание [dbo].[Users]...';


GO
CREATE TABLE [dbo].[Users] (
    [Id]           BIGINT         NOT NULL,
    [UserName]     NVARCHAR (50)  NOT NULL,
    [Password]     NVARCHAR (MAX) NOT NULL,
    [IsAdmin]      BIT            NOT NULL,
    [Employee_Id]  BIGINT         NOT NULL,
    [CreatedBy]    NVARCHAR (50)  NULL,
    [DateCreated]  DATETIME       NOT NULL,
    [ModifiedBy]   NVARCHAR (50)  NULL,
    [DateModified] DATETIME       NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Выполняется создание [dbo].[Users_Roles]...';


GO
CREATE TABLE [dbo].[Users_Roles] (
    [Id]      INT NOT NULL,
    [User_Id] INT NOT NULL,
    [Role_Id] INT NOT NULL,
    CONSTRAINT [PK_Users_Roles] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Выполняется создание [dbo].[DF_Employees_IsActive]...';


GO
ALTER TABLE [dbo].[Employees]
    ADD CONSTRAINT [DF_Employees_IsActive] DEFAULT ((1)) FOR [IsActive];


GO
PRINT N'Выполняется создание [dbo].[DF_Employees_DateCreated]...';


GO
ALTER TABLE [dbo].[Employees]
    ADD CONSTRAINT [DF_Employees_DateCreated] DEFAULT (getdate()) FOR [DateCreated];


GO
PRINT N'Выполняется создание [dbo].[DF_Roles_DateCreated]...';


GO
ALTER TABLE [dbo].[Roles]
    ADD CONSTRAINT [DF_Roles_DateCreated] DEFAULT (getdate()) FOR [DateCreated];


GO
PRINT N'Выполняется создание [dbo].[DF_Users_IsAdmin]...';


GO
ALTER TABLE [dbo].[Users]
    ADD CONSTRAINT [DF_Users_IsAdmin] DEFAULT ((1)) FOR [IsAdmin];


GO
PRINT N'Выполняется создание [dbo].[DF_Users_DateCreated]...';


GO
ALTER TABLE [dbo].[Users]
    ADD CONSTRAINT [DF_Users_DateCreated] DEFAULT (getdate()) FOR [DateCreated];


GO
PRINT N'Выполняется создание [dbo].[SP_DeleteRecordFromTable]...';


GO
-- =============================================
-- Author:		Yurii Tomka
-- Description:	Generic stored procedure to delete specified record from specified table
-- =============================================
CREATE PROCEDURE [dbo].[SP_DeleteRecordFromTable]
	-- Add the parameters for the stored procedure here
	@P_tableName nvarchar(50) = null,
	@P_Id int = null
AS
BEGIN
	SET NOCOUNT ON;
	
	declare @V_table nvarchar(50) = null
	if (@P_tableName is not null)
		select @V_table = QUOTENAME( TABLE_NAME )
		FROM INFORMATION_SCHEMA.TABLES
		WHERE TABLE_NAME = @P_tableName

	declare @V_sql as nvarchar(MAX) = null
	if (@V_table is not null and @P_Id is not null)
		select @V_sql = 'delete from ' + @V_table + 'where Id = ' + @P_Id + '; select 1;'

	if(@V_sql is not null)
		exec(@V_sql)
	else
		select 0;
END
GO
PRINT N'Выполняется создание [dbo].[SP_GetAllEmployeeSkillsByEmployeeId]...';


GO
-- =============================================
-- Author:		Yurii Tomka
-- Description:	stored procedure to get all records from Employees_Skills table by employee id
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAllEmployeeSkillsByEmployeeId]
	-- Add the parameters for the stored procedure here
	@P_Id bigint = null
AS
BEGIN
	SET NOCOUNT ON;
	
	Declare @V_sql as nvarchar(MAX) = null
	if (@P_Id is not null)
		select @V_sql = 'select * from Employees_Skills where Employee_Id = ' + @P_Id + ';'

	if(@V_sql is not null)
		exec(@V_sql)
	else
		select -1;
END
GO
PRINT N'Выполняется создание [dbo].[SP_GetAllRecordsFromTable]...';


GO
-- =============================================
-- Author:		Yurii Tomka
-- Description:	Generic stored procedure to get all records from specified table
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAllRecordsFromTable]
	-- Add the parameters for the stored procedure here
	@P_tableName nvarchar(50) = null
AS
BEGIN
	SET NOCOUNT ON;
	
	declare @V_table nvarchar(50) = null
	if (@P_tableName is not null)
		select @V_table = QUOTENAME( TABLE_NAME )
		FROM INFORMATION_SCHEMA.TABLES
		WHERE TABLE_NAME = @P_tableName

	declare @V_sql as nvarchar(MAX) = null
	if (@V_table is not null)
		select @V_sql = 'select * from ' + @V_table + ';'

	if(@V_sql is not null)
		exec(@V_sql)
	else
		select -1;
END
GO
PRINT N'Выполняется создание [dbo].[SP_GetRecordByIdFromTable]...';


GO
-- =============================================
-- Author:		Yurii Tomka
-- Description:	Generic stored procedure to get record from specified table
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetRecordByIdFromTable]
	-- Add the parameters for the stored procedure here
	@P_tableName nvarchar(50) = null,
	@P_Id bigint = null
AS
BEGIN
	SET NOCOUNT ON;
	
	declare @V_table nvarchar(50) = null
	if (@P_tableName is not null)
		select @V_table = QUOTENAME( TABLE_NAME )
		FROM INFORMATION_SCHEMA.TABLES
		WHERE TABLE_NAME = @P_tableName

	declare @V_sql as nvarchar(MAX) = null
	if (@V_table is not null and @P_Id is not null)
		select @V_sql = 'select * from ' + @V_table + ' where Id = ' + @P_Id + ';'

	if(@V_sql is not null)
		exec(@V_sql)
	else
		select -1;
END
GO
PRINT N'Выполняется создание [dbo].[SP_InsertRecordToTable]...';


GO
-- =============================================
-- Author:		Yurii Tomka
-- Description:	Generic stored procedure to insert new record to specified table
-- =============================================
CREATE PROCEDURE [dbo].[SP_InsertRecordToTable]
	-- Add the parameters for the stored procedure here
	@P_tableName nvarchar(50) = null,
	@P_columnsString nvarchar(MAX) = null, 
	@P_propertiesString nvarchar(MAX) = null
AS
BEGIN
	SET NOCOUNT ON;
	
	declare @V_table nvarchar(50) = null
	if (@P_tableName is not null)
		select @V_table = QUOTENAME( TABLE_NAME )
		FROM INFORMATION_SCHEMA.TABLES
		WHERE TABLE_NAME = @P_tableName

	declare @V_sql as nvarchar(MAX) = null
	if (@V_table is not null and @P_columnsString is not null and @P_propertiesString is not null)
		select @V_sql = 'insert into ' + @V_table + ' (' + @P_columnsString + ') values (' + @P_propertiesString + ');
		 select cast(SCOPE_IDENTITY() as int);'

	if(@V_sql is not null)
		exec(@V_sql)
	else
		select -1;
END
GO
PRINT N'Выполняется создание [dbo].[SP_UnActivateRecordInTable]...';


GO
-- =============================================
-- Author:		Yurii Tomka
-- Description:	Generic stored procedure to enable soft delete in specified table
-- =============================================
CREATE PROCEDURE [dbo].[SP_UnActivateRecordInTable]
	-- Add the parameters for the stored procedure here
	@P_tableName nvarchar(50) = null,
	@P_columnsString nvarchar(MAX) = null,
	@P_Id bigint = null
AS
BEGIN
	SET NOCOUNT ON;
	
	declare @V_table nvarchar(50) = null
	if (@P_tableName is not null)
		select @V_table = QUOTENAME( TABLE_NAME )
		FROM INFORMATION_SCHEMA.TABLES
		WHERE TABLE_NAME = @P_tableName

	declare @V_sql as nvarchar(MAX) = null
	if (@V_table is not null and @P_columnsString is not null and @P_Id is not null)
		select @V_sql = 'update ' + @V_table + ' set ' + @P_columnsString + ' where Id = ' + @P_Id + ';'

	if(@V_sql is not null)
		exec(@V_sql)
END
GO
PRINT N'Выполняется создание [dbo].[SP_UpdateRecordInTable]...';


GO
-- =============================================
-- Author:		Yurii Tomka
-- Description:	Generic stored procedure to update existing specified record in specified table
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateRecordInTable]
	-- Add the parameters for the stored procedure here
	@P_tableName nvarchar(50) = null,
	@P_columnsString nvarchar(MAX) = null,
	@P_Id bigint = null
AS
BEGIN
	SET NOCOUNT ON;
	
	declare @V_table nvarchar(50) = null
	if (@P_tableName is not null)
		select @V_table = QUOTENAME( TABLE_NAME )
		FROM INFORMATION_SCHEMA.TABLES
		WHERE TABLE_NAME = @P_tableName

	declare @V_sql as nvarchar(MAX) = null
	if (@V_table is not null and @P_columnsString is not null and @P_Id is not null)
		select @V_sql = 'update ' + @V_table + ' set ' + @P_columnsString + ' where Id = ' + @P_Id + '; select 1;'

	if(@V_sql is not null)
		exec(@V_sql)
	else
		select -1;
END
GO
PRINT N'Обновление завершено.';


GO
