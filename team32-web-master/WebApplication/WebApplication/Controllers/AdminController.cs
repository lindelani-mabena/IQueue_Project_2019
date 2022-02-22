using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Code;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApiHelperv3<Admin> _client;

        public AdminController()
        {
            _client = new ApiHelperv3<Admin>();
        }

        // GET: Admin
        [Authorize]
        [HttpGet("Admin")]
        public async Task<ActionResult> Index()
        {

            if (User.IsInRole("admin"))
            {
                return await Task.Run(View);
            }

            return Redirect("/Account/Logout");
        }
        // GET: Admin/Dashboard
        [HttpGet("Admin/Dashboard")]
        public async Task<ActionResult> Dashboard()
        {
            return await Task.Run(View);
        }
    }
}