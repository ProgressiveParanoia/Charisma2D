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

        private bool attackRight;
        private bool attackLeft;
        //end animation vars

        //movement vars
        private int MaxLocationX; //max point an enemy can go to
        private int MinLocationX; // minimum point an enemy can go to

        private int MaxLocationY;
        private int MinLocationY;
        //end movement vars

        public Enemy(Rectangle _enemyRect, Texture2D _enemyTexture)
        {
            this._enemyRect = _enemyRect;
            this._enemyTexture = _enemyTexture;

            movingRight = true;

            MaxLocationX = _enemyRect.X + 50;
            MinLocationX = _enemyRect.X - 50;
   
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
                if(SpriteSheetY>= 192)
                {
                    SpriteSheetY = 0;
                }
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
            }else
                if (movingLeft)
                {
                    if(_animationDelay > 10)
                    {
                        if(_SpriteSheetX < 288)
                        {
                            _SpriteSheetX += 96;

                            if (SpriteSheetY < 192)
                            {
                                _SpriteSheetY = 192;
                            }
                        }
                        else
                        {
                            _SpriteSheetX = 0;
                            
                            if(_SpriteSheetY < 288)
                            {
                                _SpriteSheetY += 96;
                            }else
                            {
                                _SpriteSheetX = 0;
                                _SpriteSheetY = 192;
                            }
                        }
                    _animationDelay = 0;
                    }
                }

            if (attackRight)
            {
                if(_animationDelay > 10) //add additional variable to for attack delays
                {
                    if(_SpriteSheetX < 96)
                    {
                        _SpriteSheetX += 96;
                        
                    }else
                    {
                        _SpriteSheetX = 0;
                    }
                    _animationDelay = 0;
                }
                _SpriteSheetY = 384;
            }else
                if (attackLeft)
                {
                    if(SpriteSheetX < 192)
                    {
                        SpriteSheetX = 192;
                    }
                    if(_animationDelay > 10)
                    {
                        if(_SpriteSheetX < 288)
                        {
                            _SpriteSheetX += 96;
                        }else
                       {
                            _SpriteSheetX = 0;
                       }
                        _animationDelay = 0;
                    }
                    _SpriteSheetY = 384;
                }
            _animationDelay++;
        }

        public void snowmanMovement(Rectangle target)
        {
            int posX = _enemyRect.X;
            int posY = _enemyRect.Y;

            if (movingRight)
            {
                posX += 1;
                    if(_enemyRect.X >=MaxLocationX)
                {
                    movingRight = false;
                    movingLeft = true;
                }
            }
            if (movingLeft)
            {
                posX -= 1;
                if (_enemyRect.X <= MinLocationX)
                {
                    movingRight = true;
                    movingLeft = false;
                }
            }

            if (Math.Abs(target.X - _enemyRect.X) < 200) //if the X distance of an enemy is greater than 20. similar to unity's vector3.distance
            {
                 if(_enemyRect.X < target.X)
                {
                    attackRight = true;
                    attackLeft = false;

                    movingRight = false;
                    movingLeft = false;
                }
            }else
            {
                attackRight = false;
                attackLeft = true;
            }

            _enemyRect.Location = new Point(posX,posY);
            Console.WriteLine(Math.Abs(target.X - _enemyRect.X));
        }

        public void penguinAnimation()
        {

        }
    }
}
