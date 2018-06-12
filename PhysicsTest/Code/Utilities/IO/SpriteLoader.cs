using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ParanoidGames.Utilities.IO
{
    public enum SeasonType
    {
        Regular,
        Christmas,
        Halloween
    }



    class SpriteLoader : Loader
    {
        Dictionary<string, Texture2D> platformTextureData = new Dictionary<string, Texture2D>();

        public void LoadSprites()
        {
            ContentManager content = FileHandler.Instance.GetContentManager;

            if(content == null)
            {
                Console.WriteLine("[FILE HANDLER]No reference to content manager!");
                return;
            }
        
            string[] filePaths = GetFilePaths(@"Content\Sprites\Environment\Platform");

            if(filePaths == null)
            {
                Console.WriteLine("Null file path!");
                return;
            }

            foreach (string path in filePaths)
            {
                string texPath = path.Replace(".xnb", "").Replace(@"Content\", "");

                Texture2D tex2d = content.Load<Texture2D>(@texPath);
                platformTextureData.Add(tex2d.Name, tex2d);
            }
            
        }
    }
}
