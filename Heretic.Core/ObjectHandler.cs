using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using static Heretic.Core.NPC;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Heretic.Core
{
    internal class ObjectHandler
    {
        private const string STATIC_SPRITE_PATH = @"Sprites\Static";
        private const string DYNAMIC_SPRITE_PATH = @"Sprites\Animated";        

        private Map map;
        private ObjectRenderer objectRenderer;

        private Random random = new Random();

        private List<SpriteObject> spriteList;
        private List<NPC> npcList;
        private List<Point> restricedTiles;

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
            this.map = map;
            this.objectRenderer = objectRenderer;

            spriteList = new List<SpriteObject>();
            npcList = new List<NPC>();
            npcPositions = new List<Point>();
            restricedTiles = new List<Point>();

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

            //AddNPC(new Imp(content, sound, player, map, pathFinding, this, objectRenderer, new Vector2(11f, 19f)));
            //AddNPC(new Imp(content, sound, player, map, pathFinding, this, objectRenderer, new Vector2(11.5f, 5.5f)));
            //AddNPC(new Imp(content, sound, player, map, pathFinding, this, objectRenderer, new Vector2(13.5f, 6.5f)));
            //AddNPC(new Imp(content, sound, player, map, pathFinding, this, objectRenderer, new Vector2(4f, 20f)));
            //AddNPC(new Imp(content, sound, player, map, pathFinding, this, objectRenderer, new Vector2(4f, 29f)));
            //AddNPC(new Mummy(content, sound, player, map, pathFinding, this, objectRenderer, new Vector2(5.5f, 14.5f)));
            //AddNPC(new Mummy(content, sound, player, map, pathFinding, this, objectRenderer, new Vector2(5.5f, 16.5f)));
            //AddNPC(new Knight(content, sound, player, map, pathFinding, this, objectRenderer, new Vector2(2.5f, 24.5f)));
            //AddNPC(new Knight(content, sound, player, map, pathFinding, this, objectRenderer, new Vector2(7.5f, 28.5f)));
            //AddNPC(new Malotaur(content, sound, player, map, pathFinding, this, objectRenderer, new Vector2(14.5f, 25.5f)));            
            
            SpawnNPC(content, sound, player, map, pathFinding, objectRenderer);
        }

        private void SpawnNPC(ContentManager content, Sound sound, Player player, Map map, PathFinding pathFinding, ObjectRenderer objectRenderer)
        {
            // restriced tiles around spawned player
            for (int j = 0; j < 10; j++)
                for (int i = 0; i < 10; i++)
                {
                    restricedTiles.Add(new Point(i, j));
                }

            for (int i = 0; i < Settings.MAX_NPC_NO; i++)
            {
                Point position;
                do
                {
                    position = new Point(random.Next(map.WorldMap.GetLength(1)), random.Next(map.WorldMap.GetLength(0)));
                } while (map.WorldMap[position.Y, position.X] != 0 || restricedTiles.Contains(position));

                float totalChance = Settings.IMP_SPAWN_CHANCE
                    + Settings.MUMMY_SPAWN_CHANCE
                    + Settings.KNIGHT_SPAWN_CHANCE
                    + Settings.MALOTUR_SPAWN_CHANCE;

                int typeChance = random.Next((int) totalChance);
                if (typeChance < Settings.MALOTUR_SPAWN_CHANCE)
                {
                    AddNPC(new Malotaur(content, sound, player, map, pathFinding, this, objectRenderer, new Vector2(position.X + 0.5f, position.Y + 0.5f)));
                }
                else if (typeChance < Settings.MALOTUR_SPAWN_CHANCE + Settings.KNIGHT_SPAWN_CHANCE)
                {
                    AddNPC(new Knight(content, sound, player, map, pathFinding, this, objectRenderer, new Vector2(position.X + 0.5f, position.Y + 0.5f)));
                }
                else if (typeChance < Settings.MALOTUR_SPAWN_CHANCE + Settings.KNIGHT_SPAWN_CHANCE + Settings.MUMMY_SPAWN_CHANCE)
                {
                    AddNPC(new Mummy(content, sound, player, map, pathFinding, this, objectRenderer, new Vector2(position.X + 0.5f, position.Y + 0.5f)));
                }
                else
                {
                    AddNPC(new Imp(content, sound, player, map, pathFinding, this, objectRenderer, new Vector2(position.X + 0.5f, position.Y + 0.5f)));
                }
            }
        }
        private void AddNPC(NPC npc)
        {
            restricedTiles.Add(npc.MapPosition);
            npcList.Add(npc);
        }

        private void AddSprite(SpriteObject sprite)
        {
            restricedTiles.Add(sprite.MapPosition);
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
