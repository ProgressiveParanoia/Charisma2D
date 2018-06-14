using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using ParanoidGames.Constants;

namespace ParanoidGames.Utilities.IO
{
    public enum SeasonType
    {
        Regular,
        Christmas,
        Halloween
    }

    public enum SpriteType
    {
        Undefined,
        Enemy,
        Player,
        Environment,
        UI,
    }
    public enum SpriteEnvironmentType
    {
        Undefined,
        Background,
        Platform,
        Prop
    }

    public enum SpriteUIType
    {
        Undefined,
        Ingame,
        Menu
    }

    public enum SpriteEnemyType
    {
        Undefined
    }


    class SpriteLoader : Loader
    {
        private ContentManager content = FileHandler.Instance.GetContentManager;

        #region UI texture data
        private Dictionary<string, Texture2D> ingameTextureData = new Dictionary<string, Texture2D>();
        private Dictionary<string, Texture2D> menuTextureData = new Dictionary<string, Texture2D>();
        #endregion

        #region Environment texture data
        private Dictionary<string, Texture2D> platformTextureData = new Dictionary<string, Texture2D>();
        private Dictionary<string, Texture2D> propTextureData = new Dictionary<string, Texture2D>();
        private Dictionary<string, Texture2D> PickupTextureData = new Dictionary<string, Texture2D>();
        private Dictionary<string, Texture2D> backgroundTextureData = new Dictionary<string, Texture2D>();
        #endregion

        #region Player texture data
        private Dictionary<string, Texture2D> playerTextureData = new Dictionary<string, Texture2D>();
        #endregion

        public void LoadSprites()
        {
            if(content == null)
            {
                Console.WriteLine("[FILE HANDLER]No reference to content manager!");
                return;
            }

            #region Load Platform Surfaces

            string[] filePaths = GetFilePaths(FileDirectory.Environment_Platform);

            if(filePaths == null)
            {
                Console.WriteLine("Null file path!");
                return;
            }

            //foreach (string path in filePaths)
            //{
            //    string texPath = path.Replace(".xnb", "").Replace(FileDirectory.Content, "");

            //    Texture2D tex2d = content.Load<Texture2D>(@texPath);

            //    texPath = texPath.Replace(FileDirectory.Environment_Platform, "");

            //    platformTextureData.Add(tex2d);
            //}
            //// CheckSpriteType()
            //foreach (Texture2D platTex2d in platformTextureData)
            //{
            //    Console.WriteLine("KEY LIST:"+platTex2d);
            //}

            #endregion
        }

        public void RecursiveLoadSprites()
        {
            ContentManager content = FileHandler.Instance.GetContentManager;

            if (content == null)
            {
                Console.WriteLine("[FILE HANDLER]No reference to content manager!");
                return;
            }

            string[] filePaths = RecurseGetFilePaths(FileDirectory.Sprites_Environment);

            if (filePaths == null)
            {
                Console.WriteLine("Null file path!");
                return;
            }

            List<string> spriteDirectories = new List<string>();
            spriteDirectories = FileDirectory.GetSpriteDirectories;

            foreach (string spritePath in spriteDirectories)
            {
                this.CheckSpriteTypes(spritePath);
            }


            foreach (KeyValuePair<string,Texture2D> texThing in platformTextureData)
            {
                Console.WriteLine("PLATFORM DATA:"+texThing);
            }

            foreach (KeyValuePair<string, Texture2D> texThing in menuTextureData)
            {
                Console.WriteLine("menu data:" + texThing);
            }
        }

        private void CheckSpriteTypes(string spritePath)
        {
            string[] filePaths = RecurseGetFilePaths(spritePath);

            if (filePaths == null)
            {
                Console.WriteLine("Null file path!");
                return;
            }

            foreach (string path in filePaths)
            {
                string texPath = path.Replace(".xnb", "").Replace(FileDirectory.Content, "");
                Texture2D tex2d = content.Load<Texture2D>(@texPath);

                string[] splitFilePath = texPath.Split('\\');

                string spriteTypeString = splitFilePath[1];
                string spriteSubtypeString = splitFilePath[2];
                string spriteName = splitFilePath[splitFilePath.Length - 1];

                SpriteType currentSpriteType = SpriteType.Undefined;
    
                Console.WriteLine("Current data:" + texPath);

                Enum.TryParse(spriteTypeString, out currentSpriteType);
                
                switch (currentSpriteType)
                {
                    case SpriteType.Undefined:
                        Console.WriteLine("Unknown sprite value. Skipping...");
                        break;
                       
                    case SpriteType.Environment:
                        SpriteEnvironmentType spriteEnv = SpriteEnvironmentType.Undefined;
                        Enum.TryParse(spriteSubtypeString, out spriteEnv);

                        if (spriteEnv == SpriteEnvironmentType.Undefined)
                        {
                            break;
                        }

                        this.CheckSubtype_Environment(spriteEnv, spriteName, tex2d);
                        
                        break;

                    case SpriteType.UI:
                        SpriteUIType spriteUI = SpriteUIType.Undefined;
                        Enum.TryParse(spriteSubtypeString, out spriteUI);

                        if(spriteUI == SpriteUIType.Undefined)
                        {
                            break;
                        }

                        this.CheckSubtype_UI(spriteUI, spriteName, tex2d);

                        break;
                    

                }
            }
        }

        private void CheckSubtype_Environment(SpriteEnvironmentType spriteEnvironmentType, string key, Texture2D texture)
        {
            switch (spriteEnvironmentType)
            {
                case SpriteEnvironmentType.Background:
                    backgroundTextureData.Add(key, texture);
                    break;
                case SpriteEnvironmentType.Platform:
                    platformTextureData.Add(key,texture);
                    break;
                case SpriteEnvironmentType.Prop:
                    propTextureData.Add(key, texture);
                    break;
            }
        }

        private void CheckSubtype_UI(SpriteUIType spriteUIType, string key, Texture2D texture)
        {
            switch (spriteUIType)
            {
                case SpriteUIType.Ingame:
                    ingameTextureData.Add(key,texture);
                    break;
                case SpriteUIType.Menu:
                    menuTextureData.Add(key, texture);
                    break;
            }
        }
      
    }
}
