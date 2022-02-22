using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Tracing;
using WebApplication.Code;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [RoutePrefix("api/v1/provinces")]
    public class ProvincesController : ApiController
    {
        private readonly DatabaseHelper _databaseHelper;

        public ProvincesController() : this(new DatabaseHelper()) { }
        public ProvincesController(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }
        //[Authorize]
        [Route("")]
        // GET: api/Provinces
        public async Task<IHttpActionResult> Get()
        {
            Configuration.Services.GetTraceWriter().Info(
                Request, "ProvincesController", "Get the list of provinces.");

            var province = await _databaseHelper.GetProvinces();
            if (province.Count != 0) return Ok(province);
            return Content(HttpStatusCode.NotFound, "No provinces found");
        }

        //[Authorize]
        // GET: api/v1/provinces/5
        [Route("{provinceId}")]
        public async Task<IHttpActionResult> Get(int provinceId)
        {
            Configuration.Services.GetTraceWriter().Info(
                Request, "ProvincesController", $"Get province with id: {provinceId}");

            var province = await _databaseHelper.GetProvince(provinceId);
            if (province != null) return Ok(province);
            return Content(HttpStatusCode.NotFound, $"Province with id: {provinceId} not found.");
        }

        //[Authorize(Roles = "Administrator")]
        // POST: api/v1/provinces
        [Route("")]
        [ValidateModel]
        public async Task<IHttpActionResult> Post(ProvinceModel provinceModel)
        {
            Configuration.Services.GetTraceWriter().Info(
                Request, "ProvincesController", $"Post province: {provinceModel}");

            var province = await _databaseHelper.AddProvince(provinceModel);
            if (province != null) return Ok(province);
            return Content(HttpStatusCode.BadRequest, province);
        }

        //[Authorize(Roles = "Administrator")]
        // PUT: api/v1/provinces/5
        [Route("{provinceId}")]
        public async Task<IHttpActionResult> Put(int provinceId, ProvinceModel provinceModel)
        {
            Configuration.Services.GetTraceWriter().Info(
                Request, "ProvincesController", $"Put province with id: {provinceId} by province: {provinceModel}");

            var province = await _databaseHelper.UpdateProvince(provinceId, provinceModel);
            if (province != null) return Ok(province);
            return Content(HttpStatusCode.BadRequest, $"Province with id: {provinceId} is not updated");
        }

        //[Authorize(Roles = "Administrator")]
        // DELETE: api/Provinces/5
        [Route("{provinceId}")]
        public async Task<IHttpActionResult> Delete(int provinceId)
        {
            Configuration.Services.GetTraceWriter().Info(
                Request, "ProvincesController", $"Delete province with id: {provinceId}");

            var province = await _databaseHelper.DeleteProvince(provinceId);
            if (province != null) return Ok(province);
            return Content(HttpStatusCode.BadRequest, $"Province with id: {provinceId} could not be deleted");
        }
    }
}
