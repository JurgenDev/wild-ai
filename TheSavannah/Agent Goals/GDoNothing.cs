using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TheSavannah.Agent_Goals
{
    class GDoNothing : AtomicGoal
    {
        private int count = 0;

        public GDoNothing(Animal ani)
        {
            animal = ani;
        }

        public override void Activate()
        {
            Status = Stat.ACTIVE;
        }

        public override Stat Process(GameTime t)
        {
            animal.steering = new Vector2(0, 0);

            count++;
            if (count > 100000)
                Terminate();

            return Status;
        }

        public override void Terminate()
        {
            Status = Stat.COMPLETED; 
        }
    }
}
