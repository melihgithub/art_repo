using System.ComponentModel.DataAnnotations;

namespace ArtistPortfolio.Shared.Models;

public class Artwork
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    [Required]
    public string ImageUrl { get; set; } = string.Empty;
    
    public string? Technique { get; set; }
    
    public string? Dimensions { get; set; }
    
    public int? Year { get; set; }
    
    public decimal? Price { get; set; }
    
    public bool IsForSale { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public int DisplayOrder { get; set; }
}