CREATE TABLE [dbo].[Auditlog] (
    [auditlog_id]   INT           IDENTITY (1, 1) NOT NULL,
    [user_id]       INT           NOT NULL,
    [instelling_id] INT           NOT NULL,
    [applicatie_id] INT           NOT NULL,
    [rapportage_id] INT           NULL,
    [actie]         VARCHAR (MAX) NOT NULL,
    [datetime]      DATETIME      NOT NULL,
    CONSTRAINT [PK_Auditlog] PRIMARY KEY CLUSTERED ([auditlog_id] ASC)
);

