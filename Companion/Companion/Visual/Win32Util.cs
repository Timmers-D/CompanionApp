using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Companion.Visual
{
    // structs and values in support of the C++ Win32 API functions
    class Win32Util
    {
        public const byte AC_SRC_OVER = 0x00;
        public const byte AC_SRC_ALPHA = 0x01;

        public const uint WM_DESTROY = 0x0002;
        public const uint WM_CREATE = 0x0001;
        public const uint WM_PAINT = 0x000F;
        public const uint WM_ERASEBKGND = 0x0014;
               
        public const uint WS_OVERLAPPED = 0x00000000;
        public const uint WS_MAXIMIZEBOX = 0x00010000;
        public const uint WS_MINIMIZEBOX = 0x00020000;
        public const uint WS_THICKFRAME = 0x00040000;
        public const uint WS_SYSMENU = 0x00080000;
        public const uint WS_CAPTION = 0x00C00000;
        public const uint WS_VISIBLE = 0x10000000;
        public const uint WS_EX_LAYERED = 0x00080000;
        public const uint WS_EX_TOPMOST = 0x00000008;
        public const uint WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX;
               
        public const uint IDI_APPLICATION = 32512;
               
        public const uint IDC_ARROW = 32512;
               
        public const uint COLOR_WINDOW = 5;
               
        public const int GWL_EXSTYLE = -20;
               
        public const uint MB_OK = 0x00000000;
        public const uint MB_ICONEXCLAMATION = 0x00000030;
        public const uint MB_SETFOREGROUND = 0x00010000;

        public const uint LWA_ALPHA = 0x00000002;

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left, top, right, bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SIZE
        {
            public int cx;
            public int cy;

            public SIZE(int cx, int cy)
            {
                this.cx = cx;
                this.cy = cy;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct COLORREF
        {
            public byte R;
            public byte G;
            public byte B;

            public COLORREF(byte r, byte g, byte b)
            {
                R = r;
                G = g;
                B = b;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MSG
        {
            public IntPtr hwnd;
            public uint message;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public POINT pt;
        }

        public enum MessageBoxResult : uint
        {
            Ok = 1,
            Cancel,
            Abort,
            Retry,
            Ignore,
            Yes,
            No,
            Close,
            Help,
            TryAgain,
            Continue,
            Timeout = 32000
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PAINTSTRUCT
        {
            public IntPtr hdc;
            public bool fErase;
            public RECT rcPaint;
            public bool fRestore;
            public bool fIncUpdate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] rgbReserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WNDCLASSEX
        {
            public uint cbSize;
            public ClassStyles style;
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public Win32.WndProc lpfnWndProc;
            public int cbClsExtra;
            public int cbWndExtra;
            public IntPtr hInstance;
            public IntPtr hIcon;
            public IntPtr hCursor;
            public IntPtr hbrBackground;
            public string lpszMenuName;
            public string lpszClassName;
            public IntPtr hIconSm;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DEVMODE
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public int dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;

            public BLENDFUNCTION(byte op, byte flags, byte alpha, byte format)
            {
                BlendOp = op;
                BlendFlags = flags;
                SourceConstantAlpha = alpha;
                AlphaFormat = format;
            }
        }

        [Flags]
        public enum ClassStyles : uint
        {
            ByteAlignClient = 0x1000,
            ByteAlignWindow = 0x2000,
            ClassDC = 0x40,
            DoubleClicks = 0x8,
            DropShadow = 0x20000,
            GlobalClass = 0x4000,
            HorizontalRedraw = 0x2,
            NoClose = 0x200,
            OwnDC = 0x20,
            ParentDC = 0x80,
            SaveBits = 0x800,
            VerticalRedraw = 0x1
        }
    }
}
