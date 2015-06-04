using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Xna.Framework;
using TheSavannah.Entity_Abstracts;

namespace TheSavannah.Agent_Goals
{
    class GConsume : AtomicGoal
    {
        private Food target;
        private int clock;
        private int bitesize;
        public GConsume(Animal ani, Food t, int bite)
        {
            animal = ani;
            target = t;
            bitesize = bite;
        }
        public override void Activate()
        {
            Status = Stat.ACTIVE;
            Toasts.AddToast(new Toast("Food", 500, animal.position));
        }

        public override Stat Process(GameTime t)
        {
            CheckStates();

            if (target == null)
            {
                Terminate();
                return Status;
            }
                
            if (Vector2.Distance(animal.position, target.GetPosition()) < 10)
            {
                //don't move while eating, it's rude
                animal.velocity = Vector2.Zero;
                animal.steering = Vector2.Zero;

                clock += t.ElapsedGameTime.Milliseconds;
                if (clock > 1000)
                {
                    
                    Toasts.AddToast(new Toast("nom", 200, animal.position));
                    animal.hunger += bitesize;
                    //animal.thirst += bitesize/2;

                    if (target.Consumed(bitesize) < 0 )
                    {
                        animal.worldKnowledge.RemoveEntity((PhysEntity)target);
                        if(animal is Tiger)
                            (animal as Tiger).GetFuzzy();
                        Terminate();
                        return Status;
                    }  
                    if (animal.hunger > 95)
                    {
                        Terminate();
                        return Status;
                    }
                    clock = 0;
                }
            }
            else
            {
                IGoal seek = new GSeekToPoint(animal, target.GetPosition(), 10);
                seek.Process(t);
            }

            return Status;
        }

        public override void Terminate()
        {
            Toasts.AddToast(new Toast("Done", 500, animal.position));
            Status = Stat.COMPLETED;
        }
    }
}
