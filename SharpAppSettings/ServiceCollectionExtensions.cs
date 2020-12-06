using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace SharpAppSettings
{
    public static class ServiceCollectionExtensions
    {
        public static void AddTypedSettings(this IServiceCollection services, IConfiguration configuration, params Assembly[] assemblies)
        {
            var injectableSettings = assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsDefined(typeof(AppSettingAttribute), false));

            var configureMethod = GetConfigureMethod();

            foreach (var setting in injectableSettings)
            {
                ConfigureSettings(services, configuration, setting, configureMethod);
            }
        }

        private static void ConfigureSettings(IServiceCollection services, IConfiguration configuration, Type setting, MethodInfo configureMethod)
        {
            var configurationSection = GetConfigurationSection(configuration, setting);

            Action<BinderOptions> action = options => options.BindNonPublicProperties = false;

            var generic = configureMethod.MakeGenericMethod(setting);

            generic.Invoke(null, new object[] { services, configurationSection, action });
        }

        private static IConfigurationSection GetConfigurationSection(IConfiguration configuration, Type appSettingType)
        {
            var attribute = (AppSettingAttribute)Attribute.GetCustomAttribute(appSettingType, typeof(AppSettingAttribute));

            var configurationSection = configuration.GetSection(attribute.Name);

            return configurationSection;
        }

        private static MethodInfo GetConfigureMethod()
        {
            return typeof(OptionsConfigurationServiceCollectionExtensions).GetMethod("Configure", new[]
            {
                typeof(IServiceCollection),
                typeof(IConfiguration),
                typeof(Action<BinderOptions>)
            });
        }
    }
}