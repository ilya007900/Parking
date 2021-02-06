﻿using MediatR;

namespace ParkingService.Application.Interfaces
{
    public interface IQueryHandler<TQuery, TResult> :
        IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
    }
}
