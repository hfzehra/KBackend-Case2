using ProductManagement.Application.Commands;
using ProductManagement.Application.DTOs.Product;

namespace ProductManagement.Application.Commands.Product;

public record UpdateProductCommand(Guid Id, string Name, string Description, decimal Price, int Stock) : ICommand<ProductDto>;
