using Microsoft.AspNetCore.Identity;

namespace AuthenticationProject.Models;

public class ApplicationUser : IdentityUser
{
    public string ApiKey { get; set; }
    public string SecretKey { get; set; }
}

public class ApplicationRole : IdentityRole
{
    public DateTime? ExpiredDate { get; set; }
}