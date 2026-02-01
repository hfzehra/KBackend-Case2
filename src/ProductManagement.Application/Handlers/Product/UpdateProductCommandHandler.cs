using MediatR;
using ProductManagement.Application.Commands.Product;
using ProductManagement.Application.DTOs.Product;
using ProductManagement.Application.Interfaces;
using ProductManagement.Core.Exceptions;
using ProductManagement.Core.Interfaces;

namespace ProductManagement.Application.Handlers.Product;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Repository<Core.Entities.Product>().GetByIdAsync(request.Id, cancellationToken);

        if (product == null)
        {
            throw new NotFoundException(nameof(Core.Entities.Product), request.Id);
        }

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.Stock = request.Stock;

        await _unitOfWork.Repository<Core.Entities.Product>().UpdateAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Cache invalidation
        await _cacheService.RemoveByPrefixAsync("products", cancellationToken);

        return new ProductDto(product.Id, product.Name, product.Description, product.Price, product.Stock);
    }
}
