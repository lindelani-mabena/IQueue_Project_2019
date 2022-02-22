using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using WebApplication.Code;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [RoutePrefix("api/v1/addresses")]
    public class AddressesController : ApiController
    {
        private readonly DatabaseHelper _databaseHelper;

        public AddressesController() : this(new DatabaseHelper()) { }

        public AddressesController(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        // GET: api/v1/addresses
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var addressModels = await _databaseHelper.GetAddresses();
            if (addressModels.Count > 0) return Ok(addressModels);
            return Content(HttpStatusCode.NotFound, "No addresses found");
        }

        // GET: api/v1/addresses/5
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var addressModel = await _databaseHelper.GetAddress(id);
            if (addressModel != null) return Ok(addressModel);
            return Content(HttpStatusCode.NotFound, $"Address with id: {id} not found");
        }

        // POST: api/Addresses
        [Route("")]
        public async Task<IHttpActionResult> Post(AddressModel addressModel)
        {
            var address = await _databaseHelper.AddAddress(addressModel);
            if (address != null) return Ok(address);
            return Content(HttpStatusCode.BadRequest, "Address could not be added");
        }

        // PUT: api/Addresses/5
        [Route("{id}")]
        public async Task<IHttpActionResult> Put(int id, AddressModel addressModel)
        {
            var address = await _databaseHelper.UpdateAddress(id, addressModel);
            if (addressModel != null) return Ok(address);
            return Content(HttpStatusCode.BadRequest, $"Address with id: {id} not updated");
        }

        // DELETE: api/Addresses/5
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var addressModel = await _databaseHelper.DeleteAddress(id);
            if (addressModel != null) return Ok(addressModel);
            return Content(HttpStatusCode.BadRequest, $"Could not delete address with id: {id}");
        }
    }
}
