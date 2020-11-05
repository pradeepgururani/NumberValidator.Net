using FluentAssertions;
using NumberValidator.Helpers;
using Xunit;

namespace NumberValidator.Tests.DK.CVR
{
    public class CPRFixture
    {
        private readonly Validators.DK.CPR _sut = new Validators.DK.CPR();
        
        [Fact]
        void ShouldNotFailWithNull() 
            => _sut.Invoking(_ => _.Validate(null)).Should().Throw<InvalidFormatException>();

        [Fact]
        void ShouldBeInvalidForEmptyString()
            => _sut.Invoking(_ => _.Validate("  ")).Should().Throw<InvalidFormatException>();

        [Fact]
        void ShouldBeInvalidForLessThan10Digits()
            => _sut.Invoking(_ => _.Validate("123456789")).Should().Throw<InvalidLengthException>();

        [Fact]
        void ShouldBeInvalidForBadDate() 
            => _sut.Invoking(_ => _.Validate("511062-5629")).Should().Throw<InvalidComponentException>();
        
        [Fact]
        void FutureDateShouldBeInvalid() 
            => _sut.Invoking(_ => _.Validate("2110525629")).Should().Throw<InvalidComponentException>();

        [Fact]
        void ValidShouldBeTrue()
            => _sut.IsValid("051214-7121").Should().BeTrue();
    }
}