using ArtistPortfolio.Shared.Models;

namespace ArtistPortfolio.Admin.Services;

public interface IAuthService
{
    Task<(bool Success, string Token, string Message)> LoginAsync(string username, string password);
    Task<bool> ValidateTokenAsync(string token);
    Task<User?> GetCurrentUserAsync(string token);
}