using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParanoidGames.Charisma2D
{
    class GameObject : IGameObject
    {
        protected string textureName;
        protected Rectangle rect;
        protected Texture2D texture;
        protected Color color;

        public virtual void Initialize()
        {
            this.textureName = string.Empty;
            this.rect = Rectangle.Empty;
            this.texture = null;
            this.color = Color.White;
        }

        public virtual void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
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

        public string TextureName
        {
            get
            {
                return this.textureName;
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
