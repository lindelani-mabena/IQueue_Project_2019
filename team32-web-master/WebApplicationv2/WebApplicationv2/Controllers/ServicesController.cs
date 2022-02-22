using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationv2.Models;
using WebApplicationv2.Code;


namespace WebApplicationv2.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ApiHelperv3<Service> _client;
        private readonly ApiHelperv3<Branch> _branch;
        private readonly ApiHelperv3<Ticket> _ticket;

        public ServicesController()
        {
            _client = new ApiHelperv3<Service>();
            _branch = new ApiHelperv3<Branch>();
            _ticket = new ApiHelperv3<Ticket>();
        }

        // GET: Services/1/Tickets
        [AllowAnonymous]
        [HttpGet("Branches/{branchId}/Services/{serviceId}/Tickets")]
        public async Task<ActionResult> Tickets(int branchId, int serviceId)
        {
            var branch = await _branch.GetItem($"branches/{branchId}");
            var service = await _client.GetItem($"services/{serviceId}");

            if (service != null)
            {
                if(branch != null)
                    ViewBag.Branch = branch;
                return await Task.Run(() => View(service));
            }

            return await Task.Run(View);
        }

        [AllowAnonymous]
        [HttpGet("Services/{serviceId}/GetTickets")]
        public async Task<JsonResult> GetTickets(int serviceId)
        {
            var service = await _client.GetItem($"services/{serviceId}");
            var res = new JsonResult(service);
            return res;
        }

        // GET: Services/3/Join
        [AllowAnonymous]
        [HttpGet("/Branches/{branchId}/Services/{serviceId}/Join")]
        public async Task<ActionResult> Join(int branchId, int serviceId)
        {
            if (User.IsInRole("client"))
            {
                _client.Authenticate(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value);
            }

            await _client.PostItem($"services/{serviceId}/join/branches/{branchId}", null);

            return RedirectToAction(nameof(Tickets), new {branchId, serviceId});
        }

        [AllowAnonymous]
        [HttpGet("/Branches/{branchId}/Services/{serviceId}/Join/New")]
        public async Task<JsonResult> JoinService(int branchId, int serviceId)
        {
            if (User.IsInRole("client"))
            {
                _client.Authenticate(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value);
            }

            var ticket = await _ticket.PostItem($"services/{serviceId}/join/branches/{branchId}", null);

            return ticket == null ? new JsonResult(null) : new JsonResult(ticket);
        }

        // GET: Services/List
        [AllowAnonymous]
        [HttpGet("Services/List")]
        public async Task<ActionResult> List()
        {
            var services = await _client.GetItems("services");
            
            if (services != null)
            {
                ViewData["heading"] = "Services";

                return await Task.Run(() => View(services));
            }

            return await Task.Run(View);
        }

        // GET: Services/Create
        [HttpGet("Services/Create")]
        public ActionResult Create()
        {
            ViewData["heading"] = "Create Service";
            if (TempData["ErrorMessage"] == null) return View();
            ViewBag.Message = TempData["ErrorMessage"].ToString();
            return View();
        }

        // POST: Services/Create
        [HttpPost("Services/Create")]
        public async Task<ActionResult> Create(Service model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                _client.Authenticate(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value);

                var branch = await _client.PostItem("services", model);

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

        // GET: Services/5/Update
        [HttpGet("Services/{id}/Update")]
        public async Task<ActionResult> Update(int id)
        {
            var service = await _client.GetItem($"services/{id}");

            if (service != null)
            {
                return await Task.Run(() =>
                {
                    ViewBag.Service = service;
                    return View(service);
                });
            }

            return View();
        }

        // POST: Services/5/Update
        [Authorize(Roles = "admin")]
        [HttpPost("Services/{id}/Update")]
        public async Task<ActionResult> Update(int id, Service service)
        {
            try
            {
                // TODO: Add update logic here

                _client.Authenticate(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value);

                service.LastUpdate = DateTime.Now;
                service.InitialDate = DateTime.Now;

                await _client.PutItem($"services/{id}", service);

                TempData["SuccessMessage"] = "Service Updated";

                return await Task.Run(() => RedirectToAction("Details", new { id }));
            }
            catch
            {
                return View();
            }
        }

        // GET: Services/5/Details
        [HttpGet("Services/{id}/Details")]
        [AllowAnonymous]
        public async Task<ActionResult> Details(int id)
        {
            return await Task.Run(async () =>
            {
                var service = await _client.GetItem($"services/{id}");

                if (service == null)
                {
                    //TODO: Handle Errors
                    return View();
                }

                ViewBag.Service = service;

                if (TempData["SuccessMessage"] != null)
                {
                    ViewBag.UpdateStatus = TempData["SuccessMessage"].ToString();
                }

                return View(service);
            });
        }
    }
}