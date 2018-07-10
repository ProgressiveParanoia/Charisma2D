using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ParanoidGames.Charisma2D.Platformer
{
    class PlatformerPlayer : RigidBody
    {
        public PlatformerPlayer(Rectangle rect, string name) : base(rect, name)
        {
        }
    }
}
