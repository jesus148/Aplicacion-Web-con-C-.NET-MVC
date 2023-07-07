CREATE TABLE [dbo].[Producto] (
    [Id]     CHAR (10)    NOT NULL,
    [nombre] VARCHAR (50) NULL,
    [precio] REAL         NULL,
    [fecha]  DATE         NULL,
    [idTipo] INT          NULL,
    [foto]   VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

