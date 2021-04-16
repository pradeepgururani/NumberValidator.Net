using System;
using FluentAssertions;
using NumberValidator.Helpers;
using Xunit;

namespace NumberValidator.Tests.NO.IBAN
{
    public class IBANFixture
    {
        private readonly Validators.NO.IBAN _sut = new Validators.NO.IBAN();

        [Fact]
        void ShouldBeInvalidForLessThan15Digits()
            => _sut.Invoking(_ => _.Validate("123456789")).Should().Throw<InvalidLengthException>();

        [Fact]
        void ValidIbanShouldBeTrue()
            => _sut.IsValid("NO9386011117947").Should().BeTrue();

        [Fact]
        void InValidIbanShouldBeFalse()
            => _sut.IsValid("GR1601101050000010547023795").Should().BeFalse();

        [Fact]
        void ShouldBeInvalidIfNotStartsWithNo()
         => _sut.Invoking(_ => _.Validate("GR9386011117947")).Should().Throw<InvalidComponentException>();

        [Fact]
        void ShouldBeInvalidIfKotonrContainsAlpha()
        => _sut.Invoking(_ => _.Validate("NO93860111179NO")).Should().Throw<InvalidFormatException>();

    }
}