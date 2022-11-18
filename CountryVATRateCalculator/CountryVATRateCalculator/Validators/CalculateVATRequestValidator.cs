using CountryVATCalculator.Models;
using FluentValidation;

namespace CountryVATCalculator.Validators
{
    public class CalculateVATRequestValidator : AbstractValidator<CalculateVATRequest>
    {
        public CalculateVATRequestValidator()
        {
            RuleFor(p => p.VATRate)
                .NotEmpty().WithMessage("{PropertyName} should be not empty. NEVER!")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero");

            RuleFor(p => p.Country)
                .NotEmpty().WithMessage("{PropertyName} should be not empty. NEVER!")
                .Must(ValidCountry).WithMessage("{PropertyName} name not allowed!")
                .Must(ValidVatRateForCountry).WithMessage("{PropertyName} VAT rate not allowed!");

            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x)
                .Must(request => request.ValueAddedTax > 0 || request.PriceWithoutVAT > 0 || request.PriceIncludingVAT > 0)
                .WithMessage("One of these: 'priceWithoutVAT', 'valueAddedTax' or 'priceIncludingVAT' must be greater than zero");

            RuleFor(x => x.PriceIncludingVAT)
                .GreaterThan(0)
                .When(request => request.ValueAddedTax == 0 & request.PriceWithoutVAT == 0)
                .WithMessage("{PropertyName} must be greater than zero");

            RuleFor(x => x.ValueAddedTax)
                .GreaterThan(0)
                .When(request => request.PriceIncludingVAT == 0 & request.PriceWithoutVAT == 0)
                .WithMessage("{PropertyName} must be greater than zero");

            RuleFor(x => x.PriceWithoutVAT)
                .GreaterThan(0)
                .When(request => request.ValueAddedTax == 0 & request.PriceIncludingVAT == 0)
                .WithMessage("{PropertyName} must be greater than zero");
        }

        private bool ValidVatRateForCountry(CalculateVATRequest arg1, string arg2)
        {
            switch (arg2.ToLower())
            {
                case "austria":
                    if (arg1.VATRate is 10 or 13 or 20)
                        return true;
                    break;
                case "portugal":
                    if (arg1.VATRate is 6 or 13 or 23)
                        return true;
                    break;
                case "united kingdom":
                    if (arg1.VATRate is 5 or 20)
                        return true;
                    break;
                case "singapore":
                    if (arg1.VATRate == 7)
                        return true;
                    break;
                default:
                    return false;
            }

            return false;
        }

        private bool ValidCountry(string arg)
        {
            if (arg.ToLower() == "austria" || 
                arg.ToLower() == "singapore" ||
                arg.ToLower() == "portugal" || 
                arg.ToLower() == "united kingdom")
                return true;

            return false;
        }
    }
}