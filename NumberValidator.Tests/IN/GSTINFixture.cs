using System;
using FluentAssertions;
using NumberValidator.Helpers;
using NumberValidator.Validators.IN;
using Xunit;

namespace NumberValidator.Tests.IN
{
    public class GSTINFixture
    {
        private readonly GSTIN _sut = new GSTIN();

        [Fact]
        void ShouldBeInvalidForLessThan15Digits()
            => Assert.Throws<InvalidLengthException>(() => _sut.Validate("09ASHIS1234"));

        [Fact]
        void ShouldBeInvalidFormatThirdtoTweflthletterShouldbePAN()
            => Assert.Throws<InvalidComponentException>(() => _sut.Validate("22AAAAA0000A1Z5"));

        [Fact]
        void ShouldBeInvalidFormatPatternNotFollowed()
            => Assert.Throws<InvalidFormatException>(() => _sut.Validate("23ASDG2345A1Z51"));

        [Fact]
        void ShouldBeValidForOther()
            => _sut.IsValid("22AAAAA1234A1Z5").Should().BeTrue();

        [Fact]
        void ShouldBeValidForOthers()
            => _sut.IsValid("22AAA AA123-4A1Z5").Should().BeTrue();
    }
}