using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Code;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ClientsController : Controller
    {
        private readonly ApiHelperv3<Client> _client;

        public ClientsController()
        {
            _client = new ApiHelperv3<Client>();
        }

        // GET: Clients
        public ActionResult Index()
        {
            return View();
        }

        // GET: Clients/5c6a/Details
        [HttpGet("Clients/{id}/Details")]
        public async Task<ActionResult> Details(string id)
        {
            return await Task.Run(async () =>
            {
                var client = await _client.GetItem($"clients/{id}");

                if (client == null)
                {
                    //TODO: Handle Errors
                    return View();
                }

                if (TempData["SuccessMessage"] != null)
                {
                    ViewBag.UpdateStatus = TempData["SuccessMessage"].ToString();
                }

                return View(client);
            });
        }

        // GET: Clients/List
        [HttpGet("Clients/List")]
        public async Task<ActionResult> List()
        {
            var clients = await _client.GetItems("clients");

            if (clients.Count > 0)
            {
                return await Task.Run(() => View(clients));
            }

            return await Task.Run(View);
        }

        public bool ValidateAccess()
        {
            if (Request.Cookies["Token"] == null)
            {
                return false;
            }
            _client.Authenticate(Request.Cookies["Token"]);
            return true;
        }
    }
}