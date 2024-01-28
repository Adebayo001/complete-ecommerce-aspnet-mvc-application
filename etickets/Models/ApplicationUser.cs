using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace etickets.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Display(Name ="Full Name")]
        public string fullName { get; set; }
    }
}
