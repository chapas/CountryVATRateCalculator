using CountryVATCalculator.Commands;
using System.Threading.Tasks;

namespace CountryVATCalculator.Services
{

    /// <summary>
    /// Wrapping to make an IO call safely and with log relevant information
    /// </summary>
    public interface ISafeCallService
    {
        Task CallAsync(SafeCallServiceCommand command);
        Task<T> CallAsync<T>(SafeCallServiceQuery<T> command);
    }
}