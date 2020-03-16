﻿CREATE TABLE [dbo].[Stages] (
    [Id]      UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Stage]   NVARCHAR (50)    NOT NULL,
    [HouseId] UNIQUEIDENTIFIER NOT NULL,
    [ImageId] UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);