using Heretic.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Heretic
{
    public class Heretic : Game
    {
        private Map map;
        private Player player;
        private RayCasting rayCasting;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public Heretic()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = Settings.WIDTH;
            graphics.PreferredBackBufferHeight = Settings.HEIGHT;
            IsFixedTimeStep = true;
            TargetElapsedTime = System.TimeSpan.FromSeconds(1d / Settings.FPS);
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            map = new Map();
            player = new Player(map);
            rayCasting = new RayCasting(player, map);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            player.Update(gameTime);
            rayCasting.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here            
            spriteBatch.Begin();

            //map.Draw(gameTime, spriteBatch);
            //player.Draw(gameTime, spriteBatch);
            rayCasting.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}