using LegnicaIT.BusinessLogic.Models.Common;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests.Models
{
    public class ResultStatusModelTests
    {
        [Fact]
        public void ValidData_ModelAreCorrect()
        {
            //prepare
            var code = ResultCode.Ok;
            var message = "message";
            var details = "details";

            //action
            var action = new ResultStatusModel()
            {
                Code = code,
                Message = message,
                Details = details
            };

            //check
            Assert.Equal(ResultCode.Ok, action.Code);
            Assert.Equal("message", action.Message);
            Assert.Equal("details", action.Details);
        }
    }
}
