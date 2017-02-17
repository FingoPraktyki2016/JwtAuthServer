using LegnicaIT.BusinessLogic.Models.Common;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests.Models
{
    public class ResultModelTests
    {
        [Fact]
        public void ValidData_ModelAreCorrect()
        {
            //prepare
            var code = ResultCode.NotAuthorized;
            var value = "test";

            //action
            var action = new ResultModel<string>(value, code);

            //check
            Assert.Equal(ResultCode.NotAuthorized, action.Status.Code);
            Assert.Equal("test", action.Value);
        }
    }
}
