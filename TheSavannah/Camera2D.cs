using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TheSavannah
{
    internal class Camera2D
    {
        protected float zoom { get; set; }
        public Matrix transform;
        public Vector2 position;
        protected float rotation;

        public Camera2D()
        {
            zoom = 1.0f;
            rotation = 0.0f;
            position = Vector2.Zero;
        }

        public void Move(Vector2 movement)
        {
            position += movement;
        }

        public void Zoom(int zoomlevel)
        {
            zoom = 1.0f + (zoomlevel*0.0005f);
        }
        public Matrix GetTransformation()
        {
            transform = Matrix.CreateTranslation(-position.X, -position.Y, 0)*
                        Matrix.CreateRotationZ(rotation)*
                        Matrix.CreateScale(new Vector3(zoom, zoom, 1))*
                        Matrix.CreateTranslation(new Vector3(0, 0, 0));
            
            return transform;
        }
    }

}
