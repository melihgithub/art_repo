using System.ComponentModel.DataAnnotations;

namespace ArtistPortfolio.Shared.Models;

public class FAQ
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(300)]
    public string Question { get; set; } = string.Empty;
    
    [Required]
    public string Answer { get; set; } = string.Empty;
    
    public int DisplayOrder { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    public bool IsActive { get; set; } = true;
}