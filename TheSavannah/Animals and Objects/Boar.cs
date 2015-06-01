using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheSavannah.Agent_Goals;
using TheSavannah.Entity_Abstracts;
using TheSavannah.World;

namespace TheSavannah.Animals_and_Objects
{
    class Boar : Animal, Food
    {
        public BoarDen homeDen;
        
        private CompositeGoal think;
        public int meat;
        private int clock;

        public Boar(Vector2 pos, GameWorld world, BoarDen den)
        {
            alive = true;
            worldKnowledge = world;
            homeDen = den;
            texture = TextureManager.TexBoar;
            position = pos;
            hitBoxRadius = 12;
            baseSpeed = 30.0f;
            maxSpeed = baseSpeed;
            hunger = 100;
            thirst = 100;
            energy = 100;
            mass = 5;
            meat = Game1.random.Next(50) + 25;
            think = new GBoarThink(this);
        }

        public override void Update(GameTime deltaTime)
        {
            if (alive)
            {
                clock += deltaTime.ElapsedGameTime.Milliseconds;
                if (clock > 1000)
                {
                    hunger -= 1.0;
                    thirst -= 1.0;
                }
                think.Process(deltaTime);
                base.Update(deltaTime);
            }
        }

        public override void Draw(SpriteBatch sprite)
        {
            if(Game1.DEBUG)
                think.Draw(sprite, position);

            base.Draw(sprite);
        }

        public override void Die()
        {
            homeDen.boars--;
            alive = false;
        }


        public int Consumed(int t)
        {
            return meat -= t;
        }

        public Vector2 GetPosition()
        {
            return position;
        }
    }
}
