using MediatR;

namespace ProductManagement.Application.Queries;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
