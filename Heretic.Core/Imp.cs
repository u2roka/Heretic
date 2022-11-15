using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Heretic.Core
{
    internal class Imp : NPC
    {
        public Imp(ContentManager content, Sound sound, Player player, Map map, PathFinding pathFinding, ObjectHandler objectHandler, ObjectRenderer objectRenderer, Vector2 position) : 
            base(content, sound, player, map, pathFinding, objectHandler, objectRenderer, @"Imp\IMPXA1", position, 0.18f, 0.04f, 1)
        {
            enemyType = EnemyType.IMP;

            attackDistance = random.Next(3, 7);
            attackChance = 0.1f;
            speed = 0.04f;
            size = 10;
            health = 40;
            attackDamage = random.Next(5, 13);
            accuracy = 0.5f;
        }
    }
}
