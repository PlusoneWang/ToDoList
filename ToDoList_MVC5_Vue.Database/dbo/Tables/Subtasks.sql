CREATE TABLE [dbo].[Subtasks] (
    [Id]     UNIQUEIDENTIFIER CONSTRAINT [DF_Subtasks_Id] DEFAULT (newsequentialid()) NOT NULL,
    [TaskId] UNIQUEIDENTIFIER NOT NULL,
    [Name]   NVARCHAR (MAX)   NOT NULL,
    [Sort]   INT              NOT NULL,
    CONSTRAINT [PK_Subtasks] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Subtasks_ToDoTask] FOREIGN KEY ([TaskId]) REFERENCES [dbo].[ToDoTasks] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排序', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Subtasks', @level2type = N'COLUMN', @level2name = N'Sort';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'任務名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Subtasks', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'待辦事項Id', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Subtasks', @level2type = N'COLUMN', @level2name = N'TaskId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'PK_GUID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Subtasks', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'子任務資料表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Subtasks';

