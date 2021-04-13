using System;
using FluentAssertions;
using NumberValidator.Helpers;
using Xunit;

namespace NumberValidator.Tests.NO.Fodselnummer
{
    public class FodselnummerFixture
    {
        private readonly Validators.NO.FSN _sut = new Validators.NO.FSN();

        [Fact]
        void ShouldNotFailWithNull()
            => _sut.Invoking(_ => _.Validate(null)).Should().Throw<InvalidFormatException>();

        [Fact]
        void ShouldBeInvalidForEmptyString()
            => _sut.Invoking(_ => _.Validate("  ")).Should().Throw<InvalidFormatException>();

        [Fact]
        void ShouldBeInvalidForLessThan11Digits()
            => _sut.Invoking(_ => _.Validate("123456789")).Should().Throw<InvalidLengthException>()

        [Fact]
        void ValidFodselnummerShouldBeTrue()
            => _sut.IsValid("151086 95088").Should().BeTrue();

        [Fact]
        void InValidFodselnummerShouldBeFalse()
            => _sut.IsValid("051214-2122").Should().BeFalse();
        [Fact]
        void ShouldBeInvalidForWrongChecksum()
          => Assert.Throws<InvalidChecksumException>(() => _sut.Validate("15108695077"));
    }
}