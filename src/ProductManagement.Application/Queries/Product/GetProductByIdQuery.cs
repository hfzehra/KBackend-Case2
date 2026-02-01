using ProductManagement.Application.DTOs.Product;
using ProductManagement.Application.Queries;

namespace ProductManagement.Application.Queries.Product;

public record GetProductByIdQuery(Guid Id) : IQuery<ProductDto?>;
