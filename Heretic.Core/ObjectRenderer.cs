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

        private Dictionary<int, Texture2D> wallTextures;

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

        public ObjectRenderer(GraphicsDevice graphicsDevice, ContentManager content)
        {
            this.content = content;

            wallTextures = new Dictionary<int, Texture2D>();
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

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            RenderGameObjects(spriteBatch);
        }
    }
}
