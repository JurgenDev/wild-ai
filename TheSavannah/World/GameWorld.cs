using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheSavannah.Animals_and_Objects;
using TheSavannah.World;

namespace TheSavannah
{
    class GameWorld
    {
        public List<PhysEntity> entities = new List<PhysEntity>();
        private List<PhysEntity> addQueue = new List<PhysEntity>(); 
        private List<PhysEntity> removeQueue = new List<PhysEntity>(); 
        public NavGrid navGrid;
        private Vector2 origin = Vector2.Zero;
        public Vector2 size = new Vector2(1920, 1080);

        public GameWorld()
        {
            //generate the navigation grid
            navGrid = new NavGrid(origin, size, 50);

            //add the tiger
            AddEntity(new Tiger(new Vector2(400, 400), this));

            //add a number of rocks to walk around
            for (int i = 0; i < 10; i++)
            {
                AddEntity(new Rock(new Vector2(Game1.random.Next(500, 1200), Game1.random.Next(100, 500))));
            }

            for (int i = 0; i <= 50; i++)
            {
                AddEntity(new Fruit(new Vector2(Game1.random.Next(800, 1800), Game1.random.Next(100, 1000))));
            }
            //add the boar den for food
            AddEntity(new BoarDen(new Vector2(1200, 500), 5, this ));
            AddEntity(new FruitPlant(new Vector2(1300,550), this, 5000));
            AddEntity(new FruitPlant(new Vector2(1100, 850), this, 5000));
            //add everything in the addition queue to the game world
            //we usually only run this function on update, but we need to
            //do it here in order for the navigation grid to update properly
            FlushAddQueue();

            //shadow the nodes that can't be travelled on
            //this mean they're still there but aren't used
            UpdateWorldNav();
        }

        public void Update(GameTime deltaTime)
        {
            //add any entities that have been added by entities
            FlushAddQueue();
            FlushRemoveQueue();
            //update the world
            foreach (GameEntity e in entities)
            {
                e.Update(deltaTime);
            } 
        }

        

        public void Draw(SpriteBatch sprite)
        {
            //draw the island texture
            sprite.Draw(TextureManager.TexIsland, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.1f);
            

            for (int i = -1; i < 3; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    sprite.Draw(TextureManager.TexWater, new Vector2(1000*i , 1000 * j), null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.09f);
                }
            }
            //draw entities
            foreach (GameEntity e in entities)
            {
                e.Draw(sprite);
            }

            //draw debug
            if (Game1.DEBUG)
            {
                foreach (NavNode n in navGrid.nodes)
                {
                    n.Draw(sprite);
                }
            } 
        }

        public void UpdateWorldNav()
        {
            //purge colliding entity in the world from the nav grid
            foreach (PhysEntity g in entities)
            {
                navGrid.PurgeEntity(g);
            }
        }

        public void AddEntity(PhysEntity p)
        {
            addQueue.Add(p);
        }


        private void FlushAddQueue()
        {
            //add the queue to the world and clear the queue
            foreach (PhysEntity g in addQueue)
            {
                entities.Add(g);
            }
            addQueue.Clear();
        }

        public void RemoveEntity(PhysEntity p)
        {
            removeQueue.Add(p);
        }

        private void FlushRemoveQueue()
        {
            foreach (PhysEntity g in removeQueue)
            {
                entities.Remove(g);
            }
            removeQueue.Clear();
        }
    }
}
