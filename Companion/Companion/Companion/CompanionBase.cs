using Companion.Behaviour;
using Companion.Enum;
using Companion.Struct;
using Companion.Visual;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Companion.Companion
{
    abstract class CompanionBase
    {
        // id om companions te onderscheiden
        protected int _id;
        // dictionary of states and animations
        protected Dictionary<State, Animation> _bodyAnimations = new Dictionary<State, Animation>();
        // dictionary of moods and animatinos
        protected Dictionary<Mood, Animation> _faceAnimations = new Dictionary<Mood, Animation>();
        // location of this companion
        protected Point _position;
        protected readonly Form _form = Form.Slime;
        protected Mood _mood = Mood.Neutral;
        protected State _state = State.Idle;
        protected bool _lookRight = false;
        // the current behaviour, I.E. what is it doing?
        protected IBehaviour _behaviour = new BehaviourNeutral();
        // previous time we changed behaviour, used to measure the time of behaviours so they can finish actions
        protected DateTime _previousBehaviourChange = DateTime.Now;
        // ready to go to the next?
        protected bool _nextBehaviour = false;
        // are we drawing?
        public bool Animating = true;
        // random so that we don't have to make one on the spot (and not get the same results)
        private Random _random = new Random();

        public CompanionBase(int id)
        {
            SetPosition();
            _id = id;
            Load();
        }

        public CompanionBase(int id, Form form)
        {
            SetPosition();
            _id = id;
            _form = form;
            Load();
        }

        // get a random location to put this companion
        private void SetPosition()
        {
            var size = Instances.GetWindowSize();
            _position = new Point(_random.Next(size.Width), _random.Next(size.Height));
        }

        // loading of al the images, gets done by implementation to allow for different images
        protected abstract void Load();

        public void Update()
        {
            Behaviour();
            UpdateAnimations();
        }

        // execute current behaviour and check if we need to change it
        // also update the mood, currently uses the first mood, but in the future if you have multiple people detected you can make it follow a certain person by assigning said person an id
        private void Behaviour()
        {
            var moods = Instances.GetEmotions();
            if (moods.Count != 0)
            {
                _mood = moods[0];
                if (_nextBehaviour)
                {
                    _previousBehaviourChange = DateTime.Now;
                    _behaviour = BehaviourManager.GetBehaviour(_mood);
                    _nextBehaviour = false;
                }
            }
            _nextBehaviour = _behaviour.Execute(this);
        }


        // let animations get next image if needed
        private void UpdateAnimations()
        {
            if (!Animating) { return; }

            foreach (var animation in _bodyAnimations.Values)
            {
                animation.Update();
            }

            foreach (var animation in _faceAnimations.Values)
            {
                animation.Update();
            }
        }

        // get body image
        public BitmapLocation GetCurrentBody()
        {
            var image = _bodyAnimations.ContainsKey(_state) ? _bodyAnimations[_state].GetCurrentImage() : new Bitmap(1,1);
            return new BitmapLocation(image, _position, _mood, false);
        }


        // get face image
        public BitmapLocation GetCurrentFace()
        {
            var image = _faceAnimations.ContainsKey(_mood) ? _faceAnimations[_mood].GetCurrentImage()  : new Bitmap(1, 1);
            return new BitmapLocation(image, _position, _mood, true);
        }

        public Mood GetMood()
        {
            return _mood;
        }

        public Point GetPosition()
        {
            return _position;
        }

        public void SetPosition(Point position)
        {
            _position = position;
        }

        public void SetMood(Mood mood)
        {
            _mood = mood;
        }

        public DateTime GetPreviousBehaviourChange()
        {
            return _previousBehaviourChange;
        }

        public bool LookRight()
        {
            return _lookRight;
        }

        public void MousePressed()
        {
            // TODO parameters and implementation
        }

        public void MouseReleased()
        {
            // TODO parameters and implementation
        }

        public override string ToString()
        {
            return _form.ToString() + " Companion " + _id;
        }

        public void Dispose()
        {
            Instances.GetManager().DisposeCompanion(this);
        }
    }
}
