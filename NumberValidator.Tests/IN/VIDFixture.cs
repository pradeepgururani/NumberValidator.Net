using Xunit;
using NumberValidator.Validators.IN; 

namespace NumberValidator.Tests.IN
{
    public class VIDFixture
    {
        [Fact]
        public void LessThan16Digits_ThrowsInvalidFormatException()
        {
            Assert.Throws<InvalidFormatException>(() => new Vid().Validate("12345678901234"));
        }

        [Fact]
        public void Palindrome_ThrowsInvalidFormatException()
        {
            Assert.Throws<InvalidFormatException>(() => new Vid().Validate("1234567890123456"));
        }

        [Fact]
        public void InvalidPattern_ThrowsInvalidFormatException()
        {
            Assert.Throws<InvalidFormatException>(() => new Vid().Validate("0123456789012345"));
        }

        [Fact]
        public void InvalidChecksum_ThrowsInvalidChecksumException()
        {
            var validVid = new Vid().GenerateValidVid();
            var invalidVid = validVid.Substring(0, 15) + (validVid[15] == '0' ? '1' : (char)(validVid[15] - 1));
            Assert.Throws<InvalidChecksumException>(() => new Vid().Validate(invalidVid));
        }

        [Fact]
        public void ValidVid_DoesNotThrowException()
        {
            var vidNumber = new Vid().GenerateValidVid();
            Assert.Null(Record.Exception(() => new Vid().Validate(vidNumber)));
        }

        [Fact]
        public void Format_CorrectlyFormatsVid()
        {
            var vidNumber = new Vid().GenerateValidVid();
            var formattedVid = new Vid().Format(vidNumber);
            Assert.Matches(@"^\d{4} \d{4} \d{4} \d{4}$", formattedVid);
        }

        [Fact]
        public void Mask_CorrectlyMasksVid()
        {
            var vidNumber = new Vid().GenerateValidVid();
            var maskedVid = new Vid().Mask(vidNumber);
            Assert.Matches(@"^XXXX XXXX XXXX \d{4}$", maskedVid);
        }
    }
}
