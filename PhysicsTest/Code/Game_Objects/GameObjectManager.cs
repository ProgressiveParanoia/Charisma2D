using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ParanoidGames.Charisma2D;
using ParanoidGames.Charisma2D.Utilities.IO;

namespace ParanoidGames.Charisma2D.Utilities
{
    class GameObjectManager : GameObject, IManager
    {
        #region singleton implementation
        private static GameObjectManager instance;
        public static GameObjectManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameObjectManager();
                }

                return instance;
            }
        }
        #endregion
        private List<GameObject> gameObjects;

        private ContentManager content;
        public void Initialize(ContentManager content)
        {
            this.content = content;
            this.gameObjects = new List<GameObject>();

            LevelBackground background = new LevelBackground(new Rectangle(0,0,800,600));
            gameObjects.Add(background);

        }

        public void LoadEnvironmentTextureData()
        {
            if(this.gameObjects == null)
            {
                return;
            }

            foreach (GameObject g in this.gameObjects)
            {
                if (g is LevelBackground == false)
                    continue;

                g.Initialize();

                SpriteLoader spriteLoader = FileHandler.Instance.GetSpriteLoader;

                foreach (KeyValuePair<string, Texture2D> backgroundData in spriteLoader.BackgroundTextureData)
                {
                    if (backgroundData.Key == g.TextureName)
                    {
                        g.Texture = backgroundData.Value;
                        break;
                    }
                }

                foreach (KeyValuePair<string, Texture2D> pickupTextureData in spriteLoader.PickupTextureData)
                {
                    if (pickupTextureData.Key == g.TextureName)
                    {
                        g.Texture = pickupTextureData.Value;
                        break;
                    }
                }

                foreach (KeyValuePair<string, Texture2D> propTextureData in spriteLoader.PropTextureData)
                {
                    if (propTextureData.Key == g.TextureName)
                    {
                        g.Texture = propTextureData.Value;
                        break;
                    }
                }

                foreach (KeyValuePair<string, Texture2D> platformTextureData in spriteLoader.PlatformTextureData)
                {
                    if (platformTextureData.Key == g.TextureName)
                    {
                        g.Texture = platformTextureData.Value;
                        break;
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (GameObject g in gameObjects)
            {
                g.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            foreach (GameObject g in gameObjects)
            {
                g.Draw(gameTime, spriteBatch);
            }
        }
    }
}
