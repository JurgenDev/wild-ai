using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheSavannah.World
{
    class NavNode : GameEntity
    {

        public List<NavNode> edges = new List<NavNode>();
        private float terrainWeight = 1.0f;

        public NavNode(float x, float y)
        {
            position = new Vector2(x, y);
        }

        public override void Update(GameTime deltaTime)
        {
            
        }

        public override void Draw(SpriteBatch sprite)
        {
            if(edges.Count > 0)
              sprite.Draw(TextureManager.TexNavNode, position, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.4f);
        }

        public void AddEdge(NavNode e)
        {
            edges.Add(e);
        }

        public NavNode GetRandomNeighbour()
        {
            if(edges.Count > 0)
                return edges[Game1.random.Next(edges.Count)];
            return this;
        }

        public void RemoveEdges(NavNode exception)
        {
            foreach (NavNode n in edges)
            {
                if(n != exception)
                    n.edges.Remove(this);
            }
            edges = new List<NavNode>();
        }
        public bool NextDST(List<NavNode> closed, NavNode parent, NavNode target, ref List<NavNode> result )
        {
            //Console.WriteLine("Checking " + position.ToString());

            //if this is the target, add yourself to the result and return true
            if (this == target)
            {
                result.Add(this);
                return true;
            }

            //create the new open list
            //open is a list of tuples with the node and distance to target
            List<Tuple<float, NavNode>> open = new List<Tuple<float, NavNode>>();
            foreach (NavNode n in edges)
            {
                if (!closed.Contains(n) && n != this)
                    open.Add(new Tuple<float, NavNode>( 
                        (Vector2.Distance(n.position, target.position) + Vector2.Distance(position, n.position)) * terrainWeight,
                        n
                        ));
            }

            //sort lowest to highest distance
            DSTComparer c = new DSTComparer();
            open.Sort(c);

            //go through the list and continue the algorithm
            //if the next iteration returns true add parent to the path and return true
            foreach (Tuple<float, NavNode> tup in open)
            {
                if (tup.Item2 != null) closed.Add(tup.Item2);

                if(tup.Item2.NextDST(closed, this, target, ref result))
                {
                     if(parent != null) result.Add(parent);
                    return true;
                }
                
            }
            return false;
        } 
    }
}
