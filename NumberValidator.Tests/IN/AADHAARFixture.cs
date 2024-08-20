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
        public void IsValid_ShouldReturnTrue_ForValidAadhaar()
        {
            // Arrange
            var validAadhaars = new[]
            {
                "123456789012",
                "212345678901",
                "112233445566" // Valid Aadhaar number
            };

            // Act & Assert
            foreach (var aadhaar in validAadhaars)
            {
                var result = _validator.IsValid(aadhaar);
                Assert.True(result);
            }
        }

        [Fact]
        public void IsValid_ShouldReturnFalse_ForInvalidAadhaar()
        {
            // Arrange
            var invalidAadhaars = new[]
            {
                "12345678901",      // Less than 12 digits
                "1234567890123",    // More than 12 digits
                "012345678901",     // Starts with 0
                "abcdefghijklm"     // Non-numeric
            };

            // Act & Assert
            foreach (var aadhaar in invalidAadhaars)
            {
                var result = _validator.IsValid(aadhaar);
                Assert.False(result);
            }
        }

        [Fact]
        public void Validate_ShouldNotThrowException_ForValidAadhaar()
        {
            // Arrange
            var validAadhaars = new[]
            {
                "123456789012",
                "212345678901"
            };

            // Act & Assert
            foreach (var aadhaar in validAadhaars)
            {
                var exception = Record.Exception(() => _validator.Validate(aadhaar));
                Assert.Null(exception);
            }
        }

        [Fact]
        public void Validate_ShouldThrowArgumentException_ForInvalidAadhaar()
        {
            // Arrange
            var invalidAadhaars = new[]
            {
                "12345678901",      // Less than 12 digits
                "1234567890123",    // More than 12 digits
                "012345678901",     // Starts with 0
                "abcdefghijklm",    // Non-numeric
                null                // Null input should also be invalid
            };

            // Act & Assert
            foreach (var aadhaar in invalidAadhaars)
            {
                Assert.Throws<ArgumentException>(() => _validator.Validate(aadhaar));
            }
        }
    }
}
