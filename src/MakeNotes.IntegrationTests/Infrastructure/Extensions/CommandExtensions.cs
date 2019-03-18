using System.Windows.Input;

namespace MakeNotes.IntegrationTests
{
    public static class CommandExtensions
    {
        /// <summary>
        /// Invokes the command without any parameters.
        /// </summary>
        /// <param name="command"></param>
        public static void Execute(this ICommand command)
        {
            command.Execute(null);
        }
    }
}
