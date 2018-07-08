using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace ParanoidGames.Charisma2D.Utilities.IO
{
    class FileHandler
    {
        #region XNA-Game Data Types/Objects
        private ContentManager contentManager;
        #endregion
        private SpriteLoader spriteLoader;
    
        #region singleton implementation
        private static FileHandler instance;

        public static FileHandler Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new FileHandler();
                }
                return instance;
            }
        }
        #endregion

        public void Initialize(ContentManager contentManager)
        {
            this.contentManager = contentManager; 
        }

        public SpriteLoader GetSpriteLoader
        {
            get
            {
                if(spriteLoader == null)
                {
                    this.spriteLoader = new SpriteLoader();
                }
               
                return this.spriteLoader;
            }
        }

        public ContentManager GetContentManager
        {
            get
            {
                return contentManager == null ? null : contentManager;
            }
        }
        
    }
}
