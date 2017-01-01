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
        private Texture2D _enemyTexture;

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
            this._enemyRect = _enemyRect;
            this._enemyTexture = _enemyTexture;

            movingRight = true;
        }

        public Rectangle enemyRect
        {
            set { _enemyRect = value; }
            get { return _enemyRect; }
        }
        public Texture2D enemyTexture
        {
            set { _enemyTexture = value; }
            get { return _enemyTexture; }
        }
        public int SpriteSheetX
        {
            set { _SpriteSheetX = value; }
            get { return _SpriteSheetX; }
        }
        public int SpriteSheetY
        {
            set { _SpriteSheetY = value; }
            get { return _SpriteSheetY; }
        }

        public void snowmanAnimation()
        {
            if (movingRight)
            {
                if(_animationDelay > 10)
                {
                    if (_SpriteSheetX < 288)
                    {
                        _SpriteSheetX += 96;
                    }
                    else
                    {
                        _SpriteSheetX = 0;

                        if (_SpriteSheetY < 96)
                        {
                            _SpriteSheetY += 96;
                        }
                        else
                        {
                            _SpriteSheetY = 0;
                        }
                    }
                    _animationDelay = 0;
                }
            }
            _animationDelay++;
        }
        public void penguinAnimation()
        {

        }
    }
}
