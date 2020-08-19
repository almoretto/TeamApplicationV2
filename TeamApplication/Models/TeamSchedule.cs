using System;
using System.ComponentModel.DataAnnotations;

namespace TeamApplication.Models
{
    public class TeamSchedule
    {
        [Key]
        public int TeamScheduleId { get; set; }
       
        [DataType(DataType.Date)]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data disponível:")]
        public DateTime TSDate { get; set; }
        [Required]
        [Display(Name = "Período Disponível na data (M,T,N):")]
        public char TSPeriod { get; set; }

        //Relationship Volunteer Has many TeamSchedule
        [Required]
        [Display(Name = "Voluntário:")]
        public int VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }
       
       
    }
}