using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Tracing;
using WebApplication.Code;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [RoutePrefix("api/v1/cities")]
    public class CitiesController : ApiController
    {
        private readonly DatabaseHelper _databaseHelper;

        public CitiesController(): this(new DatabaseHelper()) { }
        
        public CitiesController(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        // GET: api/v1/cities
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            Configuration.Services.GetTraceWriter().Info(
                Request, "CitiesController", "Get the list of cities.");

            var cities = await _databaseHelper.GetCities();
            if (cities.Count > 0) return Ok(cities);
            return Content(HttpStatusCode.NotFound, "No cities found");
        }

        // GET: api/v1/cities/5
        [Route("{cityId}")]
        public async Task<IHttpActionResult> Get(int cityId)
        {
            Configuration.Services.GetTraceWriter().Info(
                Request, "CitiesController", $"Get city with id: {cityId}");

            var city = await _databaseHelper.GetCity(cityId);
            if (city != null) return Ok(city);
            return Content(HttpStatusCode.NotFound, $"City with id: {cityId} not found");
        }

        // POST: api/v1/cities
        [Route("")]
        public async Task<IHttpActionResult> Post(CityModel cityModel)
        {
            Configuration.Services.GetTraceWriter().Info(
                Request, "CitiesController", $"Post city: {cityModel}");

            var city = await _databaseHelper.AddCity(cityModel);
            if (city != null) return Ok(city);
            return BadRequest("City not created");
        }

        // PUT: api/V1/cities/5
        [Route("{cityId}")]
        public async Task<IHttpActionResult> Put(int cityId, CityModel cityModel)
        {
            Configuration.Services.GetTraceWriter().Info(
                Request, "CitiesController", $"Put city with id: {cityId} by city: {cityModel}");

            var city = await _databaseHelper.UpdateCity(cityId, cityModel);
            if (city != null) return Ok(city);
            return BadRequest($"City with id: {cityId} not updated");
        }

        // DELETE: api/v1/cities/5
        [Route("{cityId}")]
        public async Task<IHttpActionResult> Delete(int cityId)
        {
            Configuration.Services.GetTraceWriter().Info(
                Request, "CitiesController", $"Delete city with id: {cityId}");

            var city = await _databaseHelper.DeleteCity(cityId);
            if (city != null) return Ok(city);
            return Content(HttpStatusCode.BadRequest, $"City with id: {cityId} not deleted");
        }
    }
}
