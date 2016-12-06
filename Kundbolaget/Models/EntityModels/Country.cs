using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kundbolaget.Models.EntityModels
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string CountryCode { get; set; }
        [Required]
        public string Region { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
    }
}