using System;
using System.Windows;

namespace MakeNotes.Framework.Extensions
{
    public static class ApplicationExtensions
    {
        /// <summary>
        /// Loads a XAML file that is located at the specified path and converts it to
        /// an instance of the object that is specified by the root element of the XAML file.
        /// </summary>
        /// <param name="app">Application instance.</param>
        /// <param name="resourcePath">Path to the application resource.</param>
        public static void InitializeComponent(this Application app, string resourcePath)
        {
            var resourceLocater = new Uri(resourcePath, UriKind.Relative);
            Application.LoadComponent(app, resourceLocater);
        }
    }
}
