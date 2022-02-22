using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationv2.Code;
using WebApplicationv2.Models;
using WebApplicationv2.Models.Views;

namespace WebApplicationv2.Controllers
{
    public class ManagersController : Controller
    {
        private readonly ApiHelperv3<Manager> _client;
        private readonly ApiHelperv3<ManagerModel> _managerClient;
        private readonly ApiHelperv3<Service> _servicesApi;
        private readonly ApiHelperv3<Branch> _branchService;

        public ManagersController()
        {
            _client = new ApiHelperv3<Manager>();
            _managerClient = new ApiHelperv3<ManagerModel>();
            _servicesApi = new ApiHelperv3<Service>();
            _branchService = new ApiHelperv3<Branch>();
        }

        // GET: Manager/Dashboard
        [Authorize(Roles = "manager")]
        [HttpGet("Manager/Dashboard")]
        public async Task<ActionResult> Index()
        {
            var branches = await _branchService.GetItems("branches");
            ViewData["heading"] = "Dashboard";
            
            if (branches != null) 
            {
                return await Task.Run(() => View(branches));
            }

            return await Task.Run(View);
        }

        // GET: Managers/List
        [HttpGet("Managers/List")]
        public async Task<ActionResult> List()
        {
            _managerClient.Authenticate(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value);

            var managers = await _managerClient.GetItems("managers/list");

            if (managers != null)
            {
                ViewData["heading"] = "Our Managers";
                return await Task.Run(() => View(managers));
            }

            return await Task.Run(View);
        }

        // GET: Managers/5dsad/Details
        [HttpGet("Managers/{id}/Details")]
        public async Task<ActionResult> Details(string id)
        {
            return await Task.Run(async () =>
            {
                var manager = await _client.GetItem($"managers/{id}");

                if (manager == null)
                {
                    //TODO: Handle Errors
                    return View();
                }

                if (TempData["SuccessMessage"] != null)
                {
                    ViewBag.UpdateStatus = TempData["SuccessMessage"].ToString();
                }

                return View(manager);
            });
        }

        // GET: Assign/Consultant/5
        [HttpGet("Assign/Consultant/{id}")]
        public async Task<ActionResult> AssignConsultant(string id)
        {
            _client.Authenticate(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value);
            await _client.PutItem($"Account/Assign/Consultant/{id}", null);
            return RedirectToAction(nameof(List), "Consultants");
        }

        // GET: UnAssign/Consultant/5
        [HttpGet("UnAssign/Consultant/{id}")]
        public async Task<ActionResult> UnAssignConsultant(string id)
        {
            _client.Authenticate(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value);
            await _client.PutItem($"Account/UnAssign/Consultant/{id}", null);
            return RedirectToAction(nameof(List), "Consultants");
        }

        [HttpGet("Manager/Profile")]
        public ActionResult Profile()
        {
            return View();
        }
    }
}