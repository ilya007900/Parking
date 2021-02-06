using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ParkingService.Application.Interfaces;

namespace ParkingService.Application.Queries
{
    public class GetParkingsQuery : IQuery<IReadOnlyList<ParkingDto>>
    {
    }

    public class GetParkingsQueryHandler : IQueryHandler<GetParkingsQuery, IReadOnlyList<ParkingDto>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetParkingsQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<IReadOnlyList<ParkingDto>> Handle(GetParkingsQuery request, CancellationToken cancellationToken)
        {
            var dtos = unitOfWork.ParkingRepository
                .Get().Select(x => new ParkingDto
                {
                    Id = x.Id,
                    Country = x.Address.Country,
                    City = x.Address.City,
                    Street = x.Address.Street,
                    LevelsCount = x.Floors.Count
                }).ToList();

            return Task.FromResult(dtos as IReadOnlyList<ParkingDto>);
        }
    }

    public class ParkingDto
    {
        public int Id { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public int LevelsCount { get; set; }
    }
}
