using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;
using WebApplication.Code;
using System;

namespace WebApplication.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ApiHelperv3<Service> _client;

        public ServicesController()
        {
            _client = new ApiHelperv3<Service>();
        }

        // GET: Services/1/Tickets
        [HttpGet("Services/{id}/Tickets")]
        public async Task<ActionResult> Tickets(int id)
        {
            var service = await _client.GetItem($"services/{id}");

            if (service != null)
            {
                ViewBag.Service = service;
                return await Task.Run(() => View((List<Ticket>) service.Tickets));
            }

            return await Task.Run(View);
        }

        // GET: Services/3/Join
        [HttpGet("Services/{id}/Join")]
        public async Task<ActionResult> Join(int id)
        {
            await _client.PostItem($"services/{id}/join", null); 

            return RedirectToAction(nameof(Tickets), new {id});
        }

        // GET: Services/List
        [HttpGet("Services/List")]
        public async Task<ActionResult> List()
        {
            var services = await _client.GetItems("services");

            if (services.Count > 0)
            {
                return await Task.Run(() => View(services));
            }

            return await Task.Run(View);
        }

        // GET: Services/Create
        [HttpGet("Services/Create")]
        public ActionResult Create()
        {
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
    }
}