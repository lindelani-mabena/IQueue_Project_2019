using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationv2.Code;
using WebApplicationv2.Models;
using WebApplicationv2.Models.Views;

namespace WebApplicationv2.Controllers
{
    public class ConsultantsController : Controller
    {
        private readonly ApiHelperv3<ConsultantModel> _client;
        private readonly ApiHelperv3<Branch> _branchClient;
        private readonly ApiHelperv3<Service> _serviceClient;
        private readonly ApiHelperv3<Ticket> _ticketClient;

        public ConsultantsController()
        {
            _client = new ApiHelperv3<ConsultantModel>();
            _branchClient = new ApiHelperv3<Branch>();
            _serviceClient = new ApiHelperv3<Service>();
            _ticketClient = new ApiHelperv3<Ticket>();
        }

        // GET: Consultant/Assign
        [Authorize(Roles = "admin, manager")]
        [HttpGet("Consultant/Assign/{branchId}")]
        public async Task<ActionResult> Assign(int branchId)
        {
            _client.Authenticate(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value);
            ViewBag.branchId = branchId;
            var consultants = await _client.GetItems("consultants");
            if (consultants == null) return await Task.Run(View);
            ViewData["heading"] = "Assign Consultant";
            return await Task.Run(() => View(consultants));
        }

        // GET: Consultant/Dashboard
        [Authorize(Roles = "consultant")]
        [HttpGet("Consultant/Dashboard")]
        public async Task<ActionResult> Index()
        {
            _client.Authenticate(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value);
            var consultant = await _client.GetItem("Consultant/Profile");
            if (consultant == null) 
                return await Task.Run(View);
            return await Task.Run(() => View(consultant));
        }

