CREATE TABLE [dbo].[User_Account] (
    [account_id]    INT          IDENTITY (1, 1) NOT NULL,
    [user_id]       INT          NOT NULL,
    [applicatie_id] INT          NULL,
    [rol_id]        INT          NULL,
    [status]        VARCHAR (50) NULL,
    CONSTRAINT [PK_User_Profile] PRIMARY KEY CLUSTERED ([account_id] ASC),
    CONSTRAINT [FK_User_Profile_Applicatie] FOREIGN KEY ([applicatie_id]) REFERENCES [dbo].[Applicatie] ([applicatie_id]),
    CONSTRAINT [FK_User_Profile_User] FOREIGN KEY ([user_id]) REFERENCES [dbo].[User] ([user_id]),
    CONSTRAINT [FK_User_Profile_User_Rol] FOREIGN KEY ([rol_id]) REFERENCES [dbo].[User_Rol] ([rol_id])
);

