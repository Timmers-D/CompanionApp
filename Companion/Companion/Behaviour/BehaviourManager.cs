using Companion.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companion.Behaviour
{
    class BehaviourManager
    {
        public static IBehaviour GetBehaviour(Mood mood)
        {
            IBehaviour behaviour = new BehaviourNeutral();
            //  TODO returns
            switch (mood)
            {
                case Mood.Anger:
                    behaviour = new BehaviourAnger();
                    break;
                case Mood.Fear:
                    behaviour = new BehaviourFear();
                    break;
                case Mood.Happiness:
                    behaviour = new BehaviourHappiness();
                    break;
                case Mood.Neutral:
                    behaviour = new BehaviourNeutral();
                    break;
                case Mood.Sadness:
                    behaviour = new BehaviourSadness();
                    break;
                case Mood.Surprise:
                    behaviour = new BehaviourSurprise();
                    break;
                case Mood.WinkyWinky:
                    behaviour = new BehaviourWink();
                    break;
            }

            return behaviour;
        }
    }
}
