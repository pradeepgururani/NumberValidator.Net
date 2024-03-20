using System;
using FluentAssertions;
using NumberValidator.Helpers;
using Xunit;

namespace NumberValidator.Tests.PAN
{
    public class PANFixture
    {
        private readonly Validators.PAN.PAN _sut = new Validators.PAN.PAN();
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
    }
}
