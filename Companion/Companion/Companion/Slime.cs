using Companion.Behaviour;
using Companion.Enum;
using Companion.Visual;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Companion.Companion
{
    class Slime : CompanionBase
    {
        public Slime(int id) : base(id) { }

        public Slime(int id, Form form) : base(id, form) { }

        protected override void Load()
        {
            var images = new List<Bitmap>
            {
                Resource1.Slime_left_down,
                Resource1.Slime_left_up
                
            };
            var animation = new Animation(images, .5);
            _bodyAnimations.Add(State.Idle, animation);

            images = new List<Bitmap>
            { 
                Resource1.sprite_face_wink00,
                Resource1.sprite_face_wink01,
                Resource1.sprite_face_wink02,
                Resource1.sprite_face_wink03,
                Resource1.sprite_face_wink04,
                Resource1.sprite_face_wink05,
                Resource1.sprite_face_wink06,
                Resource1.sprite_face_wink07,
                Resource1.sprite_face_wink08,
                Resource1.sprite_face_wink09,
                Resource1.sprite_face_wink10,
                Resource1.sprite_face_wink11,
                Resource1.sprite_face_wink12
            };
            animation = new Animation(images, .5);
            _faceAnimations.Add(Mood.WinkyWinky, animation);

            images = new List<Bitmap>
            {
                Resource1.sprite_happy0,
                Resource1.sprite_happy1,
                Resource1.sprite_happy2,
                Resource1.sprite_happy3,
                Resource1.sprite_happy4,
                Resource1.sprite_happy5
            };
            animation = new Animation(images, 1);
            _faceAnimations.Add(Mood.Happiness, animation);

            images = new List<Bitmap>
            {
                Resource1.sprite_angry0,
                Resource1.sprite_angry1,
                Resource1.sprite_angry2,
                Resource1.sprite_angry3,
                Resource1.sprite_angry4,
                Resource1.sprite_angry5
            };
            animation = new Animation(images, 1);
            _faceAnimations.Add(Mood.Anger, animation);

            images = new List<Bitmap>
            {
                Resource1.sprite_surprised_0,
                Resource1.sprite_surprised_1,
                Resource1.sprite_surprised_2,
                Resource1.sprite_surprised_3,
                Resource1.sprite_surprised_4,
                Resource1.sprite_surprised_5,
                Resource1.sprite_surprised_6
            };
            animation = new Animation(images, 1);
            _faceAnimations.Add(Mood.Surprise, animation);

            images = new List<Bitmap>
            {
                Resource1.sprite_scared_emberrased00,
                Resource1.sprite_scared_emberrased01,
                Resource1.sprite_scared_emberrased02,
                Resource1.sprite_scared_emberrased03,
                Resource1.sprite_scared_emberrased04,
                Resource1.sprite_scared_emberrased05,
                Resource1.sprite_scared_emberrased06,
                Resource1.sprite_scared_emberrased07,
                Resource1.sprite_scared_emberrased08,
                Resource1.sprite_scared_emberrased09,
                Resource1.sprite_scared_emberrased10,
                Resource1.sprite_scared_emberrased11,
                Resource1.sprite_scared_emberrased12,
                Resource1.sprite_scared_emberrased13,
                Resource1.sprite_scared_emberrased14

            };
            animation = new Animation(images, .5);
            _faceAnimations.Add(Mood.Fear, animation);

            images = new List<Bitmap>
            {
                Resource1.neutral_slime0,
                Resource1.neutral_slime1,
                Resource1.neutral_slime2,
                Resource1.neutral_slime3,
                Resource1.neutral_slime4,
                Resource1.neutral_slime5,
                Resource1.neutral_slime6,
                Resource1.neutral_slime7
            };
            animation = new Animation(images, .5);
            _faceAnimations.Add(Mood.Neutral, animation);

            images = new List<Bitmap>
            {
                Resource1.sprite_sad0,
                Resource1.sprite_sad1,
                Resource1.sprite_sad2,
                Resource1.sprite_sad3,
                Resource1.sprite_sad4,
                Resource1.sprite_sad5
            };
            animation = new Animation(images, .5);
            _faceAnimations.Add(Mood.Sadness, animation);
        }
    }
}
