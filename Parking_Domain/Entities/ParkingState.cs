namespace ParkingService.Domain.Entities
{
    public class ParkingState
    {
        public static readonly ParkingState Open = new ParkingState(1, "Open");
        public static readonly ParkingState Closed = new ParkingState(2, "Closed");

        public int Id { get; }
        public string Name { get; }

        public ParkingState(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}