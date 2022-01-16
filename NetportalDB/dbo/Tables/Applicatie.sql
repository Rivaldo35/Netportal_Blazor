﻿CREATE TABLE [dbo].[Applicatie] (
    [applicatie_id] INT          IDENTITY (1, 1) NOT NULL,
    [naam]          VARCHAR (50) NULL,
    [code]          VARCHAR (50) NULL,
    [omschrijving]  VARCHAR (50) NULL,
    CONSTRAINT [PK_Applicatie] PRIMARY KEY CLUSTERED ([applicatie_id] ASC)
);

