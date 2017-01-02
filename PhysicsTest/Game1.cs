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
        Blocks LevelEditor_iceWall;

        Blocks LevelEditor_spikeBlock;

        Blocks LevelEditor_checkPoint;
        Blocks LevelEditor_exit;

        Enemy LevelEditor_snowmen;

        int RegularBlockSize;
        int SlipBlockSize;
        int SpikeBlockSize;
        int IceWallSize;

        int snowmanSize;

        List<Blocks> RegularBlockList;
        List<Blocks> SkateBlockList;
        List<Blocks> iceWallList;
        List<Blocks> spikeBlockList;

        List<Blocks> checkPointList;
        List<Blocks> exitList;

        List<Player> playerList;

        List<Enemy> snowmenList;
        List<Enemy> penguinList;

        List<Projectile> projectiles;
        List<Projectile> snowBalls;

        List<Explosion> explosion;

        Texture2D shotgunPellet;
            
        Texture2D playerTex;
        Texture2D blockTex;
        Texture2D skateBlockTex;
        Texture2D spikeBlockTex;
        Texture2D iceWall;
        Texture2D snowmenTex;
        Texture2D snowBallTex;

        Texture2D explosionTex;

        Texture2D checkPointTexture;
        Texture2D exitTexture;

        SpriteFont sF;

        bool playMode;
        bool devMode;

        //blocks player has
        bool levelEditor_IsRegBlock;
        bool levelEditor_IsSlipBlock;
        bool levelEditor_IsWall;
        bool levelEditor_IsSpikeBlock;
        bool levelEditor_Issnowman;

        bool levelEditor_IsCheckPoint;
        bool levelEditor_IsExit;
        //end blocktypes


        //key inputs
        bool swappingModes;
        bool swappingBlocks;

        bool isPlacingBlock;
        bool isRemovingBlock;

        bool isLoading;
        //Input end

        //key points in the map
        Point spawnPoint;

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
            iceWall = Content.Load<Texture2D>(@"icewall");

            shotgunPellet = Content.Load<Texture2D>(@"shotgunPellet");

            snowmenTex = Content.Load<Texture2D>(@"snowman_sheet");
            snowBallTex = Content.Load<Texture2D>(@"snowBall");

            exitTexture = Content.Load<Texture2D>(@"exitTex");
            checkPointTexture = Content.Load<Texture2D>(@"CheckPointSheet");

            explosionTex = Content.Load<Texture2D>(@"IceExplosion");

            sF = Content.Load<SpriteFont>(@"spriteFont");

            _player = new Player(new Rectangle(0,0,64,64),playerTex,Color.White,new Point(Window.ClientBounds.Width,Window.ClientBounds.Height));
            cam = new Camera(GraphicsDevice.Viewport);

            LevelEditor_RegularBlock = new Blocks(new Rectangle(0,0,200,50),blockTex);
            LevelEditor_SlipBlock = new Blocks(new Rectangle(0,0,200,50),skateBlockTex);
            LevelEditor_iceWall = new Blocks(new Rectangle(0,0,50,100),iceWall);

            LevelEditor_checkPoint = new Blocks(new Rectangle(0,0,96,96),checkPointTexture);
            LevelEditor_exit = new Blocks(new Rectangle(0,0,70,100),exitTexture);

            LevelEditor_spikeBlock = new Blocks(new Rectangle(0,0,16,16),spikeBlockTex);

            LevelEditor_snowmen = new Enemy((new Rectangle(0,0,64,64)),snowmenTex);

            RegularBlockList = new List<Blocks>();
            SkateBlockList = new List<Blocks>();
            spikeBlockList = new List<Blocks>();
            iceWallList = new List<Blocks>();

            checkPointList = new List<Blocks>();
            exitList = new List<Blocks>();

            snowmenList = new List<Enemy>();
            penguinList = new List<Enemy>();
            snowBalls = new List<Projectile>();

            projectiles = new List<Projectile>();
            playerList = new List<Player>();

            explosion = new List<Explosion>();

            playerList.Add(_player);

            loadPlayer();

            spawnPoint = playerList[0].playerRect.Location;

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
                    foreach(Blocks b in iceWallList)
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

                if(pl.playerPos.Y > Window.ClientBounds.Height)
                {
                    pl.velocity = new Point(0,0);
                    pl.playerPos = spawnPoint;
                }

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
                foreach (Blocks b in iceWallList)
                {
                    if (p.projectileRect.Intersects(b.blockRect))
                    {
                        projectiles.Remove(p);
                        goto here;
                    }
                }
                foreach (Blocks b in SkateBlockList)
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

                    foreach(Player pl in playerList)
                    {
                        if (p.projectileRect.Intersects(pl.playerRect))
                        {
                            pl.hitMove(p.projectileRect);
                            snowBalls.Remove(p);
                            goto there;
                        }    
                    }
                }

            there:
                //end cleaning
            //end projectiles


            //enemy related stuff

            foreach(Enemy e in snowmenList)
            {
                e.snowmanMovement(playerList[0].playerRect);
                e.snowmanAnimation();

                if (e.spawnSnowBall)
                {
                    if (e.attackingRight)
                    {
                        Projectile p = new Projectile(new Rectangle(e.enemyRect.X + e.enemyRect.Width, e.enemyRect.Y + 16, 16, 16), snowBallTex, Color.White, 5, 0, 1f);
                        snowBalls.Add(p);
                        e.spawnSnowBall = false;

                    }
                    if (e.attackingLeft)
                    {
                        Projectile p = new Projectile(new Rectangle(e.enemyRect.X, e.enemyRect.Y + 16, 16, 16), snowBallTex, Color.White, -5, 0, 1f);
                        snowBalls.Add(p);
                        e.spawnSnowBall = false;
                    }
                }
            }

            foreach(Enemy e in snowmenList)
            {
                foreach(Projectile p in projectiles)
                {
                    if (e.enemyRect.Intersects(p.projectileRect))
                    {
                        Explosion exp = new Explosion(new Rectangle(e.enemyRect.X,e.enemyRect.Y,100,100),explosionTex);

                        explosion.Add(exp);

                        projectiles.Remove(p);
                        snowmenList.Remove(e);

                        goto outside;
                    }
                }
            }
            outside:
            //enemy end


            //explosion stuff

            foreach (Explosion e in explosion)
            {
                e.Animation();

                if (e.destroySprite)
                {
                    explosion.Remove(e);
                    break;
                }
            }

            //end boom boom

            //size tracker for level editor
            RegularBlockSize = RegularBlockList.Count - 1;
            SlipBlockSize = SkateBlockList.Count - 1;
            SpikeBlockSize = spikeBlockList.Count - 1;

            IceWallSize = iceWallList.Count - 1;
            snowmanSize = snowmenList.Count - 1;
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
                        spriteBatch.DrawString(sF, "[1] Regular block [2] sliding blocks [3] spikes [4] ammo [5] medkit [6] snowmen [7] penguins [8] check point [9] exit", new Vector2(p.playerRect.X - (cam.myView.Bounds.Right / 2 - p.playerRect.Width / 2), 60), Color.White);
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
                foreach (Blocks b in spikeBlockList)
                {
                    spriteBatch.Draw(b.blockTexture, b.blockRect, Color.White);
                }
                foreach (Blocks b in iceWallList)
                {
                    spriteBatch.Draw(b.blockTexture, b.blockRect, Color.White);
                }
                foreach (Blocks b in checkPointList)
                {
                    spriteBatch.Draw(b.blockTexture, b.blockRect, Color.White);
                }
                foreach (Projectile p in projectiles)
                {
                    spriteBatch.Draw(p.projectileTexture,p.projectileRect,p.projectileColor);
                }
                foreach (Projectile p in snowBalls)
                {
                    spriteBatch.Draw(p.projectileTexture, p.projectileRect, p.projectileColor);
                }

                foreach (Enemy e in snowmenList)
                {
                    spriteBatch.Draw(e.enemyTexture, e.enemyRect, new Rectangle(e.SpriteSheetX, e.SpriteSheetY, 96, 96), Color.White);
                }

                foreach (Explosion e in explosion)
                {
                    spriteBatch.Draw(e.explosionTexture, e.explosionRect, new Rectangle(e.SpriteSheetX, e.SpriteSheetY, 96, 96), Color.White);
                }

            //dev mode stuff
            if (devMode) {
                if(levelEditor_IsRegBlock)
                    spriteBatch.Draw(LevelEditor_RegularBlock.blockTexture, LevelEditor_RegularBlock.blockRect, Color.White);
                if(levelEditor_IsSlipBlock)
                    spriteBatch.Draw(LevelEditor_SlipBlock.blockTexture, LevelEditor_SlipBlock.blockRect, Color.White);
                if (levelEditor_IsSpikeBlock)
                    spriteBatch.Draw(LevelEditor_spikeBlock.blockTexture, LevelEditor_spikeBlock.blockRect, Color.White);
                if (levelEditor_IsWall)
                    spriteBatch.Draw(LevelEditor_iceWall.blockTexture,LevelEditor_iceWall.blockRect,Color.White);
                if (levelEditor_Issnowman)
                    spriteBatch.Draw(LevelEditor_snowmen.enemyTexture, LevelEditor_snowmen.enemyRect,new Rectangle(0,0,96,96), Color.White);

                if (levelEditor_IsCheckPoint)
                    spriteBatch.Draw(LevelEditor_checkPoint.blockTexture, LevelEditor_checkPoint.blockRect, new Rectangle(0,0,96,96), Color.White);
                if (levelEditor_IsExit)
                    spriteBatch.Draw(LevelEditor_exit.blockTexture,LevelEditor_exit.blockRect,new Rectangle(0,0,70,100),Color.White);
            }
            //end
           // spriteBatch.Draw(p.playerTexture, p.playerRect, new Rectangle(p.spriteSheetX, p.spriteSheetY, 192, 192), p.playerColor);
           //spriteBatch.Draw(LevelEditor_snowmen.enemyTexture,LevelEditor_snowmen.enemyRect,new Rectangle(LevelEditor_snowmen.SpriteSheetX,LevelEditor_snowmen.SpriteSheetY,96,96),Color.White);
              
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

            foreach(Enemy e in snowmenList)
            {
                sw.WriteLine(e.enemyRect.X+","+e.enemyRect.Y);
            }

            sw.WriteLine("");
            sw.WriteLine(snowmanSize);
            sw.WriteLine("");

            foreach (Blocks b in iceWallList)
            {
                sw.WriteLine(b.blockRect.X + "," + b.blockRect.Y);
            }

            sw.WriteLine("");
            sw.WriteLine(IceWallSize);
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


            //clean previous level
            for(int i = RegularBlockSize; i >0; i--)
            {
                RegularBlockList.Remove(RegularBlockList[i]);
            }

            if(RegularBlockSize!=0)
            RegularBlockList.Remove(RegularBlockList[0]);

            for (int i = SlipBlockSize; i > 0; i--)
            {
                SkateBlockList.Remove(SkateBlockList[i]);
            }

            if (SlipBlockSize != 0)
                SkateBlockList.Remove(SkateBlockList[0]);

            for(int i = SpikeBlockSize; i > 0; i--)
            {
                spikeBlockList.Remove(spikeBlockList[i]);
            }

            if (SpikeBlockSize != 0)
                spikeBlockList.Remove(spikeBlockList[0]);

            for(int i = snowmanSize; i > 0; i--)
            {
                snowmenList.Remove(snowmenList[i]);
            }

            if (snowmanSize != 0)
                snowmenList.Remove(snowmenList[0]);

            for(int i = IceWallSize; i>0; i--)
            {
                iceWallList.Remove(iceWallList[i]);
            }

            if (IceWallSize != 0)
                iceWallList.Remove(iceWallList[0]);

            playerList.Remove(playerList[0]);            


            //cleaning finish
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

                Enemy e = new Enemy(new Rectangle(posX,posY,64,64),snowmenTex);
                snowmenList.Add(e);
            }

            spaceEater = fileData;

            while ((fileData = sr.ReadLine()) != null)
            {
                if (fileData == "")
                    break;
                snowmanSize = int.Parse(fileData);
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

                Blocks b = new Blocks(new Rectangle(posX, posY, 50, 100), iceWall);
                iceWallList.Add(b);
            }

            spaceEater = fileData;

            while ((fileData = sr.ReadLine()) != null)
            {
                if (fileData == "")
                    break;
                IceWallSize = int.Parse(fileData);
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
                    levelEditor_IsWall = false;

                    levelEditor_IsSpikeBlock = false;

                    levelEditor_Issnowman = false;

                    levelEditor_IsCheckPoint = false;
                    levelEditor_IsExit = false;

                    swappingBlocks = true;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D2) && !swappingBlocks)
                {
                    levelEditor_IsRegBlock = false;
                    levelEditor_IsSlipBlock = true;
                    levelEditor_IsWall = false;

                    levelEditor_IsSpikeBlock = false;

                    levelEditor_Issnowman = false;

                    levelEditor_IsCheckPoint = false;
                    levelEditor_IsExit = false;

                    swappingBlocks = true;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D3) && !swappingBlocks)
                {
                    levelEditor_IsRegBlock = false;
                    levelEditor_IsSlipBlock = false;
                    levelEditor_IsWall = false;

                    levelEditor_IsSpikeBlock = true;

                    levelEditor_Issnowman = false;

                    levelEditor_IsCheckPoint = false;
                    levelEditor_IsExit = false;

                    swappingBlocks = true;
                }

                if(Keyboard.GetState().IsKeyDown(Keys.D0) && !swappingBlocks)
                {
                    levelEditor_IsRegBlock = false;
                    levelEditor_IsSlipBlock = false;
                    levelEditor_IsWall = true;

                    levelEditor_IsSpikeBlock = false;

                    levelEditor_Issnowman = false;

                    levelEditor_IsCheckPoint = false;
                    levelEditor_IsExit = false;

                    swappingBlocks = true;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D6) && !swappingBlocks)
                {
                    levelEditor_IsRegBlock = false;
                    levelEditor_IsSlipBlock = false;
                    levelEditor_IsWall = false;

                    levelEditor_IsSpikeBlock = false;

                    levelEditor_Issnowman = true;

                    levelEditor_IsCheckPoint = false;
                    levelEditor_IsExit = false;

                    swappingBlocks = true;
                }

                if(Keyboard.GetState().IsKeyDown(Keys.D8) && !swappingBlocks) //check point
                {
                    levelEditor_IsRegBlock = false;
                    levelEditor_IsSlipBlock = false;
                    levelEditor_IsWall = false;

                    levelEditor_IsSpikeBlock = false;

                    levelEditor_Issnowman = false;

                    levelEditor_IsCheckPoint = true;
                    levelEditor_IsExit = false;

                    swappingBlocks = true;
                }

                if(Keyboard.GetState().IsKeyDown(Keys.D9) && !swappingBlocks) //exit
                {
                    levelEditor_IsRegBlock = false;
                    levelEditor_IsSlipBlock = false;
                    levelEditor_IsWall = false;

                    levelEditor_IsSpikeBlock = false;

                    levelEditor_Issnowman = false;

                    levelEditor_IsCheckPoint = false;
                    levelEditor_IsExit = true;

                    swappingBlocks = true;
                }

                if (Keyboard.GetState().IsKeyUp(Keys.D1) && Keyboard.GetState().IsKeyUp(Keys.D2) || Keyboard.GetState().IsKeyUp(Keys.D3) || Keyboard.GetState().IsKeyUp(Keys.D4) && Keyboard.GetState().IsKeyUp(Keys.D5) || Keyboard.GetState().IsKeyUp(Keys.D6))
                {
                    swappingBlocks = false;
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

                    LevelEditor_snowmen.enemyPos = LevelEditor_RegularBlock.blockPos;
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

                    LevelEditor_snowmen.enemyPos = LevelEditor_SlipBlock.blockRect.Location;
                }
                else
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

                    LevelEditor_snowmen.enemyPos = LevelEditor_spikeBlock.blockPos;
                }else
                    if (levelEditor_IsWall)
                    {
                        //movement
                        if (Keyboard.GetState().IsKeyDown(Keys.Left))
                        {
                            LevelEditor_iceWall.Move(LevelEditor_iceWall.blockRect.X - 3, LevelEditor_iceWall.blockRect.Y);
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Right))
                        {
                            LevelEditor_iceWall.Move(LevelEditor_iceWall.blockRect.X + 3, LevelEditor_iceWall.blockRect.Y);
                        }

                        if (Keyboard.GetState().IsKeyDown(Keys.Up))
                        {
                            LevelEditor_iceWall.Move(LevelEditor_iceWall.blockRect.X, LevelEditor_iceWall.blockRect.Y - 3);
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Down))
                        {
                            LevelEditor_iceWall.Move(LevelEditor_iceWall.blockRect.X, LevelEditor_iceWall.blockRect.Y + 3);
                        }
                    //move end

                    //input place
                    if (Keyboard.GetState().IsKeyDown(Keys.Space) && !isPlacingBlock)
                    {
                        // Blocks b = new Blocks(new Rectangle(LevelEditor_spikeBlock.blockRect.Location.X, LevelEditor_spikeBlock.blockRect.Location.Y, 16, 16), spikeBlockTex);
                        Blocks e = new Blocks(new Rectangle(LevelEditor_iceWall.blockRect.X, LevelEditor_iceWall.blockRect.Y, 50, 100),iceWall);
                        iceWallList.Add(e);

                        isPlacingBlock = true;
                    }
                    if (Keyboard.GetState().IsKeyUp(Keys.Space))
                    {
                        isPlacingBlock = false;
                    }
                    //input place done

                    foreach (Blocks b in iceWallList)
                    {
                        if (LevelEditor_iceWall.blockRect.Intersects(b.blockRect))
                        {
                            if (Keyboard.GetState().IsKeyDown(Keys.B) && !isRemovingBlock)
                            {
                                iceWallList.Remove(b);
                                isRemovingBlock = true;
                                break;
                            }

                        }
                    }

                    if (Keyboard.GetState().IsKeyUp(Keys.B))
                    {
                        isRemovingBlock = false;
                    }
                LevelEditor_RegularBlock.blockPos = LevelEditor_iceWall.blockPos;
                LevelEditor_SlipBlock.blockPos = LevelEditor_iceWall.blockPos;
                LevelEditor_spikeBlock.blockPos = LevelEditor_iceWall.blockPos;
                LevelEditor_snowmen.enemyPos = LevelEditor_iceWall.blockPos;
                }
                else
                    if (levelEditor_Issnowman)
                    {

                        //movement
                        if (Keyboard.GetState().IsKeyDown(Keys.Left))
                        {
                            LevelEditor_snowmen.Move(LevelEditor_snowmen.enemyRect.X - 3, LevelEditor_snowmen.enemyRect.Y);

                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Right))
                        {
                            LevelEditor_snowmen.Move(LevelEditor_snowmen.enemyRect.X + 3, LevelEditor_snowmen.enemyRect.Y);
                        }

                        if (Keyboard.GetState().IsKeyDown(Keys.Up))
                        {
                            LevelEditor_snowmen.Move(LevelEditor_snowmen.enemyRect.X, LevelEditor_snowmen.enemyRect.Y - 3);
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Down))
                        {
                            LevelEditor_snowmen.Move(LevelEditor_snowmen.enemyRect.X, LevelEditor_snowmen.enemyRect.Y + 3);
                        }
                        //move end

                        //input place
                        if (Keyboard.GetState().IsKeyDown(Keys.Space) && !isPlacingBlock)
                        {
                            // Blocks b = new Blocks(new Rectangle(LevelEditor_spikeBlock.blockRect.Location.X, LevelEditor_spikeBlock.blockRect.Location.Y, 16, 16), spikeBlockTex);
                            Enemy e = new Enemy(new Rectangle(LevelEditor_snowmen.enemyPos.X, LevelEditor_snowmen.enemyPos.Y, 64, 64), snowmenTex);
                            snowmenList.Add(e);

                            isPlacingBlock = true;
                        }
                        if (Keyboard.GetState().IsKeyUp(Keys.Space))
                        {
                            isPlacingBlock = false;
                        }
                        //input place done

                        foreach (Enemy e in snowmenList)
                        {
                            if (LevelEditor_snowmen.enemyRect.Intersects(e.enemyRect))
                            {
                                if (Keyboard.GetState().IsKeyDown(Keys.B) && !isRemovingBlock)
                                {
                                    snowmenList.Remove(e);
                                    isRemovingBlock = true;
                                    break;
                                }

                            }
                        }

                        if (Keyboard.GetState().IsKeyUp(Keys.B))
                        {
                            isRemovingBlock = false;
                        }
                    LevelEditor_RegularBlock.blockPos = LevelEditor_snowmen.enemyPos;
                    LevelEditor_SlipBlock.blockPos = LevelEditor_snowmen.enemyPos;
                    LevelEditor_spikeBlock.blockPos = LevelEditor_snowmen.enemyPos;
                    LevelEditor_iceWall.blockPos = LevelEditor_snowmen.enemyPos;

                    IsMouseVisible = true;

                }else
                    if (levelEditor_IsCheckPoint)
                    {
                        //movement
                        if (Keyboard.GetState().IsKeyDown(Keys.Left))
                        {
                            LevelEditor_checkPoint.Move(LevelEditor_checkPoint.blockRect.X - 3, LevelEditor_checkPoint.blockRect.Y);

                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Right))
                        {
                            LevelEditor_checkPoint.Move(LevelEditor_checkPoint.blockRect.X + 3, LevelEditor_checkPoint.blockRect.Y);
                        }

                        if (Keyboard.GetState().IsKeyDown(Keys.Up))
                        {
                            LevelEditor_checkPoint.Move(LevelEditor_checkPoint.blockRect.X, LevelEditor_checkPoint.blockRect.Y - 3);
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Down))
                        {
                            LevelEditor_checkPoint.Move(LevelEditor_checkPoint.blockRect.X, LevelEditor_checkPoint.blockRect.Y + 3);
                        }
                    //move end

                        //input place
                        if (Keyboard.GetState().IsKeyDown(Keys.Space) && !isPlacingBlock)
                        {
                            // Blocks b = new Blocks(new Rectangle(LevelEditor_spikeBlock.blockRect.Location.X, LevelEditor_spikeBlock.blockRect.Location.Y, 16, 16), spikeBlockTex);
                            Blocks e = new Blocks(new Rectangle(LevelEditor_checkPoint.blockRect.X, LevelEditor_checkPoint.blockRect.Y, 96, 96), checkPointTexture);
                            checkPointList.Add(e);

                            isPlacingBlock = true;
                        }
                        if (Keyboard.GetState().IsKeyUp(Keys.Space))
                        {
                            isPlacingBlock = false;
                        }

                    foreach (Blocks e in checkPointList)
                    {
                        if (LevelEditor_checkPoint.blockRect.Intersects(e.blockRect))
                        {
                            if (Keyboard.GetState().IsKeyDown(Keys.B) && !isRemovingBlock)
                            {
                                checkPointList.Remove(e);
                                isRemovingBlock = true;
                                break;
                            }

                        }
                    }

                    if (Keyboard.GetState().IsKeyUp(Keys.B))
                    {
                        isRemovingBlock = false;
                    }
                    //input place done
                }
            }
            else
            {
                foreach (Player p in playerList)
                {
                        LevelEditor_RegularBlock.Move(p.playerRect.Location.X, p.playerRect.Location.Y);    
                        LevelEditor_SlipBlock.Move(p.playerRect.Location.X, p.playerRect.Location.Y);
                        LevelEditor_spikeBlock.Move(p.playerRect.Location.X, p.playerRect.Location.Y);
                        LevelEditor_snowmen.Move(p.playerRect.Location.X, p.playerRect.Location.Y);
                        LevelEditor_iceWall.Move(p.playerRect.Location.X, p.playerRect.Location.Y);

                        LevelEditor_checkPoint.Move(p.playerRect.Location.X, p.playerRect.Location.Y);
                        LevelEditor_exit.Move(p.playerRect.Location.X, p.playerRect.Location.Y);
                }
             //   LevelEditor_RegularBlock.Move(Mouse.GetState().X - (LevelEditor_RegularBlock.blockRect.Width * 2 + LevelEditor_RegularBlock.blockRect.Width / 3), Mouse.GetState().Y - LevelEditor_RegularBlock.blockRect.Height / 2);
                IsMouseVisible = false;
            }
        } //level editor end
    }
}
