using Companion.Companion;
using Companion.Enum;
using Companion.FaceApi;
using Companion.Struct;
using Companion.Visual;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Companion
{
    class Instances
    {
        // manager for all companions
        private static Manager _manager;
        // size of the monitor we draw on
        private static ScreenDimension _screenDimension;
        // webcam instance to get pictures for emotion recognition
        private static ImageCapture _captureImage = new ImageCapture();
        // size of the window we made
        private static Size _windowSize = new Size();

        // locking system (multithreading protection)
        private static readonly object _emotionLock = new object();
        private static List<Mood> _emotions = new List<Mood>() { Mood.Neutral };

        // emotion detection auth
        // create Face service from Cognitive Service at Azure and plug in endpoint and api key to use
        private static readonly string _faceEndpoint = YOUR_ENDPOINT_LINK;       // example of a link at the time of making this app: https://someresourcename.cognitiveservices.azure.com/
        private static readonly string _apiKey = YOUR_API_KEY;                   // example of an api key: fd4c************************a695
        private static IFaceClient _client;

        public static IFaceClient Authenticate()
        {
            return _client??= new FaceClient(new ApiKeyServiceClientCredentials(_apiKey), new System.Net.Http.DelegatingHandler[] { }) { Endpoint = _faceEndpoint };
        }

        public static Manager GetManager()
        {
            return _manager ??= new Manager();
        }

        public static ScreenDimension GetScreenDimension()
        {
            return _screenDimension ??= new ScreenDimension();
        }

        public static void SetWindowSize(Size windowSize)
        {
            _windowSize = windowSize;
        }

        public static Size GetWindowSize()
        {
            return _windowSize;
        }

        public static Bitmap GetCapture()
        {
            _captureImage ??= new ImageCapture();

            return new Bitmap(_captureImage.GetCapture());
        }

        public static List<Mood> GetEmotions()
        {
            lock (_emotionLock)
            {
                if (_emotions == null || _emotions.Count == 0)
                {
                    _emotions = new List<Mood>() { Mood.Neutral };
                }
                return _emotions;
            }
        }

        public static void SetEmotion(List<Mood> emotions)
        {
            lock (_emotionLock)
            {
                _emotions = emotions;
            }
        }
    }
}
