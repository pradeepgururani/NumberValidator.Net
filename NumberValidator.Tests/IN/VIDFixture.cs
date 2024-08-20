using Xunit;
using VidHandling;

namespace VidHandling.Tests
{
    public class VIDFixture
    {
        [Fact]
        public void ShouldBeInvalidForLessThan16Digits()
        {
            // Arrange
            string vidNumber = "12345678901234"; // 14 digits

            // Act and Assert
            Assert.Throws<InvalidFormatException>(() => Vid.Validate(vidNumber));
        }

        [Fact]
        public void ShouldBeInvalidFormatCannotBePalindrome()
        {
            // Arrange
            string vidNumber = "1234567890123456"; // 16 digits palindrome

            // Act and Assert
            Assert.Throws<InvalidFormatException>(() => Vid.Validate(vidNumber));
        }

        [Fact]
        public void ShouldBeInvalidFormatPatternNotFollowed()
        {
            // Arrange
            string vidNumber = "0123456789012345"; // Starts with 0, which is not allowed

            // Act and Assert
            Assert.Throws<InvalidFormatException>(() => Vid.Validate(vidNumber));
        }

        [Fact]
        public void ShouldBeInvalidChecksum()
        {
            // Arrange
            string validVid = Vid.GenerateValidVid();

            // Modify the last digit to ensure an invalid checksum
            char lastDigit = validVid[validVid.Length - 1];
            char invalidLastDigit = lastDigit == '0' ? '1' : (char)(lastDigit - 1);
            string invalidVid = validVid.Substring(0, 15) + invalidLastDigit;

            // Act and Assert
            Assert.Throws<InvalidChecksumException>(() => Vid.Validate(invalidVid));
        }

        [Fact]
        public void ShouldBeValid()
        {
            // Arrange
            string vidNumber = Vid.GenerateValidVid();

            // Act
            var exception = Record.Exception(() => Vid.Validate(vidNumber));

            // Assert
            Assert.Null(exception); // Should not throw an exception
        }



        [Fact]
        public void ShouldFormatCorrectly()
        {
            // Arrange
            string vidNumber = Vid.GenerateValidVid();

            // Act
            string formattedVid = Vid.Format(vidNumber);

            // Assert
            Assert.Matches(@"^\d{4} \d{4} \d{4} \d{4}$", formattedVid);
        }

        [Fact]
        public void ShouldMaskCorrectly()
        {
            // Arrange
            string vidNumber = Vid.GenerateValidVid();

            // Act
            string maskedVid = Vid.Mask(vidNumber);

            // Assert
            Assert.Matches(@"^XXXX XXXX XXXX \d{4}$", maskedVid);
        }
    }
}
