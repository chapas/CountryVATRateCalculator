using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CountryVATCalculator.Models
{
    /// <summary>
    /// Defines a Country
    /// </summary>
    public class Country
    {
        /// <summary>
        /// Reference
        /// </summary>
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public List<int> VATRates { get; set; }
    }
}