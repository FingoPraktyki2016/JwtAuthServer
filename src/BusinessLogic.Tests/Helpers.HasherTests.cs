using LegnicaIT.DataAccess.Helpers;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests
{
    public class HasherTests
    {
        [Fact]
        public void CreateHash_ForValidInput_ReturnsCorrectHash()
        {
            string password = "Some test data";
            byte[] salt = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };
            var hashedPassowrd = Hasher.CreateHash(password, salt);

            Assert.NotNull(hashedPassowrd);
        }

        [Fact]
        public void ValidateHash_ForValidInput_ReturnsTrue()
        {
            string password = "Some test data";
            byte[] salt = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };
            var hashedPassowrd = Hasher.CreateHash(password, salt);
            var result = Hasher.VerifyHashedPassword("L7zWz7F/MWYKB+dCMsSQmcIH4cHIxPaomJ6I+FNS72rtx7tgTuFoxJRoluGVskRDeXgWHTUjDfyc8sbDRG7mtBKgshhIDXy9Ug5pDW03XtfBa66EJeOuj7BtgGKHOWcjsJsEAB8Voxl7u+oRXhktI9f7V2qRBEhVd8Upz7QBxICwJO1gJYQn5vNKvHFBlQSliAcb8F2IoiLIs2djvrdqmh7XgjtJXF4aForCNup1pUJdLcrVdHjsGbW1zMA2Swrx", hashedPassowrd);
            Assert.True(result);
        }

        [Fact]
        public void ValidateHash_ForInvalidInput_ReturnsNull()
        {
            string password = "";
            byte[] salt = Hasher.GenerateRandomSalt();
            var hashedPassowrd = Hasher.CreateHash(password, salt);
            Assert.Null(hashedPassowrd);
        }
    }
}
