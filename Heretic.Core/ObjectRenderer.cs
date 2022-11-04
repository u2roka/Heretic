using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using static Heretic.Core.RayCasting;

namespace Heretic.Core
{
    internal class ObjectRenderer
    {
        private ContentManager content;
        private Player player;

        private Dictionary<int, Texture2D> wallTextures;
        private Texture2D skyImage;
        private int skyOffset;

        private ObjectToRender[] objectsToRender = new ObjectToRender[Settings.NUM_RAYS];
        public ObjectToRender[] ObjectsToRender
        {
            get
            {
                return objectsToRender;
            }
            set
            {
                objectsToRender = value;
            }
        }

        public ObjectRenderer(ContentManager content, Player player)
        {
            this.content = content;
            this.player = player;

            wallTextures = new Dictionary<int, Texture2D>();
            skyImage = GetTexture("SKY1");            
        }

        private Texture2D GetTexture(string path)
        {
            return content.Load<Texture2D>(path);
        }

        public Dictionary<int, Texture2D> LoadWallTextures()
        {
            wallTextures.Clear();

            wallTextures.Add(1, GetTexture("FLAT503"));
            wallTextures.Add(2, GetTexture("FLAT507"));
            wallTextures.Add(3, GetTexture("FLAT508"));
            wallTextures.Add(4, GetTexture("FLAT520"));
            wallTextures.Add(5, GetTexture("FLAT522"));
            wallTextures.Add(6, GetTexture("FLAT523"));

            return wallTextures;
        }

        private void RenderGameObjects(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < objectsToRender.Length; i++)
            {
                spriteBatch.Draw(
                    wallTextures[objectsToRender[i].Texture],
                    objectsToRender[i].WallColumnDestination,
                    objectsToRender[i].WallColumnSource,
                    Color.White);
            }
        }

        private void DrawBackground(SpriteBatch spriteBatch)
        {
            skyOffset = (int) (skyOffset + 4.5f * player.RelativeMovement) % Settings.WIDTH;
            spriteBatch.Draw(skyImage, new Rectangle(-skyOffset - Settings.WIDTH, 0, Settings.WIDTH, Settings.HALF_HEIGHT), Color.White);
            spriteBatch.Draw(skyImage, new Rectangle(-skyOffset, 0, Settings.WIDTH, Settings.HALF_HEIGHT), Color.White);
            spriteBatch.Draw(skyImage, new Rectangle(-skyOffset + Settings.WIDTH, 0, Settings.WIDTH, Settings.HALF_HEIGHT), Color.White);

            PrimitiveDrawer.DrawRectangle(spriteBatch, new Rectangle(0, Settings.HALF_HEIGHT, Settings.WIDTH, Settings.HEIGHT), Settings.FLOOR_COLOR);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            DrawBackground(spriteBatch);
            RenderGameObjects(spriteBatch);
        }
    }
}
