using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using Microsoft.Xna.Framework;
using TheSavannah.Entity_Abstracts;

namespace TheSavannah.Agent_Goals
{
    class GHunt : CompositeGoal
    {
        private Animal target;
        public GHunt(Animal ani, Animal targ)
        {
            animal = ani;
            target = targ;
        }
        public override void Activate()
        {
            Status = Stat.ACTIVE;
            Toasts.AddToast(new Toast("Hungry", 1500, animal.position));

            //go to, chase, eat
            if (target is Food)
            {
                Food f = (Food)target;
                AddSubGoal(new GConsume(animal, f, 10));
            }
            AddSubGoal(new GChase(animal, target));
            AddSubGoal(new GSeekToPoint(animal, target.position, 200));
        }

        public override Stat Process(GameTime t)
        {
            CheckStates();

            //if we're really tired or done, blow off the hunt
            if (ProcessSubgoal(t) || animal.energy < 10)
            {
                Terminate();
            }
            
            return Status;
        }

        public override void Terminate()
        {
            Status = Stat.COMPLETED;
            if (animal is Tiger)
            {
                (animal as Tiger).GetFuzzy();
                animal.maxSpeed = animal.baseSpeed;
            }

        }
    }
}
