namespace ProductManagement.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(Guid userId, string email);
}
