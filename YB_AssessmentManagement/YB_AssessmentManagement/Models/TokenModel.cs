//using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YB_AssessmentManagement.Models
{
    public class TokenModel
    {
        public String Id { get; set; }
        public String ReturnURL { get; set; }
        public String TokenValue { get; set; }
        public String ErrorMessage { get; set; }
    }
}
