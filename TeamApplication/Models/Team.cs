using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeamApplication.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }
        [Required]
        [Display(Name = "Voluntários")]
        public List<Volunteer> Volunteers { get; set; } = new List<Volunteer>();

        //Relation Many to Many Auxiliary Class TeamVolunteer
        public ICollection<TeamVolunteer> TeamVolunteer { get; set; }

        //Relation One Job Has one Team
        [Display(Name = "Trabalho")]
        public int JobId { get; set; }
        public Job Job { get; set; }

        
    }
}
