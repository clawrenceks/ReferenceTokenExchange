using System.Collections.Generic;

namespace Clawrenceks.Middleware.ReferenceTokenExchange.Models
{
    public class OperationResult
    {
        public string Result;
        public bool IsError;
        public List<string> Errors = new List<string>();
    }
}
