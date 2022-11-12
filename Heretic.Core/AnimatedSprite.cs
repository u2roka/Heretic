using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace Heretic.Core
{
    internal class AnimatedSprite : SpriteObject
    {
        protected float animationTime;
        protected LinkedList<Texture2D> images;
        private float animationTimePrev;
        protected bool animationTrigger;

        public AnimatedSprite(ContentManager content, Player player, ObjectRenderer objectRenderer, string path, Vector2 position, float animationTime) : base(content, player, objectRenderer, path, position)
        {
            hasAutoScaleAndShift = true;

            Initialize(path, animationTime);            
        }

        public AnimatedSprite(ContentManager content, Player player, ObjectRenderer objectRenderer, string path, Vector2 position, float scale, float shift, float animationTime) : base(content, player, objectRenderer, path, position, scale, shift)            
        {
            hasAutoScaleAndShift = false;

            Initialize(path, animationTime);            
        }

        private void Initialize(string path, float animationTime)
        {
            this.animationTime = animationTime;

            images = GetImages(Path.GetDirectoryName(path));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            CheckAnimationTime(gameTime);
            Animate(images);
        }

        public void Animate(LinkedList<Texture2D> images)
        {
            if (animationTrigger)
            {
                var first = images.First;
                images.RemoveFirst();
                images.AddLast(first);

                image = images.First.Value;

                PrepareSprite();
            }
        }

        protected void CheckAnimationTime(GameTime gameTime)
        {
            animationTrigger = false;
            float timeNow = (float) gameTime.TotalGameTime.TotalSeconds;
            if (timeNow - animationTimePrev > animationTime)
            {
                animationTimePrev = timeNow;
                animationTrigger = true;
            }
        }

        protected LinkedList<Texture2D> GetImages(string path)
        {
            LinkedList<Texture2D> result = new LinkedList<Texture2D>();

            DirectoryInfo dir = new DirectoryInfo(Path.Combine(content.RootDirectory, path));
            FileInfo[] files = dir.GetFiles("*.*");
            foreach (FileInfo file in files)
            {
                result.AddLast(content.Load<Texture2D>(Path.Combine(path, file.Name.Replace(file.Extension, string.Empty))));
            }

            return result;
        }
    }
}
