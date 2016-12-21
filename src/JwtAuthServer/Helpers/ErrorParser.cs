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
            var errorList = new List<string>();
            foreach (var modelStateKey in ModelState.Keys)
            {
                var modelStateVal = ModelState[modelStateKey];
                foreach (var error in modelStateVal.Errors)
                {
                    var key = modelStateKey;
                    var errorMessage = error.ErrorMessage;
                    errorList.Add($"Key: {key}, Error: {errorMessage}");
                }
            }
            return errorList;
        }
    }
}