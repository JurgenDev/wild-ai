using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheSavannah.Agent_Goals
{
    abstract class CompositeGoal : IGoal
    {

        public Stack<IGoal> subgoals = new Stack<IGoal>();


        public void AddSubGoal(IGoal goal)
        {
            subgoals.Push(goal);
        }

        public bool ProcessSubgoal(GameTime dt)
        {
            //this function automatically runs subgoals and removes completed goals
            if (subgoals.Count <= 0)
                return true;
            if (subgoals.Peek().Process(dt) == Stat.COMPLETED)
                subgoals.Pop();
            return false;
        }

        public override int Draw(SpriteBatch spr, Vector2 pos)
        {
            //recursively draw all subgoals, return the amount of subgoals you have
            spr.DrawString(TextureManager.FontArial, GetType().Name , pos, Color.Black, 0.0f, Vector2.Zero, 0.7f, SpriteEffects.None, 0.41f);
            pos += new Vector2(10, 20);

            foreach (var subgoal in subgoals)
            {
                int s = subgoal.Draw(spr, pos);
                pos += new Vector2(0, 20 * s);
            }
            return subgoals.Count + 1;
        }
    }
}
