using System.Collections.Generic;

namespace ReferenceTokenExchange.Models
{
    public class OperationResult
    {
        public string Result;
        public bool IsError;
        public List<string> Errors = new List<string>();
    }
}
