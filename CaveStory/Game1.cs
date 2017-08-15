using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CaveStory
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        Player player;
        Input input;

        

        private FrameCounter _frameCounter = new FrameCounter();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.AllScreens[0];
            Window.IsBorderless = true;
            Window.Position = new Point(screen.Bounds.X, screen.Bounds.Y);
            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 480;
            graphics.IsFullScreen = false;
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
            input = new Input();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            var spriteSheet = Content.Load<Texture2D>("Sprites\\MyChar");
            spriteFont = Content.Load<SpriteFont>("SpriteFont");
            
            player = new Player(320, 240, new AnimatedSprite(spriteSheet, 0, 0, Constants.kTileSize, Constants.kTileSize, 15, 3));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //Exit();

            // TODO: Add your update logic here
            input.BeginInputFrame();
            if (input.KeyPressed(Keys.Escape))
                Exit();
            /* this section will need some abstraction later */
            if (input.KeyHeld(Keys.Left) && input.KeyHeld(Keys.Right)) { player.StopMoving(); }
            else if (input.KeyHeld(Keys.Left)) { player.StartMovingLeft(); }
            else if (input.KeyHeld(Keys.Right)) { player.StartMovingRight(); }
            else { player.StopMoving(); }

            player.Update(gameTime);
            

            input.EndInputFrame();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _frameCounter.Update(deltaTime);

            var fps = string.Format("FPS: {0:#0.00}", _frameCounter.CurrentFramesPerSecond);
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, fps, new Vector2(1,1), Color.White);
            player.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
