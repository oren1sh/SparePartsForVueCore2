using CarTestCoreReVue.Data.Interfaces;
using CarTestCoreReVue.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarTestCoreReVue.Data.Repositories
{
    public class CarsRepository : ICarsRepository
    {
        private readonly AppDbContext _context;

        public CarsRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Get All Cars
        /// </summary>
        /// <returns></returns>
        public async Task<List<Car>> GetCarsAsync()
        {
            return await _context.Cars.ToAsyncEnumerable().ToList();

        }
        /// <summary>
        /// update car plate and modelname
        /// </summary>
        /// <param name="upCar"></param>
        /// <returns></returns>
        public async Task<Car> UpdateCarAsync(Car upCar)
        {
            Car refCar = await _context.Cars.FirstOrDefaultAsync(c => c.Id == upCar.Id);
            if (refCar == null || refCar == default(Car))
            {
                return null;

            }
            refCar.ModelName = upCar.ModelName;
            refCar.Plate = upCar.Plate;

            _context.Update(refCar);

            await _context.SaveChangesAsync();

            return refCar;


        }
    }
}
