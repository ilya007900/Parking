using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkingService.Application.Interfaces;

namespace ParkingService.Application.Commands
{
    public class CloseParkingCommand : ICommand
    {
        public int ParkingId { get; }
        public string Reason { get; }

        public CloseParkingCommand(int parkingId, string reason)
        {
            ParkingId = parkingId;
            Reason = reason;
        }
    }

    public class CloseParkingCommandHandler : ICommandHandler<CloseParkingCommand>
    {
        public Task<Unit> Handle(CloseParkingCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}