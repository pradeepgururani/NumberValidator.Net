using System;
using FluentAssertions;
using NumberValidator.Helpers;
using Xunit;
using NumberValidator.Validators.AR;

namespace NumberValidator.Tests.AR
{
    public class DNIFixture
    {
        private readonly DNI _sut=new DNI();
        [Fact]
        void ShouldBeInvalidForLessThan8Digits()
            => Assert.Throws<InvalidLengthException>(() => _sut.Validate("012345"));

        [Fact]
        void ShouldBeInvalidForMoreThan8Digits()
            => Assert.Throws<InvalidLengthException>(() => _sut.Validate("0123456789"));

        [Fact]
        void ShouldBeValidForOthers()
            => Assert.Throws<InvalidLengthException>(() => _sut.Validate("0123456"));

    }
}
