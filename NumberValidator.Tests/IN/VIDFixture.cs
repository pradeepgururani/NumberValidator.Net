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
            var vidNumber = "12345678901234"; // 14 digits

            // Act and Assert
            Assert.Throws<InvalidFormatException>(() => Vid.Validate(vidNumber));
        }

        [Fact]
        public void ShouldBeInvalidFormatCannotBePalindrome()
        {
            // Arrange
            var vidNumber = "1234567890123456"; // 16 digits palindrome

            // Act and Assert
            Assert.Throws<InvalidFormatException>(() => Vid.Validate(vidNumber));
        }

        [Fact]
        public void ShouldBeInvalidFormatPatternNotFollowed()
        {
            // Arrange
            var vidNumber = "0123456789012345"; // Starts with 0, which is not allowed

            // Act and Assert
            Assert.Throws<InvalidFormatException>(() => Vid.Validate(vidNumber));
        }

        [Fact]
        public void ShouldBeInvalidChecksum()
        {
            // Arrange
            // Generate a valid VID
            var validVid = Vid.GenerateValidVid();

            // Modify the last digit to ensure an invalid checksum
            char lastDigit = validVid[^1];
            char invalidLastDigit = lastDigit == '0' ? '1' : (char)(lastDigit - 1);
            var invalidVid = validVid.Substring(0, 15) + invalidLastDigit;

            // Act and Assert
            Assert.Throws<InvalidChecksumException>(() => Vid.Validate(invalidVid));
        }

        [Fact]
        public void ShouldBeValid()
        {
            // Arrange
            var vidNumber = Vid.GenerateValidVid();

            // Act and Assert
            Vid.Validate(vidNumber); // Should not throw an exception
        }

        [Fact]
        public void ShouldFormatCorrectly()
        {
            // Arrange
            var vidNumber = Vid.GenerateValidVid();

            // Act
            var formattedVid = Vid.Format(vidNumber);

            // Assert
            Assert.Matches(@"^\d{4} \d{4} \d{4} \d{4}$", formattedVid);
        }

        [Fact]
        public void ShouldMaskCorrectly()
        {
            // Arrange
            var vidNumber = Vid.GenerateValidVid();

            // Act
            var maskedVid = Vid.Mask(vidNumber);

            // Assert
            Assert.Matches(@"^XXXX XXXX XXXX \d{4}$", maskedVid);
        }
    }
}
