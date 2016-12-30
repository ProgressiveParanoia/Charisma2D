using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsTest
{
    class Blocks
    {
        private Rectangle _blockRect;
        private Texture2D _blockTexture;

        private bool isSlippery;

        public Blocks(Rectangle _blockRect, Texture2D _blockTexture)
        {
            this._blockRect = _blockRect;
            this._blockTexture = _blockTexture;
        }

        public Texture2D blockTexture
        {
            set
            {
                _blockTexture = value;
            }
            get
            {
                return _blockTexture;
            }
        }

        public Rectangle blockRect
        {
            set
            {
                _blockRect = value;
            }
            get
            {
                return _blockRect;
            }
        }

        public bool isSlipBlock
        {
            get { return isSlippery; }
            set { isSlippery = value; }
        }

        public void Move(int x, int y)
        {
            _blockRect.Location = new Point(x,y);
        }
    }
}
