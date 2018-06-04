CREATE TABLE [dbo].[ToDoLists] (
    [Id]          UNIQUEIDENTIFIER CONSTRAINT [DF_ToDoLists_Id] DEFAULT (newsequentialid()) NOT NULL,
    [Name]        NVARCHAR (MAX)   NOT NULL,
    [Sort]        INT              NOT NULL,
    [FolderId]    UNIQUEIDENTIFIER NULL,
    [TaskOrderBy] INT              CONSTRAINT [DF_ToDoLists_ItemOrderBy] DEFAULT ((0)) NOT NULL,
    [UserId]      UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ToDoLists] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ToDoLists_Folder] FOREIGN KEY ([FolderId]) REFERENCES [dbo].[Folders] ([Id]),
    CONSTRAINT [FK_ToDoLists_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'清單內待辦事項的排序方式', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ToDoLists', @level2type = N'COLUMN', @level2name = N'TaskOrderBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'資料夾Id', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ToDoLists', @level2type = N'COLUMN', @level2name = N'FolderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排序', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ToDoLists', @level2type = N'COLUMN', @level2name = N'Sort';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'清單名稱', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ToDoLists', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'PK_GUID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ToDoLists', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'待辦清單資料表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ToDoLists';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'擁有者Id', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ToDoLists', @level2type = N'COLUMN', @level2name = N'UserId';

