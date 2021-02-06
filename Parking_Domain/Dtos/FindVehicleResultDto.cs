namespace ParkingService.Domain.Dtos
{
    public class FindVehicleResultDto
    {
        public int ParkingId { get; }
        public string ParkingAddress { get; }
        public int Floor { get; }
        public int ParkingSpaceNumber { get; }

        public FindVehicleResultDto(int parkingId, string parkingAddress, int floor, int parkingSpaceNumber)
        {
            ParkingId = parkingId;
            ParkingAddress = parkingAddress;
            Floor = floor;
            ParkingSpaceNumber = parkingSpaceNumber;
        }
    }
}