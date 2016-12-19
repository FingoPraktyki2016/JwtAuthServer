using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LegnicaIT.JwtAuthServer.Helpers
{
    public class ErrorParser
    {
        public List<string> GetErrorsFromModelState(ModelStateDictionary ModelState)
        {
            var query = ModelState.Values;
            var errorList = new List<string>();
            foreach (var e in query)
            {
                foreach (var x in e.Errors)
                {
                    errorList.Add(x.ErrorMessage);
                }
            }

            return errorList;
        }
    }
}