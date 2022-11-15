using Heretic.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Globalization;

namespace Heretic
{
    public static class Settings
    {
        public static IniParser PARSER = new IniParser("heretic.ini");

        public static bool ENABLE_FULLSCREEN = int.Parse(PARSER.GetSetting("render", "enable_fullscreen")) == 1;
        public static int WIDTH = ENABLE_FULLSCREEN ? GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width : int.Parse(PARSER.GetSetting("render", "screen_width"));
        public static int HEIGHT = ENABLE_FULLSCREEN ? GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height : int.Parse(PARSER.GetSetting("render", "screen_height"));
        public static int HALF_WIDTH = WIDTH / 2;
        public static int HALF_HEIGHT = HEIGHT / 2;        
        public static int FPS = 60;

        public static Vector2 PLAYER_POS = new Vector2(1.5f, 5f);
        public static float PLAYER_ANGLE = 0f;
        public static float PLAYER_SPEED = int.Parse(PARSER.GetSetting("player", "player_speed"));
        public static float PLAYER_ROT_SPEED = int.Parse(PARSER.GetSetting("player", "player_rot_speed"));
        public static float PLAYER_SIZE_SCALE = 0.06f;
        public static int PLAYER_MAX_HEALTH = 100;

        public static float MOUSE_SENSITIVITY = float.Parse(PARSER.GetSetting("input", "mouse_sensitivity"), CultureInfo.InvariantCulture);
        public static int MOUSE_MAXIMUM_RELATIVE_MOVEMENT = 40;
        public static int MOUSE_BORDER_LEFT = 100;
        public static int MOUSE_BORDER_RIGHT = WIDTH - MOUSE_BORDER_LEFT;

        public static Color FLOOR_COLOR = new Color(30, 30, 30);

        public static float FOV = int.Parse(PARSER.GetSetting("render", "fov")) * MathF.PI / 180;
        public static float HALF_FOV = FOV / 2;
        public static int NUM_RAYS = WIDTH / 2;
        public static int HALF_NUM_RAYS = NUM_RAYS / 2;
        public static float DELTA_ANGLE = FOV / NUM_RAYS;
        public static int MAX_DEPTH = 20;

        public static float SCREEN_DIST = HALF_WIDTH / MathF.Tan(HALF_FOV);
        public static int SCALE = WIDTH / NUM_RAYS;

        public static int TEXTURE_SIZE = 296;
        public static int HALF_TEXTURE_SIZE = TEXTURE_SIZE / 2;

        public static int MAX_NPC_NO = int.Parse(PARSER.GetSetting("npc", "max_enemy_no"));
        public static bool ENABLE_NPC_AI = int.Parse(PARSER.GetSetting("npc", "enable_ai")) == 1;
        public static float IMP_SPAWN_CHANCE = float.Parse(PARSER.GetSetting("npc", "imp_spawn_chance"), CultureInfo.InvariantCulture) * 100;
        public static float MUMMY_SPAWN_CHANCE = float.Parse(PARSER.GetSetting("npc", "mummy_spawn_chance"), CultureInfo.InvariantCulture) * 100;
        public static float KNIGHT_SPAWN_CHANCE = float.Parse(PARSER.GetSetting("npc", "knight_spawn_chance"), CultureInfo.InvariantCulture) * 100;
        public static float MALOTUR_SPAWN_CHANCE = float.Parse(PARSER.GetSetting("npc", "malotaur_spawn_chance"), CultureInfo.InvariantCulture) * 100;
    }
}
