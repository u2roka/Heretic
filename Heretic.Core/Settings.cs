using Microsoft.Xna.Framework;

namespace Heretic
{
    public static class Settings
    {
        public static int WIDTH { get; set; } = 1600;
        public static int HEIGHT { get; set; } = 900;
        public static int FPS { get; set; } = 60;

        public static Vector2 PLAYER_POS { get; set; } = new Vector2(1.5f, 5f);
        public static float PLAYER_ANGLE = 0f;
        public static float PLAYER_SPEED = 2f;
        public static float PLAYER_ROT_SPEED = 1f;
    }
}
