using System.Security.Claims;

namespace Aperture.Models;

public class AccountDiagnosticsModel
{
    public List<Claim> Claims { get; set; } = new List<Claim>();
}