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
        private bool isPointy;

        public Blocks(Rectangle _blockRect, Texture2D _blockTexture)
        {
            this._blockRect = _blockRect;
            this._blockTexture = _blockTexture;
        }

        public Texture2D BlockTexture
        {
            get {  return _blockTexture; }
        }

        public Rectangle BlockRect
        {
            get { return _blockRect; }
        }

        public Point BlockPos
        {
            get { return _blockRect.Location; }
        }

        public bool isSlipBlock
        {
            get { return isSlippery; }
            set { isSlippery = value; }
        }

        public bool isSpikeBlock
        {
            get { return isPointy; }
            set { isPointy = value; }
        }
        
        
        public virtual void Move(int x, int y)
        {
            _blockRect.Location = new Point(x,y);
        }

    }
}
