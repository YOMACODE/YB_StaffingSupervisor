using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using YB_AssessmentManagement.DataAccess.UnitOfWork;
using YB_AssessmentManagement.LoginRepository.ILoginRepository;
using YB_AssessmentManagement.Models;

namespace YB_AssessmentManagement.LoginRepository
{
    public class LoginUserRepository : ILoginUserRepository
    {
        readonly IUnitOfWork _service;
        private readonly AppSettings _appSettings;
        private readonly IDataProtector _protector;
        public LoginUserRepository(IUnitOfWork service, IOptions<AppSettings> appsettings, IDataProtectionProvider protectionProvider)
        {
            _service = service;
            _appSettings = appsettings.Value;
            _protector = protectionProvider.CreateProtector(_appSettings.ProtectorValue);
        }
        public LoginUser Authenticate(string username, string password)
        {
            LoginUser loginUser = new LoginUser();
            try
            {
                var loginModel = _service.UserRepository.CheckLogin(username, password);
                //user not found
                if (loginModel == null)
                {
                    return null;
                }
                else if (!string.IsNullOrEmpty(loginModel.Message))
                {
                    loginUser.Message = loginModel.Message;
                    return loginUser;
                }
                //if user was found generate JWT Token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, _protector.Protect(Convert.ToString(loginModel.UserName)))
                }),
                    Expires = DateTime.Now.AddMinutes(_appSettings.ExpiryTime),
                    SigningCredentials = new SigningCredentials
                                    (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Issuer = _appSettings.myIssuer,
                    Audience = _appSettings.myAudience
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var writeToken = tokenHandler.WriteToken(token);
                loginUser.Token = _protector.Protect(writeToken);
                loginUser.TokenExpiresOn = (DateTime?)tokenDescriptor.Expires;
                loginUser.UserName = username;
                loginUser.Password = "";
                loginUser.StayLogin = loginModel.StayLogin;
                loginUser.MenuList = loginModel.MenuList;
                //Save User wise Token & Expiry date
                int result = _service.UserRepository.SaveLoginToken(username, writeToken, Convert.ToDateTime(loginUser.TokenExpiresOn));
                if (result == 1)
                {
                    loginUser.UserId = loginModel.UserId;
                    return loginUser;
                }
                return new LoginUser();
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return new LoginUser();
            }
        }

        public bool ValidateCurrentToken(string token, string actionUrl)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret));
                var myIssuer = _appSettings.myIssuer;
                var myAudience = _appSettings.myAudience;
                token = _protector.Unprotect(token);

                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = myIssuer,
                    ValidAudience = myAudience,
                    IssuerSigningKey = mySecurityKey,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                //Validate Token from Database
                var claimsIdentity = principal.Identity as ClaimsIdentity;
                var userName = _protector.Unprotect(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);
                var clientId = Convert.ToInt32(claimsIdentity.FindFirst("ClientId")?.Value);
                var tokenExpireOn = validatedToken.ValidTo.ToLocalTime();

                return _service.UserRepository.ValidateClientUserToken(userName, token, tokenExpireOn, actionUrl);
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return false;
            }
        }

        public bool ClearToken(long UserId)
        {
            try
            {
                return _service.UserRepository.ClearToken(UserId);
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return false;
            }
        }
    }
}