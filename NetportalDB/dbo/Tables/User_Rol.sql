CREATE TABLE [dbo].[User_Rol] (
    [rol_id]       INT          IDENTITY (1, 1) NOT NULL,
    [code]         VARCHAR (50) NULL,
    [omschrijving] VARCHAR (50) NULL,
    CONSTRAINT [PK_User_Rol] PRIMARY KEY CLUSTERED ([rol_id] ASC)
);

