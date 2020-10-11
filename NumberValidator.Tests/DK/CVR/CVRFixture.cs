using FluentAssertions;
using NumberValidator.Helpers;
using Xunit;

namespace NumberValidator.Tests.DK.CVR
{
    public class CVRFixture
    {
        private readonly Validators.DK.CVR _sut = new Validators.DK.CVR();
        
        [Fact]
        void ShouldNotFailWithNull() 
            => Assert.Throws<InvalidFormatException>(() => _sut.Validate(null));

        [Fact]
        void ShouldBeInvalidForEmptyString() 
            => Assert.Throws<InvalidFormatException>(() => _sut.Validate("   "));

        [Fact]
        void ShouldBeInvalidFormatWithBadNumber() 
            => Assert.Throws<InvalidFormatException>(() => _sut.Validate("037367606"));

        [Fact]
        void ShouldBeInvalidForLessThan8Digits() 
            => Assert.Throws<InvalidFormatException>(() => _sut.Validate("1234567"));

        [Fact]
        void ShouldBeInvalidForWrongChecksum() 
            => Assert.Throws<InvalidChecksumException>(() => _sut.Validate("37367606"));

        [Fact]
        void ShouldBeValid() 
            => _sut.IsValid("37367605").Should().BeTrue();

        [Fact]
        void ShouldBeValidWithSpacesInBetween()
            => _sut.IsValid(" 37 367 60 5 ").Should().BeTrue();

        [Fact]
        void ShouldBeValidWithSpacesInFrontAndBack()
            => _sut.IsValid(" 37367605 ").Should().BeTrue();
        
        [Fact]
        void ShouldBeValidWithCountryCode() 
            => _sut.IsValid("DK 37367605").Should().BeTrue();
    }
}