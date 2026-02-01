using MediatR;

namespace ProductManagement.Application.Commands;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
