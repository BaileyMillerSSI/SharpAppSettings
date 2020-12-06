using System;

namespace SharpAppSettings
{
    /// <summary>
    /// Binds a section of the appsettings to a class
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AppSettingAttribute : Attribute
    {
        /// <summary>
        /// The name of the section in appsettings
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Binds a section of the appsettings to a class
        /// </summary>
        /// <param name="name">The name of the section in appsettings</param>
        public AppSettingAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Section name cannot be null, empty, or white space.", nameof(name));
            }

            Name = name;
        }
    }
}