using Xunit;
using FluentAssertions;
using NumberValidator.Helpers;
using NumberValidator.Validators.IN;
using System;
namespace NumberValidator.Tests.IN
{
    public class EPICFixture
    {
        private readonly EPIC _sut = new EPIC();
        
        [Fact]
        void ShouldBeInvalidForLessThan10Characters()
            => Assert.Throws<InvalidLengthException>(() => _sut.Validate("AB1234567"));

        [Fact]
        void ShouldBeInvalidFormatIfPatternNotFollowed()
            => Assert.Throws<InvalidFormatException>(() => _sut.Validate("ABCD123456"));

        [Fact]
        void ShouldBeInvalidFormatIfEndsWithLetters()
            => Assert.Throws<InvalidFormatException>(() => _sut.Validate("ABC1234XYZ"));

        [Fact]
        void ShouldBeInvalidIfChecksumFails()
        {
            Action act = () => _sut.Validate("ABC1234562"); 
            act.Should().Throw<InvalidChecksumException>();
        }

        [Fact]
        void ShouldBeValid()
        {
            _sut.IsValid("ABC - 1234558").Should().BeTrue();
        }
    }
}
