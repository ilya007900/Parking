using MediatR;
using ParkingService.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ParkingService.Application.Commands
{
    public class DeleteParkingCommand : ICommand
    {
        public int ParkingId { get; }

        public DeleteParkingCommand(int parkingId)
        {
            ParkingId = parkingId;
        }
    }

    public class DeleteParkingCommandHandler : ICommandHandler<DeleteParkingCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteParkingCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Unit> Handle(DeleteParkingCommand request, CancellationToken cancellationToken)
        {
            unitOfWork.ParkingRepository.Remove(request.ParkingId);
            unitOfWork.Save();

            return Unit.Task;
        }
    }
}