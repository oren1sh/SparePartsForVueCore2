using System.Collections.Generic;
using System.Threading.Tasks;
using CarTestCoreReVue.Model;

namespace CarTestCoreReVue.Data.Interfaces
{
    public interface ICarsRepository
    {
        Task<List<Car>> GetCarsAsync();
        Task<Car> UpdateCarAsync(Car upCar);
    }
}