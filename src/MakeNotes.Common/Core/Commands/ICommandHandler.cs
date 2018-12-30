using System.Threading.Tasks;

namespace MakeNotes.Common.Core.Commands
{
    /// <summary>
    /// Represents a handler that modifies a state of the system and returns no value.
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public interface ICommandHandler<TCommand> : IHandler
        where TCommand : ICommand
    {
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="command">Command instance.</param>
        Task ExecuteAsync(TCommand command);
    }
}
