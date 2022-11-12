using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace Heretic.Core
{
    internal class Weapon : AnimatedSprite
    {
        private int frameCounter;
        
        private int damage;
        public int Damage
        {
            get
            {
                return damage;
            }
        }

        public Weapon(ContentManager content, Player player, string path, float scale, float animationTime) : base(content, player, null, path, Vector2.Zero, scale, 0f, animationTime)
        {
            position = new Vector2(Settings.HALF_WIDTH, Settings.HEIGHT);
            damage = 25;
            player.AttackDamage = damage;
        }

        private void AnimateShot()
        {
            if (player.Reloading)
            {
                player.Shot = false;
                if (animationTrigger)
                {
                    var first = images.First;
                    images.RemoveFirst();
                    images.AddLast(first);

                    image = images.First.Value;
                    
                    frameCounter++;
                    if (frameCounter == images.Count)
                    {
                        player.Reloading = false;
                        frameCounter = 0;
                    }
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                images.First.Value,
                position,
                null,
                Color.White,
                0f,
                new Vector2(image.Width / 2, image.Height),
                new Vector2(spriteScale, spriteScale),
                SpriteEffects.None,
                0f
            );
        }

        public override void Update(GameTime gameTime)
        {
            CheckAnimationTime(gameTime);
            AnimateShot();
        }
    }
}
