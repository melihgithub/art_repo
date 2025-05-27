using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;
using ArtistPortfolio.Admin.Services;

namespace ArtistPortfolio.Admin.Services;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ProtectedLocalStorage _localStorage;
    private readonly IJwtService _jwtService;
    private readonly IAuthService _authService;
    private readonly ILogger<CustomAuthenticationStateProvider> _logger;

    public CustomAuthenticationStateProvider(
        ProtectedLocalStorage localStorage,
        IJwtService jwtService,
        IAuthService authService,
        ILogger<CustomAuthenticationStateProvider> logger)
    {
        _localStorage = localStorage;
        _jwtService = jwtService;
        _authService = authService;
        _logger = logger;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var token = await GetTokenAsync();
            
            if (string.IsNullOrEmpty(token) || !_jwtService.ValidateToken(token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var claims = _jwtService.GetClaimsFromToken(token);
            if (claims == null)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var claimsList = claims.Select(c => new Claim(c.Key, c.Value)).ToList();
            var identity = new ClaimsIdentity(claimsList, "jwt");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting authentication state");
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }

    public async Task<bool> ValidateUser(string username, string password)
    {
        try
        {
            var result = await _authService.LoginAsync(username, password);
            
            if (result.Success && !string.IsNullOrEmpty(result.Token))
            {
                await SetTokenAsync(result.Token);
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating user {Username}", username);
            return false;
        }
    }

    public async Task LogoutAsync()
    {
        try
        {
            await RemoveTokenAsync();
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during logout");
        }
    }

    public async Task<string?> GetTokenAsync()
    {
        try
        {
            var result = await _localStorage.GetAsync<string>("authToken");
            return result.Success ? result.Value : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting token from storage");
            return null;
        }
    }

    private async Task SetTokenAsync(string token)
    {
        try
        {
            await _localStorage.SetAsync("authToken", token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error setting token to storage");
        }
    }

    private async Task RemoveTokenAsync()
    {
        try
        {
            await _localStorage.DeleteAsync("authToken");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error removing token from storage");
        }
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        var authState = await GetAuthenticationStateAsync();
        return authState.User.Identity?.IsAuthenticated ?? false;
    }

    public async Task<string?> GetCurrentUserIdAsync()
    {
        var authState = await GetAuthenticationStateAsync();
        return authState.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    public async Task<string?> GetCurrentUsernameAsync()
    {
        var authState = await GetAuthenticationStateAsync();
        return authState.User.FindFirst(ClaimTypes.Name)?.Value;
    }
}