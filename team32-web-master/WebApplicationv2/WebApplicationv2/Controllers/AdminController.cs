using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationv2.Code;
using WebApplicationv2.Models;

namespace WebApplicationv2.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApiHelperv3<Admin> _client;
        private readonly ApiHelperv3<Branch> _branchApi;
        private readonly ApiHelperv3<Service> _services;

        public AdminController()
        {
            _client = new ApiHelperv3<Admin>();
            _services = new ApiHelperv3<Service>();
            _branchApi = new ApiHelperv3<Branch>();
        }

        // GET: Admin/Dashboard
        [Authorize(Roles = "admin")]
        [HttpGet("Admin/Dashboard")]
        public async Task<ActionResult> Index()
        {
            var branches = await _branchApi.GetItems("branches");
            var services = await _services.GetItems("services");
            ViewData["heading"] = "Admin";

            if (branches != null)
            {
                if (services == null)
                {
                    return await Task.Run(() => View(branches));
                }
                else
                {
                    ViewBag.Services = services;
                    return await Task.Run(() => View(branches));
                }
                
            }
               
            return await Task.Run(View);
        }

        // GET: Assign/Manager/5
        [HttpGet("Assign/Manager/{id}")]
        public async Task<ActionResult> AssignManager(string id)
        {
            _client.Authenticate(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value);
            await _client.PutItem($"Account/Assign/Manager/{id}", null);
            return RedirectToAction("List", "Clients");
        }

        // GET: UnAssign/Manager/5
        [HttpGet("UnAssign/Manager/{id}")]
        public async Task<ActionResult> UnAssignManager(string id)
        {
            _client.Authenticate(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value);
            await _client.PutItem($"Account/UnAssign/Manager/{id}", null);
            return RedirectToAction("List", "Managers");
        }

        [HttpGet("Admin/Profile")]
        public ActionResult Profile()
        { 
            return View();
        }
    }
}