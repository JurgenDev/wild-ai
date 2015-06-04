using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheSavannah.Agent_Goals;
using TheSavannah.Fuzzy;
using TheSavannah.World;

namespace TheSavannah
{
    // if it were a lion i could say this is our mane character, but nevermind

    /*
     * our main character, this is the most complex animal in the game complete
     * with:
     * 
     * - Steering Agent physics
     * - Goal-Driven behaviour 
     * - Fuzzy Logic for decision making
     * - Path planning behavior with A*
     * - A serious appetite for murdering boars
     * 
    */

    class Tiger : Animal
    {
        public IGoal think;
        public FuzzyModule huntfuzz;
        public int clock = 0;

        public double huntInitiative;
        public double huntThreshold = 35;

        public Tiger(Vector2 pos, GameWorld world )
        {
            
            worldKnowledge = world;
            
            texture = TextureManager.TexLion;
            position = pos;
            rotation = new Vector2(1, 0);
            heading = new Vector2(0, 0);
            hitBoxRadius = 25;
            baseSpeed = 50.0f;
            maxSpeed = baseSpeed;
            mass = 15;
            hunger = 100;
            thirst = 100;
            energy = 100;
            think = new GTigerThink(this);
            alive = true;

            InitializeFuzzy();
            GetFuzzy();
        }

        public override void Update(GameTime deltaTime)
        {
            //a clock to make sure we don't drain hunger and thirst too fast, else kitty dies
            clock += deltaTime.ElapsedGameTime.Milliseconds;
            if (clock >= 1000)
            {
                hunger -= 1.0;
                thirst -= 0.5;
                clock = 0;

                if (energy < 100)
                {
                    energy += 3;
                    hunger -= 1.0;
                    thirst -= 1.0;
                }
                
                //if thirst gets low, bring it back up
                if (thirst < 20)
                {
                    thirst = 100;
                }
                Console.WriteLine("Hunger: {0}, Thirst: {1}, Energy: {2}", hunger, thirst, energy);
            }
           
            //update our goal-driven behaviour
            think.Process(deltaTime);

            //base update (vectors and steering)
            base.Update(deltaTime);
            
        }

        public override void Die()
        {
            alive = false;
        }

        public override void Draw(SpriteBatch spr)
        {
            //draw ourselves
            base.Draw(spr);

            
            //draw debug information like our current path and steering
            if (Game1.DEBUG)
            {
                spr.Draw(TextureManager.TexNavNodeRed, position + steering, null, Color.Blue, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.41f);
                foreach (NavNode n in debugPath)
                {
                    spr.Draw(TextureManager.TexNavNodeRed, n.position, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.41f);
                }


                Vector2 dataDraw = position + new Vector2(-100, -30);
                float scale = 0.7f;
                Color color = Color.Black;
                spr.DrawString(TextureManager.FontArial, 
                    "Hunger: " + hunger + "\n" + 
                    "Thirst: " + thirst + "\n" +
                    "Energy:" + energy + "\n" +
                    "Hunt:" + huntInitiative + "\n"
                , dataDraw, color, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 0.41f);

                think.Draw(spr, position + new Vector2(50, -30));

            }
            
            
        }

        private void InitializeFuzzy()
        {
            //initialize our fuzzy logic
            //this is further explained in the Tech Doc
            huntfuzz = new FuzzyModule();

            FuzzyVariable fhunger = new FuzzyVariable(0, 100);
            FuzzyVariable fthirst = new FuzzyVariable(0, 100);
            FuzzyVariable fdesire = new FuzzyVariable(0, 100);

            FuzzySet starving = new FuzzySet_Shoulder(15, 25, false);
            FuzzySet hungry = new FuzzySet_Triangle(40, 25);
            FuzzySet full = new FuzzySet_Shoulder(65, 25, true);

            FuzzySet parched = new FuzzySet_Shoulder(25, 25, false);
            FuzzySet thirsty = new FuzzySet_Triangle(50, 25);
            FuzzySet hydrated = new FuzzySet_Shoulder(75, 25, true);

            FuzzySet undesirable = new FuzzySet_Shoulder(25, 25, false);
            FuzzySet desirable = new FuzzySet_Triangle(50, 25);
            FuzzySet verydesirable = new FuzzySet_Shoulder(75, 25, true);

            fhunger.AddSet("starving", starving);
            fhunger.AddSet("hungry", hungry);
            fhunger.AddSet("full", full);

            fthirst.AddSet("parched", parched);
            fthirst.AddSet("thirsty", thirsty);
            fthirst.AddSet("hydrated", hydrated);

            fdesire.AddSet("undesirable", undesirable);
            fdesire.AddSet("desirable", desirable);
            fdesire.AddSet("verydesirable", verydesirable);

            huntfuzz.AddVariable("hunger", fhunger);
            huntfuzz.AddVariable("thirst", fthirst);
            huntfuzz.AddVariable("desirability", fdesire);

            huntfuzz.AddRule(new FuzzyRule(new FuzzyAND(starving, parched), undesirable));
            huntfuzz.AddRule(new FuzzyRule(new FuzzyAND(starving, thirsty), desirable));
            huntfuzz.AddRule(new FuzzyRule(new FuzzyAND(starving, hydrated), verydesirable));

            huntfuzz.AddRule(new FuzzyRule(new FuzzyAND(hungry, parched), undesirable));
            huntfuzz.AddRule(new FuzzyRule(new FuzzyAND(hungry, thirsty), desirable));
            huntfuzz.AddRule(new FuzzyRule(new FuzzyAND(hungry, hydrated), verydesirable));

            huntfuzz.AddRule(new FuzzyRule(new FuzzyAND(full, parched), undesirable));
            huntfuzz.AddRule(new FuzzyRule(new FuzzyAND(full, thirsty), undesirable));
            huntfuzz.AddRule(new FuzzyRule(new FuzzyAND(full, hydrated), undesirable));
        }

        public void GetFuzzy()
        {
            huntfuzz.Fuzzify("hunger", hunger);
            huntfuzz.Fuzzify("thirst", thirst);
            huntfuzz.RunRules();
            huntInitiative = huntfuzz.DeFuzzify("desirability");
            Console.WriteLine("GetFuzzyWitIt: " +  huntInitiative);
        }

        
    }
}
