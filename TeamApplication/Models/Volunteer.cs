using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamApplication.Models
{
    public class Volunteer
    {
        [Key]
        public int VolunteerId { get; set; }
        [Required]
        [StringLength(11)]
        [Display(Name = "CPF: ")]
        public string VDocCPF { get; set; }
        [Required]
        [StringLength(9)]
        [Display(Name = "RG: ")]
        public string VDocRG { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Nome: ")]
        public string VName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime VBirthDate { get; set; }
        int age;
        public int VAge
        {
            get
            {
                age = DateTime.Now.Year - VBirthDate.Year;
                if (DateTime.Now.DayOfYear < VBirthDate.DayOfYear)
                {
                    VAge -= 1;
                }
                return age;
            }
            private set
            {
                VAge=age;
            }

        }
        [Required]
        [StringLength(150)]
        [Display(Name = "Currículo: ")]
        public string VResumee { get; set; }
        [Required]
        [StringLength(15)]
        [Display(Name = "Telefone (ddd) 99999-9999: ")]
        public string VPhone { get; set; }

        [Display(Name = "Selecione se for telefone de mensagem: ")]
        public bool VMessagePhone { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "e-Mail: ")]
        public string VEmail { get; set; }
        [StringLength(50)]
        [Display(Name = "Mídia Social: ")]
        public string VSocialMidiaProfile { get; set; }
        [Display(Name = "Voluntário Ativo?: ")]
        public bool VActive { get; set; }

        //Relationship One volunteer has one Address
        [Display(Name = "Endereço: ")]
        public int AddressId { get; set; }
        public Address Address { get; set; }

        //One Volunteers Has Many TeamSchedule
        public ICollection<TeamSchedule> TeamSchedules { get; set; }

        //Relation Many Volunteers Has Many Teams
        public ICollection<TeamVolunteer> TeamVolunteer { get; set; }

       
    }
}