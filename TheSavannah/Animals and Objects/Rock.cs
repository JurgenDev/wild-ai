using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheSavannah
{
    // the Rock class, the main object we have to navigate around
    class Rock : PhysEntity
    {
        public Rock(Vector2 pos)
        {
            position = pos;
            texture = TextureManager.TexRock;
            hitBoxRadius = 50;
            canCollide = true;
            rotation = new Vector2(((float)Game1.random.Next(10) / 10) - 0.5f, ((float)Game1.random.Next(10) / 10) - 0.5f);
            rotation.Normalize();
        }
        public override void Update(GameTime deltaTime)
        {
            
        }
        

    }
}
