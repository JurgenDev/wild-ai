using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TheSavannah.Animals_and_Objects
{
    class FruitPlant  : PhysEntity
    {
        private int clock;
        private int interval;
        private GameWorld world;
        public FruitPlant(Vector2 pos, GameWorld wor, int inter)
        {
            interval = inter;
            position = pos;
            texture = TextureManager.TexFruitPlant;
            hitBoxRadius = 50;
            canCollide = false;
            rotation = new Vector2(((float)Game1.random.Next(10) / 10) - 0.5f, ((float)Game1.random.Next(10) / 10) - 0.5f);
            rotation.Normalize();
            world = wor;
        }
        public override void Update(GameTime deltaTime)
        {
            clock += deltaTime.ElapsedGameTime.Milliseconds;
            if (clock > interval)
            {
                world.AddEntity(new Fruit(position));
                clock = 0;
            }
        }
    }
}
