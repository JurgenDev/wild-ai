using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheSavannah
{
    abstract class IGoal
    {
        public Animal animal;
        public Stat Status = Stat.INACTIVE;

        public abstract void Activate();
        public abstract Stat Process(GameTime t);
        public abstract void Terminate();
        public abstract int Draw(SpriteBatch spr, Vector2 pos);
        public void CheckStates()
        {
            if (Status == Stat.INACTIVE)
                Activate();
        }
    }
}

enum Stat
{
    INACTIVE, ACTIVE, COMPLETED
}
