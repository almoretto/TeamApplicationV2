using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Nascimento: ")]
        public DateTime VBirthDate { get; set; }
       
        [Display(Name = "Idade: ")]
        public int VAge  { get; private set; }
        [Required]
        [StringLength(150)]
        [Display(Name = "Currículo: ")]
        public string VResumee { get; set; }
        [Required]
        [StringLength(15)]
        [Display(Name = "Telefone: ")]
        public string VPhone { get; set; }

        [Display(Name = "Menssagens: ")]
        public bool VMessagePhone { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "e-Mail: ")]
        public string VEmail { get; set; }
        [StringLength(50)]
        [Display(Name = "Mídia Social: ")]
        public string VSocialMidiaProfile { get; set; }
        [Display(Name = "Ativo?: ")]
        public bool VActive { get; set; }

        //Relationship One volunteer has one Address
        [Display(Name = "Endereço: ")]
        public int AddressId { get; set; }
        public Address Address { get; set; }

        //One Volunteers Has Many TeamSchedule
        public ICollection<Schedule> TeamSchedules { get; set; }

        //Relation Many Volunteers Has Many Teams
        public ICollection<TeamVolunteer> TeamVolunteer { get; set; }

       public void AgeCalculator()
        {
            int age = DateTime.Now.Year - VBirthDate.Year;
            string day, month, year;
            day = VBirthDate.Day.ToString();
            month = VBirthDate.Month.ToString();
            year = DateTime.Now.Year.ToString();
            var aniverssary = DateTime.Parse(day + "/" + month + "/" + year);
            if (DateTime.Now<aniverssary)
            {
                VAge = age- 1;
            }
            else
            {
                VAge = age;
            }
            
        }
        
    }
}