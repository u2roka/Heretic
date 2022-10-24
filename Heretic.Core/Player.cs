using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;

namespace Heretic
{
    internal class Player
    {
        private Vector2 position;
        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        public Point MapPosition
        {
            get
            {
                return new Point((int)position.X, (int)position.Y);
            }
        }

        private double angle;

        private Map map;

        public Player(Map map)
        {
            position = Settings.PLAYER_POS;
            angle = Settings.PLAYER_ANGLE;

            this.map = map;
        }

        public void Update(GameTime gameTime)
        {
            double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;

            double sinA = Math.Sin(angle);
            double cosA = Math.Cos(angle);
            double speed = Settings.PLAYER_SPEED * deltaTime;
            float speedSin = (float)(speed * sinA);
            float speedCos = (float)(speed * cosA);
            Vector2 delta = Vector2.Zero;

            var keys = Keyboard.GetState();
            if (keys.IsKeyDown(Keys.W))
            {
                delta.X += speedCos;
                delta.Y += speedSin;
            }
            if (keys.IsKeyDown(Keys.S))
            {
                delta.X -= speedCos;
                delta.Y -= speedSin;
            }
            if (keys.IsKeyDown(Keys.A))
            {
                delta.X += speedSin;
                delta.Y += -speedCos;
            }
            if (keys.IsKeyDown(Keys.D))
            {
                delta.X += -speedSin;
                delta.Y += speedCos;
            }

            CheckWallCollision(delta);

            if (keys.IsKeyDown(Keys.Left))
            {
                angle -= Settings.PLAYER_ROT_SPEED * deltaTime;
            }
            if (keys.IsKeyDown(Keys.Right))
            {
                angle += Settings.PLAYER_ROT_SPEED * deltaTime;
            }

            angle %= Math.Tau;
        }
        private bool CheckWall(Point position)
        {
            return map.WorldMap[position.Y, position.X] != 1;
        }

        private void CheckWallCollision(Vector2 position)
        {
            if (CheckWall(new Point((int)(this.position.X + position.X), (int)this.position.Y)))
            {
                this.position.X += position.X;
            }
            if (CheckWall(new Point((int)this.position.X, (int)(this.position.Y + position.Y))))
            {
                this.position.Y += position.Y;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            PrimitiveDrawer.DrawLine(
                spriteBatch,
                position * 100,
                new Vector2((float)(position.X * 100 + Settings.WIDTH * Math.Cos(angle)),
                    (float)(position.Y * 100 + Settings.HEIGHT * Math.Sin(angle))),
                Color.Yellow,
                2);

            PrimitiveDrawer.DrawRectangle(spriteBatch, new Rectangle((int)(position.X * 100 - 7.5f), (int)(position.Y * 100 - 7.5f), 15, 15), Color.Green);
        }
    }
}
