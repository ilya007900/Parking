using MediatR;

namespace Application.Interfaces
{
    public interface IQueryHandler<TQuery, TResult> :
        IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
    }
}
