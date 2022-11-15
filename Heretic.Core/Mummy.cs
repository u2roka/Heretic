using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Heretic.Core
{
    internal class Mummy : NPC
    {
        public Mummy(ContentManager content, Sound sound, Player player, Map map, PathFinding pathFinding, ObjectHandler objectHandler, ObjectRenderer objectRenderer, Vector2 position) 
            : base(content, sound, player, map, pathFinding, objectHandler, objectRenderer, @"Mummy\MUMMA1", position, 0.25f, 0.08f, 1)
        {
            enemyType = EnemyType.MUMMY;

            attackDistance = 1;
            attackChance = 0.1f;
            speed = 0.03f;
            size = 10;
            health = 80;
            attackDamage = random.Next(2, 17);
            accuracy = 0.35f;
        }
    }
}
