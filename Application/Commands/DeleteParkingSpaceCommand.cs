using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkingService.Application.Interfaces;

namespace ParkingService.Application.Commands
{
    public class DeleteParkingSpaceCommand : ICommand
    {
        public int ParkingId { get; }
        public int Floor { get; }
        public int SpaceNumber { get; }

        public DeleteParkingSpaceCommand(int parkingId, int floor, int spaceNumber)
        {
            ParkingId = parkingId;
            Floor = floor;
            SpaceNumber = spaceNumber;
        }
    }

    public class DeleteParkingSpaceCommandHandler : ICommandHandler<DeleteParkingSpaceCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteParkingSpaceCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Unit> Handle(DeleteParkingSpaceCommand request, CancellationToken cancellationToken)
        {
            var parking = unitOfWork.ParkingRepository.Find(request.ParkingId);
            var level = parking.Floors.First(x => x.Number == request.Floor);
            level.RemoveParkingSpace(request.SpaceNumber);

            unitOfWork.Save();

            return Unit.Task;
        }
    }
}