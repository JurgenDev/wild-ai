using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheSavannah
{
    //this is the basic physical entity class
    //all animals and objects inherit from this
    //also contains basic rotation-based drawing

    abstract class PhysEntity : GameEntity
    {

        public Vector2 rotation = Vector2.One;
        public int hitBoxRadius { get; set; }
        public Texture2D texture { get; set; }
        public bool canCollide = false;

        public override void Draw(SpriteBatch sprite)
        {
            rotation.Normalize();
            float rot = (float)Math.Atan2(rotation.Y, rotation.X);
            sprite.Draw(texture, position, null, null, new Vector2(texture.Width / 2, texture.Height / 2),
                rot, new Vector2(0.5f, 0.5f), null, SpriteEffects.None, 0.2f);
        }
    }
}
