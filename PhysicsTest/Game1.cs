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

        Blocks LevelEditor_spikeBlock;

        Enemy LevelEditor_snowmen;

        //Blocks LevelEditor_

        int RegularBlockSize;
        int SlipBlockSize;
        int SpikeBlockSize;

        LevelEditor editor;

        List<Blocks> RegularBlockList;
        List<Blocks> SkateBlockList;

        List<Blocks> spikeBlockList;

        List<Player> playerList;

        List<Enemy> snowmenList;
        List<Enemy> penguinList;

        List<Projectile> projectiles;
        List<Projectile> snowBalls;

        Texture2D shotgunPellet;
            
        Texture2D playerTex;
        Texture2D blockTex;
        Texture2D skateBlockTex;
        Texture2D spikeBlockTex;

        Texture2D snowmenTex;
        Texture2D snowBallTex;

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
            spikeBlockTex = Content.Load<Texture2D>(@"icicleDown");

            shotgunPellet = Content.Load<Texture2D>(@"shotgunPellet");

            snowmenTex = Content.Load<Texture2D>(@"snowman_sheet");
            snowBallTex = Content.Load<Texture2D>(@"snowBall");

            sF = Content.Load<SpriteFont>(@"spriteFont");

            _player = new Player(new Rectangle(0,0,64,64),playerTex,Color.White,new Point(Window.ClientBounds.Width,Window.ClientBounds.Height));
            cam = new Camera(GraphicsDevice.Viewport);

            LevelEditor_RegularBlock = new Blocks(new Rectangle(0,0,200,50),blockTex);
            LevelEditor_SlipBlock = new Blocks(new Rectangle(0,0,200,50),skateBlockTex);

            LevelEditor_spikeBlock = new Blocks(new Rectangle(0,0,16,16),spikeBlockTex);

            LevelEditor_snowmen = new Enemy((new Rectangle(0,0,64,64)),snowmenTex);

            Texture2D editorTex = Content.Load<Texture2D>(@"editorBackdrop");

            RegularBlockList = new List<Blocks>();
            SkateBlockList = new List<Blocks>();
            spikeBlockList = new List<Blocks>();

            snowmenList = new List<Enemy>();
            penguinList = new List<Enemy>();
            snowBalls = new List<Projectile>();

            projectiles = new List<Projectile>();
            playerList = new List<Player>();

            playerList.Add(_player);


            loadPlayer();
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
                                Projectile p1 = new Projectile(new Rectangle(pl.playerRect.X, pl.playerRect.Y + 16, 16, 16), shotgunPellet, Color.White, -10, 1, 0.3f);
                                Projectile p2 = new Projectile(new Rectangle(pl.playerRect.X, pl.playerRect.Y + 16, 16, 16), shotgunPellet, Color.White, -10, 0, 0.3f);
                                Projectile p3 = new Projectile(new Rectangle(pl.playerRect.X, pl.playerRect.Y + 16, 16, 16), shotgunPellet, Color.White, -10, -1, 0.3f);

                               // p1.lifeTime = 0.5f;

                                projectiles.Add(p1);
                                projectiles.Add(p2);
                                projectiles.Add(p3);
                                //end shotgun condition
                            }
                            if (pl.IdlingRight || pl.MoveRight)
                            {
                                //if player has shotgun do this
                                Projectile p1 = new Projectile(new Rectangle(pl.playerRect.X + pl.playerRect.Width, pl.playerRect.Y + 16, 16, 16), shotgunPellet, Color.White, 10, 1, 0.3f);
                                Projectile p2 = new Projectile(new Rectangle(pl.playerRect.X + pl.playerRect.Width, pl.playerRect.Y + 16, 16, 16), shotgunPellet, Color.White, 10, 0, 0.3f);
                                Projectile p3 = new Projectile(new Rectangle(pl.playerRect.X + pl.playerRect.Width, pl.playerRect.Y + 16, 16, 16), shotgunPellet, Color.White, 10, -1, 0.3f);

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

                    foreach(Blocks sb in spikeBlockList)
                    {
                        pl.Colliders(sb);
                    }
                }
                pl.Animation();

                //camera stuff
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
                p.Shoot();
            }

            foreach (Projectile p in snowBalls)
            {
                p.Shoot();
            }

                //projectile cleanup
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
                foreach (Projectile p in snowBalls)
                {
                    if (!p.isAlive())
                    {
                        snowBalls.Remove(p);
                        break;
                    }
                }

                //end cleaning
            //end projectiles


            //enemy related stuff
            LevelEditor_snowmen.snowmanMovement(playerList[0].playerRect);
            LevelEditor_snowmen.snowmanAnimation();

            if (LevelEditor_snowmen.spawnSnowBall)
            {
                if (LevelEditor_snowmen.attackingRight)
                {
                    Projectile p = new Projectile(new Rectangle(LevelEditor_snowmen.enemyRect.X + LevelEditor_snowmen.enemyRect.Width, LevelEditor_snowmen.enemyRect.Y + 16,16,16),snowBallTex,Color.White,5,0, 1f);
                    snowBalls.Add(p);
                    LevelEditor_snowmen.spawnSnowBall = false;
                    // Projectile p3 = new Projectile(new Rectangle(pl.playerRect.X + pl.playerRect.Width, pl.playerRect.Y + 16, 16, 16), shotgunPellet, Color.White, 10, -1);
                }
            }
            //enemy end


            //size tracker for level editor
            RegularBlockSize = RegularBlockList.Count - 1;
            SlipBlockSize = SkateBlockList.Count - 1;
            SpikeBlockSize = spikeBlockList.Count - 1;
            //end size tracker
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
                        spriteBatch.DrawString(sF, "[1] Regular block [2] sliding blocks [3] spikes [4] ammo [5] snowmen [6] penguins", new Vector2(p.playerRect.X - (cam.myView.Bounds.Right / 2 - p.playerRect.Width / 2), 60), Color.White);
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
                foreach (Projectile p in snowBalls)
                {
                    spriteBatch.Draw(p.projectileTexture, p.projectileRect, p.projectileColor);
                }

                foreach (Blocks b in spikeBlockList)
                {
                    spriteBatch.Draw(b.blockTexture, b.blockRect, Color.White);
                }

            //dev mode stuff
            if (devMode) {
                if(levelEditor_IsRegBlock)
                    spriteBatch.Draw(LevelEditor_RegularBlock.blockTexture, LevelEditor_RegularBlock.blockRect, Color.White);
                if(levelEditor_IsSlipBlock)
                    spriteBatch.Draw(LevelEditor_SlipBlock.blockTexture, LevelEditor_SlipBlock.blockRect, Color.White);
                if (levelEditor_IsSpikeBlock)
                    spriteBatch.Draw(LevelEditor_spikeBlock.blockTexture, LevelEditor_spikeBlock.blockRect, Color.White);
            }
            //end
           // spriteBatch.Draw(p.playerTexture, p.playerRect, new Rectangle(p.spriteSheetX, p.spriteSheetY, 192, 192), p.playerColor);
            spriteBatch.Draw(LevelEditor_snowmen.enemyTexture,LevelEditor_snowmen.enemyRect,new Rectangle(LevelEditor_snowmen.SpriteSheetX,LevelEditor_snowmen.SpriteSheetY,96,96),Color.White);
              
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

            foreach (Blocks b in SkateBlockList)
            {
                sw.WriteLine(b.blockRect.X + "," + b.blockRect.Y);
            }

            sw.WriteLine("");
            sw.WriteLine(SlipBlockSize);
            sw.WriteLine("");

            foreach (Blocks b in spikeBlockList)
            {
                sw.WriteLine(b.blockRect.X + "," + b.blockRect.Y);
            }

            sw.WriteLine("");
            sw.WriteLine(SpikeBlockSize);
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
            if(RegularBlockSize!=0)
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

                Blocks b = new Blocks(new Rectangle(posX, posY, 200, 50), skateBlockTex);
                b.isSlipBlock = true;
                SkateBlockList.Add(b);
            }

            spaceEater = fileData;

            while ((fileData = sr.ReadLine()) != null)
            {
                if (fileData == "")
                    break;
                SlipBlockSize = int.Parse(fileData);
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

                Blocks b = new Blocks(new Rectangle(posX, posY, 16, 16), spikeBlockTex);
                b.isSpikeBlock = true;
                spikeBlockList.Add(b);
            }

            spaceEater = fileData;

            while ((fileData = sr.ReadLine())!=null)
            {
                if (fileData == "")
                    break;
                SpikeBlockSize = int.Parse(fileData);
            }
            sr.Close();
        }
        //save and load finish

            //level editor section

        void LevelEditor()
        {
             
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

                if (Keyboard.GetState().IsKeyUp(Keys.D1)&& Keyboard.GetState().IsKeyUp(Keys.D2)|| Keyboard.GetState().IsKeyUp(Keys.D3))
                {
                    swappingBlocks = false;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D2))
                {
                    levelEditor_IsRegBlock = false;
                    levelEditor_IsSlipBlock = true;

                    levelEditor_IsSpikeBlock = false;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D3))
                {
                    levelEditor_IsRegBlock = false;
                    levelEditor_IsSlipBlock = false;

                    levelEditor_IsSpikeBlock = true;
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
                            }
                    }

                    if (Keyboard.GetState().IsKeyUp(Keys.B))
                    {
                        isRemovingBlock = false;
                    }

                    LevelEditor_SlipBlock.blockPos = LevelEditor_RegularBlock.blockPos;
                    LevelEditor_spikeBlock.blockPos = LevelEditor_RegularBlock.blockPos;
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
                            }
                        }

                        if (Keyboard.GetState().IsKeyUp(Keys.B))
                        {
                            isRemovingBlock = false;
                        }

                    LevelEditor_RegularBlock.blockPos = LevelEditor_SlipBlock.blockRect.Location;
                    LevelEditor_spikeBlock.blockPos = LevelEditor_RegularBlock.blockRect.Location;
                }else
                    if (levelEditor_IsSpikeBlock)
                {

                    //movement
                    if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    {
                        LevelEditor_spikeBlock.Move(LevelEditor_spikeBlock.blockRect.X - 3, LevelEditor_spikeBlock.blockRect.Y);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    {
                        LevelEditor_spikeBlock.Move(LevelEditor_spikeBlock.blockRect.X + 3, LevelEditor_spikeBlock.blockRect.Y);
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    {
                        LevelEditor_spikeBlock.Move(LevelEditor_spikeBlock.blockRect.X, LevelEditor_spikeBlock.blockRect.Y - 3);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    {
                        LevelEditor_spikeBlock.Move(LevelEditor_spikeBlock.blockRect.X, LevelEditor_spikeBlock.blockRect.Y + 3);
                    }
                    //move end

                    //input place
                    if (Keyboard.GetState().IsKeyDown(Keys.Space) && !isPlacingBlock)
                    {
                        Blocks b = new Blocks(new Rectangle(LevelEditor_spikeBlock.blockRect.Location.X, LevelEditor_spikeBlock.blockRect.Location.Y, 16, 16), spikeBlockTex);

                        b.isSpikeBlock = true;
                        spikeBlockList.Add(b);

                        isPlacingBlock = true;
                    }
                    if (Keyboard.GetState().IsKeyUp(Keys.Space))
                    {
                        isPlacingBlock = false;
                    }
                    //input place done

                    foreach (Blocks b in spikeBlockList)
                    {
                        if (LevelEditor_spikeBlock.blockRect.Intersects(b.blockRect))
                        {
                            if (Keyboard.GetState().IsKeyDown(Keys.B) && !isRemovingBlock)
                            {
                                spikeBlockList.Remove(b);
                                isRemovingBlock = true;
                                break;
                            }

                        }
                    }

                    if (Keyboard.GetState().IsKeyUp(Keys.B))
                    {
                        isRemovingBlock = false;
                    }

                    LevelEditor_RegularBlock.blockPos = LevelEditor_spikeBlock.blockPos;
                    LevelEditor_SlipBlock.blockPos = LevelEditor_spikeBlock.blockPos;
                }


                IsMouseVisible = true;

            }
            else
            {
                foreach (Player p in playerList)
                {
                 
                        LevelEditor_RegularBlock.Move(p.playerRect.Location.X, p.playerRect.Location.Y);    
                        LevelEditor_SlipBlock.Move(p.playerRect.Location.X, p.playerRect.Location.Y);
                        LevelEditor_spikeBlock.Move(p.playerRect.Location.X, p.playerRect.Location.Y);
                    
                }
             //   LevelEditor_RegularBlock.Move(Mouse.GetState().X - (LevelEditor_RegularBlock.blockRect.Width * 2 + LevelEditor_RegularBlock.blockRect.Width / 3), Mouse.GetState().Y - LevelEditor_RegularBlock.blockRect.Height / 2);
                IsMouseVisible = false;
            }
        } //level editor end
    }
}
