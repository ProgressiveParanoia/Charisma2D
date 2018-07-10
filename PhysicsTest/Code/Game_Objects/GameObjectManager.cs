using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ParanoidGames.Charisma2D;
using ParanoidGames.Charisma2D.Utilities.IO;
using ParanoidGames.Charisma2D.Platformer;
using Microsoft.Xna.Framework.Input;

namespace ParanoidGames.Charisma2D.Utilities
{
    class GameObjectManager : IManager
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

        private List<GameObject> activeGameObjects = new List<GameObject>();
        private List<GameObject> inactiveGameObjects = new List<GameObject>();

        private ContentManager content;

        #region events
        public event Action<GameTime> UpdateAction;
        public event Action<GameTime, SpriteBatch> DrawAction;
        public event Action DestroyAction;
        #endregion

        public void Initialize(ContentManager content)
        {
            this.content = content;
            this.gameObjectPrefabs = new Dictionary<string, GameObject>();

            //check type of game..
            //check theme..

            //place game objects here...
            #region manual addition of basic prefabs
            LevelBackground background = new LevelBackground(new Rectangle(0,0,800,600), "GameBackground");
            gameObjectPrefabs.Add(background.Name, background);
            
            Block RegularBlock = new Block(new Rectangle(400, 300, 200, 50),"Platform_Regular_Christmas",PlatformerBlockType.Regular);
            gameObjectPrefabs.Add(RegularBlock.Name, RegularBlock);

            PlatformerPlayer player = new PlatformerPlayer(new Rectangle(450, 0, 32, 32), "LIFE");
            gameObjectPrefabs.Add(player.Name, player);
            #endregion
        }


        public void LoadEnvironmentTextureData()
        {
            if(this.gameObjectPrefabs == null)
            {
                return;
            }

            SpriteLoader spriteLoader = FileHandler.Instance.GetSpriteLoader;

            foreach (KeyValuePair<string, GameObject> g in this.gameObjectPrefabs)
            {
                //if (g.Value is Environment == false)
                //    continue;

                g.Value.Initialize();

                foreach (KeyValuePair<string,Texture2D> playerData in spriteLoader.PlayerTextureData)
                {
                    if (playerData.Key == g.Value.Name)
                    {
                        g.Value.Texture = playerData.Value;
                        break;
                    }
                }

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

        public void Update(GameTime gameTime)
        {
            if(this.UpdateAction != null)
            {
                this.UpdateAction(gameTime);
            }

            //test collision
            foreach (KeyValuePair<string, GameObject> obj in gameObjectPrefabs)
            {
                CollisionManager.Instance.Update(gameTime, obj.Value);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(this.DrawAction != null)
            {
                this.DrawAction(gameTime, spriteBatch);
            }
        }
    }
}
