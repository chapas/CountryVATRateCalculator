using CountryVATCalculator.Commands;
using CountryVATCalculator.Models;
using CountryVATCalculator.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CountryVATCalculator.Repositories
{
    public class CountryRepository : IRepository<Country>
    {
        private readonly ISafeCallService _safeCallService;
        private readonly IHttpHeaderAccessor _headerAccessor;

        public CountryRepository(ISafeCallService safeCallService, IHttpHeaderAccessor headerAccessor)
        {
            _safeCallService = safeCallService;
            _headerAccessor = headerAccessor;
        }

        public Task<int> CreateAsync(Country model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Country>> GetAllAsync()
        {
            var meta = new SafeCallServiceLogMetadata(
                _headerAccessor.CorrelationId,
                typeof(CountryRepository),
                nameof(GetAllAsync));

            var command = new SafeCallServiceQuery<List<Country>>(
                SimulatedDbGetAllCountriesCall,
                meta);

            return await _safeCallService.CallAsync(command);
        }

        public async Task<Country> GetByIdAsync(int id)
        {
            var meta = new SafeCallServiceLogMetadata(
                _headerAccessor.CorrelationId,
                typeof(CountryRepository),
                nameof(GetByIdAsync));

            var command = new SafeCallServiceQuery<Country>(
                SimulatedDbCall,
                meta);

            return await _safeCallService.CallAsync(command);
        }

        private async Task<List<Country>> SimulatedDbGetAllCountriesCall()
        {
            return new List<Country>()
            {
                new()
                {
                    Id = 1,
                    Name = "Austria",
                    VATRates = new List<int>() { 10, 13, 20 }
                },
                new()
                {
                    Id = 2,
                    Name = "Portugal",
                    VATRates = new List<int>() { 6, 13, 23 }
                },
                new()
                {
                    Id = 3,
                    Name = "United Kingdom",
                    VATRates = new List<int>() { 5, 20 }
                },
                new()
                {
                    Id = 4,
                    Name = "Singapore",
                    VATRates = new List<int>() { 7 }
                }
            };
        }


        public Task<Country> UpdateAsync(int id, Country model)
        {
            throw new NotImplementedException();
        }

        public async Task<Country> SimulatedDbCall()
        {
            await Task.Delay(1000);
            return null;
        }
    }
}
