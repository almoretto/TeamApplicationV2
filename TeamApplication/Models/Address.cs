using System.ComponentModel.DataAnnotations;

namespace TeamApplication.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        [Required]
        [EnumDataType(typeof(AddressKind))]
        [Display(Name ="Tipo Logradouro - R, Av:")]
        public AddressKind AddressKind { get; set; }
        [Required]
        [StringLength(120)]
        [Display(Name = "Logradouro, número:")]
        public string Designation { get; set; }
        [Required]
        [StringLength(60)]
        [Display(Name = "Bairro:")]
        //public string  Number { get; set; }
        public string District { get; set; }
        [StringLength(60)]
        [Display(Name = "Complemento:")]
        public string Complement { get; set; }
        [Required]
        [StringLength(9, ErrorMessage = "ZipCode is 00000-000 Format.")]
        [Display(Name = "Complemento:")]
        public string ZipCode { get; set; }
        //public string  DesignationGroup { get; private set; }

        //Relationship One City has Many Address
        [Display(Name = "Cidade: ")]
        public int CityId { get; set; }
        public City City { get; set; }

        //Relationship one Volunteer has one address
        public Volunteer  Volunteer { get; set; } //Navigation 1 to 1 with volunte

        
        
    }
}
