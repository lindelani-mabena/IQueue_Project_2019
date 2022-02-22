using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Code;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ConsultantsController : Controller
    {
        private readonly ApiHelperv3<Consultant> _client;
        private readonly ApiHelperv3<Branch> _branchClient;

        public ConsultantsController()
        {
            _client = new ApiHelperv3<Consultant>();
            _branchClient = new ApiHelperv3<Branch>();
        }

        // GET: Consultants
        public ActionResult Index()
        {
            return View();
        }

        // GET: Consultants/List
        [HttpGet("Consultants/List")]
        public async Task<ActionResult> List()
        {
            var consultants = await _client.GetItems("consultants");
            var branches = await _branchClient.GetItems("branches");

            if (consultants.Count > 0)
            {
                if (branches.Count > 0)
                {
                    ViewBag.Branches = branches;
                }
                return await Task.Run(() => View(consultants));
            }

            return await Task.Run(View);
        }

        // GET: Consultants/5sda/Details
        [HttpGet("Consultants/{id}/Details")]
        public async Task<ActionResult> Details(string id)
        {
            return await Task.Run(async () =>
            {
                var consultant = await _client.GetItem($"consultants/{id}");

                if (consultant == null)
                {
                    //TODO: Handle Errors
                    return View();
                }

                if (TempData["SuccessMessage"] != null)
                {
                    ViewBag.UpdateStatus = TempData["SuccessMessage"].ToString();
                }

                return View(consultant);
            });
        }

        // GET: Consultants/Create
        public ActionResult Create()
        {
            return View();
        }
    }
}