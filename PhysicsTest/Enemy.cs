using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsTest
{
    class Enemy
    {
        private Rectangle _enemyRect;
        private Texture2D _playerTexture;

        //Animation variables
        private int _SpriteSheetX;
        private int _SpriteSheetY;

        private int _animationDelay;

        private bool movingLeft;
        private bool movingRight;
        private bool idleRight;
        private bool idleLeft;
        //end animation vars

        public Enemy(Rectangle _enemyRect, Texture2D _enemyTexture)
        {

        }
    }
}
