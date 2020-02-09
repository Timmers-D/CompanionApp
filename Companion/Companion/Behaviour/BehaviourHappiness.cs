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
    class BehaviourHappiness : IBehaviour
    {
        private bool _goingUp = true;
        public bool Execute(CompanionBase companion)
        {

            //
            // als er imitation nodig is
            // ik heb uitgezet dat ie standaard de emotie overneemt
            // de emoties die gedetecteerd worden staan in Instances.GetEmotions()
            // die zetten we in Program
            // of de companion ook die emotion eigenmaakt is afhankelijk van of je het hierzo zet
            var emotion = Instances.GetEmotions()[0];
            companion.SetMood(emotion);

            // als je wilt checken of bepaalde tijd voorbij is sinds vorige gedrag verandering, in dit geval 3 seconden
            if (Time.Elapsed(companion.GetPreviousBehaviourChange(), 3))
            {

            }
            // positie aanpassen
            var pos = companion.GetPosition();
            if (_goingUp)
            {
                // Speed for jumping.
                pos.Y += -5;
                // Check whether desired jump height has been achieved.
                if (pos.Y <= -100)
                {
                    _goingUp = false;
                }
            }
            else
            {
                // Speed for falling.
                pos.Y += 7;
                // Check whether companion is back on 'the Ground'.
                if (pos.Y >= 0)
                {
                    _goingUp = true;
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
