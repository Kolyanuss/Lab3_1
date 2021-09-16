CREATE TABLE [dbo].[Users_Roles] (
    [Id]      INT NOT NULL,
    [User_Id] INT NOT NULL,
    [Role_Id] INT NOT NULL,
    CONSTRAINT [PK_Users_Roles] PRIMARY KEY CLUSTERED ([Id] ASC)
);

