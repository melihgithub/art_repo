using Microsoft.EntityFrameworkCore;
using ArtistPortfolio.Data;
using ArtistPortfolio.Shared.Models;
using BCrypt.Net;

namespace ArtistPortfolio.Admin.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly IJwtService _jwtService;

    public AuthService(ApplicationDbContext context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    public async Task<(bool Success, string Token, string Message)> LoginAsync(string username, string password)
    {
        try
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.IsActive);

            if (user == null)
            {
                return (false, "", "Kullanıcı bulunamadı.");
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return (false, "", "Şifre hatalı.");
            }

            // Update last login
            user.LastLoginAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            var token = _jwtService.GenerateToken(user);
            return (true, token, "Giriş başarılı.");
        }
        catch (Exception ex)
        {
            return (false, "", $"Giriş sırasında hata oluştu: {ex.Message}");
        }
    }

    public async Task<bool> ValidateTokenAsync(string token)
    {
        return _jwtService.ValidateToken(token);
    }

    public async Task<User?> GetCurrentUserAsync(string token)
    {
        var userId = _jwtService.GetUserIdFromToken(token);
        if (userId == null) return null;

        return await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId && u.IsActive);
    }
}