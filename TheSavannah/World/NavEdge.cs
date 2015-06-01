using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheSavannah.World
{
    class NavEdge : GameEntity
    {
        public NavNode firstNode;
        public NavNode secondNode;

        public NavEdge(NavNode first, NavNode second)
        {
            firstNode = first;
            secondNode = second;
        }
        public override void Update(GameTime deltaTime)
        {
            
        }

        public override void Draw(SpriteBatch sprite)
        {
            
        }
    }
}
