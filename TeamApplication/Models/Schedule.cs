using System;
using System.ComponentModel.DataAnnotations;

namespace TeamApplication.Models
{
    public class Schedule
    {
        [Key]
        public int TeamScheduleId { get; set; }
       
        [DataType(DataType.Date)]
        [Required]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data :")]
        public DateTime TSDate { get; set; }
        [Required]
        [Display(Name = "Período:")]
        [StringLength(1)]
        public string TSPeriod { get; set; }

        //Relationship Volunteer Has many TeamSchedule
        [Required]
        [Display(Name = "Voluntário:")]
        public int VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }
       
       
    }
}