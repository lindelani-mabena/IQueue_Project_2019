using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationv2.Code;
using WebApplicationv2.Models;
using WebApplicationv2.Models.Views;

namespace WebApplicationv2.Controllers
{
    public class BranchesController : Controller
    {
        private readonly ApiHelperv3<Branch> _client;
        private readonly ApiHelperv3<Service> _clientServices;

        public BranchesController()
        {
            _client = new ApiHelperv3<Branch>();
            _clientServices = new ApiHelperv3<Service>();
        }

        // GET: Branches
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            var branches = await _client.GetItems("branches");

            if (branches != null)
            {
                return await Task.Run(() => View(branches));
            }

            return await Task.Run(View);
        }

        [HttpGet]
        public ActionResult CreateBranchModal()
        {
            var branch = new Branch();

            return PartialView("_CreateBranchModal", branch);
        }


        [HttpPost]
        public async Task<ActionResult> CreateBranchModal(Branch model)
        {
            if (ModelState.IsValid)
            {
                _client.Authenticate(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value);

                await _client.PostItem("branches", model);

                return RedirectToAction(nameof(List));
            }

            return PartialView("_CreateBranchModal", model);
        }

        // GET: Branches/{id}/Assign/Service
        [AllowAnonymous]
        [HttpGet("Branches/{id}/Assign/Service")]
        public async Task<ActionResult> AssignService(int id)
        {
            var services = await _clientServices.GetItems("services");

            if (services != null)
            {
                var branch = await _client.GetItem($"branches/{id}");
                if(branch != null) ViewBag.Branch = branch;
                return View(services);
            }

            return View();
        }

        // GET: Branches/{id}/Assign/Service/{serviceId}
        [HttpGet("Branches/{branchId}/Assign/Service/{serviceId}")]
        public async Task<ActionResult> AssignServiceAction(int branchId, int serviceId)
        {
            _client.Authenticate(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value);

            await _client.PostItem($"branches/{branchId}/Assign/Service/{serviceId}", null);
            return RedirectToAction(nameof(List));
        }

        // GET: Branches/{id}/Services
        [AllowAnonymous]
        [HttpGet("Branches/{id}/Services")]
        public async Task<ActionResult> Services(int id)
        {
            var branch = await _client.GetItem($"branches/{id}");

            if (branch != null)
            {
                return View(branch);
            }

            return await Task.Run(View);
        }

        // GET: Branches/List
        [HttpGet("Branches/List")]
        [AllowAnonymous]
        public async Task<ActionResult> List()
        {
            var branches = await _client.GetItems("branches");

            if (branches == null) return await Task.Run(View);
            
            ViewData["heading"] = "Branches";
            return await Task.Run(() => View(branches));
        }

        // GET: Branches/3/Assign/Consultant/1swe2
        [HttpGet("Branches/{branchId}/Assign/Consultant/{consultantId}")]
        [Authorize(Roles = "admin, manager")]
        public async Task<ActionResult> AssignConsultant(int branchId, string consultantId)
        {
            _client.Authenticate(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value);
            await _client.PostItem($"branches/{branchId}/Assign/Consultant/{consultantId}", null);
            return RedirectToAction(nameof(List));
        }

        // GET: Branches/3/UnAssign/Consultant/1swe2
        [HttpGet("Branches/{branchId}/UnAssign/Consultant/{consultantId}")]
        [Authorize(Roles = "admin, manager")]
        public async Task<ActionResult> UnAssignConsultant(int branchId, string consultantId)
        {
            _client.Authenticate(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value);
            await _client.PostItem($"branches/{branchId}/UnAssign/Consultant/{consultantId}", null);
            return RedirectToAction(nameof(List));
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
        [AllowAnonymous]
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
        [HttpPost("Branches/Create")]
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

            if (branch == null) return View();

            ViewData["heading"] = $"Update {branch.Name}";
            return await Task.Run(() => View(branch));

        }

        // POST: Branches/5/Update
        [Authorize(Roles = "admin, manager")]
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

        [AllowAnonymous]
        [HttpGet("Branches/Maps")]
        public async Task<ActionResult> Maps()
        {
            var branches = await _client.GetItems("Branches");
            if (branches == null)
            {
                return await Task.Run(View);
            }

            return await Task.Run(() => View(branches));
        }

        [AllowAnonymous]
        [HttpGet("Branches/Directions")]
        public async Task<ActionResult> Directions()
        {
            var branches = await _client.GetItems("Branches");
            if (branches == null)
            {
                return await Task.Run(View);
            }

            return await Task.Run(() => View(branches));
        }
    }
}