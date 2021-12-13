CREATE TABLE [dbo].[Instelling_Profile] (
    [profile_id]    INT IDENTITY (1, 1) NOT NULL,
    [instelling_id] INT NOT NULL,
    [type_id]       INT NOT NULL,
    CONSTRAINT [PK_Instelling_Profile] PRIMARY KEY CLUSTERED ([profile_id] ASC),
    CONSTRAINT [FK_Instelling_Profile_Instelling] FOREIGN KEY ([instelling_id]) REFERENCES [dbo].[Instelling] ([instelling_id]),
    CONSTRAINT [FK_Instelling_Profile_Instelling_Type] FOREIGN KEY ([type_id]) REFERENCES [dbo].[Instelling_Type] ([type_id])
);

