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
    class BehaviourNeutral : IBehaviour
    {
        
        public bool Execute(CompanionBase companion)
        {

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
            //pos.X += 0;
    
            // kijken of we bij de rand zijn i.v.m. klimmen, ervan uitgaande dat alle animaties 224*224 gebruiken
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
