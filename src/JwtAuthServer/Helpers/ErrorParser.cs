using LegnicaIT.JwtAuthServer.GenericResult;
using LegnicaIT.JwtAuthServer.Models;
using LegnicaIT.JwtAuthServer.Models.ResultModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LegnicaIT.JwtAuthServer.Helpers
{
    public static class ErrorParser
    {
        public static ResultModel<ErrorModel> GetErrorModel(this ModelStateDictionary ModelState)
        {
            var resultModel = new ResultModel<ErrorModel>((new ErrorModel() { ListOfErrors = GetErrorsFromModelState(ModelState) }), ResultCode.Error);
            return resultModel;
        }

        private static List<string> GetErrorsFromModelState(ModelStateDictionary ModelState)
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