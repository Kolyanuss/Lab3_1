CREATE TABLE [dbo].[Users] (
    [Id]           BIGINT         NOT NULL,
    [UserName]     NVARCHAR (50)  NOT NULL,
    [Password]     NVARCHAR (MAX) NOT NULL,
    [IsAdmin]      BIT            CONSTRAINT [DF_Users_IsAdmin] DEFAULT ((1)) NOT NULL,
    [Employee_Id]  BIGINT         NOT NULL,
    [CreatedBy]    NVARCHAR (50)  NULL,
    [DateCreated]  DATETIME       CONSTRAINT [DF_Users_DateCreated] DEFAULT (getdate()) NOT NULL,
    [ModifiedBy]   NVARCHAR (50)  NULL,
    [DateModified] DATETIME       NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);

