using System.Collections.Generic;
using System.Threading.Tasks;
using CarTestCoreReVue.Model;

namespace CarTestCoreReVue.Data.Interfaces
{
    public interface ICountriesRepository
    {
        Task<List<Country>> GetAllCountriesAsync();
    }
}