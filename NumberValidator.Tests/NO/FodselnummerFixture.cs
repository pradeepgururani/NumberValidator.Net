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
            => _sut.Invoking(_ => _.Validate("123456789")).Should().Throw<InvalidLengthException>();

        [Fact]
        void ValidFodselnummerShouldBeTrue()
            => _sut.IsValid("15108695088").Should().BeTrue();

        [Fact]
        void InValidFodselnummerShouldBeFalse()
            => _sut.IsValid("051214-2122").Should().BeFalse();

        [Fact]
        void GetGenderShouldBeFemale()
            => _sut.GetGender("151086-95088").Should().Be('F');
        
        [Fact]
        void ShouldBeInvalidForWrongCheckDigit1()
          => Assert.Throws<InvalidChecksumException>(() => _sut.Validate("15108695058"));

        [Fact]
        void ShouldBeInvalidForWrongCheckDigit2()
         => Assert.Throws<InvalidChecksumException>(() => _sut.Validate("15108695085"));

        [Fact]
        void ShouldBeInvalidForBadDate()
           => _sut.Invoking(_ => _.Validate("151386-95088")).Should().Throw<ArgumentOutOfRangeException>();

        [Fact]
        void FutureDateShouldBeInvalid()
            => _sut.BirthdateInFuture("15102395088").Should().BeTrue();

        [Fact]
        void ShouldBeInvalidForIndividualDigitsGreaterThanThousand()
           => _sut.Invoking(_ => _.Validate("151340-80088")).Should().Throw<InvalidComponentException>();

        [Fact]
        void ShouldBeInvalidForDayGreaterThaneighty()
           => _sut.Invoking(_ => _.Validate("811340-95088")).Should().Throw<InvalidComponentException>();

        [Fact]
        void CheckDigit1ShouldBeeight()
           => _sut.CheckDigit1("15108695088").Should().Be(8);

        [Fact]
        void CheckDigit2ShouldBeeight()
       => _sut.CheckDigit2("15108695088").Should().Be(8);

    }
}