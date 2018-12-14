using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using MakeNotes.Framework.Win32;
using static MakeNotes.Framework.Win32.NativeMethods;

namespace MakeNotes.Framework.Utilities
{
    /// <summary>
    /// Utility class containing WPF-specific methods associated with a display monitor.
    /// </summary>
    public static class WpfScreen
    {
        private static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        {
            GetCursorPos(out POINT lMousePosition);

            IntPtr lPrimaryScreen = MonitorFromPoint(new POINT(0, 0), MonitorOptions.MONITOR_DEFAULTTOPRIMARY);
            MONITORINFO lPrimaryScreenInfo = new MONITORINFO();
            if (GetMonitorInfo(lPrimaryScreen, lPrimaryScreenInfo) == false)
            {
                return;
            }

            IntPtr lCurrentScreen = MonitorFromPoint(lMousePosition, MonitorOptions.MONITOR_DEFAULTTONEAREST);

            MINMAXINFO lMmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));

            if (lPrimaryScreen.Equals(lCurrentScreen))
            {
                lMmi.ptMaxPosition.X = lPrimaryScreenInfo.rcWork.Left;
                lMmi.ptMaxPosition.Y = lPrimaryScreenInfo.rcWork.Top;
                lMmi.ptMaxSize.X = lPrimaryScreenInfo.rcWork.Right - lPrimaryScreenInfo.rcWork.Left;
                lMmi.ptMaxSize.Y = lPrimaryScreenInfo.rcWork.Bottom - lPrimaryScreenInfo.rcWork.Top;
            }
            else
            {
                lMmi.ptMaxPosition.X = lPrimaryScreenInfo.rcMonitor.Left;
                lMmi.ptMaxPosition.Y = lPrimaryScreenInfo.rcMonitor.Top;
                lMmi.ptMaxSize.X = lPrimaryScreenInfo.rcMonitor.Right - lPrimaryScreenInfo.rcMonitor.Left;
                lMmi.ptMaxSize.Y = lPrimaryScreenInfo.rcMonitor.Bottom - lPrimaryScreenInfo.rcMonitor.Top;
            }

            Marshal.StructureToPtr(lMmi, lParam, fDeleteOld: true);
        }
        
        private static IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == (int)WindowNotifications.WM_GETMINMAXINFO)
            {
                WmGetMinMaxInfo(hwnd, lParam);
            }

            return IntPtr.Zero;
        }

        /// <summary>
        /// Attaches a handler that is called when the window is moved to properly set the window's max height and width
        /// depending on current screen to prevent covering the taskbar by the window when it is maximized.
        /// Used in scenario when WindowStyle is set to None and the window covers the taskbar when is maximized.
        /// </summary>
        /// <param name="window">WPF window instance.</param>
        public static void AttachWindowResizingHandler(Window window)
        {
            IntPtr mWindowHandle = new WindowInteropHelper(window).Handle;
            HwndSource.FromHwnd(mWindowHandle).AddHook(new HwndSourceHook(WindowProc));
        }
    }
}
