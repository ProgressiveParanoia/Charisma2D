using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParanoidGames.Charisma2D
{
    interface ICollider
    {
        void CollisionUpdate(GameObject collidingObject);
    }
}
