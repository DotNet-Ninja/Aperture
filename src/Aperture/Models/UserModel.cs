using System.ComponentModel.DataAnnotations;
using Aperture.Data;

namespace Aperture.Models;

public class UserModel
{
    public UserModel()
    {
    }

    public UserModel(ApplicationUser user)
    {
        Id = user.Id;
        DisplayName = user.DisplayName;
        Email = user.Email;
        AvatarId = user.AvatarId;
        Roles = user.Roles.Select(r => r.Name).ToList();
    }


    public int Id { get; set; } = 0;

    [Required]
    [StringLength(64)]
    [Display(Name = "Display Name")]
    public string DisplayName { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email Address")]
    public string Email { get; set; } = string.Empty;
    public string AvatarId { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = [];
}