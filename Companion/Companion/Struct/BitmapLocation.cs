using Companion.Enum;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Companion.Struct
{
    // struct to transfer all the info we need to draw the companions
    public struct BitmapLocation
    {
        public Bitmap Bitmap;
        public PointF Position;
        public Mood Mood;
        // whether it is the image for the face or the body
        public bool IsFace;

        public BitmapLocation(Bitmap bitmap, PointF position, Mood mood, bool isFace)
        {
            Bitmap = bitmap;
            Position = position;
            Mood = mood;
            IsFace = isFace;
        }
    }
}
