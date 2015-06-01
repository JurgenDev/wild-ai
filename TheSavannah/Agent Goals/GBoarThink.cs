using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TheSavannah.Animals_and_Objects;
using TheSavannah.World;

namespace TheSavannah.Agent_Goals
{
    class GBoarThink : CompositeGoal
    {
        private int exploreRadius;
        private int smellRadius;
        private int fearRadius;
        public GBoarThink(Boar b)
        {
            animal = b;
            exploreRadius = 600 + Game1.random.Next(300);
            smellRadius = 50;
            fearRadius = 200;
        }
        public override void Activate()
        {
            Status = Stat.ACTIVE;
        }

        public override Stat Process(GameTime t)
        {
            CheckStates();

            //run subgoals, if there are none, ProcessSubgoal returns true and we navigate to a random nearby node
            
            if (ProcessSubgoal(t))
            {
                NavNode n = animal.worldKnowledge.navGrid.Find(animal.position);
                NavNode target = n.GetRandomNeighbour();
                Boar b = (Boar) animal;
                if(Vector2.Distance(target.position, b.homeDen.position) < exploreRadius)
                    AddSubGoal(new GSeekToPoint(animal, n.GetRandomNeighbour().position, 10));
            }

            //look for fruits within smellDistance or tigers within fearRadius, respond correspondingly
            if(subgoals.Count > 0)
            foreach (PhysEntity p in animal.worldKnowledge.entities)
            {
                //if we're ready for a new Goal
                if (!(subgoals.Peek() is GConsume) && !(subgoals.Peek() is GFlee))
                {
                    //if we smell a fruit
                    if (p is Fruit && Vector2.Distance(p.position, animal.position) < smellRadius && animal.hunger < 80)
                    {
                        AddSubGoal(new GConsume(animal, (Fruit)p, 5));
                        break;
                    }
                    // if we see a tiger
                    if (p is Tiger && Vector2.Distance(p.position, animal.position) < fearRadius)
                    {
                        AddSubGoal(new GFlee(animal, (Animal)p, 200));
                    }
                }
            }

            return Status;
        }

        public override void Terminate()
        {
            Status = Stat.COMPLETED;
        }
    }
}
