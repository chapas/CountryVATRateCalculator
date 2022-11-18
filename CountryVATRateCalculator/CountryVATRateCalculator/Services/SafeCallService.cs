﻿using System;
using System.Threading.Tasks;
using CountryVATCalculator.Commands;
using Serilog;

namespace CountryVATCalculator.Services
{
    /// <inheritdoc/>
    public class SafeCallService : ISafeCallService
    {
        private readonly ILogger _logger;

        public SafeCallService(ILogger logger)
        {
            _logger = logger;
        }

        public async Task CallAsync(SafeCallServiceCommand command)
        {
            _logger.ForContext(command.Metadata.Context);

            try
            {
                using (_logger.BeginTimedOperation(command.Metadata.Name, identifier: command.Metadata.CorrelationId.ToString()))
                {
                    _logger.Information("Call invoked");
                    await command.Func.Invoke();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Failed {command.Metadata.Context.Name}");
            }
        }

        public async Task<T> CallAsync<T>(SafeCallServiceQuery<T> command)
        {
            _logger.ForContext(command.Metadata.Context);

            try
            {
                using (_logger.BeginTimedOperation(command.Metadata.Name, identifier: command.Metadata.CorrelationId.ToString()))
                {
                    _logger.Information("Call invoked");
                    return await command.Func.Invoke();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Failed {command.Metadata.Context.Name}");
                return default(T);
            }
        }
    }
}
