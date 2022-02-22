using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication.Code
{
    public class ApiKeyHandler: DelegatingHandler
    {
        public string Key { get; set; }

        public ApiKeyHandler(string key)
        {
            this.Key = key;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (ValidateKey(request)) return base.SendAsync(request, cancellationToken);

            var response = new HttpResponseMessage(HttpStatusCode.Forbidden)
            {
                Content = new StringContent("Invalid API Key")
            };

            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            tsc.SetResult(response);
            return tsc.Task;
        }

        private bool ValidateKey(HttpRequestMessage message)
        {
            var query = message.RequestUri.ParseQueryString();
            var key = query["ApiKey"];
            return (key == Key);
        }
    }
}