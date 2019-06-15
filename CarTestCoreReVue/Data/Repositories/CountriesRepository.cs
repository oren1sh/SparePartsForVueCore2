using CarTestCoreReVue.Data.Interfaces;
using CarTestCoreReVue.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarTestCoreReVue.Data.Repositories
{
    public class CountriesRepository : ICountriesRepository
    {
        private readonly AppDbContext _context;

        public CountriesRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// get all countries and companies {and cars?}
        /// </summary>
        /// <returns></returns>
        public async Task<List<Country>> GetAllCountriesAsync()
        {
            return await _context.Countries.Include(c => c.Companies).ThenInclude(comp =>comp.Cars).ToAsyncEnumerable().ToList();//include list of lists

        }
    }
}
