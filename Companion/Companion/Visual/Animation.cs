using Companion.Enum;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Companion.Visual
{
    class Animation
    {
        // all the images in this animation set
        private readonly List<Bitmap> _images;
        // speed of animation
        private readonly double _fps;
        // current index of images
        private int _currentImage = 0;
        // last change of image
        private DateTime _previousTime = DateTime.Now;

        public Animation(List<Bitmap> images, double fps)
        {
            _images = images;
            _fps = fps;
        }

        // if time between frames is elapsed, get next frame
        public void Update()
        {
            if (Time.Elapsed(_previousTime, _fps))
            {
                _previousTime = DateTime.Now;
                Tick();
            }
        }

        private void Tick()
        {
            if (_currentImage == (_images.Count - 1))
            {
                _currentImage = 0;
            }
            else
            {
                _currentImage++;
            }
        }

        // return the current animation frame
        public Bitmap GetCurrentImage()
        {
            return _images[_currentImage];
        }
    }
}