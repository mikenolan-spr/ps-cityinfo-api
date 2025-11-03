using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(city => city.Id == cityId);
            if (city == null)
            {
                return this.NotFound();
            }
            return this.Ok(city.PointsOfInterest);

        }

        [HttpGet("{poiId}", Name = "GetPointOfInterest")]
        public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int poiId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(city => city.Id == cityId);
            if (city == null)
            {
                return this.NotFound();
            }

            var poi = city.PointsOfInterest.FirstOrDefault(poiRef => poiRef.Id == poiId);
            return (poi != null) ? this.Ok(poi) : this.NotFound();
        }

        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(
            int cityId,
            PointOfInterestForCreationDto pointOfInterest
            )
        {
            // Not needed when controller is annotated with [ApiController], but keeping here
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(city => city.Id == cityId);
            if (city == null)
            {
                return this.NotFound();
            }

            var maxPointOfInterestId = CitiesDataStore.Current.Cities
                                            .SelectMany(city => city.PointsOfInterest).Max(poi => poi.Id);

            var finalPointOfInterest = new PointOfInterestDto()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointsOfInterest.Add(finalPointOfInterest);

            return this.CreatedAtRoute("GetPointOfInterest",
                new
                {
                    cityId = cityId,
                    poiId = finalPointOfInterest.Id
                },
                finalPointOfInterest);
        }
    }
}