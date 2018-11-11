using System;
using System.Collections.Specialized;
using System.Web;

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
    }
}
