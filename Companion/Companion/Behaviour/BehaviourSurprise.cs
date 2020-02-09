using Companion.Companion;
using Companion.Visual;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companion.Behaviour
{
    class BehaviourSurprise : IBehaviour
    {
        private bool _goingUp = true;
        private bool _keepGoing = true;

        public bool Execute(CompanionBase companion)
        {
            var emotion = Instances.GetEmotions()[0];
            companion.SetMood(emotion);

            if (Time.Elapsed(companion.GetPreviousBehaviourChange(), 3))
            {

            }

            var pos = companion.GetPosition();
            if (_goingUp && _keepGoing)
            {
                // Speed for jumping.
                pos.Y += -10;
                // Check if desired jump height has been achieved.
                if (pos.Y <= -100)
                {
                    _goingUp = false;
                }
            }
            else if (_keepGoing)
            {
                // Speed for falling.
                pos.Y += 10;
                // Check whether companion is back on 'the ground'.
                if (pos.Y >= 0)
                {
                    _goingUp = true;
                    // Makes it jump once.
                    _keepGoing = false;
                }
            }

            if (pos.X == 0 | pos.X + 224 == Instances.GetScreenDimension().GetWidth())
            {
                // random chance of climbing
            }

            if (pos.X < 0 || pos.X + 224 > Instances.GetScreenDimension().GetWidth() || pos.Y < 0 || pos.Y + 224 > Instances.GetScreenDimension().GetHeight())
            {
                Random rnd = new Random();
                pos = new Point(rnd.Next(Instances.GetScreenDimension().GetWidth()), rnd.Next(Instances.GetScreenDimension().GetHeight()));
            }

            companion.SetPosition(pos);

            return true;
        }
    }
}
