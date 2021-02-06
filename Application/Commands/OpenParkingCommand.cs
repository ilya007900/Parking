using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkingService.Application.Interfaces;

namespace ParkingService.Application.Commands
{
    public class OpenParkingCommand : ICommand
    {
        public int ParkingId { get; }

        public OpenParkingCommand(int parkingId)
        {
            ParkingId = parkingId;
        }
    }

    public class OpenParkingCommandHandler : ICommandHandler<OpenParkingCommand>
    {
        public Task<Unit> Handle(OpenParkingCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}