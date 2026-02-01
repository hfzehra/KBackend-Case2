using ProductManagement.Application.Commands;

namespace ProductManagement.Application.Commands.Product;

public record DeleteProductCommand(Guid Id) : ICommand<bool>;
