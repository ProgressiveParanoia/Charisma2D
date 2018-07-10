using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ParanoidGames.Charisma2D.Utilities;

namespace ParanoidGames.Charisma2D
{
    class GameObject : IGameObject
    {
        protected string name;
        protected Rectangle rect;
        protected Texture2D texture;
        protected Color color;

        public GameObject()
        {

        }

        public GameObject(Rectangle rect, string name)
        {
            this.rect = rect;
            this.name = name;
        }

        public virtual void Initialize()
        {
            color = Color.White;

            GameObjectManager.Instance.DrawAction += this.Draw;
            GameObjectManager.Instance.UpdateAction += this.Update;
            GameObjectManager.Instance.DestroyAction += this.Destroy;
        }

        public virtual void Destroy()
        {
            GameObjectManager.Instance.DrawAction -= this.Draw;
            GameObjectManager.Instance.UpdateAction -= this.Update;
            GameObjectManager.Instance.DestroyAction -= this.Destroy;
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rect, color);
        }

        #region properties 
        public Rectangle Rectangle
        {
            get
            {
                return this.rect;
            }
        }

        public Texture2D Texture
        {
            protected internal set
            {
                this.texture = value;
            }
            get
            {
                return this.texture;
            }
        }

        public Color Color
        {
            get
            {
                return this.color;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public Point Position
        {
            get
            {
                return this.rect.Location;
            }
        }

        public bool SetActive
        {
            set { SetActive = value; }
            get { return SetActive; }
        }

        #endregion

        public virtual void Move(int x, int y)
        {
            this.rect.Location = new Point(x,y);
        }

        #region Level editor methods
        
        #endregion
    }
}
