CREATE TABLE [dbo].[Employees_Skills] (
    [Id]          INT NOT NULL,
    [Employee_Id] INT NOT NULL,
    [Skill_Id]    INT    NOT NULL,
    [Score_Id]    INT    NOT NULL,
    CONSTRAINT [PK_Employees_Skills] PRIMARY KEY CLUSTERED ([Id] ASC)
);

