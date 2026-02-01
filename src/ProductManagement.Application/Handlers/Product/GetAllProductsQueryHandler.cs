using MediatR;
using ProductManagement.Application.DTOs.Product;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Queries.Product;
using ProductManagement.Core.Interfaces;

namespace ProductManagement.Application.Handlers.Product;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;
    private const string CacheKey = "products:all";

    public GetAllProductsQueryHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        // Try to get from cache
        var cachedProducts = await _cacheService.GetAsync<IEnumerable<ProductDto>>(CacheKey, cancellationToken);
        
        if (cachedProducts != null)
        {
            return cachedProducts;
        }

        // Get from database
        var products = await _unitOfWork.Repository<Core.Entities.Product>().GetAllAsync(cancellationToken);
        
        var productDtos = products.Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Price, p.Stock));

        // Cache the result
        await _cacheService.SetAsync(CacheKey, productDtos, TimeSpan.FromMinutes(10), cancellationToken);

        return productDtos;
    }
}
