using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace YB_AssessmentManagement.Services
{
    /// <summary>
    /// Sets, gets, and destroys session variables.
    /// </summary>
    public class SessionService : ISessionService
    {
        
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Destroys all the sessions.
        /// </summary>
        public void ClearAllSessions()
        {
            try
            {
                //_httpContextAccessor.HttpContext.Session.Clear();
            }
            catch (Exception error)
            {
                //Console.WriteLine("LOGOUT ERROR: " + error.Message);
            }
        }
        /// <summary>
        /// Destroys session from the sessions.
        /// </summary>
        public void ClearSession(string name)
        {
            try
            {
               // _httpContextAccessor.HttpContext.Session.Remove(name);
            }
            catch (Exception error)
            {
                //Console.WriteLine("LOGOUT ERROR: " + error.Message);
            }
        }


        /// <summary>
        /// Return session token value, if saved, or an empty string.
        /// </summary>
        public string GetSession(string name)
        {
            return TryGetString(name);
        }

        /// <summary>
        /// Sets the access and refresh tokens based on an HTTP response.
        /// </summary>
        public void SetSession(string name, string value)
        {
            try
            {
                _httpContextAccessor.HttpContext.Session.SetString(name, value);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Return session value as a string (if it exists), or an empty string.
        /// </summary>
        private string TryGetString(string name)
        {
            byte[] valueBytes = new Byte[700];
            bool valueOkay = _httpContextAccessor.HttpContext.Session.TryGetValue(name, out valueBytes);
            if (valueOkay)
            {
                return System.Text.Encoding.UTF8.GetString(valueBytes);
            }
            return null;
        }
    }
}