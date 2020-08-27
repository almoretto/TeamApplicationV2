﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace TeamApplication.Models
{
    public class Entity
    {
        [Key]
        public int EntityId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Entidade: ")]
        public string EntityName { get; set; }

        [Required]
        [EnumDataType(typeof(DayOfWeek))]
        [Display(Name = "Dia da vista: ")]
        public DayOfWeek VisitDay { get; set; }

        [Required]
        [DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = false)]
        [Display(Name = "Horário de visita: ")]
        public DateTime VisitTime { get; set; }

        [Required]
        //[DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = false)]
        [Display(Name = "Duração Máxima: ")]
        public TimeSpan VisitDuration { get; set; }

        [Required]
        [EnumDataType(typeof(VisitScale))]
        [Display(Name = "Escala de vista: ")]
        public VisitScale VisitScale { get; set; }

        [Required]
        [Display(Name = "Máximo de voluntários: ")]
        public int MaxVolunteer { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Contato: ")]
        public string Contact { get; set; }
        [Required]
        [StringLength(15)]
        [Display(Name = "Telefone: ")]
        public string Phone { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "e-Mail: ")]
        public string Email { get; set; }

       //One Entity Has many Jobs
        public ICollection<Job> Jobs { get; set; } 

    }
}
