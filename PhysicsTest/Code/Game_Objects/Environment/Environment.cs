using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParanoidGames.Charisma2D
{
    class Environment : GameObject
    {
        public Environment(Rectangle rect, string name)
        {
            this.rect = rect;
            this.name = name;
        }
    }
}
