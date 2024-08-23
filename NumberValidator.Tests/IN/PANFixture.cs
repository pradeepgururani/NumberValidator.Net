using System;
using FluentAssertions;
using NumberValidator.Helpers;
using NumberValidator.Validators.IN;
using Xunit;

namespace NumberValidator.Tests.IN
{
    public class PANFixture
    {
        private readonly PAN _sut = new PAN();

        [Fact]
        void ShouldBeInvalidForLessThan10Digits()
            => Assert.Throws<InvalidLengthException>(() => _sut.Validate("012345678"));

        [Fact]
        void ShouldBeInvalidFormatFivethtoNinthlettercannotbezero()
            => Assert.Throws<InvalidComponentException>(() => _sut.Validate("1234000005"));

        [Fact]
        void ShouldBeInvalidFormatPatternNotFollowed()
            => Assert.Throws<InvalidFormatException>(() => _sut.Validate("ABCD1234EF"));

        [Fact]
        void ShouldBeValidForOther()
            => _sut.IsValid("ACUPA7085R").Should().BeTrue();

        [Fact]
        void ShouldBeValidForOthers()
            => _sut.IsValid("ACU PA-708 5-R").Should().BeTrue();
    }
}