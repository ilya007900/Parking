using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using ParkingService.Application.Interfaces;
using ParkingService.Domain.Entities;
using ParkingService.Domain.FunctionalExtensions;

namespace ParkingService.Application.Commands
{
    public class AddFloorCommand : ICommand<Result<int>>
    {
        public int ParkingId { get; }
        public int Floor { get; }

        public AddFloorCommand(int parkingId, int floor)
        {
            ParkingId = parkingId;
            Floor = floor;
        }
    }

    public class AddFloorCommandHandler : ICommandHandler<AddFloorCommand, Result<int>>
    {
        private readonly IUnitOfWork unitOfWork;

        public AddFloorCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result<int>> Handle(AddFloorCommand request, CancellationToken cancellationToken)
        {
            var parking = unitOfWork.ParkingRepository.Find(request.ParkingId);
            if (parking == null)
            {
                return Task.FromResult(Result<int>.Failure(""));
            }

            var parkingLevel = new Floor(request.Floor);
            var result = parking.AddFloor(parkingLevel);
            if (!result.IsSuccess)
            {
                return Task.FromResult(Result<int>.Failure(result.ErrorMessage));
            }

            unitOfWork.Save();

            return Task.FromResult(Result<int>.Success(parkingLevel.Id));
        }
    }

    public class AddFloorRequest
    {
        [Required]
        public int Floor { get; set; }
    }
}
