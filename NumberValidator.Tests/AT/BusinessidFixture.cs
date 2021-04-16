using System;
using FluentAssertions;
using NumberValidator.Helpers;
using Xunit;

namespace NumberValidator.Tests.AT
{
    public class BusinessidFixture
    {
        private readonly Validators.AT.BUSINESSID _sut = new Validators.AT.BUSINESSID();

        [Fact]
        void ValidBusinesidShouldBeTrue()
                => _sut.IsValid("122119m").Should().BeTrue();
        [Fact]
        void InValidBusinessShouldBeFalse()
           => _sut.IsValid("FN 122119m").Should().BeFalse();

    }
}