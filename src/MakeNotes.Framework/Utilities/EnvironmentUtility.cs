using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Windows;
using System.Windows.Markup;

namespace MakeNotes.Framework.Utilities
{
    /// <summary>
    /// Contains additional methods associated with <see cref="Environment"/>.
    /// </summary>
    public static class EnvironmentUtility
    {
        /// <summary>
        /// Retrieves the value of a variable from the command-line arguments. 
        /// </summary>
        /// <param name="variable">Variable key.</param>
        /// <returns></returns>
        public static string GetCommandLineVariable(string variable)
        {
            var variables = GetCommandLineVariables();
            var value = variables[variable];

            return value;
        }

        /// <summary>
        /// Returns a name-value collection containing variables from the command-line arguments.
        /// </summary>
        /// <returns></returns>
        public static NameValueCollection GetCommandLineVariables()
        {
            var args = Environment.GetCommandLineArgs();
            var queryString = String.Join("&", args);
            var variables = HttpUtility.ParseQueryString(queryString);

            return variables;
        }

        /// <summary>
        /// Sets current locale of the application equal to the specified culture.
        /// </summary>
        /// <param name="locale">Culture name.</param>
        public static void SetCurrentLocale(string locale)
        {
            if (String.IsNullOrWhiteSpace(locale))
            {
                return;
            }

            var culture = new CultureInfo(locale);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            var metadata = new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag));
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), metadata);
        }
    }
}
