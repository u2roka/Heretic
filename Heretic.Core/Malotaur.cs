using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Heretic.Core
{
    internal class Malotaur : NPC
    {
        public Malotaur(ContentManager content, Sound sound, Player player, Map map, PathFinding pathFinding, ObjectHandler objectHandler, ObjectRenderer objectRenderer, Vector2 position) 
            : base(content, sound, player, map, pathFinding, objectHandler, objectRenderer, @"Malotaur\MNTRA1", position, 0.21f, 0.09f, 1)
        {   
            enemyType = EnemyType.MALOTAUR;

            attackDistance = 6;
            attackChance = 0.1f;
            speed = 0.03f;
            size = 10;
            health = 350;
            attackDamage = random.Next(4, 33);
            accuracy = 0.55f;
        }
    }
}
