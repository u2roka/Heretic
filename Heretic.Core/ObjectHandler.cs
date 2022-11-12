using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace Heretic.Core
{
    internal class ObjectHandler
    {
        private const string NPC_SPRITE_PATH = @"Sprites\NPCs";
        private const string STATIC_SPRITE_PATH = @"Sprites\Static";
        private const string DYNAMIC_SPRITE_PATH = @"Sprites\Animated";

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
            spriteList = new List<SpriteObject>();
            npcList = new List<NPC>();
            npcPositions = new List<Point>();

            AddSprite(new SpriteObject(content, player, objectRenderer, Path.Combine(STATIC_SPRITE_PATH, "BARLA0"), new Vector2(10.5f, 3.5f), 0.58f, 0.3554f));
            AddSprite(new SpriteObject(content, player, objectRenderer, Path.Combine(STATIC_SPRITE_PATH, "BARLA0"), new Vector2(11.5f, 3.5f)));
            AddSprite(new SpriteObject(content, player, objectRenderer, Path.Combine(STATIC_SPRITE_PATH, "BARLA0"), new Vector2(11.5f, 4.5f)));
            AddSprite(new AnimatedSprite(content, player, objectRenderer, Path.Combine(DYNAMIC_SPRITE_PATH, @"Candelabra\SRTCA0"), new Vector2(5.75f, 3.25f), 0.12f));
            AddSprite(new AnimatedSprite(content, player, objectRenderer, Path.Combine(DYNAMIC_SPRITE_PATH, @"Candelabra\SRTCA0"), new Vector2(5.75f, 4.75f), 0.12f));
            AddSprite(new AnimatedSprite(content, player, objectRenderer, Path.Combine(DYNAMIC_SPRITE_PATH, @"Cauldron\KFR1A0"), new Vector2(1.5f, 1.5f), 0.12f));
            AddSprite(new AnimatedSprite(content, player, objectRenderer, Path.Combine(DYNAMIC_SPRITE_PATH, @"Cauldron\KFR1A0"), new Vector2(1.5f, 7.5f), 0.12f));
            AddSprite(new AnimatedSprite(content, player, objectRenderer, Path.Combine(DYNAMIC_SPRITE_PATH, @"Cauldron\KFR1A0"), new Vector2(14.5f, 1.5f), 0.12f));
            AddSprite(new AnimatedSprite(content, player, objectRenderer, Path.Combine(DYNAMIC_SPRITE_PATH, @"Cauldron\KFR1A0"), new Vector2(14.5f, 7.5f), 0.12f));

            AddNPC(new NPC(content, sound, player, map, pathFinding, this, objectRenderer, Path.Combine(NPC_SPRITE_PATH, @"Imp\IMPXA1"), new Vector2(10.5f, 5.5f), 0.18f, 1));
            AddNPC(new NPC(content, sound, player, map, pathFinding, this, objectRenderer, Path.Combine(NPC_SPRITE_PATH, @"Imp\IMPXA1"), new Vector2(11.5f, 4.5f), 0.18f, 1));
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
