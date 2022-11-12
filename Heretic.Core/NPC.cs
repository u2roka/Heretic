using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Heretic.Core
{
    internal class NPC : AnimatedSprite
    {
        private Random random = new Random();

        private int frameCounter;

        private LinkedList<Texture2D> attackImages;
        private LinkedList<Texture2D> deathImages;
        private LinkedList<Texture2D> idleImages;
        private LinkedList<Texture2D> painImages;
        private LinkedList<Texture2D> walkImages;

        private Texture2D attackEventImage;
        private int attackDistance;
        private float attackChance;
        private int attackDamage;

        private float speed;
        private int size;
        private int health;
        private bool attacking;

        private float accuracy;
        private bool pain;
        private bool rayCastValue;
        private bool playerSearchTrigger;
        private Vector2 nextPosition;

        private Sound sound;
        private Map map;
        private PathFinding pathFinding;
        private ObjectHandler objectHandler;        

        private bool alive;
        public bool Alive
        {
            get
            {
                return alive;
            }
        }

        public Point MapPosition
        {
            get
            {
                return new Point((int)position.X, (int)position.Y);
            }
        }

        public NPC(ContentManager content, Sound sound, Player player, Map map, PathFinding pathFinding, ObjectHandler objectHandler, ObjectRenderer objectRenderer, string path, Vector2 position, float animationTime, int attackEventImageIndex) : base(content, player, objectRenderer, path, position, animationTime)
        {
            this.sound = sound;
            this.player = player;
            this.map = map;
            this.pathFinding = pathFinding;
            this.objectHandler = objectHandler;
            
            string directory = Path.GetDirectoryName(path);

            attackImages = GetImages(Path.Combine(directory, "Attack"));
            deathImages = GetImages(Path.Combine(directory, "Death"));
            idleImages = GetImages(Path.Combine(directory, "Idle"));
            painImages = GetImages(Path.Combine(directory, "Pain"));
            walkImages = GetImages(Path.Combine(directory, "Walk"));

            attackEventImage = attackImages.ElementAt(attackEventImageIndex);
            attackDistance = random.Next(3, 7);
            attackChance = 0.1f;
            speed = 0.03f;
            size = 10;
            health = 100;
            attackDamage = 10;
            accuracy = 0.5f;
            alive = true;
        }

        public override void Update(GameTime gameTime)
        {
            CheckAnimationTime(gameTime);
            GetSprite();
            RunLogic(gameTime);
        }

        private void Movement(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            nextPosition = pathFinding.GetPath(MapPosition.ToVector2(), player.MapPosition.ToVector2());
            if (!objectHandler.NPCPositions.Contains(nextPosition.ToPoint()))
            {
                float angle = MathF.Atan2(nextPosition.Y + 0.5f - position.Y, nextPosition.X + 0.5f - position.X);
                Vector2 delta = new Vector2(MathF.Cos(angle), MathF.Sin(angle));
                delta *= speed;
                CheckWallCollision(deltaTime, delta);
            }
        }

        private void Attack()
        {
            sound.NPCAttack.Play();
            if (random.NextSingle() < accuracy)
            {
                player.GetDamage(attackDamage);
            }
        }

        private bool CheckWall(Point position)
        {
            return map.WorldMap[position.Y, position.X] == 0;
        }

        private void CheckWallCollision(float deltaTime, Vector2 position)
        {
            if (deltaTime == 0) return;

            if (CheckWall(new Point((int)(this.position.X + position.X * size), (int)this.position.Y)))
            {
                this.position.X += position.X;
            }
            if (CheckWall(new Point((int)this.position.X, (int)(this.position.Y + position.Y * size))))
            {
                this.position.Y += position.Y;
            }
        }

        private void AnimateAttack()
        {
            if (animationTrigger)
            {
                if (frameCounter < attackImages.Count - 1)
                {
                    var first = attackImages.First;
                    attackImages.RemoveFirst();
                    attackImages.AddLast(first);

                    image = attackImages.First.Value;
                    if (image.Equals(attackEventImage))
                    {
                        Attack();
                    }

                    PrepareSprite();

                    frameCounter++;
                }

                if (frameCounter == attackImages.Count - 1)
                {
                    frameCounter = 0;
                    attacking = false;
                }
            }
        }

        private void AnimateDeath()
        {
            if (!alive)
            {
                animationTime = 0.04f;                

                if (animationTrigger && frameCounter < deathImages.Count - 1)
                {
                    var first = deathImages.First;
                    deathImages.RemoveFirst();
                    deathImages.AddLast(first);

                    image = deathImages.First.Value;

                    PrepareSprite();

                    frameCounter++;
                }
            }
        }

        private void AnimatePain()
        {
            Animate(painImages);
            if (animationTrigger)
            {
                frameCounter = 0;
                pain = false;
            }
        }

        private void CheckHitInNPC()
        {
            if (rayCastValue && player.Shot)
            {
                if (Settings.HALF_WIDTH - spriteHalfWidth < screenX && screenX < Settings.HALF_WIDTH + spriteHalfWidth)
                {
                    player.Shot = false;
                    pain = true;
                    health -= player.AttackDamage;
                    CheckHealth();
                }
            }
        }

        private void CheckHealth()
        {
            if (health < 1)
            {
                alive = false;
                sound.NPCDeath.Play();
            }
            else 
            { 
                sound.NPCPain.Play(); 
            }
        }

        private void RunLogic(GameTime gameTime)
        {
            if (alive)
            {
                rayCastValue = RayCastPlayerNPC();
                CheckHitInNPC();
                
                if (pain)
                {
                    AnimatePain();
                }
                else if (rayCastValue)
                {
                    playerSearchTrigger = true;

                    if (attacking)
                    {
                        AnimateAttack();
                    }
                    else
                    {
                        if (distance < attackDistance && random.NextSingle() < attackChance)
                        {
                            attacking = true;
                        }
                        else
                        {
                            Animate(walkImages);
                            Movement(gameTime);
                        }
                    }
                }
                else if (playerSearchTrigger)
                {
                    Animate(walkImages);
                    Movement(gameTime);
                }
                else
                {
                    Animate(idleImages);
                }
            }
            else
            {
                AnimateDeath();
            }
        }

        private bool RayCastPlayerNPC()
        {
            if (player.MapPosition.Equals(MapPosition))
            {
                return true;
            }

            float wallDistanceVertical = 0;
            float wallDistanceHorizontal = 0;
            float playerDistanceVertical = 0;
            float playerDistanceHorizontal = 0;

            Vector2 o = player.Position;
            Point mapPosition = player.MapPosition;
                        
            Vector2 delta = new();
            Vector2 horizontal = new();
            Vector2 vertical = new();

            float rayAngle = theta;
            float sinAngle = MathF.Sin(rayAngle);
            float cosAngle = MathF.Cos(rayAngle);

            // horizontal
            horizontal.Y = sinAngle > 0 ? mapPosition.Y + 1f : mapPosition.Y - 0.000001f;
            delta.Y = sinAngle > 0 ? 1f : -1f;

            float depthHorizontal = (horizontal.Y - o.Y) / sinAngle;
            horizontal.X = o.X + depthHorizontal * cosAngle;

            float deltaDepth = delta.Y / sinAngle;
            delta.X = deltaDepth * cosAngle;

            for (int i = 0; i < Settings.MAX_DEPTH; i++)
            {
                horizontal.X = Math.Clamp(horizontal.X, 0, map.WorldMap.GetLength(1) - 1);
                horizontal.Y = Math.Clamp(horizontal.Y, 0, map.WorldMap.GetLength(0) - 1);
                Point horizontalTile = new Point((int)horizontal.X, (int)horizontal.Y);
                if (horizontalTile.Equals(MapPosition))
                {
                    playerDistanceHorizontal = depthHorizontal;
                    break;
                }
                if (map.WorldMap[horizontalTile.Y, horizontalTile.X] != 0)
                {
                    wallDistanceHorizontal = depthHorizontal;
                    break;
                }
                horizontal += delta;
                depthHorizontal += deltaDepth;
            }

            // verticals
            vertical.X = cosAngle > 0 ? mapPosition.X + 1f : mapPosition.X - 0.000001f;
            delta.X = cosAngle > 0 ? 1f : -1f;

            float depthVertical = (vertical.X - o.X) / cosAngle;
            vertical.Y = o.Y + depthVertical * sinAngle;

            deltaDepth = delta.X / cosAngle;
            delta.Y = deltaDepth * sinAngle;

            for (int i = 0; i < Settings.MAX_DEPTH; i++)
            {
                vertical.X = Math.Clamp(vertical.X, 0, map.WorldMap.GetLength(1) - 1);
                vertical.Y = Math.Clamp(vertical.Y, 0, map.WorldMap.GetLength(0) - 1);
                Point verticalTitle = new Point((int)vertical.X, (int)vertical.Y);
                if (verticalTitle.Equals(MapPosition))
                {
                    playerDistanceVertical = depthVertical;
                    break;
                }
                if (map.WorldMap[verticalTitle.Y, verticalTitle.X] != 0)
                {
                    wallDistanceVertical = depthVertical;
                    break;
                }
                vertical += delta;
                depthVertical += deltaDepth;
            }

            float playerDistance = MathF.Max(playerDistanceVertical, playerDistanceHorizontal);
            float wallDistance = MathF.Max(wallDistanceVertical, wallDistanceHorizontal);

            return (0 < playerDistance && playerDistance < wallDistance);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (rayCastValue) PrimitiveDrawer.DrawRectangle(spriteBatch, new Rectangle((int) nextPosition.X * 100,  (int) nextPosition.Y * 100, 100, 100), Color.Blue);

            PrimitiveDrawer.DrawRectangle(spriteBatch, new Rectangle((int)(position.X * 100 - 7.5f), (int)(position.Y * 100 - 7.5f), 15, 15), Color.Red);

            if (RayCastPlayerNPC())
            {
                PrimitiveDrawer.DrawLine(
                spriteBatch,
                position * 100,
                player.Position * 100,
                Color.Orange,
                2);
            }
        }
    }
}
