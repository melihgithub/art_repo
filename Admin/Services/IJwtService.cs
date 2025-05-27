using ArtistPortfolio.Shared.Models;

namespace ArtistPortfolio.Admin.Services;

public interface IJwtService
{
    /// <summary>
    /// Kullanıcı için JWT token oluşturur
    /// </summary>
    /// <param name="user">Token oluşturulacak kullanıcı</param>
    /// <returns>JWT token string</returns>
    string GenerateToken(User user);

    /// <summary>
    /// JWT token'ın geçerli olup olmadığını kontrol eder
    /// </summary>
    /// <param name="token">Kontrol edilecek token</param>
    /// <returns>Token geçerli ise true, değilse false</returns>
    bool ValidateToken(string token);

    /// <summary>
    /// JWT token'dan kullanıcı ID'sini çıkarır
    /// </summary>
    /// <param name="token">Kullanıcı ID'si çıkarılacak token</param>
    /// <returns>Kullanıcı ID'si veya null</returns>
    int? GetUserIdFromToken(string token);

    /// <summary>
    /// JWT token'dan kullanıcı adını çıkarır
    /// </summary>
    /// <param name="token">Kullanıcı adı çıkarılacak token</param>
    /// <returns>Kullanıcı adı veya null</returns>
    string? GetUsernameFromToken(string token);

    /// <summary>
    /// JWT token'dan email adresini çıkarır
    /// </summary>
    /// <param name="token">Email adresi çıkarılacak token</param>
    /// <returns>Email adresi veya null</returns>
    string? GetEmailFromToken(string token);

    /// <summary>
    /// JWT token'ın ne zaman sona ereceğini döndürür
    /// </summary>
    /// <param name="token">Kontrol edilecek token</param>
    /// <returns>Token'ın sona erme tarihi veya null</returns>
    DateTime? GetTokenExpiration(string token);

    /// <summary>
    /// JWT token'ın süresi dolmuş mu kontrol eder
    /// </summary>
    /// <param name="token">Kontrol edilecek token</param>
    /// <returns>Token süresi dolmuş ise true, değilse false</returns>
    bool IsTokenExpired(string token);

    /// <summary>
    /// Refresh token oluşturur
    /// </summary>
    /// <returns>Refresh token string</returns>
    string GenerateRefreshToken();

    /// <summary>
    /// Token'dan tüm claim'leri çıkarır
    /// </summary>
    /// <param name="token">Claim'leri çıkarılacak token</param>
    /// <returns>Claim'ler dictionary'si</returns>
    Dictionary<string, string>? GetClaimsFromToken(string token);
}