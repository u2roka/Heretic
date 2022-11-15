using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace Heretic.Core
{
    internal class ObjectHandler
    {
        private const string STATIC_SPRITE_PATH = @"Sprites\Static";
        private const string DYNAMIC_SPRITE_PATH = @"Sprites\Animated";

        private ObjectRenderer objectRenderer;
        
        private List<SpriteObject> spriteList;
        private List<NPC> npcList;
        
        private List<Point> npcPositions;
        public List<Point> NPCPositions
        {
            get
            {
                return npcPositions;
            }
        }
        
        public ObjectHandler(ContentManager content, Sound sound, Player player, Map map, PathFinding pathFinding, ObjectRenderer objectRenderer)
        {
            this.objectRenderer = objectRenderer;

            spriteList = new List<SpriteObject>();
            npcList = new List<NPC>();
            npcPositions = new List<Point>();

            AddSprite(new SpriteObject(content, player, objectRenderer, Path.Combine(STATIC_SPRITE_PATH, "BARLA0"), new Vector2(10.5f, 3.5f), 0.58f, 0.3554f));
            AddSprite(new SpriteObject(content, player, objectRenderer, Path.Combine(STATIC_SPRITE_PATH, "BARLA0"), new Vector2(11.5f, 3.5f)));
            AddSprite(new SpriteObject(content, player, objectRenderer, Path.Combine(STATIC_SPRITE_PATH, "BARLA0"), new Vector2(11.5f, 4.5f)));
            AddSprite(new SpriteObject(content, player, objectRenderer, Path.Combine(STATIC_SPRITE_PATH, "BARLA0"), new Vector2(12.5f, 20.5f)));
            AddSprite(new SpriteObject(content, player, objectRenderer, Path.Combine(STATIC_SPRITE_PATH, "BARLA0"), new Vector2(10.5f, 20.5f)));
            AddSprite(new SpriteObject(content, player, objectRenderer, Path.Combine(STATIC_SPRITE_PATH, "BARLA0"), new Vector2(9.5f, 20.5f)));
            AddSprite(new SpriteObject(content, player, objectRenderer, Path.Combine(STATIC_SPRITE_PATH, "BARLA0"), new Vector2(7.5f, 20.5f)));
            AddSprite(new SpriteObject(content, player, objectRenderer, Path.Combine(STATIC_SPRITE_PATH, "BRPLA0"), new Vector2(1.5f, 24.5f), 1f, 0f));
            AddSprite(new SpriteObject(content, player, objectRenderer, Path.Combine(STATIC_SPRITE_PATH, "BRPLA0"), new Vector2(1.5f, 30.5f), 1f, 0f));
            AddSprite(new SpriteObject(content, player, objectRenderer, Path.Combine(STATIC_SPRITE_PATH, "BRPLA0"), new Vector2(14.5f, 24.5f), 1f, 0f));
            AddSprite(new SpriteObject(content, player, objectRenderer, Path.Combine(STATIC_SPRITE_PATH, "BRPLA0"), new Vector2(14.5f, 30.5f), 1f, 0f));
            AddSprite(new AnimatedSprite(content, player, objectRenderer, Path.Combine(DYNAMIC_SPRITE_PATH, @"Candelabra\SRTCA0"), new Vector2(5.75f, 3.25f), 0.12f));
            AddSprite(new AnimatedSprite(content, player, objectRenderer, Path.Combine(DYNAMIC_SPRITE_PATH, @"Candelabra\SRTCA0"), new Vector2(5.75f, 4.75f), 0.12f));
            AddSprite(new AnimatedSprite(content, player, objectRenderer, Path.Combine(DYNAMIC_SPRITE_PATH, @"Candelabra\SRTCA0"), new Vector2(4.25f, 16.5f), 0.12f));
            AddSprite(new AnimatedSprite(content, player, objectRenderer, Path.Combine(DYNAMIC_SPRITE_PATH, @"Candelabra\SRTCA0"), new Vector2(2.75f, 16.5f), 0.12f));
            AddSprite(new AnimatedSprite(content, player, objectRenderer, Path.Combine(DYNAMIC_SPRITE_PATH, @"Cauldron\KFR1A0"), new Vector2(1.5f, 1.5f), 0.12f));
            AddSprite(new AnimatedSprite(content, player, objectRenderer, Path.Combine(DYNAMIC_SPRITE_PATH, @"Cauldron\KFR1A0"), new Vector2(1.5f, 7.5f), 0.12f));
            AddSprite(new AnimatedSprite(content, player, objectRenderer, Path.Combine(DYNAMIC_SPRITE_PATH, @"Cauldron\KFR1A0"), new Vector2(14.5f, 1.5f), 0.12f));
            AddSprite(new AnimatedSprite(content, player, objectRenderer, Path.Combine(DYNAMIC_SPRITE_PATH, @"Cauldron\KFR1A0"), new Vector2(14.5f, 7.5f), 0.12f));
            AddSprite(new AnimatedSprite(content, player, objectRenderer, Path.Combine(DYNAMIC_SPRITE_PATH, @"Cauldron\KFR1A0"), new Vector2(9.5f, 16.5f), 0.12f));
            AddSprite(new AnimatedSprite(content, player, objectRenderer, Path.Combine(DYNAMIC_SPRITE_PATH, @"Cauldron\KFR1A0"), new Vector2(13.5f, 16.5f), 0.12f));
            AddSprite(new AnimatedSprite(content, player, objectRenderer, Path.Combine(DYNAMIC_SPRITE_PATH, @"Torch\TRCHA0"), new Vector2(3.5f, 26.9f), 0.25f, -0.5f, 0.12f));
            AddSprite(new AnimatedSprite(content, player, objectRenderer, Path.Combine(DYNAMIC_SPRITE_PATH, @"Torch\TRCHA0"), new Vector2(3.5f, 28.1f), 0.25f, -0.5f, 0.12f));
            AddSprite(new AnimatedSprite(content, player, objectRenderer, Path.Combine(DYNAMIC_SPRITE_PATH, @"Torch\TRCHA0"), new Vector2(2.9f, 27.5f), 0.25f, -0.5f, 0.12f));
            AddSprite(new AnimatedSprite(content, player, objectRenderer, Path.Combine(DYNAMIC_SPRITE_PATH, @"Torch\TRCHA0"), new Vector2(4.1f, 27.5f), 0.25f, -0.5f, 0.12f));
            AddSprite(new AnimatedSprite(content, player, objectRenderer, Path.Combine(DYNAMIC_SPRITE_PATH, @"Torch\TRCHA0"), new Vector2(7.5f, 26.9f), 0.25f, -0.5f, 0.12f));
            AddSprite(new AnimatedSprite(content, player, objectRenderer, Path.Combine(DYNAMIC_SPRITE_PATH, @"Torch\TRCHA0"), new Vector2(7.5f, 28.1f), 0.25f, -0.5f, 0.12f));
            AddSprite(new AnimatedSprite(content, player, objectRenderer, Path.Combine(DYNAMIC_SPRITE_PATH, @"Torch\TRCHA0"), new Vector2(6.9f, 27.5f), 0.25f, -0.5f, 0.12f));
            AddSprite(new AnimatedSprite(content, player, objectRenderer, Path.Combine(DYNAMIC_SPRITE_PATH, @"Torch\TRCHA0"), new Vector2(8.1f, 27.5f), 0.25f, -0.5f, 0.12f));
            AddSprite(new AnimatedSprite(content, player, objectRenderer, Path.Combine(DYNAMIC_SPRITE_PATH, @"Torch\TRCHA0"), new Vector2(11.5f, 26.9f), 0.25f, -0.5f, 0.12f));
            AddSprite(new AnimatedSprite(content, player, objectRenderer, Path.Combine(DYNAMIC_SPRITE_PATH, @"Torch\TRCHA0"), new Vector2(11.5f, 28.1f), 0.25f, -0.5f, 0.12f));
            AddSprite(new AnimatedSprite(content, player, objectRenderer, Path.Combine(DYNAMIC_SPRITE_PATH, @"Torch\TRCHA0"), new Vector2(10.9f, 27.5f), 0.25f, -0.5f, 0.12f));
            AddSprite(new AnimatedSprite(content, player, objectRenderer, Path.Combine(DYNAMIC_SPRITE_PATH, @"Torch\TRCHA0"), new Vector2(12.1f, 27.5f), 0.25f, -0.5f, 0.12f));

            AddNPC(new Imp(content, sound, player, map, pathFinding, this, objectRenderer, new Vector2(11f, 19f)));
            AddNPC(new Imp(content, sound, player, map, pathFinding, this, objectRenderer, new Vector2(11.5f, 5.5f)));
            AddNPC(new Imp(content, sound, player, map, pathFinding, this, objectRenderer, new Vector2(13.5f, 6.5f)));
            AddNPC(new Imp(content, sound, player, map, pathFinding, this, objectRenderer, new Vector2(4f, 20f)));
            AddNPC(new Imp(content, sound, player, map, pathFinding, this, objectRenderer, new Vector2(4f, 29f)));
            AddNPC(new Mummy(content, sound, player, map, pathFinding, this, objectRenderer, new Vector2(5.5f, 14.5f)));
            AddNPC(new Mummy(content, sound, player, map, pathFinding, this, objectRenderer, new Vector2(5.5f, 16.5f)));
            AddNPC(new Knight(content, sound, player, map, pathFinding, this, objectRenderer, new Vector2(2.5f, 24.5f)));
            AddNPC(new Knight(content, sound, player, map, pathFinding, this, objectRenderer, new Vector2(7.5f, 28.5f)));
            AddNPC(new Malotaur(content, sound, player, map, pathFinding, this, objectRenderer, new Vector2(14.5f, 25.5f)));
        }
        private void AddNPC(NPC npc)
        {
            npcList.Add(npc);
        }

        private void AddSprite(SpriteObject sprite)
        {
            spriteList.Add(sprite);
        }

        public void Update(GameTime gameTime)
        {
            foreach (SpriteObject next in spriteList)
            {
                next.Update(gameTime);
            }

            npcPositions.Clear();
            foreach (NPC next in npcList)
            {
                if (next.Alive)
                {
                    npcPositions.Add(next.MapPosition);
                }                
            }

            foreach (NPC next in npcList)
            {
                next.Update(gameTime);
            }

            if (npcPositions.Count == 0)
            {
                objectRenderer.PlayerVictoryEventFired();
            }
        }        

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (NPC next in npcList)
            {
                next.Draw(gameTime, spriteBatch);
            }
        }
    }
}
