using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TheSavannah.World
{
    class NavGrid
    {
        public List<NavNode> nodes = new List<NavNode>();
        private int width;
        private int height;
        private int step;

        //generates navnodes at point origin with size area to the bottom right
        //with a node every stepsize pixels
        public NavGrid(Vector2 origin, Vector2 area, int stepsize)
        {
            step = stepsize;
            width = (int)area.X/stepsize;
            height = (int)area.Y/stepsize;

            for (int x = (int)origin.X; x < width; x++)
            {
                for (int y = (int)origin.Y; y < height; y++)
                {
                    nodes.Add(new NavNode(x*stepsize, y*stepsize));
                }
            }

            RenewGrid();
        }
        //A simple Find, finds the NavNode to the top left of Vector loc
        public NavNode Find(Vector2 loc)
        {
            int x = (int)loc.X/step;
            int y = (int)loc.Y/step;
            if (loc.X%step > 25)
                x++;
            if (loc.Y%step > 25)
                y++;
            return nodes[x*height + y];
        }

        //get a dijkstra path using NavNode's .NextDST()
        public List<NavNode> GetDijkstraPath(Vector2 begin, Vector2 end)
        {   
            List<NavNode> path = new List<NavNode>(); 
            Find(begin).NextDST(new List<NavNode>(), Find(begin), Find(end),ref path);
            return path;
        }

        //orphans the nodes around the object ents
        //note that all NavNodes are still there, but won't be used because they don't have neighbours

        public void PurgeEntity(PhysEntity ents)
        {
            if (!ents.canCollide)
                return;
            NavNode node = Find(ents.position);
            foreach (NavNode n in node.edges)
            {
                if (Vector2.Distance(ents.position, n.position) < ents.hitBoxRadius * 1)
                {
                    n.RemoveEdges(node);
                }
            }
            if (Vector2.Distance(ents.position, node.position) < ents.hitBoxRadius * 1)
            {
                node.RemoveEdges(null);
            }
        }
        
        //connects all nodes to their neighbours
        public void RenewGrid()
        {
            int[] neighbours = { -1, height - 1, height, height + 1, 1, -height - 1, -height, -height + 1 };

            for (int i = 0; i <= nodes.Count - 1; i++)
            {
                foreach (int neigh in neighbours)
                {
                    //if we're not checking outside of the grid (which would cause nullpointers), try and create a node
                    if (i + neigh >= 0 && i + neigh < nodes.Count)
                    {
                        //if we're a bottom node and adding a bottom neighbour, break
                        if (i % height == 0 && neigh % height == 1)
                            break;

                        nodes[i].AddEdge(nodes[i + neigh]);
                    }
                }
            }
        }
    }
}
