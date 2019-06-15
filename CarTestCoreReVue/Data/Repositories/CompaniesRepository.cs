using CarTestCoreReVue.Data.Interfaces;
using CarTestCoreReVue.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarTestCoreReVue.Data.Repositories
{
    public class CompaniesRepository : ICompaniesRepository
    {
        private readonly AppDbContext _context;

        public CompaniesRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// get all companies
        /// </summary>
        public async Task<List<Company>> GetAllCompaniesAsync()
        {
            return await _context.Companies.Include(c => c.Cars).ToAsyncEnumerable().ToList();//include list


        }


    }
}
