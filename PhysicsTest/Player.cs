using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace PhysicsTest
{
    class Player
    {
        private Collision collision;
        private Projectile projectile;
        private SaveAndLoad saveAndLoad;

        private Rectangle _playerRect;
        private Texture2D _playerTexture;
        private Color _playerColor;
        private Point _bounds;


        private bool isJumping;
        private bool isGrounded;

        private bool isPressingJump;
        private bool isShooting;

        //physics stuff
        private bool slideLeft;
        private bool slideRight;

        private float _physicsTimer;
        private float _jumpTime;
        private float _slideTime;

        private Point _velocity;
        //end physics stuff

        //Animation variables
        private int _SpriteSheetX;
        private int _SpriteSheetY;

        private int _animationDelay;

        private bool movingLeft;
        private bool movingRight;
        private bool idleRight;
        private bool idleLeft;
        //end animation variables

        //Weapon Variables
        private int _ammoCounter;
        //end weapon vars

        public Player(Rectangle _playerRect, Texture2D _playerTexture, Color _playerColor, Point _bounds)
        {
            this._playerRect = _playerRect;
            this._playerTexture = _playerTexture;
            this._playerColor = _playerColor;
            this._bounds = _bounds;

            collision = new Collision();
            saveAndLoad = new SaveAndLoad();

            _jumpTime = 0.2f;
            _physicsTimer = 0.5f;
            _slideTime = 1f;

            _ammoCounter = 8;

            idleRight = true;
        }

        public Rectangle playerRect
        {
            set
            {
                _playerRect = value;
            }

            get
            {
                return _playerRect;
            }
        }

        public Texture2D playerTexture
        {
            set
            {
                _playerTexture = value;
            }
            get
            {
                return _playerTexture;
            }
        }

        public Color playerColor
        {
            get
            {
                return _playerColor;
            }
        }

        public float physicsTimer
        {
            set
            {
                _physicsTimer = value;
            }
            get
            {
                return _physicsTimer;
            }

        }

        public Point velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public int ammoCounter
        {
            get { return _ammoCounter; }
            set { _ammoCounter = value; }
        }

        public float jumpTime
        {
            set
            {
                _jumpTime = value;
            }
            get
            {
                return _jumpTime;
            }
        }

        public bool jumping
        {
            set { isJumping = value; }
            get
            {
                return isJumping;
            }
        }
        public bool grounded
        {
            set
            {
                isGrounded = value;
            }
            get{ return isGrounded; }
        }

        public int spriteSheetX
        {
            get { return _SpriteSheetX; }
        }

        public int spriteSheetY
        {
            get { return _SpriteSheetY; }
        }

        public bool shooting
        {
            get { return isShooting; }
            set { isShooting = value; }
        }

        public bool MoveLeft
        {
            get { return movingLeft; }
        }

        public bool IdlingLeft
        {
            get { return idleLeft; }
        }

        public bool MoveRight
        {
            get { return movingRight; }
        }

        public bool IdlingRight
        {
            get { return idleRight; }
        }

        public void Animation()
        {
            if (idleRight)
            {
                if (_animationDelay > 60)
                {
                    if (_SpriteSheetX < 192)
                    {
                        _SpriteSheetX += 192;
                    }
                    else
                    {
                        _SpriteSheetX = 0;
                        //_SpriteSheetY += 192;
                    }

                    _animationDelay = 0;
                }
            }else
             if (idleLeft)
            {
                _SpriteSheetY = 384;

                if (_animationDelay > 60)
                {
                    if (_SpriteSheetX < 192)
                    {
                        _SpriteSheetX += 192;
                    }
                    else
                    {
                        _SpriteSheetX = 0;

                    }

                    _animationDelay = 0;
                }
            }

            if (movingRight)
            {
                if (_animationDelay > 5)
                {
                    if (_SpriteSheetX < 576)
                    {
                        _SpriteSheetX += 192;
                    }
                    else
                    {
                        _SpriteSheetX = 0;

                        if (_SpriteSheetY < 192)
                        {
                            _SpriteSheetY += 192;
                        }
                        else
                        if(_SpriteSheetY == 192)
                        {
                            _SpriteSheetY = 0;
                            _SpriteSheetX = 384;
                        }
                    }

                    _animationDelay = 0;
                }
            }else
            if (movingLeft)
            {
                if (_animationDelay > 5)
                {
                    if (_SpriteSheetX < 576)
                    {
                        _SpriteSheetX += 192;
                    }
                    else
                    {
                        _SpriteSheetX = 0;

                        if (_SpriteSheetY < 576)
                        {
                            _SpriteSheetY += 192;
                        }
                        else
                        {
                            _SpriteSheetY = 384;
                            _SpriteSheetX = 384;
                        }
                    }

                    _animationDelay = 0;
                }
            }
            _animationDelay++;
        }

        public void move()
        {
            int posX = _playerRect.X;
            int posY = _playerRect.Y;

            if (!isJumping)
            {
                if (!isPressingJump && Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    isJumping = true;
                    isPressingJump = true;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                posX += 3;
                
                if (!movingRight)
                {
                    _SpriteSheetX = 384;
                    _SpriteSheetY = 0;

                    slideRight = true;
                    slideLeft = false;
                }

                movingRight = true;
                movingLeft = false;
                idleRight = false;
                idleLeft = false;

                _slideTime = 1f;


            }

            if (Keyboard.GetState().IsKeyUp(Keys.D) && movingRight && !idleRight && !movingLeft)
            {
                    idleRight = true;

                    _SpriteSheetX = 0;
                    _SpriteSheetY = 0;        
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                posX -= 3;

               
                if (!movingLeft)
                {
                    _SpriteSheetX = 384;
                    _SpriteSheetY = 384;

                    slideRight = false;
                    slideLeft = true;
                }

                movingRight = false;
                movingLeft = true;
                idleRight = false;
                idleLeft = false;

                _slideTime = 1f;
            }

            if (Keyboard.GetState().IsKeyUp(Keys.A) && !movingRight && !idleLeft && movingLeft)
            {

                idleLeft = true;

                _SpriteSheetX = 0;
                _SpriteSheetY = 384;

            }


            if (idleRight)
            {
                movingRight = false;
                movingLeft = false;
                idleLeft = false;
            }else
                if (idleLeft)
            {
                movingRight = false;
                movingLeft = false;
                idleRight = false;
            }
          
            
            _playerRect.Location = new Point(posX,posY);

            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                saveAndLoad.savePlayer(_playerRect.Location);
            }
        }

        public void DoPhysics()
        {

            physicsTimers();
      
            _playerRect.Y += _velocity.Y;
            _playerRect.X += _velocity.X;            

            if (isJumping)
            {
                _velocity.Y -= (int)1 * (int)1.5;

            }
            else
                if (!isJumping && playerRect.Y < _bounds.Y -50 )
            {
                    _velocity.Y += (int)1 * (int)1.5;
            }
            if (_playerRect.Y > _bounds.Y - 50)
            {
                _playerRect.Location = new Point(50, 0);
            }

            Console.WriteLine("left:" + slideLeft + "right:" + slideRight);

            _playerRect.Location = new Point(_playerRect.X, _playerRect.Y);
        }

        public void Colliders(Blocks block)
        {
         
                if (collision.TouchTopOf(_playerRect, block.blockRect))
                {
                    _velocity.Y = 0;

                    if (isJumping)
                    {
                        _velocity.Y -= (int)1 * (int)1.5;

                    }
                    else
                    {
                        isJumping = false;
                        isPressingJump = false;
                    }

                    if (Math.Abs(_playerRect.Y - block.blockRect.Y) < _playerRect.Height) //push player up if player rammed into the ground
                    {
                        _playerRect.Y -= 1;
                    }

                    if (block.isSlipBlock)
                    {
                        if (slideRight)
                        {
                            if (_velocity.X < 3 && _slideTime > 0)
                            {
                                _velocity.X += ((int)1 * (int)1.5);

                            }
                            else
                                if (_velocity.X > 0 && _slideTime <= 0)
                            {
                                _velocity.X -= ((int)1 * (int)1.5);
                            }

                            if (_velocity.X <= 0)
                            {
                                _velocity.X = 0;
                            }
                        _slideTime -= 1 / 60f;
                    }
                    else
                        if (slideLeft)
                          {
                              if (_velocity.X > -3 && _slideTime > 0)
                              {
                                  _velocity.X -= ((int)1 * (int)1.5);

                              }
                              else
                                 if (_velocity.X < 0 && _slideTime <= 0)
                              {
                                  _velocity.X += ((int)1 * (int)1.5);
                              }

                              if (_velocity.X >= 0)
                              {
                                  _velocity.X = 0;
                              }

                              _slideTime -= 1 / 60f;
                          }
                    }
                    else
                    {
                        _velocity.X = 0;

                    }
                }

            if (collision.TouchBottomOf(_playerRect, block.blockRect))
            {
                _velocity.Y += -_velocity.Y / 2;
            }

          
            if (block.blockRect.Intersects(_playerRect))
            {
                if (block.blockRect.Left > _playerRect.Left)
                {
                   if (block.blockRect.Top != _playerRect.Bottom - 1)
                    {
                        _playerRect.X -= 3;
                    }   
                }
                if (block.blockRect.Right < _playerRect.Right)
                {
                   if (block.blockRect.Top != _playerRect.Bottom - 1)
                    {
                        _playerRect.X += 3;
                    }
                }
            }
       
        }

        private void physicsTimers()
        {
            if (physicsTimer < 2f)
                physicsTimer += 1 / 60f;
            else
                physicsTimer = 0.5f;

            if (jumping)
            {
                if (jumpTime > 0)
                {
                    jumpTime -= 1 / 60f;
                }
                else
                {
                    isJumping = false;
                    jumpTime = 0.2f;
                }
            }
        }
    }
}
