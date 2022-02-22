using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Code;
using WebApplication.Models;
using WebApplication.Models.Account;

namespace WebApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApiHelperv3<UserModel> _client;

        public AccountController()
        {
            _client = new ApiHelperv3<UserModel>();
        }

        // GET: Account/Register
        [AllowAnonymous]
        [HttpGet("Account/Register")]
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [AllowAnonymous]
        [HttpPost("Account/Register")]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _client.Register(model))
                {
                    TempData["Message"] = "You are registered. Please Login";
                    TempData["Style"] = "success";
                    return RedirectToAction(nameof(Login));
                }

                return View(model);
                //await HttpContext.AuthenticateAsync("Token");
                //var results = await _client.GetItem("account/register/client");
                //if(results.)
            }

            return View(model);
        }

        // GET: Account/Login
        [AllowAnonymous]
        [HttpGet("Account/Login")]
        public async Task<ActionResult> Login(string returnUrl)
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }

            if (TempData["Style"] != null)
            {
                ViewBag.Style = TempData["Style"].ToString();
            }
            //var services = await _client.GetItems("services");
            var branch = await _client.GetItems("branch/1");
            if (branch != null)
                ViewBag.Branch = branch;
            return View();
        }

        // POST: Account/Login
        [AllowAnonymous]
        [HttpPost("Account/Login")]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var accessToken = await _client.Authenticate(model);

                if (accessToken == null)
                {
                    ViewBag.Message = "Email or password is incorrect";
                    ViewBag.Style = "danger";
                    return View(model);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim("access_token", accessToken.access_token),
                    new Claim(ClaimTypes.Role, "admin")
                };

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    //IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return Redirect(!string.IsNullOrWhiteSpace(returnUrl) ? returnUrl : "/");
            }
            catch
            {
                return View(model);
            }
        }


        // POST: Account/Logout
        [Authorize]
        [HttpGet("Account/Logout")]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult> Logout()
        {
            try
            {
                // TODO: Add insert logic here
                if (!await _client.Logout()) return RedirectToAction(nameof(Login));
                await HttpContext.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
                TempData["Message"] = "You are logged out";
                TempData["Style"] = "success";
                return RedirectToAction(nameof(Login));
            }
            catch
            {
                return RedirectToAction(nameof(Login));
            }
        }
    }
}