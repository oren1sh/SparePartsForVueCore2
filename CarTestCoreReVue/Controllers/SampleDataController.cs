using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTestCoreReVue.Data;
using CarTestCoreReVue.Data.Interfaces;
using CarTestCoreReVue.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarTestCoreReVue.Controllers
{

    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private readonly AppDbContext _context;

        private readonly ICarsRepository _cars;

        private readonly ICompaniesRepository _companies;

        private readonly ICountriesRepository _countries;




        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public SampleDataController(AppDbContext context, ICarsRepository cars, ICompaniesRepository companies, ICountriesRepository countries)
        {
            _context = context;
            _cars = cars;
            _companies = companies;
            _countries = countries;
        }
        /// <summary>
        /// get all countries data
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<List<Country>> GetCountriesAsync()
        {
            return await _countries.GetAllCountriesAsync();

        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {

            List<Company> refCompanies = await _companies.GetAllCompaniesAsync();
           //JsonResult
            return refCompanies;
            //JsonConvert()

        }

        [HttpPost("[action]")]
        public async Task<Car> UpdateCarAsync([FromBody]Car upCar)
        {
            try
            {
                return await _cars.UpdateCarAsync(upCar);
            }
            catch (Exception)
            {


                
            }

            return null;
        }


        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
