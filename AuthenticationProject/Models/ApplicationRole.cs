using Microsoft.AspNetCore.Identity;

namespace AuthenticationProject.Models;

public class ApplicationRole : IdentityRole
{
    public DateTime? ExpiredDate { get; set; }
}