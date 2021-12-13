CREATE TABLE [dbo].[Instelling_Type] (
    [type_id]      INT          IDENTITY (1, 1) NOT NULL,
    [code]         VARCHAR (50) NULL,
    [omschrijving] VARCHAR (50) NULL,
    CONSTRAINT [PK_Instelling_Type] PRIMARY KEY CLUSTERED ([type_id] ASC)
);

