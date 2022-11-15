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
        private ObjectRenderer objectRenderer;
        private ObjectHandler objectHandler;
        private Weapon weapon;
        private Sound sound;
        private PathFinding pathFinding;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;        

        public Heretic()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Settings.WIDTH;
            graphics.PreferredBackBufferHeight = Settings.HEIGHT;
            graphics.IsFullScreen = Settings.ENABLE_FULLSCREEN;
            IsFixedTimeStep = true;
            TargetElapsedTime = System.TimeSpan.FromSeconds(1d / Settings.FPS);
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            map = new Map();
            sound = new Sound(Content);
            player = new Player(map, sound);
            weapon = new Weapon(Content, player, @"Weapons\Elvenwand\GWNDA0", 1f, 0.09f);
            objectRenderer = new ObjectRenderer(Content, player, weapon);
            rayCasting = new RayCasting(player, map, objectRenderer);
            pathFinding = new PathFinding(map);
            objectHandler = new ObjectHandler(Content, sound, player, map, pathFinding, objectRenderer);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            objectRenderer.LoadWallTextures();
            objectRenderer.LoadDigitTextures();
            objectRenderer.LoadCharTextures();
            objectRenderer.RegisterPlayerEvents();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (player.Active)
            {
                player.Update(gameTime);
                rayCasting.Update();
                pathFinding.Update(objectHandler.NPCPositions);
                objectHandler.Update(gameTime);
                weapon.Update(gameTime);
            }
            else
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    Initialize();
                }   
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here            
            spriteBatch.Begin();

            objectRenderer.Draw(gameTime, spriteBatch);
            //map.Draw(gameTime, spriteBatch);
            //objectHandler.Draw(gameTime, spriteBatch);
            //player.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}