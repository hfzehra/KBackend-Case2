using MediatR;
using ProductManagement.Application.Commands.Product;
using ProductManagement.Application.Interfaces;
using ProductManagement.Core.Exceptions;
using ProductManagement.Core.Interfaces;

namespace ProductManagement.Application.Handlers.Product;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Repository<Core.Entities.Product>().GetByIdAsync(request.Id, cancellationToken);

        if (product == null)
        {
            throw new NotFoundException(nameof(Core.Entities.Product), request.Id);
        }

        await _unitOfWork.Repository<Core.Entities.Product>().DeleteAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Cache invalidation
        await _cacheService.RemoveByPrefixAsync("products", cancellationToken);

        return true;
    }
}
