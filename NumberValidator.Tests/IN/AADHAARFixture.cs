using Xunit;
using FluentAssertions;
using NumberValidator.Helpers;

namespace NumberValidator.Tests.IN
{
    public class AADHAARFixture
    {
        private readonly Validators.IN.AADHAAR _sut = new Validators.IN.AADHAAR();
        [Fact]
        void ShouldBeInvalidForLessThan12Digits()
            => Assert.Throws<InvalidLengthException>(() => _sut.Validate("22345678"));

        [Fact]
        void ShouldBeInvalidFormatCanNotBePalindrome()
            => Assert.Throws<InvalidFormatException>(() => _sut.Validate("223567765322"));

        [Fact]
        void ShouldBeInvalidFormatPatternNotFollowed()
            => Assert.Throws<InvalidFormatException>(() => _sut.Validate("128565765309"));

        [Fact]
        void ShouldBeValid()
            => _sut.IsValid("234675789836").Should().BeTrue();
    }
}