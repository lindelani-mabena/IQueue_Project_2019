using System;

namespace WebApplicationv2.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Zip { get; set; }
        public int BranchId { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string ClientId { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime LastUpdate { get; set; }

        public string Display()
        {
            var str = $"Street: {Address1} <br />Town: {Address2} <br />City: {City}" +
                      $"<br />Province: {Province}<br />Zip: {Zip}";
            return str;
        }
    }
}
