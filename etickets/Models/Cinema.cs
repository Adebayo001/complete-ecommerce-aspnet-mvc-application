using etickets.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace etickets.Models
{
    public class Cinema:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Cinema Logo")]
        [Required(ErrorMessage = "Logo is Required")]
        public string Logo { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is Required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "The name must be greater than 5")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; }

        //Relationship
        public List<Movie>? Movie { get; set; }
    }
}
