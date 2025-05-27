using System.ComponentModel.DataAnnotations;

namespace ArtistPortfolio.Shared.Models;

public class Workshop
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public WorkshopType Type { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    public bool IsActive { get; set; } = true;
}

public enum WorkshopType
{
    About = 1,
    StudentWork = 2,
    Process = 3
}