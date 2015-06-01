using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheSavannah.Agent_Goals
{
    abstract class AtomicGoal : IGoal
    {

        public void AddSubGoal(IGoal goal)
        {
            //do nothing
        }

        public override int Draw(SpriteBatch spr, Vector2 pos)
        {
            spr.DrawString(TextureManager.FontArial, GetType().Name, pos, Color.Black, 0.0f, Vector2.Zero, 0.7f, SpriteEffects.None, 0.41f);
            return 1;
        }
    }
}
