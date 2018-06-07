using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsTest
{
    class Projectile
    {
        private Rectangle _projectileRect;
        private Texture2D _projectileTexture;
        private Color _projectileColor;

        private int dirX;
        private int dirY;

        private float Timer;

        public Projectile(Rectangle _projectileRect, Texture2D _projectileTexture, Color _projectileColor, int dirX, int dirY,float Timer)
        {
            this._projectileRect = _projectileRect;
            this._projectileTexture = _projectileTexture;
            this._projectileColor = _projectileColor;

            this.dirX = dirX;
            this.dirY = dirY;

            this.Timer = Timer;
        }

        public Rectangle projectileRect
        {
            get { return _projectileRect; }
            set { _projectileRect = value; }
        }
        public Texture2D projectileTexture
        {
            get { return _projectileTexture; }
            set { _projectileTexture = value; }
        }
        public Color projectileColor
        {
            get { return _projectileColor; }
            set { _projectileColor = value; }
        }
        public float lifeTime
        {
            get { return Timer; }
            set { Timer = value; }
        }

        public void Shoot()
        {
            _projectileRect.Location = new Point(_projectileRect.X + dirX,_projectileRect.Y+dirY);
        }

       public void setTargetLoc(Rectangle target)
        {
         
        }

        public bool isAlive()
        {
            Timer-= 1 / 60f;

            if (Timer <= 0)
            {
                return false;
            }
            else
                return true;
        }
    }
}
