using Application.Interfaces;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppContext appContext;

        private IParkingRepository parkingRepository;

        public IParkingRepository ParkingRepository => parkingRepository ??= new ParkingRepository(appContext);

        public UnitOfWork(AppContext appContext)
        {
            this.appContext = appContext;
        }

        public void Save()
        {
            appContext.SaveChanges();
        }
    }
}