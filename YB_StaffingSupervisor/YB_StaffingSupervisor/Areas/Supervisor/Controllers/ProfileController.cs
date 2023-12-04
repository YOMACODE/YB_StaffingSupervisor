using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YB_StaffingSupervisor.Common;
using YB_StaffingSupervisor.Controllers;
using YB_StaffingSupervisor.DataAccess.Entities;
using YB_StaffingSupervisor.DataAccess.Entities.Custom;
using YB_StaffingSupervisor.DataAccess.UnitOfWork;
using YB_StaffingSupervisor.LoginRepository.ILoginRepository;

namespace YB_StaffingSupervisor.Areas.Supervisor.Controllers
{
    [Area("Supervisor")]
    public class ProfileController : BaseController
    {
        private readonly IDataProtector _dataProtector;
        private readonly AppSettings _appSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoginUserRepository _loginUserRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProfileController(IUnitOfWork unitOfWork, IDataProtectionProvider protectionProvider, IWebHostEnvironment webHostEnvironment, ILoginUserRepository loginUserRepo, IOptions<AppSettings> appsettings = null) : base(unitOfWork, protectionProvider, loginUserRepo, appsettings)
        {
            _appSettings = appsettings.Value;
            _webHostEnvironment = webHostEnvironment;
            _dataProtector = protectionProvider.CreateProtector(_appSettings.ProtectorValue);
            _unitOfWork = unitOfWork;
            _loginUserRepo = loginUserRepo;
        }
        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("UserId")))
            {
                return RedirectToAction("Logout", "Home", new { area = "" }).WithWarning("Warning !", "Unauthorized Access.");
            }
            UserProfileModel userProfileModel = await _service.UserProfileRepository.UserProfile(_dataProtector.Unprotect(baseModel.UserId));

            return View(userProfileModel);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string newPassword, string Token)
        {
            string msg = "";
            try
            {
                var routeValues = ControllerContext.HttpContext.Request.RouteValues;
                var url = $"/{routeValues["area"]}/{routeValues["controller"]}/{routeValues["action"]}";
                bool checkToken = _loginUserRepo.ValidateCurrentToken(Token, url);
                if (checkToken == false)
                {
                    return RedirectToAction("Logout", "Home").WithWarning("Warning !", "Unauthorized Access.");
                }
                else
                {
                    if (!string.IsNullOrEmpty(newPassword))
                    {
                        long result = await _service.UserProfileRepository.ChangePassword(newPassword, _dataProtector.Unprotect(baseModel.UserId));
                        if (result == 1)
                        {
                            msg = "Password change successfully.";
                        }
                        else
                        {
                            msg = "Something went wrong,Please try again.";
                        }
                    }
                    else
                    {
                        msg = "empty password";
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return Json(new { msg });

        }

        [HttpPost]
        public async Task<IActionResult> ChangeProfileImage(string imgSrcPath, string Token)
        {
            string msg = "";
            try
            {
                var routeValues = ControllerContext.HttpContext.Request.RouteValues;
                var url = $"/{routeValues["area"]}/{routeValues["controller"]}/{routeValues["action"]}";
                bool checkToken = _loginUserRepo.ValidateCurrentToken(Token, url);
                if (checkToken == false)
                {
                    return RedirectToAction("Logout", "Home").WithWarning("Warning !", "Unauthorized Access.");
                }
                else
                {
                    if (!string.IsNullOrEmpty(imgSrcPath))
                    {
                        var profileImageFilePath = baseModel.SiteURL + imgSrcPath;
                        long result = await _service.UserProfileRepository.ChangeProfileImage(profileImageFilePath, _dataProtector.Unprotect(baseModel.UserId));
                        if (result == 1)
                        {
                            msg = "Profile image change successfully.";
                        }
                        else
                        {
                            msg = "Something went wrong,Please try again.";
                        }
                    }
                    else
                    {
                        msg = "empty image path";
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return Json(new { msg });

        }
        [HttpPost]
        public async Task<IActionResult> UploadProfileImage(IFormFile profileImageFile, string Token)
        {
            string msg = "";
            try
            {
                var routeValues = ControllerContext.HttpContext.Request.RouteValues;
                var url = $"/{routeValues["area"]}/{routeValues["controller"]}/{routeValues["action"]}";
                bool checkToken = _loginUserRepo.ValidateCurrentToken(Token, url);
                if (checkToken == false)
                {
                    return RedirectToAction("Logout", "Home").WithWarning("Warning !", "Unauthorized Access.");
                }
                else
                {
                    if (profileImageFile != null && profileImageFile.Length > 0)
                    {
                        // Check if the file is a valid image type (JPEG or PNG)
                        string[] allowedFileTypes = { "image/jpeg", "image/png", "image/jpg" };
                        if (!allowedFileTypes.Contains(profileImageFile.ContentType))
                        {
                            return Json(new { msg = "Invalid file type. Please upload a JPEG or PNG file." });
                        }

                        // Check file size (between 10 KB and 5 MB)
                        int maxSize = 5 * 1024 * 1024; // 5 MB
                        int minSize = 1 * 1024; // 1 KB

                        if (profileImageFile.Length > maxSize || profileImageFile.Length < minSize)
                        {
                            return Json(new { msg = "File size should be between 10 KB and 5 MB." });
                        }

                        string uploadsFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Image/Areas/Supervisor/ProfileImages");
                        if (!Directory.Exists(uploadsFolderPath))
                        {
                            Directory.CreateDirectory(uploadsFolderPath);
                        }
                        var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(profileImageFile.FileName);
                        var filePath = Path.Combine(uploadsFolderPath, fileName);
                        using var fs = new FileStream(filePath, FileMode.Create);
                        await profileImageFile.CopyToAsync(fs);
                        var profileImageFilePath = baseModel.SiteURL + "Image/Areas/Supervisor/ProfileImages/" + fileName;
                        long result = await _service.UserProfileRepository.ChangeProfileImage(profileImageFilePath, _dataProtector.Unprotect(baseModel.UserId));
                        if (result == 1)
                        {
                            return Json(new { msg = "Profile image change successfully." });
                        }
                        else
                        {
                            return Json(new { msg = "Something went wrong,Please try again." });
                        }
                    }
                    else
                    {
                        return Json(new { msg = "invalid image" });
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return Json(new { msg });
        }
    }
}
