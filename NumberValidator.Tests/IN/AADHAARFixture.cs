using System;
using Xunit;
using NumberValidator.Validators;

namespace NumberValidator.Tests
{
    public class AadhaarFixture
    {
        private readonly AadhaarValidator _validator;

        public AadhaarFixture()
        {
            _validator = new AadhaarValidator();
        }

        [Fact]
        public void IsValid_ReturnsTrue_ForValidAadhaar()
        {
            string[] validAadhaars = new[]
            {
                "123456789012",
                "212345678901",
                "112233445566"
            };

            foreach (string aadhaar in validAadhaars)
            {
                Assert.True(_validator.IsValid(aadhaar));
            }
        }

        [Fact]
        public void IsValid_ReturnsFalse_ForInvalidAadhaar()
        {
            string[] invalidAadhaars = new[]
            {
                "12345678901",    // Too short
                "1234567890123",  // Too long
                "abcdefghijklm",  // Non-numeric
                "",               // Empty string
                null              // Null
            };

            foreach (string aadhaar in invalidAadhaars)
            {
                Assert.False(_validator.IsValid(aadhaar));
            }
        }

        [Fact]
        public void Validate_DoesNotThrowException_ForValidAadhaar()
        {
            string[] validAadhaars = new[]
            {
                "123456789012",
                "212345678901"
            };

            foreach (string aadhaar in validAadhaars)
            {
                Assert.Null(Record.Exception(() => _validator.Validate(aadhaar)));
            }
        }

        [Fact]
        public void Validate_ThrowsFormatException_ForInvalidAadhaar()
        {
            string[] invalidAadhaars = new[]
            {
                "12345678901",    // Too short
                "1234567890123",  // Too long
                "abcdefghijklm",  // Non-numeric
                "",               // Empty string
                null              // Null
            };

            foreach (string aadhaar in invalidAadhaars)
            {
                Assert.Throws<FormatException>(() => _validator.Validate(aadhaar));
            }
        }
    }
}
