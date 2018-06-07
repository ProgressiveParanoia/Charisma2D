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
    class PickUp
    {
        private Rectangle _pickUpRect;
        private Texture2D _pickUpTexture;
        
        public PickUp(Rectangle _pickUpRect, Texture2D _pickUpTexture)
        {
            this._pickUpRect = _pickUpRect;
            this._pickUpTexture = _pickUpTexture;
        } 

        public Rectangle pickUpRect
        {
            get { return _pickUpRect; }
            set { _pickUpRect = value; }
        }

        public Texture2D pickUpTexture
        {
            get { return _pickUpTexture; }
            set { _pickUpTexture = value; }
        }

        public void Move(int x, int y)
        {
            _pickUpRect.Location = new Point(x, y);
        }
    }
}
