using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Code;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class BranchesController : Controller
    {
        private readonly ApiHelperv3<Branch> _client;

        public BranchesController()
        {
            _client = new ApiHelperv3<Branch>();
        }

        // GET: Branches
        public async Task<ActionResult> Index()
        {
            var branches = await _client.GetItems("branches");

            if (branches != null)
            {
                return await Task.Run(() => View(branches));
            }

            return await Task.Run(View);
        }

        // GET: Branches/{id}/Services
        [HttpGet("Branches/{id}/Services")]
        public async Task<ActionResult> Services(int id)
        {
            var branch = await _client.GetItem($"branches/{id}");

            if (branch != null)
            {
                return await Task.Run(() => View((List<Service>) branch.Services));
            }

            return await Task.Run(View);
        }

        // GET: Branches/List
        [HttpGet("Branches/List")]
        public async Task<ActionResult> List()
        {
            var branches = await _client.GetItems("branches");

            if (branches.Count > 0)
            {
                return await Task.Run(() => View(branches));
            }

            return await Task.Run(View);
        }

        // GET: Branches/Add
        public async Task<ActionResult> Add()
        {
            return await Task.Run(View);
        }

        public async Task<ActionResult> Select()
        {
            return await Task.Run(View);
        }

        // GET: Branches/5/Details
        [HttpGet("Branches/{id}/Details")]
        public async Task<ActionResult> Details(int id)
        {
            return await Task.Run(async () =>
            {
                var branch = await _client.GetItem($"branches/{id}");

                if (branch == null)
                {
                    //TODO: Handle Errors
                    return View();
                }

                ViewBag.Branch = branch;

                if (TempData["SuccessMessage"] != null)
                {
                    ViewBag.UpdateStatus = TempData["SuccessMessage"].ToString();
                }

                return View();
            });
        }

        // GET: Branches/Create
        [HttpGet("Branches/Create")]
        public ActionResult Create()
        {
            if (TempData["ErrorMessage"] == null) return View();

            ViewBag.Message = TempData["ErrorMessage"].ToString();

            return View();
        }

        // POST: Branches/Create
        [HttpPost("/Branches/Create")]
        public async Task<ActionResult> Create(Branch model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                _client.Authenticate(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value);

                var branch = await _client.PostItem("branches", model);

                if (branch == null)
                {
                    TempData["ErrorMessage"] = "Forbidden";
                    return View();
                }

                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }

        // GET: Branches/5/Update
        [HttpGet("Branches/{id}/Update")]
        public async Task<ActionResult> Update(int id)
        {
            var branch = await _client.GetItem($"branches/{id}");

            if (branch != null)
            {
                return await Task.Run(() =>
                {
                    ViewBag.Branch = branch;
                    return View();
                });
            }

            return View();
        }

        // POST: Branches/5/Update
        [Authorize(Roles = "admin")]
        [HttpPost("Branches/{id}/Update")]
        public async Task<ActionResult> Update(int id, Branch branch)
        {
            try
            {
                // TODO: Add update logic here

                _client.Authenticate(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value);

                await _client.PutItem($"branches/{id}", branch);

                TempData["SuccessMessage"] = "Branch Updated";

                return await Task.Run(() => RedirectToAction("Details", new { id }));
            }
            catch
            {
                return View();
            }
        }
    }
}