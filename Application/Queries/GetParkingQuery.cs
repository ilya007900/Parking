using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ParkingService.Application.Interfaces;

namespace ParkingService.Application.Queries
{
    public class GetParkingQuery : IQuery<ParkingInfoDto>
    {
        public int Id { get; }

        public GetParkingQuery(int id)
        {
            Id = id;
        }
    }

    public class GetParkingQueryHandler : IQueryHandler<GetParkingQuery, ParkingInfoDto>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetParkingQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<ParkingInfoDto> Handle(GetParkingQuery request, CancellationToken cancellationToken)
        {
            var parkingInfo = unitOfWork.ParkingRepository.Find(request.Id);
            return Task.FromResult(new ParkingInfoDto
            {
                Id = parkingInfo.Id,
                Country = parkingInfo.Address.Country,
                City = parkingInfo.Address.City,
                Street = parkingInfo.Address.Street,
                Floors = parkingInfo.Floors.Select(l => new ParkingInfoDto.ParkingFloorInfoDto
                {
                    Number = l.Number,
                    Spaces = l.ParkingSpaces.Select(s => new ParkingInfoDto.ParkingFloorInfoDto.ParkingSpaceInfoDto
                    {
                        Number = s.Number,
                        State = s.State.ToString(),
                    }).ToList()
                }).ToList()
            });
        }
    }

    public class ParkingInfoDto
    {
        public int Id { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public IReadOnlyList<ParkingFloorInfoDto> Floors { get; set; }

        public class ParkingFloorInfoDto
        {
            public int Number { get; set; }

            public IReadOnlyList<ParkingSpaceInfoDto> Spaces { get; set; }

            public class ParkingSpaceInfoDto
            {
                public int Number { get; set; }

                public string State { get; set; }
            }
        }
    }
}
