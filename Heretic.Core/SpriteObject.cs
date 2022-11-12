using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Heretic.Core
{
    internal class SpriteObject
    {
        protected ContentManager content;
        protected Player player;
        private ObjectRenderer objectRenderer;
        protected Vector2 position;
        protected float spriteScale;
        private float spriteHeightShift;
        protected float spriteHalfWidth;
        protected bool hasAutoScaleAndShift;
        
        protected Texture2D image;
        private float imageHalfWidth;
        private float imageRatio;

        protected float theta;
        protected float screenX;
        protected float distance;
        private float normalizedDistance;

        public SpriteObject(ContentManager content, Player player, ObjectRenderer objectRenderer, string path, Vector2 position)
        {
            hasAutoScaleAndShift = true;

            Initialize(content, player, objectRenderer, path, position);            
        }

        public SpriteObject(ContentManager content, Player player, ObjectRenderer objectRenderer, string path, Vector2 position, float scale, float shift)
        {
            hasAutoScaleAndShift = false;

            Initialize(content, player, objectRenderer, path, position);            

            spriteScale = scale;
            spriteHeightShift = shift;            
        }

        private void Initialize(ContentManager content, Player player, ObjectRenderer objectRenderer, string path, Vector2 position)
        {
            this.content = content;
            this.player = player;
            this.objectRenderer = objectRenderer;
            this.position = position;            

            image = content.Load<Texture2D>(path);

            PrepareSprite();
        }

        protected void PrepareSprite()
        {
            imageHalfWidth = image.Width / 2;
            imageRatio = image.Width / (float)image.Height;

            if (hasAutoScaleAndShift)
            {
                spriteScale = image.Height / 1f / Settings.TEXTURE_SIZE;
                spriteHeightShift = (Settings.TEXTURE_SIZE - image.Height) / 2f / image.Height;
            }
        }

        private void GetSpriteProjection()
        {
            float projection = Settings.SCREEN_DIST / normalizedDistance * spriteScale;
            float projectionWidth = projection * imageRatio;
            float projectionHeight = projection;

            spriteHalfWidth = projectionWidth / 2;
            int heightShift = (int) (projectionHeight * spriteHeightShift);
            Point position = new Point((int) (screenX - spriteHalfWidth), Settings.HALF_HEIGHT - (int) (projectionHeight / 2) + heightShift);
            Rectangle spriteColumnDestionation = new Rectangle(position.X, position.Y, (int) projectionWidth, (int) projectionHeight);

            RayCasting.ObjectToRender sprite = new RayCasting.ObjectToRender(normalizedDistance, image, spriteColumnDestionation);

            objectRenderer.ObjectsToRender.Add(sprite);
        }

        public void GetSprite()
        {
            Vector2 deltaPosition = new Vector2(position.X - player.Position.X, position.Y - player.Position.Y);
            theta = MathF.Atan2(deltaPosition.Y, deltaPosition.X);

            float delta = theta - player.Angle;
            if ((deltaPosition.X > 0 && player.Angle > MathF.PI) || (deltaPosition.X < 0 && deltaPosition.Y < 0))
            {
                delta += MathF.Tau;
            }
            if (delta > MathF.PI) delta -= MathF.Tau;

            float deltaRays = delta / Settings.DELTA_ANGLE;
            screenX = (Settings.HALF_NUM_RAYS + deltaRays) * Settings.SCALE;

            distance = Hypot(deltaPosition.X, deltaPosition.Y);
            normalizedDistance = distance * MathF.Cos(delta);

            if (-imageHalfWidth < screenX && screenX < (Settings.WIDTH + imageHalfWidth) && normalizedDistance > 0.5f)
            {
                GetSpriteProjection();
            }
        }

        private float Hypot(float x, float y)
        {
            return MathF.Sqrt(MathF.Pow(x, 2) + MathF.Pow(y, 2));
        }

        public virtual void Update(GameTime gameTime)
        {
            GetSprite();
        }
    }
}
