using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeamApplication.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "City name cannot be longer than 100 characters.")]
        [Display(Name = "Cidade: ")]
        public string CityName { get; set; }

        //Relationship Between City and State - One State has many cities
        [Display(Name = "Estado: ")]
        public int StateId { get; set; }
        public State State { get; set; }

        //Relationship one City Has Many Address
        public ICollection<Address> Addresses { get; set; } //Relashionship
      //  public virtual ICollection<State>States { get; set; }
        


    }
}
