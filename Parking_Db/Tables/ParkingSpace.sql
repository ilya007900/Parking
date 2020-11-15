CREATE TABLE [dbo].[ParkingSpace]
(
	[Id] INT IDENTITY(1, 1) NOT NULL,
	[Number] INT NOT NULL,
	[VehicleWeight] INT NULL,
	[VehicleLicensePlate] VARCHAR(10) NULL,
	[ParkingLevelId] INT NOT NULL,

	CONSTRAINT [PK_ParkingPlace_Id] PRIMARY KEY([Id]),
	CONSTRAINT [FK_ParkingLevel_ParkingPlace] FOREIGN KEY([ParkingLevelId]) REFERENCES [dbo].[ParkingLevel]([Id]) ON DELETE CASCADE
)
