using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheSavannah
{
    abstract class GameEntity
    {
        public Vector2 position { get; set; }

        public abstract void Update(GameTime deltaTime);
        public abstract void Draw(SpriteBatch sprite);

    }
}
