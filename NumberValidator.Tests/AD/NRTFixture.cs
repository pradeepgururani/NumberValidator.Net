using NumberValidator.Helpers;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NumberValidator.Tests.DK
{
    public class NRTFixture
    {
        private readonly Validators.DK.NRT _sut = new Validators.DK.NRT();

        [Fact]
        void ShouldBeInvalidForLessThan8Digits()
            => Assert.Throws<InvalidFormatException>(() => _sut.Validate("1234567"));

        [Fact]
        void ShouldBeInvalidFormatFirstandlastcharactermustbletters()
            => Assert.Throws<InvalidFormatException>(() => _sut.Validate("A1234567"));

        [Fact]
        void ShouldBeInvalidFormatFirstlettermustBe()
            => Assert.Throws<InvalidComponentException>(() => _sut.Validate("X059888N"));

        [Fact]
        void ShouldBeValidForother()
            => _sut.IsValid("U132950X").Should().BeTrue();

        [Fact]
        void ShouldBeInValidcode()
           => _sut.IsValid("U13925000X").Should().BeFalse();

        [Fact]
        void ShouldBeInvalidFormatNumberHigher()
           => Assert.Throws<InvalidComponentException>(() => _sut.Validate("F700000A"));

        [Fact]
        void ShouldBeInvalidFormatNumberBetween()
           => Assert.Throws<InvalidComponentException>(() => _sut.Validate("A800000X"));



    }
}
       