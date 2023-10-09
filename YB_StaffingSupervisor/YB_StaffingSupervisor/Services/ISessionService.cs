using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace YB_StaffingSupervisor.Services
{
    public interface ISessionService
    {
        void SetSession(string name, string value);
        void ClearAllSessions();
        void ClearSession(string name);
        string GetSession(string name);
    }
}
