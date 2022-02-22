using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplicationv2.Code;

namespace WebApplicationv2.Models
{
    public class AjaxPartialModel: PageModel
    {
        private ApiHelperv3<Service> _client = new ApiHelperv3<Service>();
        public Service Service { get; set; }
        public void OnGet()
        {
        }
        public PartialViewResult OnGetTicketsPartial()
        {
            //Service = await _client.GetItem("services/1");
            return Partial("_TicketsPartial", null);
        }
    }
}
