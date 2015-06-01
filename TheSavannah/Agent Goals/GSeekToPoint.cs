using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TheSavannah.Agent_Goals
{
    class GSeekToPoint : AtomicGoal
    {
        private Vector2 point;
        private int targetDist;
        public GSeekToPoint(Animal a, Vector2 goal, int targetDistance)
        {
            animal = a;
            point = goal;
            targetDist = targetDistance;
        }
        public override void Activate()
        {
            Status = Stat.ACTIVE;
        }

        public override Stat Process(GameTime t)
        {
            CheckStates();

            Vector2 steer = point - animal.position;
            float dist = Vector2.Distance(point, animal.position);

            if(dist < 150)
                steer += Vector2.Negate(animal.velocity)/(1 + (dist/100));

            animal.steering = steer;
            

            if (Vector2.Distance(point, animal.position) < targetDist)
            {
                Terminate();
            }
            return Status;
        }

        public override void Terminate()
        {
            Status = Stat.COMPLETED;
        }
    }
}
