CREATE TABLE [dbo].[ParkingLevelState]
(
	[Id] INT NOT NULL,
	[Name] NVARCHAR(256) NOT NULL

	CONSTRAINT [PK_ParkingLevelState_Id] PRIMARY KEY([Id])
	CONSTRAINT [UQ_ParkingLevelState_Name] UNIQUE([Name])
)
