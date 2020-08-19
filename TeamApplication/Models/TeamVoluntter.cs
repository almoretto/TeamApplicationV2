using System.ComponentModel.DataAnnotations;

namespace TeamApplication.Models
{
    public class TeamVolunteer
    {
        [Key]
        public int TeamVolunteerId { get; set; }
        //Relation one volunteer has many teamvolunteer
        [Required]
        [Display(Name = "Voluntário:")]
        public int VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }

        //relation one team has many teamvolunteer
        [Required]
        [Display(Name = "Equipe:")]
        public int TeamId { get; set; }
        public Team Team { get; set; }
        
    }
}
