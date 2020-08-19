using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeamApplication.Models
{
    public class State
    {
        [Key]
        public int StateId { get; set; }
        [Required]
        [StringLength(2)]
        [Display(Name = "Sigla")]
        public string UFAbreviation { get; set; }
        [Required]
        [StringLength(30)]
        [Display(Name = "Estado")]
        public string UFName { get; set; }

        public ICollection<City> Cities { get; set; } //Relation many Cities to 1 State
    }
}
