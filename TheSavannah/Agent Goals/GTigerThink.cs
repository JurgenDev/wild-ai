using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TheSavannah.Animals_and_Objects;

namespace TheSavannah.Agent_Goals
{
    class GTigerThink : CompositeGoal
    {
        private Tiger tiger;
        private int clock;

        public GTigerThink(Tiger t)
        {
            animal = t;
            tiger = (Tiger) animal;
            clock = 0;
            AddSubGoal(new GTakePathTo(animal, new Vector2(400, 450)));
        }

        public override void Activate()
        {
            Status = Stat.ACTIVE;
        }

        public override Stat Process(GameTime t)
        {
            CheckStates();

            if (ProcessSubgoal(t))
            {
                AddRandomPatrolRoute();
            }

            clock += t.ElapsedGameTime.Milliseconds;
            if (clock > 1000)
            {
                tiger.GetFuzzy();
                clock = 0;
            }

            if(subgoals.Count > 0)
            if(tiger.huntInitiative > tiger.huntThreshold && !(subgoals.Peek() is GHunt) && tiger.energy > 80)
            {

                foreach (PhysEntity ph in tiger.worldKnowledge.entities)
                {
                    if (ph is Boar)
                    {
                        AddSubGoal(new GHunt(tiger, (Animal)ph));
                        break;
                    }
                }
                
            }
            return Status;
        }

        public override void Terminate()
        {
            Status = Stat.COMPLETED;
        }

        private void AddRandomPatrolRoute()
        {
            Random r = new Random(DateTime.Now.Millisecond);
            int randomNode = r.Next(tiger.worldKnowledge.navGrid.nodes.Count);
            AddSubGoal(new GTakePathTo(tiger, tiger.worldKnowledge.navGrid.nodes[randomNode].position));
        }
    }
}
