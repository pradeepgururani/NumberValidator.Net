using System;
using FluentAssertions;
using NumberValidator.Helpers;
using Xunit;

namespace NumberValidator.Tests.AR
{
    public class CBUFixture
    { 
        private readonly Validators.AR.cbu _sut = new Validators.AR.cbu();

        [Fact]
        void ShouldBeInvalidForMoreThan22Digits()
           => Assert.Throws<InvalidLengthException>(() => _sut.Validate("281119094009041813520134"));

        [Fact]
        void ShouldBeInvalidForLessThan22Digits()
           => Assert.Throws<InvalidLengthException>(() => _sut.Validate("28105909400904181301"));


        [Fact]
        void ShouldBeInvalidForWrongChecksumTill7DoeNotEqualTODigit8()
           => Assert.Throws<InvalidChecksumException>(() => _sut.Validate("2850590840090418135201"));

        [Fact]
        void ShouldBeInvalidForWrongChecksum0fTilll8isNotEqualToLastDigit()
           => Assert.Throws<InvalidChecksumException>(() => _sut.Validate("2850590940090418135207"));

        [Fact]
        void ShouldBeValid()
           => _sut.IsValid("2850590940090418135201").Should().BeTrue();

        [Fact]
        void ShouldBeValidWithSpaces()
            => _sut.IsValid(" 28505909 40090418135201 ").Should().BeTrue();

        [Fact]
        void ShouldBeValidWithDashes()
            => _sut.IsValid("285059-0940090418135201").Should().BeTrue();

    }
}
