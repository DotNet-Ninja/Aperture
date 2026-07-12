namespace Aperture.Constants;

public static class AppRoles
{
    public const string Owner = "Owner";
    public const string Contributor = "Contributor";
    public const string Admin = $"{Owner},{Contributor}";
    public const string FamilyMember = "Family";
    public const string Friend = "Friend";
}