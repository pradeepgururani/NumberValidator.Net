using Xunit;
using NumberValidator; // Add this namespace reference

namespace NumberValidator.Tests
{
    public class VIDFixture
    {
        [Fact]
        public void LessThan16Digits_ThrowsInvalidFormatException()
        {
            Assert.Throws<NumberValidator.InvalidFormatException>(() => Vid.Validate("12345678901234"));
        }

        [Fact]
        public void Palindrome_ThrowsInvalidFormatException()
        {
            Assert.Throws<NumberValidator.InvalidFormatException>(() => Vid.Validate("1234567890123456"));
        }

        [Fact]
        public void InvalidPattern_ThrowsInvalidFormatException()
        {
            Assert.Throws<NumberValidator.InvalidFormatException>(() => Vid.Validate("0123456789012345"));
        }

        [Fact]
        public void InvalidChecksum_ThrowsInvalidChecksumException()
        {
            var validVid = Vid.GenerateValidVid();
            var invalidVid = validVid.Substring(0, 15) + (validVid[15] == '0' ? '1' : (char)(validVid[15] - 1));
            Assert.Throws<NumberValidator.InvalidChecksumException>(() => Vid.Validate(invalidVid));
        }

        [Fact]
        public void ValidVid_DoesNotThrowException()
        {
            var vidNumber = Vid.GenerateValidVid();
            Assert.Null(Record.Exception(() => Vid.Validate(vidNumber)));
        }

        [Fact]
        public void Format_CorrectlyFormatsVid()
        {
            var vidNumber = Vid.GenerateValidVid();
            var formattedVid = Vid.Format(vidNumber);
            Assert.Matches(@"^\d{4} \d{4} \d{4} \d{4}$", formattedVid);
        }

        [Fact]
        public void Mask_CorrectlyMasksVid()
        {
            var vidNumber = Vid.GenerateValidVid();
            var maskedVid = Vid.Mask(vidNumber);
            Assert.Matches(@"^XXXX XXXX XXXX \d{4}$", maskedVid);
        }
    }
}