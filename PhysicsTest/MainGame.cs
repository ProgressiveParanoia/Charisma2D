using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ParanoidGames.Charisma2D;
using ParanoidGames.Charisma2D.Utilities;
using ParanoidGames.Charisma2D.Utilities.Constants;
using ParanoidGames.Charisma2D.Utilities.IO;
using System;
using System.Collections.Generic;
using System.IO;

namespace ParanoidGames.BadElf
{
    class MainGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public MainGame()
        {
            this.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {
            //Find and load file directories
            FileDirectory.Initialize();
            //Initialize the content of file directories
            FileHandler.Instance.Initialize(this.Content);


            //TODO: LEVEL DATA INITIALIZATION

            //Instantiate and initialize all game objects for future references
            GameObjectManager.Instance.Initialize(this.Content);

            Console.WriteLine("Initialized");
            base.Initialize();
        }

        protected override void LoadContent()
        {
            //Load sprites found in directories
            FileHandler.Instance.GetSpriteLoader.LoadSprites();

            //Attach loaded sprite data to game objects
            GameObjectManager.Instance.LoadEnvironmentTextureData();

            Console.WriteLine("Load content");
            spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            
            GameObjectManager.Instance.Draw(gameTime, this.spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
