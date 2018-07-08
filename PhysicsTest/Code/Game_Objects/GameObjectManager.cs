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
        /// <summary>
        /// Keeps a dictionary of game object references for instantiation.
        /// </summary>
        private Dictionary<string, GameObject> gameObjectPrefabs = new Dictionary<string, GameObject>();
        
        private ContentManager content;

        public void Initialize(ContentManager content)
        {
            this.content = content;
            this.gameObjectPrefabs = new Dictionary<string, GameObject>();

            //place game objects here...
            LevelBackground background = new LevelBackground(new Rectangle(0,0,800,600), "GameBackground");
            gameObjectPrefabs.Add(background.Name, background);

        }

        public void LoadEnvironmentTextureData()
        {
            if(this.gameObjectPrefabs == null)
            {
                return;
            }

            foreach (KeyValuePair<string, GameObject> g in this.gameObjectPrefabs)
            {
                if (g.Value is Environment == false)
                    continue;

                g.Value.Initialize();

                SpriteLoader spriteLoader = FileHandler.Instance.GetSpriteLoader;

                foreach (KeyValuePair<string, Texture2D> backgroundData in spriteLoader.BackgroundTextureData)
                {
                    if (backgroundData.Key == g.Value.Name)
                    {
                        g.Value.Texture = backgroundData.Value;
                        break;
                    }
                }

                foreach (KeyValuePair<string, Texture2D> pickupTextureData in spriteLoader.PickupTextureData)
                {
                    if (pickupTextureData.Key == g.Value.Name)
                    {
                        g.Value.Texture = pickupTextureData.Value;
                        break;
                    }
                }

                foreach (KeyValuePair<string, Texture2D> propTextureData in spriteLoader.PropTextureData)
                {
                    if (propTextureData.Key == g.Value.Name)
                    {
                        g.Value.Texture = propTextureData.Value;
                        break;
                    }
                }

                foreach (KeyValuePair<string, Texture2D> platformTextureData in spriteLoader.PlatformTextureData)
                {
                    if (platformTextureData.Key == g.Value.Name)
                    {
                        g.Value.Texture = platformTextureData.Value;
                        break;
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (KeyValuePair<string, GameObject> g in gameObjectPrefabs)
            {
                g.Value.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            foreach (KeyValuePair<string, GameObject> g in gameObjectPrefabs)
            {
                g.Value.Draw(gameTime, spriteBatch);
            }
        }
    }
}
