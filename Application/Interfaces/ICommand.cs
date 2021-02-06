using MediatR;

namespace ParkingService.Application.Interfaces
{
    public interface ICommand : IRequest
    {
    }

    public interface ICommand<TResult> : IRequest<TResult>
    {

    }
}
