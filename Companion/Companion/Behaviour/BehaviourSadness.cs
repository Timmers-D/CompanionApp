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
    class BehaviourSadness : IBehaviour
    {
        public bool Execute(CompanionBase companion)
        {
            var emotion = Instances.GetEmotions()[0];
            companion.SetMood(emotion);

            if (Time.Elapsed(companion.GetPreviousBehaviourChange(), 3))
            { 
            
            }

            var pos = companion.GetPosition();
            pos.X += -2;

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
