using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TheSavannah.World;

namespace TheSavannah.Agent_Goals
{
    class GTakePathTo : CompositeGoal
    {
        private List<NavNode> path;
        public GTakePathTo(Animal ani, Vector2 target)
        {
            animal = ani;
            path = animal.worldKnowledge.navGrid.GetDijkstraPath(animal.position, target);
            path.Reverse();

        }
        public override void Activate()
        {
            Status = Stat.ACTIVE;
            animal.debugPath = path;
            AddSeek();
        }

        public override Stat Process(GameTime t)
        {
            CheckStates();

            //follow the path
            if (ProcessSubgoal(t))
            {
                if (path.Count <= 0)
                {
                    Terminate();
                    return Status;
                }
                AddSeek();
            }
            return Status;
        }

        public override void Terminate()
        {
             Status = Stat.COMPLETED;
        }

        private void AddSeek()
        {
            if (path.Count <= 0)
            {
                Terminate();
                return;
            }
                
            AddSubGoal(new GSeekToPoint(animal, path[0].position, 25));
            path.RemoveAt(0);
        }
    }
}
