CREATE TABLE [dbo].[users] (
    [user_id] INT            NOT NULL,
    [name]    NVARCHAR (100) NULL,
    [email]   NVARCHAR (100) NULL,
    [password] NVARCHAR(25) NULL, 
	[connection_id] UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([user_id] ASC)
);

CREATE TABLE [dbo].[endusers] (
    [end_user_id] INT            NOT NULL,
    [name]    NVARCHAR (100) NULL,
    [email]   NVARCHAR (100) NULL,
	[connection_id] UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([end_user_id] ASC)
);

CREATE TABLE [dbo].[chatsessions] (
    [chat_session_id] INT NOT NULL, 
    [end_user_id]  INT           NOT NULL,
    [custom_value] NVARCHAR (25) NULL,
    [start_time]   DATETIME      NULL,
    [end_time]     DATETIME      NULL,
    [mode]    INT				NULL, -- 1: Waiting, 2: Connected, 3: Ended
    CONSTRAINT [FK_chatconnection_endusers] FOREIGN KEY ([end_user_id]) REFERENCES [dbo].[endusers] ([end_user_id]), 
    CONSTRAINT [PK_chatsessions] PRIMARY KEY ([chat_session_id])
);


CREATE TABLE [dbo].[chatmessages]
(
	[chat_session_id] INT NOT NULL,
	[user_id] INT NOT NULL,
    [initiator] INT NOT NULL, [message] NVARCHAR(1024) NOT NULL, -- 0: System, 1: user, 2: end user
    [timestamp] DATETIME NOT NULL,      
    CONSTRAINT [FK_chatmessages_session] FOREIGN KEY ([chat_session_id]) REFERENCES [chatsessions]([chat_session_id]), 
    CONSTRAINT [FK_chatmessages_users] FOREIGN KEY ([user_id]) REFERENCES [users]([user_id])
)
