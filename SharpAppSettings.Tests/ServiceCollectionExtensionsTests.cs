using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Linq;
using Xunit;

namespace SharpAppSettings.Tests
{
    public class ServiceCollectionExtensionsTests
    {
        private readonly IServiceCollection serviceCollection;
        private readonly IConfiguration configuration;

        public ServiceCollectionExtensionsTests()
        {
            serviceCollection = new ServiceCollection();
            configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        [Fact]
        public void ItWillRegisterSettingsCorrectly()
        {
            var countBefore = serviceCollection.Count();

            serviceCollection
                .AddTypedSettings(configuration, typeof(ServiceCollectionExtensionsTests).Assembly);

            var countAfter = serviceCollection.Count();

            countAfter
                .Should()
                .BeGreaterThan(countBefore);
        }

        [Fact]
        public void ItWillInjectOptionCorrectly()
        {
            var injectedSettings = GetTestOption();

            injectedSettings
                .Should()
                .NotBeNull();
        }

        [Fact]
        public void ItWillInjectOptionMonitorCorrectly()
        {
            var injectedSettings = GetTestOptionMonitor();

            injectedSettings
                .Should()
                .NotBeNull();
        }

        [Fact]
        public void ItWillInjectOptionPropertiesCorrectly()
        {
            var injectedSettings = GetTestOption().Value;

            injectedSettings.StringValue.Should().Be("value");
            injectedSettings.NumberValue.Should().Be(1);
            injectedSettings.BooleanValue.Should().BeFalse();
            injectedSettings.ArrayOfNumbersValue.Should().BeEquivalentTo(new int[] { 0, 1, 2, 3 });
            injectedSettings.ArrayOfStringsValue.Should().BeEquivalentTo(new string[] { "a", "b", "c" });
            injectedSettings.NestedValue.StringValue.Should().Be("nested value");
        }

        [Fact]
        public void ItWillInjectOptionMonitorPropertiesCorrectly()
        {
            var injectedSettings = GetTestOptionMonitor().CurrentValue;

            injectedSettings.StringValue.Should().Be("value");
            injectedSettings.NumberValue.Should().Be(1);
            injectedSettings.BooleanValue.Should().BeFalse();
            injectedSettings.ArrayOfNumbersValue.Should().BeEquivalentTo(new int[] { 0, 1, 2, 3 });
            injectedSettings.ArrayOfStringsValue.Should().BeEquivalentTo(new string[] { "a", "b", "c" });
            injectedSettings.NestedValue.StringValue.Should().Be("nested value");
        }

        private IOptions<TestSettings> GetTestOption()
        {
            serviceCollection
                .AddTypedSettings(configuration, typeof(ServiceCollectionExtensionsTests).Assembly);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider.GetService<IOptions<TestSettings>>();
        }

        private IOptionsMonitor<TestSettings> GetTestOptionMonitor()
        {
            serviceCollection
                .AddTypedSettings(configuration, typeof(ServiceCollectionExtensionsTests).Assembly);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider.GetService<IOptionsMonitor<TestSettings>>();
        }
    }
}