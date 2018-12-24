using System;
using System.Collections.Specialized;
using System.Web;

namespace MakeNotes.Common.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Returns a string containing key-value pairs separated by the specified separator as a name-value collection.
        /// Default separator is semicolon.
        /// </summary>
        /// <param name="source">Original string containing key-value pairs.</param>
        /// <returns></returns>
        public static NameValueCollection ToNameValueCollection(this string source, char separator = ';')
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var queryString = source.Replace(separator, '&');
            return HttpUtility.ParseQueryString(queryString);
        }

        /// <summary>
        /// Returns a string array containing key-value pairs as a name-value collection.
        /// </summary>
        /// <param name="source">String array containing key-value pairs.</param>
        /// <returns></returns>
        public static NameValueCollection ToNameValueCollection(this string[] source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var queryString = String.Join("&", source);
            return HttpUtility.ParseQueryString(queryString);
        }

        /// <summary>
        /// Returns a key and value as a key-value string.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToKeyValuePairString(string key, string value)
        {
            return $"{key}={value}";
        }
    }
}
