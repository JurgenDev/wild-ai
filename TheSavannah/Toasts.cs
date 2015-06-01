using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheSavannah
{
    class Toasts
    {
        private static List<Toast> toasts = new List<Toast>();
        private static List<Toast> removal = new List<Toast>(); 
        public static void AddToast(Toast t)
        {
            toasts.Add(t);
        }

        public static void Update(GameTime t)
        {
            

            foreach (Toast to in toasts)
            {
                if (to.Update(t))
                {
                    removal.Add(to);
                }
            }
            foreach (Toast to in removal)
            {
                toasts.Remove(to);
            }
        }

        public static void Draw(SpriteBatch spr)
        {
            if (!Game1.TOAST)
                return;

            foreach (Toast to in toasts)
            {
                to.Draw(spr);
            }
        }
        

    }

    class Toast
    {
        public string text;
        public int duration;
        public int clock;
        public Vector2 position;

        public Toast(string txt, int dur, Vector2 pos)
        {
            text = txt;
            duration = dur;
            Vector2 offset = new Vector2(Game1.random.Next(15), Game1.random.Next(15));
            position = pos + offset;
            clock = 0;
        }

        public bool Update(GameTime t)
        {
            clock += t.ElapsedGameTime.Milliseconds;
            return clock > duration;
        }

        public void Draw(SpriteBatch spr)
        {
            
            spr.DrawString(TextureManager.FontArial, text, position, Color.Red, 0.0f, Vector2.Zero, 0.7f, SpriteEffects.None, 0.41f);
        }
  
    }
}
