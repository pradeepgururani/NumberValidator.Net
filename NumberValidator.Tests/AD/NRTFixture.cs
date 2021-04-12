using NumberValidator.Helpers;
using FluentAssertions;
using Xunit;

namespace NumberValidator.Tests.AD
{
    public class NRTFixture
    {
        private readonly Validators.AD.NRT _sut = new Validators.AD.NRT();

       [Fact]
        void ShouldBeInvalidForLessThan8Digits()
            => Assert.Throws<InvalidLengthException>(() => _sut.Validate("0123456"));

        [Fact]
        void ShouldBeInvalidFormatFirstandLastCharacterMustbLetters()
            => Assert.Throws<InvalidFormatException>(() => _sut.Validate("A1234567"));

        [Fact]
        void ShouldBeInvalidFormatFirstLetterMustBe()
            => Assert.Throws<InvalidComponentException>(() => _sut.Validate("X059888N"));

        [Fact]
        void ShouldBeValidForOther()
            => _sut.IsValid("U132950X").Should().BeTrue();

        [Fact]
        void ShouldBeInValidCode()
           => _sut.IsValid("U13925000X").Should().BeFalse();

        [Fact]
        void ShouldBeInvalidFormatNumberHigher()
           => Assert.Throws<InvalidComponentException>(() => _sut.Validate("F700000A"));

        [Fact]
        void ShouldBeInvalidFormatNumberBetween()
           => Assert.Throws<InvalidComponentException>(() => _sut.Validate("A800000X"));

    }
}
       