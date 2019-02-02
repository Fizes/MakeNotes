using System;
using System.Threading.Tasks;
using MakeNotes.DAL.Models;
using MakeNotes.Notebook.Core.Commands;
using MakeNotes.Notebook.Core.Commands.Handlers;

namespace MakeNotes.IntegrationTests.Infrastructure.DataGenerators
{
    public static class TabGenerator
    {
        public async static Task<Tab> CreateTab(int order, string name = null)
        {
            var command = new CreateTab(name ?? Guid.NewGuid().ToString("N"), order);
            var handler = DependencyResolver.Resolve<TabCommandHandler>();
            var id = await handler.ExecuteAsync(command);

            return new Tab
            {
                Id = id,
                Name = command.Name,
                Order = command.Order
            };
        }
    }
}
