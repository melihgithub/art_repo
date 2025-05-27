using System.ComponentModel.DataAnnotations;

namespace ArtistPortfolio.Shared.Models;

public class Exhibition
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public string? Location { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public ExhibitionType Type { get; set; }
    
    public ExhibitionStatus Status { get; set; }
    
    public bool IsInternational { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    public bool IsActive { get; set; } = true;
}

public enum ExhibitionType
{
    Solo = 1,
    Group = 2,
    Participation = 3
}

public enum ExhibitionStatus
{
    Upcoming = 1,
    Active = 2,
    Past = 3
}