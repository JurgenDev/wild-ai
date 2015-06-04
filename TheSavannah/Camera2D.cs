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
        private Vector2 tlbound;
        private Vector2 brbound;

        public Camera2D()
        {
            zoom = 1.0f;
            rotation = 0.0f;
            position = Vector2.Zero;
            tlbound = new Vector2(-1000, -1000);
            brbound = new Vector2(512, 512);
        }

        public void Move(Vector2 movement)
        {
            position += movement;
            if (position.X < tlbound.X)
                position.X = tlbound.X;

            if (position.Y < tlbound.Y)
                position.Y = tlbound.Y;

            if (position.X > brbound.X)
                position.X = brbound.X;

            if (position.Y > brbound.Y)
                position.Y = brbound.Y;

        }

        public void Zoom(int zoomlevel)
        {
            zoom = 1.0f + (zoomlevel*0.0005f);
            if (zoom < 0.5f) zoom = 0.5f;
            if (zoom > 1.5f) zoom = 1.5f;
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
