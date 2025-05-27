using System.ComponentModel.DataAnnotations;

namespace ArtistPortfolio.Shared.Models;

public class NewsItem
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string Content { get; set; } = string.Empty;
    
    public string? ImageUrl { get; set; }
    
    public string? VideoUrl { get; set; }
    
    public NewsType Type { get; set; }
    
    public DateTime PublishedDate { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    public bool IsActive { get; set; } = true;
}

public enum NewsType
{
    Program = 1,
    Interview = 2,
    Magazine = 3,
    Newspaper = 4
}