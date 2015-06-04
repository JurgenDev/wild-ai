using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TheSavannah
{
    class TextureManager
    {
        public static Texture2D TexLion;
        public static Texture2D TexNavNode;
        public static Texture2D TexNavNodeRed;
        public static Texture2D TexIsland;
        public static Texture2D TexRock;
        public static Texture2D TexBoar;
        public static Texture2D TexBoarDen;
        public static Texture2D TexFruit;
        public static SpriteFont FontArial;
        public static Texture2D TexFruitPlant;
        public static Texture2D TexWater;

        public static void Load(ContentManager cm)
        {
            TexLion = cm.Load<Texture2D>("tigersprite");
            TexNavNode = cm.Load<Texture2D>("navnode");
            TexNavNodeRed = cm.Load<Texture2D>("navnodered");
            TexIsland = cm.Load<Texture2D>("islandbg");
            TexRock = cm.Load<Texture2D>("rock");
            TexBoar = cm.Load<Texture2D>("boarsprite");
            TexBoarDen = cm.Load<Texture2D>("boarden");
            TexFruit = cm.Load<Texture2D>("fruit");
            FontArial = cm.Load<SpriteFont>("Robo");
            TexFruitPlant = cm.Load<Texture2D>("fruitplant");
            TexWater = cm.Load<Texture2D>("water");
        }
    }
}
