using Xunit;
using FluentAssertions;
using NumberValidator.Helpers;

namespace NumberValidator.Tests.IN
{
    public class AadhaarFixture
    {
        private readonly Validators.IN.Aadhaar _sut = new Validators.IN.Aadhaar();

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
