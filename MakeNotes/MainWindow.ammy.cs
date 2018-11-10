using System;
using MakeNotes.Framework;

namespace MakeNotes
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            SourceInitialized += Window_SourceInitialized;
        }
        
        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            WpfScreen.AttachWindowResizingHandler(this);
        }
    }
}
