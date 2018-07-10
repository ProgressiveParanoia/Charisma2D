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

        private bool spawnProjectile;
        //end animation vars

        //movement vars
        private int MaxLocationX; //max point an enemy can go to
        private int MinLocationX; // minimum point an enemy can go to

        private int MaxLocationY;
        private int MinLocationY;

        private float attackDelay;
        //end movement vars

        public Enemy(Rectangle _enemyRect, Texture2D _enemyTexture)
        {
            this._enemyRect = _enemyRect;
            this._enemyTexture = _enemyTexture;

            movingRight = true;

            MaxLocationX = _enemyRect.X + 50;
            MinLocationX = _enemyRect.X - 50;

            attackDelay = 0.5f;
   
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
        public bool spawnSnowBall
        {
            set { spawnProjectile = value; }
            get { return spawnProjectile; }
        }
        public bool attackingLeft
        {
            get { return attackLeft; }
        }
        public bool attackingRight
        {
            get { return attackRight; }
        }
        public Point enemyPos
        {
            get { return _enemyRect.Location; }
            set { _enemyRect.Location = value; }
        }
        public void snowmanAnimation()
        {
            if (movingRight)
            {
                if(SpriteSheetY>= 192)
                {
                    SpriteSheetY = 0;
                }
                if(_animationDelay > 20)
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
                if(_animationDelay > 20) //add additional variable to for attack delays
                {
                    if(_SpriteSheetX < 96)
                    {
                        _SpriteSheetX += 96;
                        spawnSnowBall = true;
                        
                    }else
                    {
                        _SpriteSheetX = 0;
                        spawnSnowBall = false;
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
                    if(_animationDelay > 20)
                    {
                        if(_SpriteSheetX < 288)
                        {
                            _SpriteSheetX += 96;
                            spawnSnowBall = true;
                        }
                        else
                       {
                            _SpriteSheetX = 0;
                            spawnSnowBall = false;
                       }
                        _animationDelay = 0;
                    }
                    _SpriteSheetY = 384;
                }
            _animationDelay++;
        }

        public void snowmanMovement(Player_1 target)
        {
            int posX = _enemyRect.X;
            int posY = _enemyRect.Y;

                if (movingRight)
                {
                    posX += 1;
                    if (_enemyRect.X >= MaxLocationX)
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

            if (target.PlayerLife != 0)
            {
                if (Math.Abs(target.playerRect.X - _enemyRect.X) < 220 && Math.Abs(target.playerRect.Y - _enemyRect.Y) < 120) //if the X distance of an enemy is greater than 20. similar to unity's vector3.distance
                {
                    if (_enemyRect.X < target.playerRect.X)
                    {
                        attackRight = true;
                        attackLeft = false;

                        movingRight = false;
                        movingLeft = false;
                    }

                    if (_enemyRect.X > target.playerRect.X)
                    {
                        attackRight = false;
                        attackLeft = true;

                        movingRight = false;
                        movingLeft = false;
                    }

                }
                else
                {
                    if (!movingLeft && !movingRight)
                    {
                        movingLeft = true;
                        attackRight = false;
                        attackLeft = false;
                    }
                }
            }
            else
            {
                attackLeft = false;
                attackRight = false;

                movingLeft = true;
            }

            _enemyRect.Location = new Point(posX,posY);
        }

        public void penguinAnimation()
        {

        }

        public void Move(int x, int y)
        {
            _enemyRect.Location = new Point(x, y);
        }
    }
}
