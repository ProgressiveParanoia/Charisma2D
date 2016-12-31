using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace PhysicsTest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player _player;
        Camera cam;

        Blocks LevelEditor_RegularBlock;
        Blocks LevelEditor_SlipBlock;

        int RegularBlockSize;

        LevelEditor editor;

        List<Blocks> RegularBlockList;
        List<Blocks> SkateBlockList;

        List<Player> playerList;

        List<Projectile> projectiles;

        Texture2D shotgunPellet;

        Texture2D playerTex;
        Texture2D blockTex;
        Texture2D skateBlockTex;

        SpriteFont sF;

        bool playMode;
        bool devMode;


        //blocks player has
        bool levelEditor_IsRegBlock;
        bool levelEditor_IsSlipBlock;

        bool levelEditor_IsSpikeBlock;
        bool levelEditor_IsJumpBlock;
        //end blocktypes


        //key inputs
        bool swappingModes;
        bool swappingBlocks;

        bool isPlacingBlock;
        bool isRemovingBlock;

        bool isLoading;
        //Input end
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            devMode = false;
            playMode = true;

            levelEditor_IsRegBlock = true;
        }
        protected override void Initialize()
        {
            playerTex = Content.Load<Texture2D>(@"Elf_shotty_sheet");
            blockTex = Content.Load<Texture2D>(@"BlockTest");
            skateBlockTex = Content.Load<Texture2D>(@"skateBlocks");

            shotgunPellet = Content.Load<Texture2D>(@"enemyProjectile");

            sF = Content.Load<SpriteFont>(@"spriteFont");

            _player = new Player(new Rectangle(0,0,64,64),playerTex,Color.White,new Point(Window.ClientBounds.Width,Window.ClientBounds.Height));
            cam = new Camera(GraphicsDevice.Viewport);

            LevelEditor_RegularBlock = new Blocks(new Rectangle(0,0,200,50),blockTex);
            LevelEditor_SlipBlock = new Blocks(new Rectangle(0,0,200,50),skateBlockTex);

            Texture2D editorTex = Content.Load<Texture2D>(@"editorBackdrop");

     
            RegularBlockList = new List<Blocks>();
            SkateBlockList = new List<Blocks>();
            projectiles = new List<Projectile>();
            playerList = new List<Player>();

           
            for(int i = 0; i < 15; i++)
            {
                if (i < 3)
                {
                    Blocks b = new Blocks(new Rectangle(-100 + (i * 100), Window.ClientBounds.Height / 2, 200, 50), blockTex);
                    RegularBlockList.Add(b);
                }else
                if(i<6 && i >3)
                {
                    Blocks b = new Blocks(new Rectangle(-100 + (i * 10), Window.ClientBounds.Height / 2- 60, 200, 50), blockTex);
                    RegularBlockList.Add(b);
                }
                else
                if(i<10 && i >6)
                {
                    Blocks b = new Blocks(new Rectangle(-400 + (i * 105), Window.ClientBounds.Height / 2 - 60, 200, 50), blockTex);
                    RegularBlockList.Add(b);
                }else
                {
                    Blocks sb = new Blocks(new Rectangle(500 + (i * 100), Window.ClientBounds.Height / 2 - 60, 200, 50), skateBlockTex);
                    sb.isSlipBlock = true;
                    SkateBlockList.Add(sb);
                }
                    
            }


            playerList.Add(_player);

            base.Initialize();
        }

   
        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);

        }

            
        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            LevelEditor();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //player related methods
           
            foreach (Player pl in playerList) {
                pl.move(devMode);

                if (!devMode)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Space) && !pl.shooting)
                    {
                        if (pl.ammoCounter > 0)
                        {
                            if (pl.IdlingLeft || pl.MoveLeft)
                            {
                                //if player has shotgun do this                               
                                Projectile p1 = new Projectile(new Rectangle(pl.playerRect.X, pl.playerRect.Y + 16, 16, 16), shotgunPellet, Color.White, -10, 1);
                                Projectile p2 = new Projectile(new Rectangle(pl.playerRect.X, pl.playerRect.Y + 16, 16, 16), shotgunPellet, Color.White, -10, 0);
                                Projectile p3 = new Projectile(new Rectangle(pl.playerRect.X, pl.playerRect.Y + 16, 16, 16), shotgunPellet, Color.White, -10, -1);

                                projectiles.Add(p1);
                                projectiles.Add(p2);
                                projectiles.Add(p3);
                                //end shotgun condition
                            }
                            if (pl.IdlingRight || pl.MoveRight)
                            {
                                //if player has shotgun do this
                                Projectile p1 = new Projectile(new Rectangle(pl.playerRect.X + pl.playerRect.Width, pl.playerRect.Y + 16, 16, 16), shotgunPellet, Color.White, 10, 1);
                                Projectile p2 = new Projectile(new Rectangle(pl.playerRect.X + pl.playerRect.Width, pl.playerRect.Y + 16, 16, 16), shotgunPellet, Color.White, 10, 0);
                                Projectile p3 = new Projectile(new Rectangle(pl.playerRect.X + pl.playerRect.Width, pl.playerRect.Y + 16, 16, 16), shotgunPellet, Color.White, 10, -1);

                                projectiles.Add(p1);
                                projectiles.Add(p2);
                                projectiles.Add(p3);
                                //end shotgun condition
                            }
                            pl.shooting = true;
                        }
                    }
                    else
                        if (Keyboard.GetState().IsKeyUp(Keys.Space))
                    {
                        pl.shooting = false;
                    }

                    pl.DoPhysics();

                    foreach (Blocks b in RegularBlockList)
                    {
                        pl.Colliders(b);
                    }

                    foreach (Blocks sb in SkateBlockList)
                    {
                        pl.Colliders(sb);
                    }
                }
                pl.Animation();

                cam.setToCenter(pl.playerRect, new Point(Window.ClientBounds.Width, Window.ClientBounds.Height));
            }
                //save and load inputs
                if (Keyboard.GetState().IsKeyDown(Keys.F))
                {
                    savePlayer();
                }

                if (Keyboard.GetState().IsKeyDown(Keys.L) && !isLoading)
                {
                    loadPlayer();
                    isLoading = true;
                }

                if (Keyboard.GetState().IsKeyUp(Keys.L))
                {
                    isLoading = false;
                }

            if (Keyboard.GetState().IsKeyDown(Keys.T) && !swappingModes)
            {
                if (playMode)
                {
                    playMode = false;
                    devMode = true;
                }
                else
                    if (devMode)
                {
                    devMode = false;
                    playMode = true;
                }
                swappingModes = true;
            }

            if (Keyboard.GetState().IsKeyUp(Keys.T))
            {
                swappingModes = false;
            }

            //end save and load

            //end player methods

            //projectile stuff

            foreach (Projectile p in projectiles)
            {
                p.shotgunShoot();
            }

            foreach (Projectile p in projectiles)
            {
                if (!p.isAlive())
                {
                    projectiles.Remove(p);
                    break;
                }
                foreach (Blocks b in RegularBlockList)
                {
                    if (p.projectileRect.Intersects(b.blockRect))
                    {
                        projectiles.Remove(p);
                        goto here;
                    }
                }
            }

            here:
            //end projectiles

            //camera related methods

            //new Point(_player.playerRect.X + (_player.playerRect.Width/2),);

            //end camera

            RegularBlockSize = RegularBlockList.Count - 1;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,null,null,null,null,cam.Transform);
            //spriteBatch.Draw(_player.playerTexture, _player.playerRect, new Rectangle(_player.spriteSheetX, _player.spriteSheetY, 192, 192), _player.playerColor);


                foreach (Player p in playerList)
                {
                    spriteBatch.Draw(p.playerTexture,p.playerRect, new Rectangle(p.spriteSheetX, p.spriteSheetY, 192, 192), p.playerColor);

                    spriteBatch.DrawString(sF, "Player X:" + p.playerRect.X + " Y:" + p.playerRect.Y, new Vector2(p.playerRect.X - (cam.myView.Bounds.Right / 2 - p.playerRect.Width / 2), 0), Color.White);
                    spriteBatch.DrawString(sF, "Editorblock X:" + LevelEditor_RegularBlock.blockRect.X + " Y:" + LevelEditor_RegularBlock.blockRect.Y, new Vector2(p.playerRect.X - (cam.myView.Bounds.Right / 2 - p.playerRect.Width / 2), 20), Color.White);
                    spriteBatch.DrawString(sF, "[F]Save [L]Load [T]Level Editor",new Vector2(p.playerRect.X - (cam.myView.Bounds.Right / 2 - p.playerRect.Width / 2), 40),Color.White);
                    

                    if (devMode)
                    {
                        spriteBatch.DrawString(sF, "[1] Regular block [2] sliding blocks", new Vector2(p.playerRect.X - (cam.myView.Bounds.Right / 2 - p.playerRect.Width / 2), 60), Color.White);
                        spriteBatch.DrawString(sF, "[Space]Place Block [B]Remove Block", new Vector2(p.playerRect.X - (cam.myView.Bounds.Right / 2 - p.playerRect.Width / 2), 1000), Color.White);
                }
                    
                    //  new Rectangle(0, 0, Window.ClientBounds.Width / 4, Window.ClientBounds.Height),editorTex
                }
                foreach (Blocks b in RegularBlockList)
                {
                    spriteBatch.Draw(b.blockTexture,b.blockRect,Color.White);
                }

                foreach(Blocks sb in SkateBlockList)
                {
                    spriteBatch.Draw(sb.blockTexture, sb.blockRect, Color.White);
                }

                foreach(Projectile p in projectiles)
                {
                    spriteBatch.Draw(p.projectileTexture,p.projectileRect,p.projectileColor);
                }

            //dev mode stuff
            if (devMode) {
                if(levelEditor_IsRegBlock)
                    spriteBatch.Draw(LevelEditor_RegularBlock.blockTexture, LevelEditor_RegularBlock.blockRect, Color.White);
                if(levelEditor_IsSlipBlock)
                    spriteBatch.Draw(LevelEditor_SlipBlock.blockTexture, LevelEditor_SlipBlock.blockRect, Color.White);
            }
            //end
              
            spriteBatch.End();

            base.Draw(gameTime);
        }


        //saving and loading methods

        void LevelSave()
        {
            StreamWriter sw = new StreamWriter("Stage1.SWAG");

            sw.WriteLine(playerList[0].playerRect.X+","+playerList[0].playerRect.Y); //save player pos

            sw.WriteLine("");
            foreach (Blocks b in RegularBlockList)
            {
                sw.WriteLine(b.blockRect.X+","+b.blockRect.Y);
            }
            sw.WriteLine("");

            sw.WriteLine(RegularBlockSize);
            sw.WriteLine("");

            sw.Close();
        }

        void savePlayer()
        {
            LevelSave();
        }

        void loadPlayer()
        {
            StreamReader sr = new StreamReader("Stage1.SWAG");
            string spaceEater;

            for(int i = RegularBlockSize; i >0; i--)
            {
                RegularBlockList.Remove(RegularBlockList[i]);
            }
            RegularBlockList.Remove(RegularBlockList[0]);

            playerList.Remove(playerList[0]);            

            string fileData = "";

            while ((fileData=sr.ReadLine())!=null)
            {
                string[] PosData = fileData.Split(',');

                if (fileData == "")
                {
                    break;
                }

                int posX = int.Parse(PosData[0]);
                int posY = int.Parse(PosData[1]);

               
                Player P = new Player(new Rectangle(posX, posY, 64, 64), playerTex, Color.White, new Point(Window.ClientBounds.Width, Window.ClientBounds.Height));
                playerList.Add(P);
            }

            spaceEater = fileData;

            while ((fileData = sr.ReadLine()) != null)
            {
                string[] PosData = fileData.Split(',');

                if (fileData == "")
                {
                    break;
                }

                int posX = int.Parse(PosData[0]);
                int posY = int.Parse(PosData[1]);


       
                Blocks b = new Blocks(new Rectangle(posX,posY, 200, 50), blockTex);
                RegularBlockList.Add(b);
            }

            spaceEater = fileData;
            while ((fileData = sr.ReadLine()) != null)
            {
                if (fileData == "")
                    break;
                RegularBlockSize = int.Parse(fileData);

            }
            sr.Close();
        }
        //save and load finish

        void LevelEditor()
        {
            Console.WriteLine("Player Size:"+playerList.Count+"Regular blocks count:"+RegularBlockList.Count);
            
            if (devMode)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D1) && !swappingBlocks)
                {
                    levelEditor_IsRegBlock = true;
                    levelEditor_IsSlipBlock = false;

                    levelEditor_IsSpikeBlock = false;
                    //levelEditor_IsSpikeBlock = false;

                    swappingBlocks = true;
                }

                if (Keyboard.GetState().IsKeyUp(Keys.D1))
                {
                    swappingBlocks = false;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D2))
                {
                    levelEditor_IsRegBlock = false;
                    levelEditor_IsSlipBlock = true;

                    levelEditor_IsSpikeBlock = false;
                }

                //regular block
                    if (levelEditor_IsRegBlock)
                    {
                        //movement
                        if (Keyboard.GetState().IsKeyDown(Keys.Left))
                        {
                            LevelEditor_RegularBlock.Move(LevelEditor_RegularBlock.blockRect.X-3,LevelEditor_RegularBlock.blockRect.Y);
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Right))
                        {
                            LevelEditor_RegularBlock.Move(LevelEditor_RegularBlock.blockRect.X + 3, LevelEditor_RegularBlock.blockRect.Y);
                        }

                        if (Keyboard.GetState().IsKeyDown(Keys.Up))
                        {
                            LevelEditor_RegularBlock.Move(LevelEditor_RegularBlock.blockRect.X, LevelEditor_RegularBlock.blockRect.Y-3);
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Down))
                        {
                            LevelEditor_RegularBlock.Move(LevelEditor_RegularBlock.blockRect.X, LevelEditor_RegularBlock.blockRect.Y+3);
                        }
                        //move end

                        //input place
                    if (Keyboard.GetState().IsKeyDown(Keys.Space) && !isPlacingBlock)
                        {
                            Blocks b = new Blocks(new Rectangle(LevelEditor_RegularBlock.blockRect.Location.X, LevelEditor_RegularBlock.blockRect.Location.Y, 200, 50), blockTex);
                            RegularBlockList.Add(b);

                            isPlacingBlock = true;
                        }
                            if(Keyboard.GetState().IsKeyUp(Keys.Space))
                        {
                            isPlacingBlock = false;
                        }
                    //input place done

                    foreach (Blocks b in RegularBlockList)
                    {
                        
                            if (LevelEditor_RegularBlock.blockRect.Intersects(b.blockRect))
                            {
                                if (Keyboard.GetState().IsKeyDown(Keys.B) && !isRemovingBlock)
                                {
                                    RegularBlockList.Remove(b);
                                    isRemovingBlock = true;
                                    break;
                                }
                            Console.WriteLine("HAHAHA");
                            }
                    }

                    if (Keyboard.GetState().IsKeyUp(Keys.B))
                    {
                        isRemovingBlock = false;
                    }
                }//end reugular block
                else 
                    if (levelEditor_IsSlipBlock)
                    {
                        //movement
                        if (Keyboard.GetState().IsKeyDown(Keys.Left))
                        {
                            LevelEditor_SlipBlock.Move(LevelEditor_SlipBlock.blockRect.X - 3, LevelEditor_SlipBlock.blockRect.Y);
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Right))
                        {
                            LevelEditor_SlipBlock.Move(LevelEditor_SlipBlock.blockRect.X + 3, LevelEditor_SlipBlock.blockRect.Y);
                        }

                        if (Keyboard.GetState().IsKeyDown(Keys.Up))
                        {
                            LevelEditor_SlipBlock.Move(LevelEditor_SlipBlock.blockRect.X, LevelEditor_SlipBlock.blockRect.Y - 3);
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Down))
                        {
                            LevelEditor_SlipBlock.Move(LevelEditor_SlipBlock.blockRect.X, LevelEditor_SlipBlock.blockRect.Y + 3);
                        }
                        //move end

                        //input place
                        if (Keyboard.GetState().IsKeyDown(Keys.Space) && !isPlacingBlock)
                        {
                            Blocks b = new Blocks(new Rectangle(LevelEditor_SlipBlock.blockRect.Location.X, LevelEditor_SlipBlock.blockRect.Location.Y, 200, 50), skateBlockTex);
                            b.isSlipBlock = true;
                            SkateBlockList.Add(b);

                            isPlacingBlock = true;
                        }
                        if (Keyboard.GetState().IsKeyUp(Keys.Space))
                        {
                            isPlacingBlock = false;
                        }
                        //input place done

                        foreach (Blocks b in SkateBlockList)
                        {

                            if (LevelEditor_SlipBlock.blockRect.Intersects(b.blockRect))
                            {
                                if (Keyboard.GetState().IsKeyDown(Keys.B) && !isRemovingBlock)
                                {
                                    SkateBlockList.Remove(b);
                                    isRemovingBlock = true;
                                    break;
                                }
                                Console.WriteLine("HAHAHA");
                            }
                        }

                        if (Keyboard.GetState().IsKeyUp(Keys.B))
                        {
                            isRemovingBlock = false;
                        }
                    }


                IsMouseVisible = true;

            }
            else
            {
                foreach (Player p in playerList)
                {
                    if (levelEditor_IsRegBlock)
                    {
                        LevelEditor_RegularBlock.Move(p.playerRect.Location.X, p.playerRect.Location.Y);
                    }else
                        if (levelEditor_IsSlipBlock)
                    {
                        LevelEditor_SlipBlock.Move(p.playerRect.Location.X, p.playerRect.Location.Y);
                    }
                }
             //   LevelEditor_RegularBlock.Move(Mouse.GetState().X - (LevelEditor_RegularBlock.blockRect.Width * 2 + LevelEditor_RegularBlock.blockRect.Width / 3), Mouse.GetState().Y - LevelEditor_RegularBlock.blockRect.Height / 2);
                IsMouseVisible = false;
            }
        }
    }
}
