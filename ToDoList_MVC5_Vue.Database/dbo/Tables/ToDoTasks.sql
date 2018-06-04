CREATE TABLE [dbo].[ToDoTasks] (
    [Id]         UNIQUEIDENTIFIER CONSTRAINT [DF_ToDoTasks_Id] DEFAULT (newsequentialid()) NOT NULL,
    [ListId]     UNIQUEIDENTIFIER NOT NULL,
    [Name]       NVARCHAR (MAX)   NOT NULL,
    [Sort]       INT              NOT NULL,
    [Done]       BIT              NOT NULL,
    [DoneTime]   DATETIME         NULL,
    [IsStarred]  BIT              NOT NULL,
    [StarTime]   DATETIME         NULL,
    [CreateTime] DATETIME         CONSTRAINT [DF_ToDoTasks_CreateTime] DEFAULT (getdate()) NOT NULL,
    [ExpiryDate] DATE             NULL,
    [RemindTime] DATETIME         NULL,
    [Note]       NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_ToDoTasks] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ToDoTasks_ToDoList] FOREIGN KEY ([ListId]) REFERENCES [dbo].[ToDoLists] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'筆記', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ToDoTasks', @level2type = N'COLUMN', @level2name = N'Note';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'提醒時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ToDoTasks', @level2type = N'COLUMN', @level2name = N'RemindTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'到期日', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ToDoTasks', @level2type = N'COLUMN', @level2name = N'ExpiryDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ToDoTasks', @level2type = N'COLUMN', @level2name = N'CreateTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'標為星號的時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ToDoTasks', @level2type = N'COLUMN', @level2name = N'StarTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'標為星號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ToDoTasks', @level2type = N'COLUMN', @level2name = N'IsStarred';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'完成時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ToDoTasks', @level2type = N'COLUMN', @level2name = N'DoneTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否已完成', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ToDoTasks', @level2type = N'COLUMN', @level2name = N'Done';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排序', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ToDoTasks', @level2type = N'COLUMN', @level2name = N'Sort';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'事項名稱', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ToDoTasks', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'清單Id', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ToDoTasks', @level2type = N'COLUMN', @level2name = N'ListId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'PK_GUID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ToDoTasks', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'待辦事項資料表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ToDoTasks';

