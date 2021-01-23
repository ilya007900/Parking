using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using Domain.Entities;
using Domain.FunctionalExtensions;

namespace Application.Commands
{
    public class CreateParkingCommand : ICommand<Result<int>>
    {
        public string Country { get; }
        public string City { get; }
        public string Street { get; }

        public CreateParkingCommand(string country, string city, string street)
        {
            Country = country;
            City = city;
            Street = street;
        }
    }

    public class CreateParkingCommandHandler : ICommandHandler<CreateParkingCommand, Result<int>>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateParkingCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result<int>> Handle(CreateParkingCommand command, CancellationToken cancellationToken)
        {
            var address = Address.Create(command.Country, command.City, command.Street);
            if (!address.IsSuccess)
            {
                return Task.FromResult(Result<int>.Failure(address.ErrorMessage));
            }

            var parking = new Parking(address.Value);
            var addedParking = unitOfWork.ParkingRepository.Add(parking);
            unitOfWork.Save();

            return Task.FromResult(Result<int>.Success(addedParking.Id));
        }
    }

    public class CreateParkingDto
    {
        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Country { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string City { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Street { get; set; }
    }
}
