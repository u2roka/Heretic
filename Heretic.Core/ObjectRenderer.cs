using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
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

        private Texture2D bloodScreen;
        private bool drawPlayerDamageSplatter;

        private List<ObjectToRender> objectsToRender = new List<ObjectToRender> ();
        public List<ObjectToRender> ObjectsToRender
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
            skyImage = GetTexture(@"Textures\SKY1");
            bloodScreen = GetTexture(@"Textures\SPLATTER1");
        }

        private Texture2D GetTexture(string path)
        {
            return content.Load<Texture2D>(path);
        }

        public Dictionary<int, Texture2D> LoadWallTextures()
        {
            wallTextures.Clear();

            wallTextures.Add(1, GetTexture(@"Textures\FLAT503"));
            wallTextures.Add(2, GetTexture(@"Textures\FLAT507"));
            wallTextures.Add(3, GetTexture(@"Textures\FLAT508"));
            wallTextures.Add(4, GetTexture(@"Textures\FLAT520"));
            wallTextures.Add(5, GetTexture(@"Textures\FLAT522"));
            wallTextures.Add(6, GetTexture(@"Textures\FLAT523"));

            return wallTextures;
        }

        public void RegisterPlayerDamageEvent()
        {
            player.OnDamage += PlayerDamageEventFired;
        }

        private void RenderGameObjects(SpriteBatch spriteBatch)
        {
            objectsToRender.Sort((x, y) => x.Depth.CompareTo(y.Depth));
            objectsToRender.Reverse();

            foreach (ObjectToRender next in objectsToRender)
            {
                float channel = 1 / (1 + MathF.Pow(next.Depth, 5) * 0.00002f);
                Color color = new(channel, channel, channel);
                spriteBatch.Draw(
                    next.TextureIndex != -1 ? wallTextures[next.TextureIndex] : next.Texture,
                    next.WallColumnDestination,
                    next.WallColumnSource,
                    color);
            }
        }

        private void PlayerDamageEventFired()
        {
            drawPlayerDamageSplatter = true;
        }

        private void PlayerDamage(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bloodScreen, new Rectangle(0, 0, Settings.WIDTH, Settings.HEIGHT), Color.Red * 0.2f);
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
            if (drawPlayerDamageSplatter)
            {
                PlayerDamage(spriteBatch);
                drawPlayerDamageSplatter = false;
            }
        }
    }
}
