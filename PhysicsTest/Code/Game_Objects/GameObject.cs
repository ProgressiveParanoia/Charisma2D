using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParanoidGames.Code
{
    class GameObject
    {
        internal Rectangle rect;
        internal Texture2D texture;
        internal Color color;
       
        public Rectangle Rectangle
        {
            get
            {
                return rect;
            }
        }

        public Texture2D Texture
        {
            get
            {
                return texture;
            }
        }

        public Color Color
        {
            get
            {
                return color;
            }
        }
       
        public Point Position
        {
            get
            {
                return rect.Location;
            }

            internal set
            {
                rect.Location = value;
            }
        }
    }
}
