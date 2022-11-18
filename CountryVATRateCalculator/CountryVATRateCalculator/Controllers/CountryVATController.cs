using CountryVATCalculator.Models;
using CountryVATCalculator.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CountryVATCalculator.Controllers
{
    /// <summary>
    /// VAT Controller
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    [Produces("application/json")]
    public class CountryVATController : Controller
    {
        private readonly IRepository<Country> _repository;
        private IValidator<CalculateVATRequest> _validator;

        public CountryVATController(IRepository<Country> repository,
            IValidator<CalculateVATRequest> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        /// <summary>
        /// Gets a <see cref="Country"/> by Id
        /// </summary>
        /// <param name="id">Item Id</param>
        /// <returns>ToDo Item</returns>
        [HttpGet("{id:int}")]
        [ProducesDefaultResponseType(typeof(Country))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var item = await _repository.GetByIdAsync(id);

            if (item == null)
            {
                return NotFound(new { id });
            }

            return Ok(item);
        }

        /// <summary>
        /// Return a list of available <see cref="Country"/>
        /// </summary>
        /// <returns>A list of <see cref="Country"/></returns>
        [HttpGet("GetAll")]

        [ProducesDefaultResponseType(typeof(List<Country>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllCountries()
        {
            var item = await _repository.GetAllAsync();

            if (!item.Any())
            {
                return NotFound();
            }

            return Ok(item);
        }

        /// <summary>
        /// Return a list of available <see cref="Country"/>
        /// </summary>
        /// <returns>A list of <see cref="Country"/></returns>
        [HttpPost("Calculate")]
        [ProducesDefaultResponseType(typeof(List<Country>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CalculateVATforCountry([FromBody] CalculateVATRequest calculateVatRequest)
        {
            var item = await _repository.GetAllAsync();

            ValidationResult result = await _validator.ValidateAsync(calculateVatRequest);

            if (!result.IsValid)
            {
                result.AddToModelState(ModelState);

                return BadRequest(result);
            }

            //Does not belong here, should go on it's own class
            return Ok(CalculateVAT(calculateVatRequest));
        }

        /// <summary>
        /// Calculates the VAT
        /// </summary>
        /// <param name="calculateVatRequest"></param>
        /// <returns></returns>
        private CalculateVATResponse CalculateVAT(CalculateVATRequest calculateVatRequest)
        {
            CalculateVATResponse calculateVATResponse = null;

            double vatRate = (double)calculateVatRequest.VATRate / 100;

            if (calculateVatRequest.PriceWithoutVAT > 0)
            {
                calculateVATResponse = new CalculateVATResponse()
                {
                    PriceWithoutVAT = calculateVatRequest.PriceWithoutVAT,
                    ValueAddedTax = Math.Round(calculateVatRequest.PriceWithoutVAT * vatRate, 2),
                    PriceIncludingVAT = Math.Round(calculateVatRequest.PriceWithoutVAT * (1 + vatRate), 2)
                };
            }

            if (calculateVatRequest.ValueAddedTax > 0)
            {
                calculateVATResponse = new CalculateVATResponse()
                {
                    PriceWithoutVAT = Math.Round(calculateVatRequest.ValueAddedTax / vatRate, 2),
                    ValueAddedTax = calculateVatRequest.ValueAddedTax,
                    PriceIncludingVAT = Math.Round(calculateVatRequest.ValueAddedTax * (1 + vatRate) / vatRate, 2),
                };
            }

            if (calculateVatRequest.PriceIncludingVAT > 0)
            {
                calculateVATResponse = new CalculateVATResponse()
                {
                    PriceWithoutVAT = Math.Round(calculateVatRequest.PriceIncludingVAT / (1 + vatRate), 2),
                    ValueAddedTax = Math.Round(calculateVatRequest.PriceIncludingVAT * vatRate / (1 + vatRate), 2),
                    PriceIncludingVAT = calculateVatRequest.PriceIncludingVAT,
                };
            }

            return calculateVATResponse;
        }
    }
}
