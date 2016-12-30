using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsTest
{
    class LevelEditor
    {
        private Rectangle _editorRect;
        private Texture2D _editorTexture;
         
        public LevelEditor(Rectangle _editorRect, Texture2D _editorTexture)
        {
            this._editorRect = _editorRect;
            this._editorTexture = _editorTexture;
        }

        public Rectangle editorRect
        {
            get { return _editorRect; }
            set { _editorRect = value; }
        }

        public Texture2D editorTexture
        {
            get { return _editorTexture; }
            set { _editorTexture = value; }
        }
    }
}
