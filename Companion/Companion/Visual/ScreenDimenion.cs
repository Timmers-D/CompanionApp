using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using static Companion.Visual.Win32Util;

namespace Companion.Visual
{
    // C++ Win32 api class to get the size of the current monitor
    class ScreenDimension
    {
        private const int _ENUM_CURRENT_SETTINGS = -1;
        private DEVMODE _devMode = default;

        public ScreenDimension()
        {
            // magic
            _devMode.dmSize = (short)Marshal.SizeOf(_devMode);
            Win32.EnumDisplaySettings(null, _ENUM_CURRENT_SETTINGS, ref _devMode);
        }

        public ScreenDimension(int cx, int cy)
        {
            _devMode.dmPelsWidth = cx;
            _devMode.dmPelsHeight = cy;
        }

        public int GetWidth()
        {
            return _devMode.dmPelsWidth;
        }

        public int GetHeight()
        {
            return _devMode.dmPelsHeight;
        }
    }
}
