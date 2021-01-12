namespace Application.Interfaces
{
    public interface IUnitOfWork
    {
        public IParkingRepository ParkingRepository { get; }

        public void Save();
    }
}
