using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheSavannah.World;

namespace TheSavannah
{   
    //This is the animal superclass, all animals in the sim inherit from this class
    //it contains basic steering agent behavior and a navigation grid

    abstract class Animal : PhysEntity
    {
        //steering agent properties
        public Vector2 heading = Vector2.One;
        public Vector2 velocity { get; set; }
        public Vector2 perpendicular { get; set; }
        public Vector2 steering;
        public float baseSpeed;
        public float maxSpeed;
        public int mass;

        //misc properties
        public GameWorld worldKnowledge;
        public bool alive;
        public List<NavNode> debugPath = new List<NavNode>();

        //animal properties
        public double hunger;
        public double thirst;
        public double energy;


        public override void Update(GameTime deltaTime)
        {
            //base update, this updates all the vectors of an animal
            float deltafloat = (float) deltaTime.ElapsedGameTime.TotalSeconds;

            UpdateVectors(deltafloat);
        }
        // TODO physical collision detection
        // maybe some day

        //Updates velocity based on steering
        public void UpdateVectors(float delta)
        {
            if (!alive)
                return;
            //mutate velocity based on steering
            velocity += steering * delta;

            //update the heading
            heading = velocity;
            heading.Normalize();

            // if we're moving too fast, truncate our velocity
            if (velocity.Length() > maxSpeed)
            {
                Vector2 newvel = Vector2.Normalize(velocity);
                newvel *= maxSpeed;
                velocity = newvel;
            }
            
            //update position based on velocity
            position += velocity * delta;

            //if we're not standing still, update our rotation
            if (velocity.Length() > 0.0000001)
            {
                rotation = heading;
                Matrix perpMat = Matrix.CreateRotationZ(MathHelper.ToRadians(90));
                perpendicular = Vector2.Transform(rotation, perpMat);
            } 
        }

        public abstract void Die();
    }
}
