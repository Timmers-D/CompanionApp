using Companion.Companion;
using Companion.Enum;
using Companion.FaceApi;
using Companion.Visual;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace Companion
{
    class Program
    {
        // reference to the window object
        private static Window _window;
        // does the application continue?
        private static bool _run = true;
        // how often do we refresh the screen
        private static readonly double _refreshRate = 1 / 30;
        // how often do we capture an image to recognize emotions
        private static readonly double _captureRate = 3;
        // previous times we did so
        private static DateTime _previousDrawTime = DateTime.Now;
        private static DateTime _previousCaptureTime = DateTime.Now;


        [STAThread]
        static void Main(string[] args)
        {
            // Create window
            _window = new Window();

            // Create initial companion(s)
            var companion = new Slime(0, Form.Slime);
            Instances.GetManager().AddCompanion(companion);
            companion = new Slime(0, Form.Slime);
            Instances.GetManager().AddCompanion(companion);
            companion = new Slime(0, Form.Slime);
            Instances.GetManager().AddCompanion(companion);
            companion = new Slime(0, Form.Slime);
            Instances.GetManager().AddCompanion(companion);
            companion = new Slime(0, Form.Slime);
            Instances.GetManager().AddCompanion(companion);

            // Main message loop
            while (_run)
            {
                // Read emotion once every _captureRate seconds
                if (Time.Elapsed(_previousCaptureTime, _captureRate))
                {
                    // update last time
                    _previousCaptureTime = DateTime.Now;

                    // static emotion for testing
                    //StaticEmotion();

                    // random emotion for testing
                    //RandomEmotion();

                    // emotion through emotion recognition
                    CaptureEmotion();
                }

                // Update companions and their logic
                _run = Instances.GetManager().Update();

                // check if we need to quit
                if (!_run)
                {
                    return;
                }

                // Redraw _refreshrate times per second
                if (Time.Elapsed(_previousDrawTime, _refreshRate))
                {
                    _previousDrawTime = DateTime.Now;
                    Win32.InvalidateRect(_window.hWnd, IntPtr.Zero, false);
                }

                // handle windows messages, including the redraw we ordered through InvalidateRect
                int msg= Win32.GetMessage(out Win32Util.MSG Msg, IntPtr.Zero, 0, 0);
                if (msg > 0)
                {
                    Win32.TranslateMessage(ref Msg);
                    Win32.DispatchMessage(ref Msg);
                }
                else if (msg == 0)
                {
                    // no message means window is closed
                    _run = false;
                }

                Thread.Sleep(1);
            }
        }

        private static void StaticEmotion()
        {
            Instances.SetEmotion(new List<Mood>() { Mood.WinkyWinky });
        }

        private static void RandomEmotion()
        {
            var rnd = new Random();
            var mood = (Mood)rnd.Next(System.Enum.GetNames(typeof(Mood)).Length);
            Instances.SetEmotion(new List<Mood>() { mood });
        }

        private static void CaptureEmotion()
        {
            using (var bitmap = Instances.GetCapture())
            {
                Instances.SetEmotion(EmotionDetection.DetectFaceExtract(bitmap).Result);
            }
        }
    }
}
