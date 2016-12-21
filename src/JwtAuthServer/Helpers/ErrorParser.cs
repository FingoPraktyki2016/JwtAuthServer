using LegnicaIT.JwtAuthServer.Models.ResultModel;
using LegnicaIT.JwtAuthServer.ResultPattern;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

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
