using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsTest
{
    class Camera
    {
        private Matrix camTransform;
        private Viewport view;

        private Vector2 center;

        public Camera(Viewport myView)
        {
            view = myView;
        }

        public Vector2 Center
        {
            get { return Center; }
            set { center = value; }
        }

        public Viewport myView
        {
            get { return view; }
        }

        public Matrix Transform
        {
            get { return camTransform; }
        }

        public void setToCenter(Rectangle obj, Point bounds)
        {
            center = new Vector2(obj.X + (obj.Width/2) - bounds.X/2,0);

            camTransform = Matrix.CreateScale(new Vector3(1,1,0)) * Matrix.CreateTranslation(new Vector3(-center.X,-center.Y,0));
        }
    }
}
