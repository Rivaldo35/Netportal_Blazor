CREATE TABLE [dbo].[Instelling] (
    [instelling_id]   INT          IDENTITY (1, 1) NOT NULL,
    [Swift_code]      VARCHAR (50) NULL,
    [code]            VARCHAR (50) NULL,
    [naam]            VARCHAR (50) NULL,
    [omschrijving]    TEXT         NULL,
    [adres]           VARCHAR (50) NULL,
    [kkfnr]           VARCHAR (50) NULL,
    [telnr_1]         VARCHAR (50) NULL,
    [telnr_2]         VARCHAR (50) NULL,
    [email]           VARCHAR (50) NULL,
    [status]          VARCHAR (50) NULL,
    [datum_opgericht] DATE         NULL,
    [datum_opgeheven] DATE         NULL,
    CONSTRAINT [PK_Instelling] PRIMARY KEY CLUSTERED ([instelling_id] ASC)
);

