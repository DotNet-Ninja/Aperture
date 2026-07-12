namespace Aperture.Models;

public class UserEditModel
{
    public UserEditModel()
    {
    }

    public UserEditModel(UserModel user, List<string> allRoles)
    {
        User = user;
        AllRoles = allRoles;
    }

    public UserModel User { get; set; }

    public List<string> AllRoles { get; set; } = [];
}