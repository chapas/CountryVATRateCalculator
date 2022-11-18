namespace CountryVATCalculator.Models
{
    /// <summary>
    /// Defines the request for the VAT calculation
    /// </summary>
    public class CalculateVATRequest
    {
        /// <summary>
        /// Country name for VAT
        /// </summary>
        /// <remarks>Get or sets the country name property</remarks>
        public string Country { get; set; }

        /// <summary>
        /// VAT applicable rate
        /// </summary>
        public int VATRate { get; set; }

        /// <summary>
        /// Price without VAT
        /// </summary>
        public double PriceWithoutVAT { get; set; }

        /// <summary>
        /// Value Added Tax
        /// </summary>
        public double ValueAddedTax { get; set; }

        /// <summary>
        /// Price Including VAT
        /// </summary>
        public double PriceIncludingVAT { get; set; }
    }
}