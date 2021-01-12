using MediatR;

namespace Application.Interfaces
{
    public interface IQuery<TResult> : IRequest<TResult>
    {
    }
}
