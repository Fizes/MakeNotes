using System;
using System.IO;
using MakeNotes.Common.Infrastructure.Extensions;
using MakeNotes.DAL.Infrastructure;
using Xunit;

namespace MakeNotes.UnitTests.DAL
{
    public class SQLiteConnectionStringParserTests
    {
        private const string ApplicationFolderName = "MakeNotes";

        private static string GetSpecialFolderFullPath(string folder)
        {
            var specialFolder = Enum.Parse<Environment.SpecialFolder>(folder);
            return Path.Combine(Environment.GetFolderPath(specialFolder), ApplicationFolderName);
        }

        private static string GetResultConnectionString(string original)
        {
            return StringExtensions.ToKeyValuePairString(SQLiteConnectionStringKeys.DataSource, original);
        }

        [Fact]
        public void Parse_ShouldReturnLocalPath_WhenNoDataSourceGiven()
        {
            var connectionString = "Database=test";

            var result = SQLiteConnectionStringParser.Parse(connectionString);

            var expected = GetResultConnectionString(Path.Combine(Environment.CurrentDirectory, "test.db"));
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Parse_ShouldReturnRelativePath_WhenDataSourceIsNotSpecialFolderAndNotRootedPath()
        {
            var connectionString = @"Data Source=TestFolder\SubFolder;Database=test";

            var result = SQLiteConnectionStringParser.Parse(connectionString);

            var expected = GetResultConnectionString(Path.Combine(Environment.CurrentDirectory, @"TestFolder\SubFolder", ApplicationFolderName, "test.db"));
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Parse_ShouldReturnAbsolutePath_WhenDataSourceIsRootedPath()
        {
            var connectionString = @"Data Source=c:\TestFolder\SubFolder;Database=test";

            var result = SQLiteConnectionStringParser.Parse(connectionString);

            var expected = GetResultConnectionString(Path.Combine(@"c:\TestFolder\SubFolder", ApplicationFolderName, "test.db"));
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Desktop")]
        [InlineData("MyDocuments")]
        [InlineData("ApplicationData")]
        public void Parse_ShouldReturnSystemPath_WhenSpecialFolderAsDataSourceGiven(string specialFolder)
        {
            var connectionString = $"Data Source={specialFolder};Database=test";

            var result = SQLiteConnectionStringParser.Parse(connectionString);

            var expected = GetResultConnectionString(Path.Combine(GetSpecialFolderFullPath(specialFolder), "test.db"));
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void Parse_ShouldNotFail_WhenRedundantParametersGiven()
        {
            var connectionString = @";Redundant1=random; ;Data Source=ApplicationData;Some ;Redundant=test value;Database=test";

            var result = SQLiteConnectionStringParser.Parse(connectionString);

            var expected = GetResultConnectionString(Path.Combine(GetSpecialFolderFullPath("ApplicationData"), "test.db"));
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Parse_ShouldThrowArgumentNullException_WhenNoDatabaseGiven()
        {
            var connectionString = @"Data Source=Test;";
            
            Assert.Throws<ArgumentNullException>(() => SQLiteConnectionStringParser.Parse(connectionString));
        }

        [Fact]
        public void Parse_ShouldThrowArgumentNullException_WhenNoConnectionStringGiven()
        {
            var connectionString = " ";

            Assert.Throws<ArgumentNullException>(() => SQLiteConnectionStringParser.Parse(connectionString));
        }
    }
}
