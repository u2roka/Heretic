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

        private Texture2D[] wallColumns = new Texture2D[Settings.NUM_RAYS];

        private Dictionary<int, Texture2D> wallTextures;

        private Dictionary<int, Color[]> wallData;
        public Dictionary<int, Color[]> WallData 
        { 
            get
            {
                return wallData;
            }
        }

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
            wallData = new Dictionary<int, Color[]>();

            for (int i = 0; i < wallColumns.Length; i++)
            {
                wallColumns[i] = new Texture2D(graphicsDevice, Settings.SCALE, Settings.TEXTURE_SIZE);
            }
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

            GenerateWallData();

            return wallTextures;
        }

        private void GenerateWallData()
        {
            wallData.Clear();

            for (int i = 0; i < wallTextures.Count; i++)
            {
                Color[] data = new Color[Settings.TEXTURE_SIZE * Settings.TEXTURE_SIZE];
                wallTextures[i + 1].GetData(data);

                wallData.Add(i + 1, data);
            }
        }

        private void RenderGameObjects(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < objectsToRender.Length; i++)
            {
                wallColumns[i].SetData(objectsToRender[i].WallColumnData);
                
                spriteBatch.Draw(
                    wallColumns[i],
                    objectsToRender[i].WallSegment,
                    Color.White);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            RenderGameObjects(spriteBatch);
        }
    }
}
