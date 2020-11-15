CREATE TABLE [dbo].[ParkingLevel]
(
	[Id] INT IDENTITY(1, 1) NOT NULL,
	[Floor] INT NOT NULL,
	[ParkingId] INT NOT NULL

	CONSTRAINT [PK_ParkingLevel_Id] PRIMARY KEY([Id]),
	CONSTRAINT [FK_Parking_ParkingLevel] FOREIGN KEY([ParkingId]) REFERENCES [dbo].[Parking]([Id]) ON DELETE CASCADE
)
