using Microsoft.AspNetCore.Identity;

namespace PrepodPortal.DataAccess.Entities;

public class ApplicationUser : IdentityUser
{
    public UserProfile? UserProfile { get; set; }
}