using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Code;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ManagersController : Controller
    {
        private readonly ApiHelperv3<Manager> _client;

        public ManagersController()
        {
            _client = new ApiHelperv3<Manager>();
        }

        // GET: Managers
        public ActionResult Index()
        {
            return View();
        }

        // GET: Managers/List
        [HttpGet("Managers/List")]
        public async Task<ActionResult> List()
        {
            var managers = await _client.GetItems("managers");

            if (managers.Count > 0)
            {
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
    }
}