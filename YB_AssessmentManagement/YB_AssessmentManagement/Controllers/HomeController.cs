using System;
using System.Web;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using YB_AssessmentManagement.LoginRepository.ILoginRepository;
using YB_AssessmentManagement.Models;
using YB_AssessmentManagement.Services;

namespace YB_AssessmentManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataProtector _protector;
        private readonly ILoginUserRepository _loginUserRepo;
        private readonly AppSettings _appSettings;

        private readonly DataAccess.UnitOfWork.IUnitOfWork _service;
        public HomeController(DataAccess.UnitOfWork.IUnitOfWork service, IDataProtectionProvider protectionProvider, ILoginUserRepository loginUserRepo, IOptions<AppSettings> appsettings = null)
        {
            _service = service;
            _appSettings = appsettings.Value;
            _loginUserRepo = loginUserRepo;
            _protector = protectionProvider.CreateProtector(_appSettings.ProtectorValue);
        }

        /// <summary>
        /// if session timeout happen it will bind the ReturnUrl
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
       // public static string CaptchaText;

        private int loginCount = 0;
        public IActionResult Index(string returnurl)
        {
            LoginUser loginUser = new LoginUser();
            var s = HttpContext.Session;
            if (s.GetString("UserId") != null && s.GetString("UserId") != "")
            {
                return RedirectToAction("Index", "MyDashboard", new { area = "Dashboard" });
            }
            return View(loginUser);
        }
        public static string XmlDecode(string encodedString)
        {
            var workingBuffer = new StringBuilder(encodedString);
            workingBuffer.Replace("&lt;", "<")
                .Replace("&gt;", ">")
                .Replace("&quot;", "\"")
                .Replace("&apos;", "'")
                .Replace("&amp;", "&");
            return workingBuffer.ToString();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string returnurl, LoginUser loginUser)
        {
            if (ModelState.IsValid)
            {
                if (loginCount > 3)
                {
                    TempData["LoginLimitExceeded"] = "Maximum Attempts of Incorrect Login Reached";
                    loginCount++;
                    return View(loginUser);
                }
                if (loginUser.Captcha == null)
                {
                    ResetCaptcha();
                    TempData["Message"] = "Please Enter the Captcha";
                    return RedirectToAction("Index", "Home");
                }
                if (loginUser.Captcha != HttpContext.Session.GetString("Captcha"))
                {
                    ResetCaptcha();
                    TempData["Message"] = "Wrong Captcha Entered";

                    return RedirectToAction("Index", "Home");
                }
                var loginUserStatus = _loginUserRepo.Authenticate(loginUser.UserName, loginUser.Password);
                if (loginUserStatus == null || loginUserStatus.Token == null)
                {
                    TempData["Message"] = "Invalid Username or Password";
                    loginCount++;
                    ResetCaptcha();
                    return RedirectToAction("Index", "Home");
                }
                if (loginUserStatus.Message == null)
                {
                    var userTokens = await _service.UserTokensRepository.GetTokensList(loginUserStatus.UserId);
                    if (userTokens != null && userTokens.Count > 0)
                    {
                        foreach (var token in userTokens)
                        {
                            HttpContext.Session.SetString(token.Provider + "AccessToken", token.AccessToken);
                            HttpContext.Session.SetString(token.Provider + "RefreshToken", token.RefreshToken);
                        }
                    }

                    HttpContext.Session.SetString("UserId", _protector.Protect(Convert.ToString(loginUserStatus.UserId)));
                    HttpContext.Session.SetString("MenuList", XmlDecode(XmlDecode(loginUserStatus.MenuList)));
                    //Authenticate using the Token
                    HttpContext.Session.SetString("JWToken", loginUserStatus.Token);
                    if (!string.IsNullOrEmpty(returnurl) && Url.IsLocalUrl(returnurl))
                    {
                        return Redirect(returnurl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "MyDashboard", new { area = "Dashboard" });
                    }
                }
                else if (Convert.ToInt32(loginUser.Message) == -2)
                {
                    TempData["Message"] = "Username or password is wrong.";
                    ResetCaptcha();
                }
                else
                {
                    TempData["Message"] = "Something went wrong,please try again.";
                    ResetCaptcha();
                }
            }
            else
            {
                var errors = (from state in ModelState.Values
                              from error in state.Errors
                              select error.ErrorMessage).ToList();
                if (errors.ToList().Count == 2)
                {
                    TempData["Message"] = "* Please enter username and password";
                }
                else if (errors.ToList().Count == 1)
                {
                    TempData["Message"] = errors[0].ToString();
                }
                else
                {
                    TempData["Message"] = "Something went wrong,please try again.";
                }
            }
            return View(loginUser);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            try
            {
                if (HttpContext.Session.GetString("UserId") != null && HttpContext.Session.GetString("UserId") != "")
                {
                    _loginUserRepo.ClearToken(Convert.ToInt64(_protector.Unprotect(HttpContext.Session.GetString("UserId"))));
                }
                HttpContext.Session.Clear();
                HttpContext.Session.SetString("UserId", "");
                // clear all cookies 
                foreach (var cookie in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cookie);
                }
                // here end
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }

        [HttpPost]
        public ActionResult ResetCountVariable()
        {
            loginCount = 0;
            return RedirectToAction("Index", "Home");
        }
        Random random = new Random();
        [HttpGet]
        
        public JsonResult ResetCaptcha()
        {
            string CaptchaText = Convert.ToString(random.Next(100000, 1000000));
            HttpContext.Session.SetString("Captcha", CaptchaText);
            return Json(CaptchaText);
        }



        /// <summary>
        /// Send URL is InValid 
        /// </summary>
        /// <returns></returns>
        public IActionResult PermissionDenied()
        {
            return View();
        }

        public IActionResult AssociateAcceptRejectStatus()
        {
            return View();
        }
    }
}
