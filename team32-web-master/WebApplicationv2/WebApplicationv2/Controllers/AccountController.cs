using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationv2.Code;
using WebApplicationv2.Models;
using WebApplicationv2.Models.Account;

namespace WebApplicationv2.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApiHelperv3<UserModel> _client;
        private readonly ApiHelperv3<ProfileModel> _profile;

        public AccountController()
        {
            _client = new ApiHelperv3<UserModel>();
            _profile = new ApiHelperv3<ProfileModel>();
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

                ViewBag.Message = "Email already exist or Password Too Weak";
                ViewBag.Style = "danger";
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
        public ActionResult Login(string returnUrl)
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }

            if (TempData["Style"] != null)
            {
                ViewBag.Style = TempData["Style"].ToString();
            }

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

                var xClaims = await _client.GetClaims(accessToken.access_token);

                if (xClaims != null)
                {
                    var identity = await _client.GetIdentity(accessToken.access_token);

                    if (identity != null)
                    {
                        if (!string.IsNullOrWhiteSpace(identity.FirstName) ||
                            !string.IsNullOrWhiteSpace(identity.LastName) ||
                            !string.IsNullOrWhiteSpace(identity.Title))
                        {
                            HttpContext.Session.SetString("FirstName", identity.FirstName);
                            HttpContext.Session.SetString("LastName", identity.LastName);
                            HttpContext.Session.SetString("Title",
                                identity.Title);
                        }

                        var claims = new List<Claim>
                    {
                        new Claim("access_token", accessToken.access_token),
                        new Claim(ClaimTypes.Role, xClaims.FirstOrDefault()?.ClaimValue)
                        // new Claim("Title", userInfo.Title)
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
                    }
                }

                return Redirect(!string.IsNullOrWhiteSpace(returnUrl) ? returnUrl : "/");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Account/Profile
        [Authorize]
        [HttpGet("Account/Profile")]
        public async Task<ActionResult> Profile()
        {
            var profile = await _client.GetUserProfile(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.ToString());
            return View(profile);
        }

        // POST: Account/Profile
        [Authorize]
        [HttpPost("Account/Profile/Update")]
        public async Task<ActionResult> UpdateProfile(ProfileModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Profile", model);
            }

            _profile.Authenticate(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value);
            await _profile.PostItem("account/profile/update", model);

            var identity = await _client.GetIdentity(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value);

            if (!string.IsNullOrWhiteSpace(identity.FirstName) ||
                !string.IsNullOrWhiteSpace(identity.LastName) ||
                !string.IsNullOrWhiteSpace(identity.Title))
            {
                HttpContext.Session.SetString("FirstName", identity.FirstName);
                HttpContext.Session.SetString("LastName", identity.LastName);
                HttpContext.Session.SetString("Title",
                    identity.Title);
            }

            return Redirect("/");
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
                //if (!await _client.Logout(User.Claims.FirstOrDefault(x => x.Type == "access_token")?.ToString())) return RedirectToAction(nameof(Login));
                await HttpContext.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);

                HttpContext.Session.Remove("FirstName");
                HttpContext.Session.Remove("LastName");
                HttpContext.Session.Remove("Title");

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