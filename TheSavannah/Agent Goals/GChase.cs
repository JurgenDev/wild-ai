using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Xna.Framework;

namespace TheSavannah.Agent_Goals
{
    class GChase : AtomicGoal
    {
        private Animal target;
        private int clock;

        public GChase(Animal ani, Animal targ)
        {
            animal = ani;
            target = targ;
            clock = 0;
        }
        public override void Activate()
        {
            Status = Stat.ACTIVE;
            Toasts.AddToast(new Toast("Attack!", 1000, animal.position));
            animal.maxSpeed = animal.baseSpeed * 4;
        }

        public override Stat Process(GameTime t)
        {
            CheckStates();

            //drain energy
            clock += t.ElapsedGameTime.Milliseconds;
            if (clock > 20)
            {
                animal.energy--;
                clock = 0;
            }   

            //steer sharply towards target
            Vector2 steer = target.position - animal.position;
            steer *= 1.5f;

            animal.steering += steer;

            //if we're withing 25 units, we're done chasing
            if (Vector2.Distance(target.position, animal.position) < 20)
            {
                Terminate();
            }


            return Status;
        }

        public override void Terminate()
        {
            Status = Stat.COMPLETED;
            animal.maxSpeed = animal.baseSpeed;
            Toasts.AddToast(new Toast("Kill!", 500, animal.position));
            target.Die();
        }
    }
}
