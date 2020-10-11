using FluentAssertions;
using NumberValidator.Helpers;
using Xunit;

namespace NumberValidator.Tests
{
    public class ExtensionFixture
    {
        [Fact]
        void ShouldRemoveSpacesFromFrontAndBack() 
            => " 1234 ".Clean().Should().Be("1234");
        
        [Fact]
        void ShouldRemoveSpacesFromEverywhere() 
            => " 1 2 3 4 ".Clean().Should().Be("1234");
    }
}