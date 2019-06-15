using System.Collections.Generic;
using System.Threading.Tasks;
using CarTestCoreReVue.Model;

namespace CarTestCoreReVue.Data.Interfaces
{
    public interface ICompaniesRepository
    {
        Task<List<Company>> GetAllCompaniesAsync();
    }
}