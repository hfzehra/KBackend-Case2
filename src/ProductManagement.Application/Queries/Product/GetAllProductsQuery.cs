using ProductManagement.Application.DTOs.Product;
using ProductManagement.Application.Queries;

namespace ProductManagement.Application.Queries.Product;

public record GetAllProductsQuery : IQuery<IEnumerable<ProductDto>>;
