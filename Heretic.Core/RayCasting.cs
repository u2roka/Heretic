using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Heretic.Core
{
    internal class RayCasting
    {
        private Player player;
        private Map map;

        private float[] depth = new float[Settings.NUM_RAYS];
        private float[] projectHeight = new float[Settings.NUM_RAYS];        

        public RayCasting(Player player, Map map)
        {
            this.player = player;
            this.map = map;
        }

        private void RayCast()
        {
            Vector2 o = player.Position;
            Point mapPosition = player.MapPosition;

            Vector2 delta = new();
            Vector2 horizontal = new();
            Vector2 vertical = new();

            float rayAngle = player.Angle - Settings.HALF_FOV + 0.0001f;
            for (int ray = 0; ray < Settings.NUM_RAYS; ray++)            
            {
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
                    if (map.WorldMap[(int)horizontal.Y, (int)horizontal.X] == 1)
                    {
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
                    if (map.WorldMap[(int)vertical.Y, (int)vertical.X] == 1)
                    {
                        break;
                    }
                    vertical += delta;
                    depthVertical += deltaDepth;
                }

                depth[ray] = (depthVertical < depthHorizontal) ? depthVertical : depthHorizontal;
                depth[ray] *= MathF.Cos(player.Angle - rayAngle);

                projectHeight[ray] = Settings.SCREEN_DIST / (depth[ray] + 0.0001f);

                rayAngle += Settings.DELTA_ANGLE;
            }
        }

        public void Update()
        {
            RayCast();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int ray = 0; ray < Settings.NUM_RAYS; ray++)
            {
                float channel = 1 / (1 + MathF.Pow(depth[ray], 5) * 0.00002f);
                Color color = new(channel, channel, channel);
                PrimitiveDrawer.DrawRectangle(
                    spriteBatch, 
                    new Rectangle(ray * Settings.SCALE, Settings.HALF_HEIGHT - (int) (projectHeight[ray] / 2), Settings.SCALE, (int) projectHeight[ray]),
                    color);
            }
        }
    }
}
