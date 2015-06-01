using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TheSavannah.Agent_Goals
{
    class GThink : CompositeGoal
    {

        public GThink(Animal ani, IGoal start)
        {
            animal = ani;
            subgoals.Push(start);
        }

        public override void Activate()
        {
             Status = Stat.ACTIVE;
        }

        public override Stat Process(GameTime dt)
        {
            CheckStates();
            ProcessSubgoal(dt);


            return Status;
        }

        public override void Terminate()
        {
            
        }
    }
}
