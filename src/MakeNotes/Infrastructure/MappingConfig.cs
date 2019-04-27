using MakeNotes.Common.Models;
using MakeNotes.DAL.Models;
using Mapster;

namespace MakeNotes.Infrastructure
{
    public static class MappingConfig
    {
        public static void Configure()
        {
            TypeAdapterConfig<Tab, NavbarTabItem>
                .NewConfig()
                .Map(dest => dest.Header, src => src.Name);
        }
    }
}
