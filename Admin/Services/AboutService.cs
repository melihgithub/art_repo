using Microsoft.EntityFrameworkCore;
using ArtistPortfolio.Data;
using ArtistPortfolio.Shared.Models;

namespace ArtistPortfolio.Admin.Services;

public class AboutService : IAboutService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<AboutService> _logger;

    public AboutService(ApplicationDbContext context, ILogger<AboutService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<About>> GetAboutsAsync()
    {
        try
        {
            return await _context.Abouts
                .OrderByDescending(a => a.UpdatedAt)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting abouts");
            throw;
        }
    }

    public async Task<About?> GetAboutAsync(int id)
    {
        try
        {
            return await _context.Abouts.FindAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting about with id {Id}", id);
            throw;
        }
    }

    public async Task<About> CreateAboutAsync(About about)
    {
        try
        {
            about.CreatedAt = DateTime.UtcNow;
            about.UpdatedAt = DateTime.UtcNow;

            _context.Abouts.Add(about);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Created new about with id {Id}", about.Id);
            return about;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating about");
            throw;
        }
    }

    public async Task<About> UpdateAboutAsync(About about)
    {
        try
        {
            var existingAbout = await _context.Abouts.FindAsync(about.Id);
            if (existingAbout == null)
            {
                throw new ArgumentException($"About with id {about.Id} not found");
            }

            existingAbout.Title = about.Title;
            existingAbout.Content = about.Content;
            existingAbout.ImageUrl = about.ImageUrl;
            existingAbout.IsActive = about.IsActive;
            existingAbout.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Updated about with id {Id}", about.Id);
            return existingAbout;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating about with id {Id}", about.Id);
            throw;
        }
    }

    public async Task<bool> DeleteAboutAsync(int id)
    {
        try
        {
            var about = await _context.Abouts.FindAsync(id);
            if (about == null)
            {
                return false;
            }

            _context.Abouts.Remove(about);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Deleted about with id {Id}", id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting about with id {Id}", id);
            throw;
        }
    }

    public async Task<List<About>> GetActiveAboutsAsync()
    {
        try
        {
            return await _context.Abouts
                .Where(a => a.IsActive)
                .OrderByDescending(a => a.UpdatedAt)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting active abouts");
            throw;
        }
    }

    public async Task<bool> ToggleActiveStatusAsync(int id, bool isActive)
    {
        try
        {
            var about = await _context.Abouts.FindAsync(id);
            if (about == null)
            {
                return false;
            }

            about.IsActive = isActive;
            about.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            _logger.LogInformation("Toggled active status for about with id {Id} to {IsActive}", id, isActive);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error toggling active status for about with id {Id}", id);
            throw;
        }
    }

    public async Task<(List<About> Items, int TotalCount)> GetPagedAboutsAsync(int page, int pageSize)
    {
        try
        {
            var query = _context.Abouts.OrderByDescending(a => a.UpdatedAt);
            
            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting paged abouts for page {Page} with size {PageSize}", page, pageSize);
            throw;
        }
    }

    public async Task<List<About>> SearchAboutsAsync(string searchTerm)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetAboutsAsync();
            }

            var lowerSearchTerm = searchTerm.ToLower();
            return await _context.Abouts
                .Where(a => a.Title.ToLower().Contains(lowerSearchTerm) || 
                           a.Content.ToLower().Contains(lowerSearchTerm))
                .OrderByDescending(a => a.UpdatedAt)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching abouts with term {SearchTerm}", searchTerm);
            throw;
        }
    }

    public async Task<List<About>> GetAboutsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        try
        {
            return await _context.Abouts
                .Where(a => a.CreatedAt >= startDate && a.CreatedAt <= endDate)
                .OrderByDescending(a => a.UpdatedAt)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting abouts by date range {StartDate} to {EndDate}", startDate, endDate);
            throw;
        }
    }

    public async Task<int> GetAboutsCountAsync()
    {
        try
        {
            return await _context.Abouts.CountAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting abouts count");
            throw;
        }
    }

    public async Task<int> GetActiveAboutsCountAsync()
    {
        try
        {
            return await _context.Abouts.CountAsync(a => a.IsActive);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting active abouts count");
            throw;
        }
    }
}