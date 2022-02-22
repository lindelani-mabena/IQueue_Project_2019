using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;

namespace WebServiceSecured.Code
{
    public class Maps
    {
        /*Ready to use code :  simple copy paste GetLatLong*/
        public class AddressComponent
        {
            public string long_name { get; set; }
            public string short_name { get; set; }
            public List<string> types { get; set; }
        }

        public class Northeast
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }

        public class Southwest
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }

        public class Bounds
        {
            public Northeast northeast { get; set; }
            public Southwest southwest { get; set; }
        }

        public class Location
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }

        public class Northeast2
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }

        public class Southwest2
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }

        public class Viewport
        {
            public Northeast2 northeast { get; set; }
            public Southwest2 southwest { get; set; }
        }

        public class Geometry
        {
            public Bounds bounds { get; set; }
            public Location location { get; set; }
            public string location_type { get; set; }
            public Viewport viewport { get; set; }
        }

        public class Result
        {
            public List<AddressComponent> address_components { get; set; }
            public string formatted_address { get; set; }
            public Geometry geometry { get; set; }
            public string place_id { get; set; }
            public List<string> types { get; set; }
        }

        public class RootObject
        {
            public List<Result> results { get; set; }
            public string status { get; set; }
        }


        public static RootObject GetLatLongByAddress(string address)
        {
            var root = new RootObject();

            var url =
                $"https://maps.googleapis.com/maps/api/geocode/json?address={address}&sensor=true_or_false&key=AIzaSyC78_KDVSY1e1b2NyPdd-i5-iyhgCKIXSs";
            var req = (HttpWebRequest)WebRequest.Create(url);

            var res = (HttpWebResponse)req.GetResponse();

            using (var streamreader = new StreamReader(res.GetResponseStream() ?? throw new InvalidOperationException()))
            {
                var result = streamreader.ReadToEnd();

                if (!string.IsNullOrWhiteSpace(result))
                {
                    root = JsonConvert.DeserializeObject<RootObject>(result);
                }
            }
            return root;
        }
    }
}