CREATE TABLE [dbo].[Houses]	
(

	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [Position] [sys].[geography] NOT NULL, 
    [Object] NVARCHAR(50) NOT NULL, 
    [ObjectDescription] NVARCHAR(MAX) NOT NULL
)