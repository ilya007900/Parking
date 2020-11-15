namespace Parking_Domain.ParkingLevels
{
    public class ParkingLevelRepository
    {
        private readonly ParkingContext _context;

        public ParkingLevelRepository(ParkingContext context)
        {
            _context = context;
        }

        public ParkingLevel GetById(int id)
        {
            var level = _context.ParkingLevels.Find(id);
            if (level == null)
            {
                return null;
            }

            _context.Entry(level).Collection(x=>x.ParkingSpaces).Load();
            return level;
        }
    }
}