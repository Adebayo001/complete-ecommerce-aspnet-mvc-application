using etickets.Data.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace etickets.Models
{
    public class Actor:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "Profile Picture is Required")]
        public string ProfilePictureURL { get; set; }
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is Required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Your Full Name must be more than 10")]
        public string FullName { get; set; }
        [Display(Name = "Biography")]
        [Required(ErrorMessage = "Biography is Required")]
        public string Bio { get; set; }

#nullable disable
        public List<Actor_Movie> Actors_Movies { get; set; }
    }
}
