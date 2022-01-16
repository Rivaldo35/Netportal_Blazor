CREATE TABLE [dbo].[User] (
    [user_id]          INT           IDENTITY (1, 1) NOT NULL,
    [instelling_id]    INT           NOT NULL,
    [voornaam]         VARCHAR (50)  NOT NULL,
    [achternaam]       VARCHAR (50)  NOT NULL,
    [username]         VARCHAR (50)  NOT NULL,
    [password]         VARCHAR (MAX) NOT NULL,
    [email]            VARCHAR (50)  NOT NULL,
    [status]           VARCHAR (50)  NOT NULL,
    [internal_user]    VARCHAR (50)  NOT NULL,
    [failed_attempts]  INT           CONSTRAINT [DF_User_failed_attempts] DEFAULT ((0)) NOT NULL,
    [pwd_exp_date]     DATE          NOT NULL,
    [pwd_changed_date] DATE          NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([user_id] ASC),
    CONSTRAINT [FK_User_Instelling] FOREIGN KEY ([instelling_id]) REFERENCES [dbo].[Instelling] ([instelling_id]),
    CONSTRAINT [IX_User] UNIQUE NONCLUSTERED ([username] ASC)
);

