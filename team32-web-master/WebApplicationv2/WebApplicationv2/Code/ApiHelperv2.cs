using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WebApplicationv2.Code
{
    public class ApiHelperv2
    {
        private readonly HttpClient _client;
        private readonly string _baseAddress = "http://10.254.198.242:8080";
        public ApiHelperv2() : this(new HttpClient()) { }
      
        public ApiHelperv2(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(_baseAddress);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void Logout(string accessToken)
        {
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            var url = $"{_baseAddress}/api/account/logout";

            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(
                    "application/json"));

            _client.PostAsync(url, null);
        }

    }
}
