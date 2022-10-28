using Microsoft.Xna.Framework;
using System;
using System.Runtime.Serialization;

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

        public static float FOV = MathF.PI / 3;
        public static float HALF_FOV = FOV / 2;
        public static int NUM_RAYS = WIDTH / 2;
        public static int HALF_NUM_RAYS = NUM_RAYS / 2;
        public static float DELTA_ANGLE = FOV / NUM_RAYS;
        public static int MAX_DEPTH = 20;
    }
}
