using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TheSavannah.Agent_Goals
{
    class GFlee : AtomicGoal
    {
        private Animal ani;
        private Animal predator;
        private int safedistance;
        public GFlee(Animal ani, Animal pred, int distance)
        {
            animal = ani;
            predator = pred;
            safedistance = distance;
        }
        public override void Activate()
        {
            Status = Stat.ACTIVE;
            Toasts.AddToast(new Toast("Scary!", 500, animal.position));

            animal.maxSpeed = animal.baseSpeed*2;
        }

        public override Stat Process(GameTime t)
        {
            CheckStates();

            Vector2 steer = animal.position - predator.position;
            //Vector2.Negate(steer);

            animal.steering += steer;

            if (Vector2.Distance(predator.position, animal.position) > safedistance)
            {
                Terminate();
            }

            return Status;
        }

        public override void Terminate()
        {
            Status = Stat.COMPLETED;
            animal.maxSpeed = animal.baseSpeed;
        }
    }
}
