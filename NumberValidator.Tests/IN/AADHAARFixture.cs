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
            var validAadhaars = new[]
            {
                "123456789012",
                "212345678901",
                "112233445566"
            };

            foreach (var aadhaar in validAadhaars)
            {
                Assert.True(_validator.IsValid(aadhaar));
            }
        }

        [Fact]
        public void IsValid_ReturnsFalse_ForInvalidAadhaar()
        {
            var invalidAadhaars = new[]
            {
                "12345678901",
                "1234567890123",
                "012345678901",
                "abcdefghijklm"
            };

            foreach (var aadhaar in invalidAadhaars)
            {
                Assert.False(_validator.IsValid(aadhaar));
            }
        }

        [Fact]
        public void Validate_DoesNotThrowException_ForValidAadhaar()
        {
            var validAadhaars = new[]
            {
                "123456789012",
                "212345678901"
            };

            foreach (var aadhaar in validAadhaars)
            {
                Assert.Null(Record.Exception(() => _validator.Validate(aadhaar)));
            }
        }

        [Fact]
        public void Validate_ThrowsArgumentException_ForInvalidAadhaar()
        {
            var invalidAadhaars = new[]
            {
                "12345678901",
                "1234567890123",
                "012345678901",
                "abcdefghijklm",
                null
            };

            foreach (var aadhaar in invalidAadhaars)
            {
                Assert.Throws<ArgumentException>(() => _validator.Validate(aadhaar));
            }
        }
    }
}