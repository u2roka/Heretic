using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Heretic.Core
{
    internal class Knight : NPC
    {
        public Knight(ContentManager content, Sound sound, Player player, Map map, PathFinding pathFinding, ObjectHandler objectHandler, ObjectRenderer objectRenderer, Vector2 position) 
            : base(content, sound, player, map, pathFinding, objectHandler, objectRenderer, @"Knight\KNIGA1", position, 0.2f, 0.1f, 1)
        {
            enemyType = EnemyType.KNIGHT;

            attackDistance = 5;
            attackChance = 0.1f;
            speed = 0.05f;
            size = 10;
            health = 200;
            attackDamage = random.Next(3, 25);
            accuracy = 0.6f;
        }
    }
}
