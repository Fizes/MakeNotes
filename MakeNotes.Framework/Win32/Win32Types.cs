using System.Runtime.InteropServices;

namespace MakeNotes.Framework.Win32
{
    /// <summary>
    /// Multi monitor function codes.
    /// </summary>
    public enum MonitorOptions : uint
    {
        /// <summary>
        /// Returns NULL.
        /// </summary>
        MONITOR_DEFAULTTONULL = 0x00000000,
        /// <summary>
        /// Returns a handle to the primary display monitor.
        /// </summary>
        MONITOR_DEFAULTTOPRIMARY = 0x00000001,
        /// <summary>
        /// Returns a handle to the display monitor that is nearest to the window.
        /// </summary>
        MONITOR_DEFAULTTONEAREST = 0x00000002
    }

    public enum WindowNotifications : uint
    {
        /// <summary>
        /// Sent to a window when the size or position of the window is about to change.
        /// </summary>
        WM_GETMINMAXINFO = 0x0024
    }

    /// <summary>
    /// Defines the x- and y- coordinates of a point.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;

        public POINT(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    /// <summary>
    /// Contains information about a window's maximized size and position and its minimum and maximum tracking size.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MINMAXINFO
    {
        public POINT ptReserved;
        public POINT ptMaxSize;
        public POINT ptMaxPosition;
        public POINT ptMinTrackSize;
        public POINT ptMaxTrackSize;
    };

    /// <summary>
    /// Contains information about a display monitor.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class MONITORINFO
    {
        public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));
        public RECT rcMonitor = new RECT();
        public RECT rcWork = new RECT();
        public int dwFlags = 0;
    }

    /// <summary>
    /// Defines the coordinates of the upper-left and lower-right corners of a rectangle.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public RECT(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }
    }
}
