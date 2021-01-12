using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;

namespace Application.Queries
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
                Levels = parkingInfo.ParkingLevels.Select(l => new ParkingInfoDto.ParkingLevelInfoDto
                {
                    Id = l.Id,
                    Floor = l.Floor,
                    Spaces = l.ParkingSpaces.Select(s => new ParkingInfoDto.ParkingLevelInfoDto.ParkingSpaceInfoDto
                    {
                        Id = s.Id,
                        Number = s.Number,
                        IsFree = s.IsFree,
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

        public IReadOnlyList<ParkingLevelInfoDto> Levels { get; set; }

        public class ParkingLevelInfoDto
        {
            public int Id { get; set; }

            public int Floor { get; set; }

            public IReadOnlyList<ParkingSpaceInfoDto> Spaces { get; set; }

            public class ParkingSpaceInfoDto
            {
                public int Id { get; set; }

                public int Number { get; set; }

                public bool IsFree { get; set; }
            }
        }
    }
}
