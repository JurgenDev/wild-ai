using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheSavannah
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        public static bool DEBUG = false;
        public static bool TOAST = true;


        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private GameWorld world;
        private Camera2D cam;
        private Vector2 oldMousePosition;
        public static Random random = new Random(DateTime.Now.Millisecond);

        private bool debugToggle;
        private bool toastToggle;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            cam = new Camera2D();

            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();

            world = new GameWorld();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureManager.Load(Content);

            


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState currentKB = Keyboard.GetState();

            if (currentKB.IsKeyDown(Keys.B) && debugToggle)
            {
                DEBUG = !DEBUG;
                debugToggle = false;
            }
            if (currentKB.IsKeyUp(Keys.B))
            {
                debugToggle = true;
            }

            if (currentKB.IsKeyDown(Keys.N) && toastToggle)
            {
                TOAST = !TOAST;
                toastToggle = false;
            }
            if (currentKB.IsKeyUp(Keys.N))
            {
                toastToggle = true;
            }


            
                


            world.Update(gameTime);
            UpdateCamera();
            Toasts.Update(gameTime);

            base.Update(gameTime);
        }
   
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            spriteBatch.Begin(SpriteSortMode.FrontToBack,
                BlendState.AlphaBlend,
                null, null, null, null,
                cam.GetTransformation());
            



            world.Draw(spriteBatch);
            Toasts.Draw(spriteBatch);

            spriteBatch.DrawString(TextureManager.FontArial, 
                "Press B for Debug View \n " +
                "Press N to toggle Toasts\n" +
                "You can move the screen around by dragging the right mouse button \n" +
                "You can also zoom in and out by scrolling", 
                cam.position + new Vector2(20, 20), Color.Black, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.41f);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void UpdateCamera()
        {
            MouseState ms = Mouse.GetState();

            Vector2 move = oldMousePosition - new Vector2(ms.X, ms.Y);
            if (ms.RightButton == ButtonState.Pressed)
            {
                cam.Move(move);
            }
            oldMousePosition = new Vector2(ms.X, ms.Y);

            cam.Zoom(ms.ScrollWheelValue);
        }
    }
}
