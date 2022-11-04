using Microsoft.Xna.Framework;
using System;

namespace Heretic.Core
{
    internal class RayCasting
    {
        private Player player;
        private Map map;
        private ObjectRenderer objectRenderer;

        private RayCastingResult[] rayCastingResults = new RayCastingResult[Settings.NUM_RAYS];

        public RayCasting(Player player, Map map, ObjectRenderer objectRenderer)
        {
            this.player = player;
            this.map = map;
            this.objectRenderer = objectRenderer;            
        }

        private void GetObjectsToRender()
        {
            for (int i = 0; i < rayCastingResults.Length; i++)
            {
                RayCastingResult values = rayCastingResults[i];

                int textureHeight = Settings.TEXTURE_SIZE * Settings.HEIGHT / (int) values.ProjectHeight;

                Rectangle wallColumnSource = values.ProjectHeight < Settings.HEIGHT
                    ? new Rectangle((int)(values.Offset * (Settings.TEXTURE_SIZE - Settings.SCALE)), 0, Settings.SCALE, Settings.TEXTURE_SIZE)
                    : new Rectangle((int)(values.Offset * (Settings.TEXTURE_SIZE - Settings.SCALE)), Settings.HALF_TEXTURE_SIZE - textureHeight / 2, Settings.SCALE, textureHeight);
                Rectangle wallColumnDestination = values.ProjectHeight < Settings.HEIGHT
                    ? new Rectangle(i * Settings.SCALE, Settings.HALF_HEIGHT - (int)values.ProjectHeight / 2, Settings.SCALE, (int)values.ProjectHeight)
                    : new Rectangle(i * Settings.SCALE, 0, Settings.SCALE, Settings.HEIGHT);

                objectRenderer.ObjectsToRender[i] = new ObjectToRender(values.Depth,
                    values.Texture,
                    wallColumnSource,
                    wallColumnDestination
                );
            }
        }

        private void RayCast()
        {
            Vector2 o = player.Position;
            Point mapPosition = player.MapPosition;

            int textureVertical = 1;
            int textureHorizontal = 1;

            float depth;
            float projectHeight;
            int texture;
            float offset;            

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
                    if (map.WorldMap[(int)horizontal.Y, (int)horizontal.X] != 0)
                    {
                        textureHorizontal = map.WorldMap[(int)horizontal.Y, (int)horizontal.X];
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
                    if (map.WorldMap[(int)vertical.Y, (int)vertical.X] != 0)
                    {
                        textureVertical = map.WorldMap[(int)vertical.Y, (int)vertical.X];
                        break;
                    }
                    vertical += delta;
                    depthVertical += deltaDepth;
                }

                if (depthVertical < depthHorizontal)
                {
                    depth = depthVertical;
                    texture = textureVertical;
                    vertical.Y %= 1;
                    offset = cosAngle > 0 ? vertical.Y : 1 - vertical.Y;
                }
                else
                {
                    depth = depthHorizontal;
                    texture = textureHorizontal;
                    horizontal.X %= 1;
                    offset = sinAngle > 0 ? 1 - horizontal.X: horizontal.X;
                }
                    
                depth *= MathF.Cos(player.Angle - rayAngle);

                projectHeight = Settings.SCREEN_DIST / (depth + 0.0001f);

                rayCastingResults[ray] = new RayCastingResult(depth, projectHeight, texture, offset);

                rayAngle += Settings.DELTA_ANGLE;
            }
        }

        public void Update()
        {
            RayCast();
            GetObjectsToRender();
        }        

        public struct RayCastingResult
        {
            public float Depth { get; set; }
            public float ProjectHeight { get; set; }
            public int Texture { get; set; }
            public float Offset { get; set; }

            public RayCastingResult(float depth, float projectHeight, int texture, float offset)
            {
                Depth = depth;
                ProjectHeight = projectHeight;
                Texture = texture;
                Offset = offset;
            }
        }

        public struct ObjectToRender
        {
            public float Depth { get; set; }
            public int Texture { get; set; }
            public Rectangle WallColumnSource { get; set; }
            public Rectangle WallColumnDestination { get; set; }

            public ObjectToRender(float depth, int texture, Rectangle wallColumnSource, Rectangle wallColumnDestination)
            {
                Depth = depth;
                Texture = texture;
                WallColumnSource = wallColumnSource;
                this.WallColumnDestination = wallColumnDestination;
            }
        }
    }
}
