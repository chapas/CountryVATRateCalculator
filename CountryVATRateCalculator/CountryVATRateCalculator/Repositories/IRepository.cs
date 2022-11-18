using System.Collections.Generic;
using System.Threading.Tasks;

namespace CountryVATCalculator.Repositories
{
    /// <summary>
    /// Repository definition
    /// </summary>
    /// <typeparam name="TModel">DTO model stored</typeparam>
    public interface IRepository<TModel> where TModel : class
    {
        Task<int> CreateAsync(TModel model);
        Task<TModel> GetByIdAsync(int id);
        Task<TModel> UpdateAsync(int id, TModel model);
        Task<bool> DeleteByIdAsync(int id);
        Task<List<TModel>> GetAllAsync();
    }
}
