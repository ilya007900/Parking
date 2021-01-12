using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Parking_Domain.Entities;
using Parking_Domain.FunctionalExtensions;

namespace Application.Commands
{
    public class AddParkingLevelCommand : ICommand<Result<int>>
    {
        public int ParkingId { get; }
        public int Floor { get; }

        public AddParkingLevelCommand(int parkingId, int floor)
        {
            ParkingId = parkingId;
            Floor = floor;
        }
    }

    public class AddParkingLevelCommandHandler : ICommandHandler<AddParkingLevelCommand, Result<int>>
    {
        private readonly IUnitOfWork unitOfWork;

        public AddParkingLevelCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result<int>> Handle(AddParkingLevelCommand request, CancellationToken cancellationToken)
        {
            var parking = unitOfWork.ParkingRepository.Find(request.ParkingId);
            if (parking == null)
            {
                return Task.FromResult(Result<int>.Failure(""));
            }

            var parkingLevel = new ParkingLevel(request.Floor);
            parking.AddParkingLevel(parkingLevel);

            unitOfWork.Save();

            return Task.FromResult(Result<int>.Success(parkingLevel.Id));
        }
    }

    public class AddParkingLevelDto
    {
        [Required]
        public int Floor { get; set; }
    }
}
