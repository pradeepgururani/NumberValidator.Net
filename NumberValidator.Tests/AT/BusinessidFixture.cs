using FluentAssertions;
using NumberValidator.Validators.AT;
using Xunit;

namespace NumberValidator.Tests.AT
{
    public class BusinessIdFixture
    {
        private readonly BusinessId _sut = new BusinessId();

        [Fact]
        void ValidBusinessidShouldBeTrue()
                => _sut.IsValid("122119m").Should().BeTrue();

        [Fact]
        void InValidBusinessShouldBeFalse()
                => _sut.IsValid("FN 122119m").Should().BeFalse();
    }
}