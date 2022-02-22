using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using WebApplication.Code;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [RoutePrefix("api/v1/users")]
    public class AccountController : ApiController
    {
        private readonly DatabaseHelper _databaseHelper;

        public AccountController() : this(new DatabaseHelper())
        {
        }

        public AccountController(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        // GET: api/v1/users
        [AcceptVerbs("GET")]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var userModels = await _databaseHelper.GetUsers();
            if (userModels.Count != 0) return Ok(userModels);
            return Content(HttpStatusCode.NotFound, "No users found");
        }

        // GET: api/v1/users/5
        [HttpGet]
        [Route("{id}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            var userModel = await _databaseHelper.GetUser(id);
            if (userModel != null) return Ok(userModel);
            return Content(HttpStatusCode.NotFound, $"User with Id: {id} is not found");
        }

        // POST: api/v1/users
        [HttpPost]
        [ValidateModel]
        [Route("", Name = "UploadUser")]
        public async Task<IHttpActionResult> Post(UserModel userModel)
        {
            var user = await _databaseHelper.AddUser(userModel);
            if (user == null) return Content(HttpStatusCode.Conflict, "User already exists");
            return Ok(user);
        }

        // PUT: api/v1/users/5
        [Route("{id}", Name = "UpdateUserById")]
        [AcceptVerbs("PUT")]
        public async Task<IHttpActionResult> Put(int id, UserModel userModel)
        {
            var user = await _databaseHelper.UpdateUser(id, userModel);
            if (user != null) return Ok(user);
            return BadRequest($"Could not update user with Id: {id} by User: {userModel}");
        }

        // DELETE: api/v1/users/5
        [HttpDelete]
        [Route("{id}", Name = "DeleteUserById")]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            var userModel = await _databaseHelper.DeleteUser(id);
            if (userModel != null) return Ok(userModel);
            return BadRequest($"Could not delete user with Id: {id}");
        }
    }
}
