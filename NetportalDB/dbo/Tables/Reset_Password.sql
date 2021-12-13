CREATE TABLE [dbo].[Reset_Password] (
    [id]       INT           IDENTITY (1, 1) NOT NULL,
    [user_id]  INT           NOT NULL,
    [username] VARCHAR (50)  NOT NULL,
    [email]    VARCHAR (50)  NOT NULL,
    [token]    VARCHAR (255) NOT NULL,
    [datetime] DATETIME      NULL,
    CONSTRAINT [PK_Reset_Password] PRIMARY KEY CLUSTERED ([id] ASC)
);

