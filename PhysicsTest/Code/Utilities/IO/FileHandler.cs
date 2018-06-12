using System;
using System.IO;
using System.Collections.Generic;


namespace ParanoidGames.Utilities.IO
{
    class FileHandler
    {
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

        public SpriteLoader GetSpriteLoader
        {
            get
            {
                if(spriteLoader == null)
                {
                    spriteLoader = new SpriteLoader();
                }
                return spriteLoader;
            }
        }
        
    }
}
