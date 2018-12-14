using Microsoft.Extensions.Configuration;

namespace MakeNotes.Infrastructure.Extensions
{
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Gets a configuration sub-section with a key that equals to the name of the specified type.
        /// </summary>
        /// <typeparam name="T">Type as a key.</typeparam>
        /// <param name="configuration"><see cref="IConfiguration"/> instance.</param>
        /// <returns></returns>
        public static IConfigurationSection GetSection<T>(this IConfiguration configuration) where T : class, new()
        {
            return configuration.GetSection(typeof(T).Name);
        }

        /// <summary>
        /// Gets the configuration instance of the specified type from section with a key that equals to the name of the specified type.
        /// If no such section found a default instance of <see cref="T"/> is returned.
        /// </summary>
        /// <typeparam name="T">Type of section.</typeparam>
        /// <param name="configuration"><see cref="IConfiguration"/> instance.</param>
        /// <returns></returns>
        public static T GetConfiguration<T>(this IConfiguration configuration) where T : class, new()
        {
            return configuration.GetSection<T>().Get<T>() ?? new T();
        }

        /// <summary>
        /// Gets the configuration instance of the specified type from section with the specified key.
        /// If no such section found a default instance of <see cref="T"/> is returned.
        /// </summary>
        /// <typeparam name="T">Type of section.</typeparam>
        /// <param name="configuration"><see cref="IConfiguration"/> instance.</param>
        /// <param name="key">Section key.</param>
        /// <returns></returns>
        public static T GetConfiguration<T>(this IConfiguration configuration, string key) where T : class, new()
        {
            return configuration.GetSection(key).Get<T>() ?? new T();
        }
    }
}
