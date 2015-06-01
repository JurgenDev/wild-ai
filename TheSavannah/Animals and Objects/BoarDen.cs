using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TheSavannah.Animals_and_Objects
{
    class BoarDen : PhysEntity
    {
        private int boarCount;
        public int boars = 0;
        private GameWorld world;
        public BoarDen(Vector2 pos, int boarcount, GameWorld gworld )
        {
            position = pos;
            texture = TextureManager.TexBoarDen;
            world = gworld;
            hitBoxRadius = 50;
            boarCount = boarcount;
            canCollide = false;
            Vector2 rotVec = position - (world.size/2) ;
            rotation = rotVec;
            rotation.Normalize();
        }
        public override void Update(GameTime deltaTime)
        {
            if (boars < boarCount)
            {
                world.AddEntity(new Boar(position, world, this));
                boars++;
            }
        }
    }
}
