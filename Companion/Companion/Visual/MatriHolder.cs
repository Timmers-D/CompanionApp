using Companion.Enum;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;

namespace Companion.Visual
{
    class MatrixHolder
    {
        // colour matrixes for colour filter blending
        private static float[] _redColorMatrixElements = { 1, 0, 0, 0, 0 };
        private static float[] _greenColorMatrixElements = { 0, 1, 0, 0, 0 };
        private static float[] _blueColorMatrixElements = { 0, 0, 1, 0, 0 };
        private static float[] _lightBlueColorMatrixElements = { 0, 1, 2, 0, 0 };
        private static float[] _yellowColorMatrixElements = { 1.2F, 1.2F, 0, 0, 0 };
        private static float[] _turgoiseColorMatrixElements = { .5F, 1, 1, 0, 0 };
        private static float[] _pinkColorMatrixElements = { 2, 1, 1, 0, 0 };
        private static float[] _orangeColorMatrixElements = { 2, 1, 0, 0, 0 };
        private static float[] _purpleColorMatrixElements = { 1, .5F, 1, 0, 0 };
        private static float[] _randomColorMatrixElements = { 1, .5f, .5f, 0, 0 };
        private static float[] _random1ColorMatrixElements = { 1, 1, .5f, 0, 0 };
        private static float[] _lightPurpleColorMatrixElements = { 1, 1, 2, 0, 0 };

        // colours and emotions connected
        private static Dictionary<Mood, float[]> _colours = new Dictionary<Mood, float[]>()
        {
            { Mood.Anger,       _redColorMatrixElements },          // red
            //{ Mood.Contempt,    _pinkColorMatrixElements },       // pink
            //{ Mood.Disgust,     _purpleColorMatrixElements },     // purple
            { Mood.Fear,        _greenColorMatrixElements },        // dark green
            { Mood.Happiness,   _yellowColorMatrixElements },       // yellow???
            { Mood.Neutral,     _orangeColorMatrixElements },       // orange
            { Mood.Sadness,     _blueColorMatrixElements },         // blue
            { Mood.Surprise,    _lightBlueColorMatrixElements },    // lightblue
            { Mood.WinkyWinky,  _pinkColorMatrixElements }          // pink
        };

        // pad our elements to make a full matrix
        private static float[][] CreateMatrix(float[] transform)
        {
            float[][] colorMatrix =
            {
                transform,
                new float[] {0,0,0,0,0},
                new float[] {0,0,0,0,0},
                new float[] {0,0,0,1,0}, // keep alpha the same
                new float[] {0,0,0,0,0}
            };
            return colorMatrix;
        }

        public static ImageAttributes GetMatrix(Mood mood)
        {
            // create an imageAttributes which saves the colour blend we want to execute
            ImageAttributes imageAttributes = new ImageAttributes();
            ColorMatrix colorMatrix = new ColorMatrix(CreateMatrix(_colours[mood]));

            imageAttributes.SetColorMatrix(
               colorMatrix,
               ColorMatrixFlag.Default,
               ColorAdjustType.Bitmap);

            return imageAttributes;
        }

    }
}
