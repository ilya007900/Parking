using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Parking_Domain;
using Parking_Domain.FunctionalExtensions;

namespace Application.Commands
{
    public class UpdateParkingAddressCommand : ICommand<Result>
    {
        public int Id { get; }
        public string Country { get; }
        public string City { get; }
        public string Street { get; }

        public UpdateParkingAddressCommand(int id, string country, string city, string street)
        {
            Id = id;
            Country = country;
            City = city;
            Street = street;
        }
    }

    public class UpdateParkingAddressCommandHandler
        : ICommandHandler<UpdateParkingAddressCommand, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateParkingAddressCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(UpdateParkingAddressCommand request, CancellationToken cancellationToken)
        {
            var address = Address.Create(request.Country, request.City, request.Street);
            if (!address.IsSuccess)
            {
                return Task.FromResult(Result.Failure(address.ErrorMessage));
            }

            var parking = unitOfWork.ParkingRepository.Find(request.Id);
            if (parking == null)
            {
                return Task.FromResult(Result.Failure($"Parking with id: {request.Id} not found"));
            }

            parking.UpdateAddress(address.Value);

            unitOfWork.Save();

            return Task.FromResult(Result.Success());
        }
    }

    public class UpdateParkingAddressDto
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
