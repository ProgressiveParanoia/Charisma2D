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

    public enum SpriteEnemyType
    {
        Undefined
    }


    class SpriteLoader : Loader
    {
        private ContentManager content = FileHandler.Instance.GetContentManager;

        private HashSet<Texture2D> platformTextureData = new HashSet<Texture2D>();
        private Dictionary<string, Texture2D> propTextureData = new Dictionary<string, Texture2D>();
        private Dictionary<string, Texture2D> PickupTextureData = new Dictionary<string, Texture2D>();
        private Dictionary<string, Texture2D> backgroundTextureData = new Dictionary<string, Texture2D>();


        private Dictionary<string, Texture2D> playerTextureData = new Dictionary<string, Texture2D>();


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

            foreach (string path in filePaths)
            {
                string texPath = path.Replace(".xnb", "").Replace(FileDirectory.Content, "");

                Texture2D tex2d = content.Load<Texture2D>(@texPath);

                texPath = texPath.Replace(FileDirectory.Environment_Platform, "");

                platformTextureData.Add(tex2d);
            }
            // CheckSpriteType()
            foreach (Texture2D platTex2d in platformTextureData)
            {
                Console.WriteLine("KEY LIST:"+platTex2d);
            }

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


            foreach (Texture2D texThing in platformTextureData)
            {
                Console.WriteLine("LOAD SPRITES 2:"+texThing);
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

                SpriteType currentSpriteType = SpriteType.Undefined;

                Console.WriteLine("Current data:" + texPath);

                Enum.TryParse(spriteTypeString, out currentSpriteType);

                switch (currentSpriteType)
                {
                    case SpriteType.Undefined:
                        Console.WriteLine("Unknown sprite value. Skipping...");
                        break;
                        
                    case SpriteType.Environment:
                        this.CheckSubtype(SpriteType.Environment, tex2d);
                        break;
                }
            }
        }

        private void CheckSubtype_Environment(Texture2D texture)
        {

        }

        private void CheckSubtype(Enum enumType, Texture2D texture)
        {

        }

    }
}
