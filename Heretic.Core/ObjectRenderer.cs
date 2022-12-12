using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using static Heretic.Core.RayCasting;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Heretic.Core
{
    internal class ObjectRenderer
    {
        public enum TextAlign
        {
            LEFT,
            CENTER,
            RIGHT
        }

        private ContentManager content;
        private Player player;
        private Weapon weapon;

        private Dictionary<int, Texture2D> wallTextures;

        private Texture2D skyImage;
        private int skyOffset;

        private Texture2D bloodScreen;
        private Texture2D deathScreen;
        private Texture2D hereticLogo;
        private bool drawPlayerDamageSplatter;
        private bool drawPlayerGameOver;

        private bool drawPlayerVictory;        

        private Texture2D healPotion;
        private Dictionary<int, Texture2D> digitTextures;
        private Dictionary<char, Texture2D> charTextures;

        private List<ObjectToRender> objectsToRender = new List<ObjectToRender>();
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

        public ObjectRenderer(ContentManager content, Player player, Weapon weapon)
        {
            this.content = content;
            this.player = player;
            this.weapon = weapon;

            wallTextures = new Dictionary<int, Texture2D>();
            skyImage = GetTexture(@"Textures\SKY1");
            bloodScreen = GetTexture(@"Textures\SPLATTER1");
            healPotion = GetTexture(@"Textures\ARTIPTN2");
            deathScreen = GetTexture(@"Textures\AUTOPAGE");
            hereticLogo = GetTexture(@"Textures\M_HTIC");
            digitTextures = new Dictionary<int, Texture2D>();
            charTextures = new Dictionary<char, Texture2D>();
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

        public Dictionary<int, Texture2D> LoadDigitTextures()
        {
            digitTextures.Clear();

            for (int i = 16; i < 26; i++)
            {
                digitTextures.Add(i, GetTexture(String.Format(@"Textures\Digits\FONTB{0}", i)));
            }

            return digitTextures;
        }

        public Dictionary<char, Texture2D> LoadCharTextures()
        {
            charTextures.Clear();

            charTextures.Add('!', GetTexture(@"Textures\Chars\FONTA01"));
            charTextures.Add('a', GetTexture(@"Textures\Chars\FONTA33"));
            charTextures.Add('b', GetTexture(@"Textures\Chars\FONTA34"));
            charTextures.Add('c', GetTexture(@"Textures\Chars\FONTA35"));
            charTextures.Add('d', GetTexture(@"Textures\Chars\FONTA36"));
            charTextures.Add('e', GetTexture(@"Textures\Chars\FONTA37"));
            charTextures.Add('f', GetTexture(@"Textures\Chars\FONTA38"));
            charTextures.Add('g', GetTexture(@"Textures\Chars\FONTA39"));
            charTextures.Add('h', GetTexture(@"Textures\Chars\FONTA40"));
            charTextures.Add('i', GetTexture(@"Textures\Chars\FONTA41"));
            charTextures.Add('j', GetTexture(@"Textures\Chars\FONTA42"));
            charTextures.Add('k', GetTexture(@"Textures\Chars\FONTA43"));
            charTextures.Add('l', GetTexture(@"Textures\Chars\FONTA44"));
            charTextures.Add('m', GetTexture(@"Textures\Chars\FONTA45"));
            charTextures.Add('n', GetTexture(@"Textures\Chars\FONTA46"));
            charTextures.Add('o', GetTexture(@"Textures\Chars\FONTA47"));
            charTextures.Add('p', GetTexture(@"Textures\Chars\FONTA48"));
            charTextures.Add('q', GetTexture(@"Textures\Chars\FONTA49"));
            charTextures.Add('r', GetTexture(@"Textures\Chars\FONTA50"));
            charTextures.Add('s', GetTexture(@"Textures\Chars\FONTA51"));
            charTextures.Add('t', GetTexture(@"Textures\Chars\FONTA52"));
            charTextures.Add('u', GetTexture(@"Textures\Chars\FONTA53"));
            charTextures.Add('v', GetTexture(@"Textures\Chars\FONTA54"));
            charTextures.Add('w', GetTexture(@"Textures\Chars\FONTA55"));
            charTextures.Add('x', GetTexture(@"Textures\Chars\FONTA56"));
            charTextures.Add('y', GetTexture(@"Textures\Chars\FONTA57"));
            charTextures.Add('z', GetTexture(@"Textures\Chars\FONTA57"));

            return charTextures;
        }

        public void RegisterPlayerEvents()
        {
            player.OnDamage += PlayerDamageEventFired;
            player.OnDeath += PlayerDeathEventFired;
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

        private void PlayerDeathEventFired()
        {
            drawPlayerGameOver = true;
        }

        public void PlayerVictoryEventFired()
        {
            player.KilledAllEnemies = true;
            drawPlayerVictory = true;
        }

        private void DrawBackground(SpriteBatch spriteBatch)
        {
            skyOffset = (int) (skyOffset + 4.5f * player.RelativeMovement) % Settings.WIDTH;
            spriteBatch.Draw(skyImage, new Rectangle(-skyOffset - Settings.WIDTH, 0, Settings.WIDTH, Settings.HALF_HEIGHT), Color.White);
            spriteBatch.Draw(skyImage, new Rectangle(-skyOffset, 0, Settings.WIDTH, Settings.HALF_HEIGHT), Color.White);
            spriteBatch.Draw(skyImage, new Rectangle(-skyOffset + Settings.WIDTH, 0, Settings.WIDTH, Settings.HALF_HEIGHT), Color.White);

            PrimitiveDrawer.DrawRectangle(spriteBatch, new Rectangle(0, Settings.HALF_HEIGHT, Settings.WIDTH, Settings.HEIGHT), Settings.FLOOR_COLOR);
        }

        private void DrawPlayerHealth(SpriteBatch spriteBatch)
        {
            DrawNumber(spriteBatch, player.Health, new Point(80, Settings.HEIGHT - 75));            

            spriteBatch.Draw(healPotion, new Rectangle(30, Settings.HEIGHT - 83, healPotion.Width / 2, healPotion.Height / 2), Color.White);
        }

        private void DrawNumber(SpriteBatch spriteBatch, string text, Point position)
        {
            int width = 0;
            for (int i = 0; i < text.Length; i++)
            {
                var texture = digitTextures[int.Parse(text[i].ToString()) + 16];
                spriteBatch.Draw(texture, new Vector2(position.X + width, position.Y), Color.White);
                width += texture.Width;
            }
        }

        private void DrawText(SpriteBatch spriteBatch, string text, Point position, int fontSize, Color color, TextAlign align)
        {
            int offset = 0;
            switch (align)
            {
                case TextAlign.CENTER:
                    offset = CalculateTextWidth(text, fontSize) / -2;
                    break;
                case TextAlign.RIGHT:
                    throw new NotImplementedException();
            }
                        
            int width = 0;
            for (int i = 0; i < text.Length; i++)
            {
                char nextChar = text[i];

                int charWidth = fontSize;
                if (nextChar != ' ')
                {
                    var charTexture = charTextures[nextChar];
                    charWidth = (int)(charTexture.Width / (float)charTexture.Height * fontSize);
                    spriteBatch.Draw(charTexture, new Rectangle(position.X + width + offset, position.Y, charWidth, fontSize), color);
                }                
                width += charWidth;
            }
        }

        private int CalculateTextWidth(string text, int fontSize)
        {
            int result = 0;

            for (int i = 0; i < text.Length; i++)
            {
                char nextChar = text[i];

                int charWidth = fontSize;
                if (nextChar != ' ')
                {
                    var charTexture = charTextures[nextChar];
                    charWidth = (int)(charTexture.Width / (float)charTexture.Height * fontSize);
                }
                result += charWidth;
            }

            return result;
        }

        private void DrawEndScreen(SpriteBatch spriteBatch, string text, Color color)
        {
            spriteBatch.Draw(deathScreen, new Rectangle(0, 0, Settings.WIDTH, Settings.HEIGHT), Color.White);
            spriteBatch.Draw(hereticLogo, new Rectangle(50, 50, hereticLogo.Width / 2, hereticLogo.Height / 2), Color.White);
            DrawText(spriteBatch, text, new Point(Settings.HALF_WIDTH, Settings.HALF_HEIGHT), 72, color, TextAlign.CENTER);
            DrawText(spriteBatch, "press enter to restart", new Point(Settings.HALF_WIDTH, Settings.HEIGHT - 50), 28, Color.White, TextAlign.CENTER);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (drawPlayerGameOver)
            {
                DrawEndScreen(spriteBatch, "game over", Color.Red);
            }
            else if (drawPlayerVictory)
            {
                DrawEndScreen(spriteBatch, "victory!", Color.Yellow);
            }
            else
            {
                DrawBackground(spriteBatch);
                RenderGameObjects(spriteBatch);
                weapon.Draw(gameTime, spriteBatch);
                DrawPlayerHealth(spriteBatch);
                if (drawPlayerDamageSplatter)
                {
                    spriteBatch.Draw(bloodScreen, new Rectangle(0, 0, Settings.WIDTH, Settings.HEIGHT), Color.Red * 0.2f);
                    drawPlayerDamageSplatter = false;
                }
            }
        }
    }
}