        // GET: Consultants/List
        [HttpGet("Consultants/List")]
        public async Task<ActionResult> List()
        {
            _client.Authenticate(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value); 

            var consultants = await _client.GetItems("consultants");

            if (consultants != null)
            {
                ViewData["heading"] = "Our Consultants";
                return  View(consultants);
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

        // GET: Consultant/Services
        [Authorize(Roles = "consultant")]
        [HttpGet("Consultant/Services")]
        public async Task<ActionResult> Services()
        {
            _serviceClient.Authenticate(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value);

            var services = await _serviceClient.GetItems("consultant/services");

            if (services != null)
            {
                return View(services);
            }

            return View();
        }

        // GET: Consultants/Dashboard
        [HttpGet("Consultants/Dashboard")]
        public async Task<ActionResult> Dashboard()
        {
            var branches = await _branchClient.GetItems("branches");

            if (branches != null)
            {
                return View(branches);
            }

            return View();
        }

        // GET: Consultant/Consult
        [HttpGet("Consultant/Consultation/Services/{serviceId}")]
        public async Task<ActionResult> Consult(int serviceId)
        {
            var service = await _serviceClient.GetItem($"services/{serviceId}");
            var start = true;

            if (service == null) return await Task.Run(View);

            var ticket = service.Tickets.FirstOrDefault(x => x.StartAt != TimeSpan.Zero && x.EndAt == TimeSpan.Zero && x.ConsultantId != null);

            if (ticket != null)
            {
                start = false;
            }
            ViewBag.Start = start;

            return await Task.Run(() => View(service));
        }

        // GET: Consultant/Profile
        [HttpGet("Consultant/Profile")]
        public async Task<ActionResult> Profile()
        {
            _client.Authenticate(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value);

            var consultantModel = await _client.GetItem("Consultant/Profile");

            if (consultantModel == null)
            {
                return await Task.Run(View);
            }

            return await Task.Run(() => View(consultantModel));
        }

        // GET: Consultants/Branch/5/Services
        [HttpGet("Consultants/Branch/{id}/Services")]
        public async Task<ActionResult> Services(int id)
        {
            var branch = await _branchClient.GetItem($"branches/{id}");

            if (branch != null)
            {
                return View(branch);
            }

            return View();
        }

        // GET: Consultants/Branches/4/Service/5/Tickets
        [HttpGet("Consultants/Branches/{branchId}/Services/{serviceId}/Tickets")]
        public async Task<ActionResult> Tickets(int branchId, int serviceId)
        {
            var branch = await _branchClient.GetItem($"branches/{branchId}");
            var service = await _serviceClient.GetItem($"services/{serviceId}");

            if (service != null)
            {
                if (branch != null)
                {
                    ViewBag.Branch = branch;
                }
                return View(service);
            }

            return View();
        }

        // GET: Consultants/Services/4/Tickets/5/Serve
        [HttpGet("Consultants/Branches/{branchId}/Services/{serviceId}/Tickets/{ticketId}/Serve")]
        public async Task<ActionResult> ServeTicket(int branchId, int serviceId, int ticketId)
        {
            var ticket = await _ticketClient.GetItem($"tickets/{ticketId}");

            if (ticket == null) return Redirect("/");

            await _client.PostItem($"Tickets/{ticketId}/Serve", null);

            return RedirectToAction(nameof(Tickets), new {branchId, serviceId});

        }

        // GET: Consultants/Services/4/Tickets/5/Serve
        [HttpGet("Consultant/Ticket/Consultation/Start/Services/{serviceId}")]
        public async Task<ActionResult> StartConsultation(int serviceId)
        {
            _client.Authenticate(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value);

            await _client.PostItem($"Consultant/Ticket/Consultation/Start/Services/{serviceId}", null);

            return RedirectToAction(nameof(Consult), new {serviceId});

        }

        // GET: Consultants/Services/4/Tickets/5/Serve
        [HttpGet("Consultant/Ticket/Consultation/Start/Services/{serviceId}/New")]
        public async Task<JsonResult> StartConsultationNew(int serviceId)
        {
            _ticketClient.Authenticate(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value);

            var ticket = await _ticketClient.PostItem($"Consultant/Ticket/Consultation/Start/Services/{serviceId}", null);

            if (ticket == null)
            {
                return new JsonResult(null);
            }

            return new JsonResult(ticket);

        }

        // GET: Consultants/Services/4/Tickets/5/Serve
        [HttpGet("Consultant/Ticket/Consultation/End/Services/{serviceId}")]
        public async Task<ActionResult> EndConsultation(int serviceId)
        {
            _ticketClient.Authenticate(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value);

            var ticket = await _ticketClient.PostItem($"Consultant/Ticket/Consultation/End/Services/{serviceId}", null);

            return RedirectToAction(nameof(Consult), new { serviceId });

        }

        // GET: Consultants/Services/4/Tickets/5/Serve
        [HttpGet("Consultant/Ticket/Consultation/End/Services/{serviceId}/New")]
        public async Task<JsonResult> EndConsultationNew(int serviceId)
        {
            _ticketClient.Authenticate(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value);

            var ticket = await _ticketClient.PostItem($"Consultant/Ticket/Consultation/End/Services/{serviceId}", null);

            if (ticket == null)
            {
                return new JsonResult(null);
            }

            return new JsonResult(ticket);

        }

        // GET: Consultants/Services/4/Tickets/5/Serve
        [HttpGet("Consultant/Ticket/Consultation/End/Services/{serviceId}/New/Comments/{comments}")]
        public async Task<JsonResult> EndConsultationNewComments(int serviceId, string comments)
        {
            _ticketClient.Authenticate(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value);

            if(string.IsNullOrWhiteSpace(comments))
            {
                comments = "No Comment";
            }

            var ticket = await _ticketClient.PostItem($"Consultant/Ticket/Consultation/End/Services/{serviceId}/Comments/{comments}", null);

            if (ticket == null)
            {
                return new JsonResult(null);
            }

            return new JsonResult(ticket);

        }

        // POST: Account/Profile
        [HttpPost("Consultant/Profile/Update")]
        public async Task<ActionResult> Profile(ConsultantModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _client.Authenticate(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value);
            
            await _client.PostItem("consultant/profile/update", model);

            var identity = await _client.GetIdentity(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value);

            if (!string.IsNullOrWhiteSpace(identity.FirstName) ||
                !string.IsNullOrWhiteSpace(identity.LastName) ||
                !string.IsNullOrWhiteSpace(identity.Title))
            {
                HttpContext.Session.SetString("FirstName", identity.FirstName);
                HttpContext.Session.SetString("LastName", identity.LastName);
                HttpContext.Session.SetString("Title",
                    identity.Title);
            }

            return Redirect("/Consultant/Profile");
        }

        // GET: Consultants/Create
        public ActionResult Create()
        {
            return View();
        }
    }
}