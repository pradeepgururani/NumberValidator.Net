using Xunit;
using NumberValidator.Validators.IN;

namespace NumberValidator.Tests.IN
{
    public class VIDFixture
    {
        [Fact]
        public void LessThan16Digits_ThrowsInvalidFormatException()
        {
            Assert.Throws<NumberValidator.Validators.IN.InvalidFormatException>(() => new VID().Validate("12345678901234"));
        }

        [Fact]
        public void Palindrome_ThrowsInvalidFormatException()
        {
            Assert.Throws<NumberValidator.Validators.IN.InvalidFormatException>(() => new VID().Validate("1234567890123456"));
        }

        [Fact]
        public void InvalidPattern_ThrowsInvalidFormatException()
        {
            Assert.Throws<NumberValidator.Validators.IN.InvalidFormatException>(() => new VID().Validate("0123456789012345"));
        }

        [Fact]
        public void InvalidChecksum_ThrowsInvalidChecksumException()
        {
            var validVid = new VID().GenerateValidVid();
            var invalidVid = validVid.Substring(0, 15) + (validVid[15] == '0' ? '1' : (char)(validVid[15] - 1));
            Assert.Throws<NumberValidator.Validators.IN.InvalidChecksumException>(() => new VID().Validate(invalidVid));
        }

        [Fact]
        public void ValidVid_DoesNotThrowException()
        {
            var vidNumber = new VID().GenerateValidVid();
            Assert.Null(Record.Exception(() => new VID().Validate(vidNumber)));
        }

        [Fact]
        public void Format_CorrectlyFormatsVid()
        {
            var vidNumber = new VID().GenerateValidVid();
            var formattedVid = new VID().Format(vidNumber);
            Assert.Matches(@"^\d{4} \d{4} \d{4} \d{4}$", formattedVid);
        }

        [Fact]
        public void Mask_CorrectlyMasksVid()
        {
            var vidNumber = new VID().GenerateValidVid();
            var maskedVid = new VID().Mask(vidNumber);
            Assert.Matches(@"^XXXX XXXX XXXX \d{4}$", maskedVid);
        }
    }
}
