using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheSavannah.Entity_Abstracts;

namespace TheSavannah.Animals_and_Objects
{
    class Fruit : PhysEntity, Food
    {
        private int food;
        private Vector2 velocity;
        public Fruit(Vector2 pos)
        {
            food = 40;
            position = pos;
            texture = TextureManager.TexFruit;
            hitBoxRadius = 5;
            canCollide = false;
            rotation = new Vector2(((float)Game1.random.Next(10) / 10) - 0.5f, ((float)Game1.random.Next(10) / 10) - 0.5f);
            velocity = new Vector2(((float)Game1.random.Next(10) / 10) - 0.5f, ((float)Game1.random.Next(10) / 10) - 0.5f);
            velocity *= Game1.random.Next(200);
            rotation.Normalize();
        }
        public override void Update(GameTime deltaTime)
        {
            position += velocity*(float) deltaTime.ElapsedGameTime.TotalSeconds;
            velocity *= 0.99f;
        }

        public override void Draw(SpriteBatch sprite)
        {
            if(Game1.DEBUG)
                sprite.DrawString(TextureManager.FontArial, food.ToString(), position, Color.Black, 0.0f, Vector2.Zero, 0.7f, SpriteEffects.None, 0.41f);
            base.Draw(sprite);
        }

        public int Consumed(int t)
        {
            return food -= t;
        }

        public Vector2 GetPosition()
        {
            return position;
        }
    }
}
