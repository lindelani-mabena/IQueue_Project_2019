using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationv2.Code;
using WebApplicationv2.Models;

namespace WebApplicationv2.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ApiHelperv3<Ticket> _client;

        public TicketsController()
        {
            _client = new ApiHelperv3<Ticket>();
        }

        // GET: Tickets
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Tickets/List
        [AllowAnonymous]
        [HttpGet("Tickets/List")]
        public async Task<ActionResult> List()
        {
            var tickets = await _client.GetItems("ticket");

            if (tickets != null)
            {
                return await Task.Run(() => View(tickets));
            }

            return await Task.Run(View);
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tickets/Create
        [HttpGet("Tickets/Create")]
        public ActionResult Create()
        {
            if (TempData["ErrorMessage"] == null) return View();

            ViewBag.Message = TempData["ErrorMessage"].ToString();

            return View();
        }

        // POST: Tickets/Create
        [HttpPost("Tickets/Create")]
        public async Task<ActionResult> Create(Ticket model)
        {
            try
            {
                // TODO: Add insert logic here

                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "Missing fields";
                    return RedirectToAction(nameof(Create));
                }

                var service = await _client.PostItem("tickets", model);

                if (service == null)
                {
                    TempData["ErrorMessage"] = "Forbidden";
                    return RedirectToAction(nameof(Create));
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