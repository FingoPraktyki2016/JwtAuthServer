using System.Collections.Generic;
using LegnicaIT.BusinessLogic.Models.Common;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests.Models
{
    public class ErrorModelTests
    {
        [Fact]
        public void ValidData_ModelAreCorrect()
        {
            //prepare
            var list = new List<string> {"test"};

            //action
            var action = new ErrorModel
            {
                ListOfErrors = list
            };

            //check
            Assert.Equal("test", action.ListOfErrors[0]);
        }
    }
}
