using System;
using System.Runtime.InteropServices;

namespace MakeNotes.Framework.Win32
{
    /// <summary>
    /// Contains methods of Win32 Api.
    /// </summary>
    internal static class NativeMethods
    {
        [DllImport(Win32AssemblyNames.User32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport(Win32AssemblyNames.User32, SetLastError = true)]
        public static extern IntPtr MonitorFromPoint(POINT pt, MonitorOptions dwFlags);

        [DllImport(Win32AssemblyNames.User32)]
        public static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);
    }
}
