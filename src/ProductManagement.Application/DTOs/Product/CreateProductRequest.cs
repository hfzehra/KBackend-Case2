namespace ProductManagement.Application.DTOs.Product;

public record CreateProductRequest(string Name, string Description, decimal Price, int Stock);
