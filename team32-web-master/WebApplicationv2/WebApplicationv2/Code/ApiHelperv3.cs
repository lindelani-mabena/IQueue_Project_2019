using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApplicationv2.Models.Account;

namespace WebApplicationv2.Code
{
    public class ApiHelperv3<T>
    {
        private readonly HttpClient _client;
        private const string BaseAddress = "http://10.254.198.242:8080";

        public ApiHelperv3()
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(BaseAddress)
            };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void Authenticate(string accessToken)
        {
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);
        }

        public async Task<List<T>> GetItems(string path)
        {
            var items = new List<T>();

            var response = await _client.GetAsync($"api/{path}");

            if (response.IsSuccessStatusCode)
            {
                items = await response.Content.ReadAsAsync<List<T>>();
            }

            return items;
        }

        public async Task<T> GetItem(string path)
        {
            var item = default(T);

            var response = await _client.GetAsync($"api/{path}");
            if (response.IsSuccessStatusCode)
            {
                item = await response.Content.ReadAsAsync<T>();
            }

            return item;
        }

        public async Task<ICollection<IdentityUserClaim>> GetClaims(string accessToken)
        {

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _client.GetAsync($"api/account/claims");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<ICollection<IdentityUserClaim>>();
            }

            return null;
        }

        public async Task<IdentityModel> GetIdentity(string accessToken)
        {
            var userInfo = new IdentityModel();

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _client.GetAsync($"api/account/identity");

            if (response.IsSuccessStatusCode)
            {
                userInfo = await response.Content.ReadAsAsync<IdentityModel>();
            }

            return userInfo;
        }

        public async Task<ProfileModel> GetUserProfile(string accessToken)
        {
            var userInfo = new ProfileModel();

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _client.GetAsync($"api/account/profile");

            if (response.IsSuccessStatusCode)
            {
                userInfo = await response.Content.ReadAsAsync<ProfileModel>();
            }

            return userInfo;
        }


        public async Task PutItem(string path, T item)
        {
            var response = await _client.PutAsJsonAsync($"api/{path}", item);

            if (response.IsSuccessStatusCode)
            {
                //TODO: ...
            }
            // TODO: ...
        }

        public async Task<T> PostItem(string path, T item)
        {
            var response = await _client.PostAsJsonAsync($"api/{path}", item);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }

            return default;
        }

        public async Task<Token> Authenticate(LoginModel model)
        {
            var pair = new Dictionary<string, string> { { "username", model.Email }, { "password", model.Password }, { "grant_type", "password" } };

            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(
                    "application/json"));

            var response = await _client.PostAsync("Token", new FormUrlEncodedContent(pair));
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<Token>();
            }

            return null;
        }

        public async Task<bool> Logout(string accessToken)
        {
            _client.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _client.PostAsync("api/account/logout", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Register(RegisterModel model)
        {
            var response = await _client.PostAsJsonAsync("api/account/register", model);
            return response.IsSuccessStatusCode;
        }
    }
}
