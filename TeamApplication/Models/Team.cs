﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamApplication.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }
        [Display(Name = "Voluntários")]
        [NotMapped]
        public List<int> Volunteers { get; set; } 

        //Relation Many to Many Auxiliary Class TeamVolunteer
        public ICollection<TeamVolunteer> TeamVolunteer { get; set; }

        //Relation One Job Has one Team
        [Display(Name = "Trabalho")]
        public int JobId { get; set; }
        public Job Job { get; set; }

        
    }
}
