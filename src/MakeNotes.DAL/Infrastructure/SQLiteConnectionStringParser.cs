using System;
using System.IO;
using System.Web;

namespace MakeNotes.DAL.Infrastructure
{
    public static class SQLiteConnectionStringParser
    {
        private const string ApplicationFolderName = "MakeNotes";

        /// <summary>
        /// Parses the specified connection string to an appropriate string for sqlite.
        /// </summary>
        /// <param name="connectionString">Raw connection string.</param>
        /// <returns></returns>
        public static string Parse(string connectionString)
        {
            if (String.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            var args = connectionString.Replace(';', '&');
            var values = HttpUtility.ParseQueryString(args);

            var folderValue = values.Get(SQLiteConnectionStringKeys.DataSource);
            var databaseValue = values.Get(SQLiteConnectionStringKeys.Database);

            if (String.IsNullOrWhiteSpace(databaseValue))
            {
                throw new ArgumentNullException(SQLiteConnectionStringKeys.Database, $"Parameter '{SQLiteConnectionStringKeys.Database}' not specified");
            }

            var folder = Environment.CurrentDirectory;
            var database = $"{databaseValue}.db";

            if (String.IsNullOrWhiteSpace(folderValue))
            {
                return $"{SQLiteConnectionStringKeys.DataSource}={Path.Combine(folder, database)}";
            }

            // 1. If it is a special folder, get the folder path ending with the application name;
            // 2. If it is relative path, take current directory as a root folder and then the specified path ending with the application name;
            // 3. If it is not relative path, use the specified path ending with the application name
            if (Enum.TryParse(folderValue, out Environment.SpecialFolder specialFolder))
            {
                folder = Path.Combine(Environment.GetFolderPath(specialFolder), ApplicationFolderName);
            }
            else if (!Path.IsPathRooted(folderValue))
            {
                folder = Path.Combine(folder, folderValue, ApplicationFolderName);
            }
            else
            {
                folder = Path.Combine(folderValue, ApplicationFolderName);
            }

            return $"{SQLiteConnectionStringKeys.DataSource}={Path.Combine(folder, database)}";
        }
    }
}
