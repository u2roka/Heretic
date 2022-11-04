using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heretic
{
    public class Map
    {
        public readonly int[,] WorldMap = {
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 5, 5, 5, 5, 0, 0, 0, 2, 2, 2, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, 0, 2, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, 0, 2, 0, 0, 1 },
            { 1, 0, 0, 5, 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 3, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
        };
        
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int y = 0; y < WorldMap.GetLength(0); y++)
                for (int x = 0; x < WorldMap.GetLength(1); x++)
                {
                    PrimitiveDrawer.DrawRectangle(spriteBatch, new Rectangle(x * WorldMap[y, x] * 100, y * WorldMap[y, x] * 100, 100, 100), Color.LightSlateGray);
                }
        }
    }
}
