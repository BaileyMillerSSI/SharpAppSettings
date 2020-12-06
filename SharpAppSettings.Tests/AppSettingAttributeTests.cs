using FluentAssertions;
using System;
using Xunit;

namespace SharpAppSettings.Tests
{
    public class AppSettingAttributeTests
    {
        [Theory]
        [InlineData("EmailSettings")]
        [InlineData("OtherSettings")]
        public void ItWillSetNameCorrectly(string sectionName)
        {
            var attribute = new AppSettingAttribute(sectionName);

            attribute
                .Name
                .Should()
                .Be(sectionName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ItWillThrowCorrectly(string sectionName)
        {
            Action act = () => new AppSettingAttribute(sectionName);

            act
                .Should()
                .Throw<ArgumentException>();
        }
    }
}