using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParanoidGames.Charisma2D
{
    class GameObject : IGameObject
    {
        protected string name;
        protected Rectangle rect;
        protected Texture2D texture;
        protected Color color;

        public GameObject(Rectangle rect, string name)
        {
            this.rect = rect;
            this.name = name;
        }

        public GameObject()
        {

        }

        public virtual void Initialize()
        {
            color = Color.White;
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
           
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

            protected set
            {
                this.rect.Location = value;
            }
        }

        public bool SetActive
        {
            set { SetActive = value; }
            get { return SetActive; }
        }

        #endregion


    }
}
