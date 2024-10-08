﻿using System;
using Xunit;
using NumberValidator.Validators.IN;

namespace NumberValidator.Tests.IN
{
    public class AADHAARFixture
    {
        private readonly AADHAAR _validator;

        public AADHAARFixture()
        {
            _validator = new AADHAAR();
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
                "12345678901",    // Too short
                "1234567890123",  // Too long
                "abcdefghijklm",  // Non-numeric
                "",               // Empty string
                null              // Null
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
        public void Validate_ThrowsFormatException_ForInvalidAadhaar()
        {
            var invalidAadhaars = new[]
            {
                "12345678901",    // Too short
                "1234567890123",  // Too long
                "abcdefghijklm",  // Non-numeric
                "",               // Empty string
                null              // Null
            };
            foreach (var aadhaar in invalidAadhaars)
            {
                Assert.Throws<FormatException>(() => _validator.Validate(aadhaar));
            }
        }
    }
}
