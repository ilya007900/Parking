using ParkingService.Domain.Common;

namespace ParkingService.Domain.Entities
{
    public class FloorState : Entity
    {
        public static readonly FloorState Open = new FloorState(1, "Open");
        public static readonly FloorState Closed = new FloorState(2, "Closed");

        public int Id { get; private set; }
        public string Name { get; private set; }

        protected FloorState(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}