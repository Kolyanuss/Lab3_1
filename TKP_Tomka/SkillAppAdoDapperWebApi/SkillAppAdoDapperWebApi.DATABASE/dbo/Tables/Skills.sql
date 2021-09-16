CREATE TABLE [dbo].[Skills] (
    [Id]                     INT            NOT NULL,
    [Name]                   NVARCHAR (MAX) NOT NULL,
    [SkillParentCategory_Id] INT            NULL,
    CONSTRAINT [PK_Skills] PRIMARY KEY CLUSTERED ([Id] ASC)
);



