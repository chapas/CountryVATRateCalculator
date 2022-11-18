namespace CountryVATCalculator.Models
{
    /// <summary>
    /// Defines the response for the VAT calculation
    /// </summary>
    public class CalculateVATResponse
    {
        /// <summary>
        /// Price without VAT
        /// </summary>
        public double PriceWithoutVAT { get; set; }

        /// <summary>
        /// Value-Added Tax
        /// </summary>
        public double ValueAddedTax { get; set; }


        /// <summary>
        /// Price incl. VAT
        /// </summary>
        public double PriceIncludingVAT { get; set; }
    }
}