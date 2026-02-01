using ProductManagement.Application.Commands;
using ProductManagement.Application.DTOs.Auth;

namespace ProductManagement.Application.Commands.Auth;

public record RegisterCommand(string Email, string Password, string FullName) : ICommand<AuthResponse>;
