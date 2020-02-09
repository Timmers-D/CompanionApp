using Companion.Companion;
using Companion.Enum;
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Companion.Visual
{
    // create a custom window so we can draw at sufficient fps without having ghosting
    class Window
    {
        // pointer to our window
        public IntPtr hWnd;

        private readonly string AppName = "Companion";
        private readonly string ClassName = "Companion";
        private readonly Win32.WndProc _wndProcReferece;

        // setup window
        public Window()
        {
            _wndProcReferece = WndProc;
            if (RegisterClass() == 0)
                return;
            if (Create() == 0)
                return;
        }

        // create a window class to setup our window
        private int RegisterClass()
        {
            var wcex = new Win32Util.WNDCLASSEX
            {
                style = Win32Util.ClassStyles.DoubleClicks,
                lpfnWndProc = _wndProcReferece,
                cbClsExtra = 0,
                cbWndExtra = 0,
                hIcon = Win32.LoadIcon(IntPtr.Zero, (IntPtr)Win32Util.IDI_APPLICATION),
                hCursor = Win32.LoadCursor(IntPtr.Zero, (int)Win32Util.IDC_ARROW),
                hIconSm = IntPtr.Zero,
                hbrBackground = (IntPtr)(Win32Util.COLOR_WINDOW + 1),
                lpszMenuName = null,
                lpszClassName = ClassName
            };
            wcex.cbSize = (uint)Marshal.SizeOf(wcex);
            if (Win32.RegisterClassEx(ref wcex) == 0)
            {
                Win32.MessageBox(IntPtr.Zero, "RegisterClassEx failed", AppName,
                    (int)(Win32Util.MB_OK | Win32Util.MB_ICONEXCLAMATION | Win32Util.MB_SETFOREGROUND));
                return (0);
            }
            return (1);
        }

        // create the window and set it's properties
        private int Create()
        {
            var size = Instances.GetScreenDimension();
            // TODO remove after testing
            size = new ScreenDimension(500, 500);
            hWnd = Win32.CreateWindowEx(Win32Util.WS_EX_TOPMOST, ClassName, AppName, Win32Util.WS_VISIBLE | Win32Util.WS_EX_LAYERED,
                0, 0, size.GetWidth(), size.GetHeight(), IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
            if (hWnd != IntPtr.Zero)
                return (1);

            Instances.SetWindowSize(new Size(size.GetWidth(), size.GetHeight()));
            
            Win32.MessageBox(IntPtr.Zero, "CreateWindow failed", AppName,
                (int)(Win32Util.MB_OK | Win32Util.MB_ICONEXCLAMATION | Win32Util.MB_SETFOREGROUND));
            return (0);
        }

        // windows message handler
        private IntPtr WndProc(IntPtr hWnd, uint message, IntPtr wParam, IntPtr lParam)
        {
            switch (message)
            {
                // draw
                case Win32Util.WM_PAINT:
                    {
                        Instances.GetManager().DrawCompanions(hWnd);
                        //var hDC = Win32.BeginPaint(hWnd, out Win32Util.PAINTSTRUCT ps);
                        //Win32.TextOut(hDC, 0, 0, Hello, Hello.Length);
                        //Win32.EndPaint(hWnd, ref ps);
                        return IntPtr.Zero;
                    }
                // remove background
                case Win32Util.WM_ERASEBKGND:
                    {
                        return IntPtr.Zero;
                    }
                case Win32Util.WM_DESTROY:
                    {
                        Win32.PostQuitMessage(0);
                        return IntPtr.Zero;
                    }
                //case Win32.WM_CREATE:
                //    return IntPtr.Zero;
                default:
                    return (Win32.DefWindowProc(hWnd, message, wParam, lParam));
            }
        }
    }
}
