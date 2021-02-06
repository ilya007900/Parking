using System;

namespace ParkingService.Domain.Entities
{
    public class ParkingEvent
    {
        public int Id { get; private set; }
        public string Description { get; private set; }
        public DateTime Date { get; private set; }

        public ParkingEvent(string description, DateTime date)
        {
            Description = description;
            Date = date;
        }

        protected ParkingEvent()
        {
            
        }

        public static ParkingEvent Create(string description)
        {
            return new(description, DateTime.Now);
        }
    }
}