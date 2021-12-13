CREATE TABLE [dbo].[User] (
    [Id]          INT NOT NULL IDENTITY ,
    [instelling_id]    INT            NOT NULL,
    [voornaam]         VARCHAR (50)   NOT NULL,
    [achternaam]       VARCHAR (50)   NOT NULL,
    [username]         VARCHAR (50)   NULL,
    [password]         VARCHAR (MAX)  NULL,
    [email]            VARCHAR (50)   NOT NULL,
    [status]           VARCHAR (50)   NULL,
    [internal_user]    VARCHAR (50)   NULL,
    [failed_attempts]  INT            CONSTRAINT [DF_User_failed_attempts] DEFAULT ((0)) NULL,
    [pwd_exp_date]     DATE           NULL,
    [pwd_changed_date] DATE           NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_User_Instelling] FOREIGN KEY ([instelling_id]) REFERENCES [dbo].[Instelling] ([instelling_id])
);

