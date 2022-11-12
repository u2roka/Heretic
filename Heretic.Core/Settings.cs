using Microsoft.Xna.Framework;
using System;
using System.Runtime.Serialization;

namespace Heretic
{
    public static class Settings
    {
        public static int WIDTH { get; set; } = 1600;
        public static int HEIGHT { get; set; } = 900;
        public static int HALF_WIDTH = WIDTH / 2;
        public static int HALF_HEIGHT = HEIGHT / 2; 
        public static int FPS { get; set; } = 60;

        public static Vector2 PLAYER_POS { get; set; } = new Vector2(1.5f, 5f);
        public static float PLAYER_ANGLE = 0f;
        public static float PLAYER_SPEED = 4f;
        public static float PLAYER_ROT_SPEED = 2f;
        public static float PLAYER_SIZE_SCALE = 0.06f;
        public static int PLAYER_MAX_HEALTH = 100;

        public static float MOUSE_SENSITIVITY = 0.3f;
        public static int MOUSE_MAXIMUM_RELATIVE_MOVEMENT = 40;
        public static int MOUSE_BORDER_LEFT = 100;
        public static int MOUSE_BORDER_RIGHT = WIDTH - MOUSE_BORDER_LEFT;

        public static Color FLOOR_COLOR = new Color(30, 30, 30);

        public static float FOV = MathF.PI / 3;
        public static float HALF_FOV = FOV / 2;
        public static int NUM_RAYS = WIDTH / 2;
        public static int HALF_NUM_RAYS = NUM_RAYS / 2;
        public static float DELTA_ANGLE = FOV / NUM_RAYS;
        public static int MAX_DEPTH = 20;

        public static float SCREEN_DIST = HALF_WIDTH / MathF.Tan(HALF_FOV);
        public static int SCALE = WIDTH / NUM_RAYS;

        public static int TEXTURE_SIZE = 296;
        public static int HALF_TEXTURE_SIZE = TEXTURE_SIZE / 2;
    }
}
