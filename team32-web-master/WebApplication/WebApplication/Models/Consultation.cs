using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Consultation
    {
        public int Id { get; set; }
        public string FeedBack { get; set; }
        public virtual Ticket Ticket { get; set; }
        public virtual Consultant Consultant { get; set; }
    }
}
