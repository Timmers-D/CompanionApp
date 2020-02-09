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
    class BehaviourWink : IBehaviour
    {
        private bool _goingSide = true;
        public bool Execute(CompanionBase companion)
        {

            var emotion = Instances.GetEmotions()[0];
            companion.SetMood(emotion);

            //Time counter Animation
            if (Time.Elapsed(companion.GetPreviousBehaviourChange(), 3))
            {

            }
            // Position
            var pos = companion.GetPosition();
            if (_goingSide)
            {
                // Speed for wiggle.
                pos.X += -1;
                // Check whether desired wiggle distance has been achieved.
                if (pos.X <= -30)
                {
                    _goingSide = false;
                }
            }
            else
            {
                // Speed for wiggling back.
                pos.X += 1;
                // Check whether companion is on orginal spot..
                if (pos.X >= 0)
                {
                    _goingSide = true;
                }
            }


            //Near side?      
            if (pos.X == 0 | pos.X + 224 == Instances.GetScreenDimension().GetWidth())
            {
                // random chance of climbing
            }

            if (pos.X < 0 || pos.X + 224 > Instances.GetScreenDimension().GetWidth() || pos.Y < 0 || pos.Y + 224 > Instances.GetScreenDimension().GetHeight())
            {
                Random rnd = new Random();
                pos = new Point(rnd.Next(Instances.GetScreenDimension().GetWidth()), rnd.Next(Instances.GetScreenDimension().GetHeight()));
            }

            // position
            companion.SetPosition(pos);

            return true;
        }
    }
}
