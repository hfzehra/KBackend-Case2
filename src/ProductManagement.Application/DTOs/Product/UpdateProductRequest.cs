namespace ProductManagement.Application.DTOs.Product;

public record UpdateProductRequest(string Name, string Description, decimal Price, int Stock);
