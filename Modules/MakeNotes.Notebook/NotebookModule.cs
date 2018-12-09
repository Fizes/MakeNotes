﻿using Autofac;

namespace MakeNotes.Notebook
{
    public class NotebookModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(GetType().Assembly).PublicOnly();
        }
    }
}
