using Microsoft.AspNetCore.Identity;

namespace ModaVista_Back.Models
{
    public class AppUser : IdentityUser
    {
        public string Fullname { get; set; }
    }
}
