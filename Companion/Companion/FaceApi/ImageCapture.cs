using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companion.FaceApi
{
    // uses Emgu to get an image from the webcam
    class ImageCapture
    {
        private readonly VideoCapture _capture = new VideoCapture();

        public Bitmap GetCapture()
        {
            return _capture.QueryFrame().Bitmap;
        }
    }
}
