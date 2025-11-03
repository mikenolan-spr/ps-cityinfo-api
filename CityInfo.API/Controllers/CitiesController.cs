using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers 
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController  : ControllerBase
    {
        [HttpGet()]
        public /*JsonResult*/ ActionResult<IEnumerable<CityDto>> GetCities()
        {
            return this.Ok(CitiesDataStore.Current.Cities);
            //return new JsonResult(CitiesDataStore.Current.Cities);
        }

        [HttpGet("{id}")]
        public /*JsonResult*/ ActionResult<CityDto> GetCity(int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(city => city.Id == id);
            return (city != null) ? this.Ok(city) : this.NotFound();
            //return new JsonResult(CitiesDataStore.Current.Cities.FirstOrDefault(city => city.Id == id));
        }
    }
}