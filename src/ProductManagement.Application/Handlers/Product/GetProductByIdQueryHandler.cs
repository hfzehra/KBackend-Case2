using MediatR;
using ProductManagement.Application.DTOs.Product;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Queries.Product;
using ProductManagement.Core.Interfaces;

namespace ProductManagement.Application.Handlers.Product;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"products:{request.Id}";

        // Try to get from cache
        var cachedProduct = await _cacheService.GetAsync<ProductDto>(cacheKey, cancellationToken);
        
        if (cachedProduct != null)
        {
            return cachedProduct;
        }

        // Get from database
        var product = await _unitOfWork.Repository<Core.Entities.Product>().GetByIdAsync(request.Id, cancellationToken);
        
        if (product == null)
        {
            return null;
        }

        var productDto = new ProductDto(product.Id, product.Name, product.Description, product.Price, product.Stock);

        // Cache the result
        await _cacheService.SetAsync(cacheKey, productDto, TimeSpan.FromMinutes(10), cancellationToken);

        return productDto;
    }
}
