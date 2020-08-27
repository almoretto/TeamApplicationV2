using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace TeamApplication.Models
{
    public class Job
    {
        [Key]
        public int JobId { get; set; }
        [Required]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data:")]
        [DataType(DataType.Date)]
        public DateTime JobDay { get; set; }
        [Required]
        [StringLength(1)]
        [Display(Name = "Periodo:")]
        public string JobPeriod { get; set; } //M, T, N

        [Required]
        [EnumDataType(typeof(ActionKind))]
        [Display(Name = "Tipo:")]
        public ActionKind ActionKind { get; set; }

        [Display(Name = "Nº Volunt.:")]
        public int MaxVolunteer { get; set; }
        //Onde Entity has many Jobs
        [Required]
        [Display(Name = "Entidade:")]
        public int EntityId { get; set; }
        public Entity Entity { get; set; }

        //Relation one Job has one Team
        public Team Team { get; set; }

    }
}
