using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Zip { get; set; }

        public string Display()
        {
            var str = $"Street: {Address1} <br />Town: {Address2} <br />City: {City}" +
                      $"<br />Province: {Province}<br />Zip: {Zip}";
            return str;
        }
    }
}
