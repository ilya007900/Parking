using MediatR;

namespace ParkingService.Application.Interfaces
{
    public interface IQuery<TResult> : IRequest<TResult>
    {
    }
}
