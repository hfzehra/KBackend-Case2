using ProductManagement.Application.Commands;
using ProductManagement.Application.DTOs.Auth;

namespace ProductManagement.Application.Commands.Auth;

public record LoginCommand(string Email, string Password) : ICommand<AuthResponse>;
