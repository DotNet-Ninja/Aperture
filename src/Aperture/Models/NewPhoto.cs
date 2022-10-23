using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Aperture.Models;

public class NewPhoto
{
    [Required, MaxLength(128)]
    [RegularExpression(@"^[a-z0-9]{1}[a-z0-9\-]*[a-z0-9]{1}$", ErrorMessage = "Must contain contain only characters a-z, 0-9, and - and cannot start/end with -")]
    public string Slug { get; set; } = string.Empty;

    [Required, MaxLength(128)]
    public string Title { get; set; } = string.Empty;
    
    [StringLength(256)]
    public string? Caption { get; set; } = string.Empty;

    [Required]
    [DisplayName("Photo File")]
    public IFormFile? File { get; set; }
}
