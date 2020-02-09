using Companion.Struct;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Threading;

namespace Companion.Visual
{
    class Drawing
    {
        public static void DrawBitmaps(IntPtr hWnd, List<BitmapLocation> bitLocs)
        {
            // setup canvas to draw all ze things
            var size = Instances.GetScreenDimension();
            var memBmp = new Bitmap(size.GetWidth(), size.GetHeight(), PixelFormat.Format32bppArgb);

            // get all the handles and objects needed
            var clientDC = Graphics.FromHwnd(hWnd);
            var hdc = clientDC.GetHdc();
            var memdc = Win32.CreateCompatibleDC(hdc);
            var hbitmap = memBmp.GetHbitmap();
            Win32.SelectObject(memdc, hbitmap);
            var memDC = Graphics.FromHdc(memdc);

            // stamp all the individual images onto the canvas
            foreach (var bitLoc in bitLocs)
            {
                var attributes = bitLoc.IsFace ? new ImageAttributes() : MatrixHolder.GetMatrix(bitLoc.Mood);
                memDC.DrawImage(
                    bitLoc.Bitmap,
                    new Rectangle((int)bitLoc.Position.X, (int)bitLoc.Position.Y, bitLoc.Bitmap.Width, bitLoc.Bitmap.Height),
                    0, 0,
                    bitLoc.Bitmap.Width,
                    bitLoc.Bitmap.Height,
                    GraphicsUnit.Pixel,
                    attributes);
            }

            // get the handle to the canvas
            var hMemdc = memDC.GetHdc();

            // we only want to copy the final image over (AC_SRC_OVER) to the destination, and keep alpha values (AC_SRC_ALPHA) to retain transparancy
            var bf = new Win32Util.BLENDFUNCTION(Win32Util.AC_SRC_OVER, 0, 0xff, Win32Util.AC_SRC_ALPHA);
            // the actual copying
            Win32.AlphaBlend(hdc, 0, 0, size.GetWidth(), size.GetHeight(), hMemdc, 0, 0, size.GetWidth(), size.GetHeight(), bf);

            // IT'S HAMMER, I mean, garbage time!
            memBmp.Dispose();
            Win32.DeleteObject(hbitmap);
            Win32.DeleteDC(hMemdc);
        }
    }
}
