CREATE TABLE [dbo].[Scores] (
    [Id]    INT           NOT NULL,
    [Name]  NVARCHAR (50) NOT NULL,
    [score] INT           NOT NULL,
    CONSTRAINT [PK_Scores] PRIMARY KEY CLUSTERED ([Id] ASC)
);

