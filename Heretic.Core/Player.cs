using Heretic.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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

        private int relativeMovement;
        public int RelativeMovement 
        { 
            get
            {
                return relativeMovement;
            }
        }

        private float angle;
        public float Angle 
        { 
            get
            {
                return angle;
            }
        }

        private bool shot;
        public bool Shot
        {
            get 
            { 
                return shot; 
            }
            set 
            { 
                shot = value; 
            }
        }

        private bool reloading;
        public bool Reloading
        {
            get
            {
                return reloading;
            }
            set
            {
                reloading = value;
            }
        }

        private Map map;
        private Sound sound;

        public Player(Map map, Sound sound)
        {
            position = Settings.PLAYER_POS;
            angle = Settings.PLAYER_ANGLE;

            this.map = map;
            this.sound = sound;
        }

        private void Movement(float deltaTime)
        {
            float sinA = MathF.Sin(angle);
            float cosA = MathF.Cos(angle);
            float speed = Settings.PLAYER_SPEED * deltaTime;
            float speedSin = speed * sinA;
            float speedCos = speed * cosA;
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

            CheckWallCollision(deltaTime, delta);

            //if (keys.IsKeyDown(Keys.Left))
            //{
            //    angle -= Settings.PLAYER_ROT_SPEED * deltaTime;
            //}
            //if (keys.IsKeyDown(Keys.Right))
            //{
            //    angle += Settings.PLAYER_ROT_SPEED * deltaTime;
            //}

            angle %= MathF.Tau;
        }

        private void MouseControl(float deltaTime)
        {
            MouseState mouseState = Mouse.GetState();
            Point mouse = mouseState.Position;
            if (mouse.X < Settings.MOUSE_BORDER_LEFT || mouse.X > Settings.MOUSE_BORDER_RIGHT)
            {
                Mouse.SetPosition(Settings.HALF_WIDTH, Settings.HALF_HEIGHT);
            }
            
            relativeMovement = (Settings.HALF_WIDTH - mouse.X) * -1;
            relativeMovement = Math.Max(-Settings.MOUSE_MAXIMUM_RELATIVE_MOVEMENT, Math.Min(Settings.MOUSE_MAXIMUM_RELATIVE_MOVEMENT, relativeMovement));
            angle += relativeMovement * Settings.MOUSE_SENSITIVITY * deltaTime;
            Mouse.SetPosition(Settings.HALF_WIDTH, Settings.HALF_HEIGHT);

            if (mouseState.LeftButton == ButtonState.Pressed && !Shot && !Reloading)
            {
                sound.ElvenWand.Play();
                Shot = true;
                Reloading = true;
            }                
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Movement(deltaTime);
            MouseControl(deltaTime);
        }

        private bool CheckWall(Point position)
        {
            return map.WorldMap[position.Y, position.X] == 0;
        }

        private void CheckWallCollision(float deltaTime, Vector2 position)
        {
            if (deltaTime == 0) return;

            float scale = Settings.PLAYER_SIZE_SCALE / deltaTime;

            if (CheckWall(new Point((int)(this.position.X + position.X * scale), (int)this.position.Y)))
            {
                this.position.X += position.X;
            }
            if (CheckWall(new Point((int)this.position.X, (int)(this.position.Y + position.Y * scale))))
            {
                this.position.Y += position.Y;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            PrimitiveDrawer.DrawLine(
                spriteBatch,
                position * 100,
                new Vector2(position.X * 100 + Settings.WIDTH * MathF.Cos(angle), position.Y * 100 + Settings.HEIGHT * MathF.Sin(angle)),
                Color.Yellow,
                2);

            PrimitiveDrawer.DrawRectangle(spriteBatch, new Rectangle((int)(position.X * 100 - 7.5f), (int)(position.Y * 100 - 7.5f), 15, 15), Color.Green);
        }
    }
}
