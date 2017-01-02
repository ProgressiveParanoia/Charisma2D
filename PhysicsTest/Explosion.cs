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
    class Explosion
    {
        private Rectangle _explosionRect;
        private Texture2D _explosionTexture;

        //Animation variables

        private int _SpriteSheetX;
        private int _SpriteSheetY;

        private int _animationDelay;

        private bool _destroySprite;

        //end animation vars
        public Explosion(Rectangle _explosionRect, Texture2D _explosionTexture)
        {
            this._explosionRect = _explosionRect;
            this._explosionTexture = _explosionTexture;
        }

        public Rectangle explosionRect
        {
            get { return _explosionRect; }
            set { _explosionRect = value; }
        }

        public Texture2D explosionTexture
        {
            get { return _explosionTexture; }
            set { _explosionTexture = value; }
        }

        public bool destroySprite
        {
            get { return _destroySprite; }
        }

        public int SpriteSheetX
        {
            get { return _SpriteSheetX; }
        }

        public int SpriteSheetY
        {
            get { return _SpriteSheetY; }
        }

        public void Animation()
        {
            if(_animationDelay > 5)
            {
                if (_SpriteSheetX < 192)
                {
                    _SpriteSheetX += 96;
                }else
                {
                    _SpriteSheetX = 0;
                    
                    if(_SpriteSheetY < 192)
                    {
                        _SpriteSheetY += 96;
                    }else
                    {
                        _destroySprite = true;
                    }
                }
                _animationDelay = 0;
            }
            _animationDelay++;
        }

    }
}
