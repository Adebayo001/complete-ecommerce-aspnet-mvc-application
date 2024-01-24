using etickets.Data.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace etickets.Models
{
    public class Producer:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        
        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "Profile Picture is required")]
        public string ProfilePictureURL { get; set; }
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "The length must be between 5 and 50")]
        public string FullName { get; set; }
        [Display(Name ="Biography")]
        [Required(ErrorMessage = "Biography is required")]
        public string Bio { get; set; }

        //Relationship
        #nullable disable
        public List<Movie> Movie { get; set; }
    }
}
