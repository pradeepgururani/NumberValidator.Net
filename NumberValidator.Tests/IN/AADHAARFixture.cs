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

        [Theory]
        [InlineData("123456789012", true)]
        [InlineData("212345678901", true)]
        [InlineData("112233445566", true)] // Valid Aadhaar number
        [InlineData("12345678901", false)] // Less than 12 digits
        [InlineData("1234567890123", false)] // More than 12 digits
        [InlineData("012345678901", false)] // Starts with 0
        [InlineData("abcdefghijklm", false)] // Non-numeric
        public void IsValid_ShouldReturnExpectedResult(string aadhaar, bool expected)
        {
            // Act
            var result = _validator.IsValid(aadhaar);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("123456789012")]
        [InlineData("212345678901")]
        public void Validate_ShouldNotThrowException_ForValidAadhaar(string aadhaar)
        {
            // Act & Assert
            var exception = Record.Exception(() => _validator.Validate(aadhaar));
            Assert.Null(exception);
        }

        [Theory]
        [InlineData("12345678901")] // Less than 12 digits
        [InlineData("1234567890123")] // More than 12 digits
        [InlineData("012345678901")] // Starts with 0
        [InlineData("abcdefghijklm")] // Non-numeric
        [InlineData(null)] // Null input should also be invalid
        public void Validate_ShouldThrowArgumentException_ForInvalidAadhaar(string aadhaar)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _validator.Validate(aadhaar));
        }
    }
}
