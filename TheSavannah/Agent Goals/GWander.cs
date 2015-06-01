using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TheSavannah.Agent_Goals
{
    class GWander : AtomicGoal
    {
        private int count = 0;
        private int wanderRadius = 35;
        private int wanderDistance = 35;
        private int wanderJitter = 1;
        private Random r;

        public GWander(Animal ani)
        {
            animal = ani;
        }
        public override void Activate()
        {
            Status = Stat.ACTIVE;
            r = new Random();
        }

        public override Stat Process(GameTime t)
        {
            CheckStates();

            count++;
            if (count > 500)
                Terminate();

            double x = (r.Next() - 0.5) * 2;
            double y = (r.Next() - 0.5) * 2;

            Vector2 v = new Vector2((int)x * wanderJitter, (int)y * wanderJitter);
            v.Normalize();
            v *= wanderRadius;

            v += animal.heading*wanderRadius;

            animal.steering = v;

            return Status;
        }

        public override void Terminate()
        {
            Status = Stat.COMPLETED;
        }
    }
}
