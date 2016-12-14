using Xunit;

namespace UnitTests
{
    public class Test
    {
        [Fact]
        public void Test1()
        {
        }

        [Theory]
        [InlineData(1, 3, 4)]
        [InlineData(1, 3, 5)]
        public void TestSum(int a, int b, int expectedResult)
        {
            int sum = a + b;
            Assert.Equal(expectedResult, sum);
        }
    }
}
