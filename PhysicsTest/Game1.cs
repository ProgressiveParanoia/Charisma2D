using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhysicsTest.Code.Constants;
using System;
using System.Collections.Generic;
using System.IO;

namespace PhysicsTest
{

    public class Game1 : Game
    {
        ScoreTracker scoreTracker;
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

        Blocks LevelEditor_Tree;

        PickUp LevelEditor_Ammo;
        PickUp LevelEditor_Health;
        PickUp LevelEditor_Key;

        Enemy LevelEditor_snowmen;

        int RegularBlockSize;
        int SlipBlockSize;
        int SpikeBlockSize;
        int IceWallSize;

        int treeSize;

        int snowmanSize;

        int MouseX;
        int MouseY;

        List<Blocks> RegularBlockList;
        List<Blocks> SkateBlockList;
        List<Blocks> iceWallList;
        List<Blocks> spikeBlockList;

        List<Blocks> checkPointList;
        List<Blocks> exitList;

        Blocks[] playerHP;
        Blocks[] Buttons;

        List<Player> playerList;

        List<Enemy> snowmenList;
        List<Enemy> penguinList;

        List<Projectile> projectiles;
        List<Projectile> snowBalls;

        List<Explosion> explosion;

        List<PickUp> healthPickUp;
        List<PickUp> ammoPickUp;
        List<PickUp> KeyList;

        List<ScoreTracker> scores;

        List<Blocks> TreeList;

        Texture2D ingameBackground;

        Texture2D shotgunPellet;
            
        Texture2D playerTex;
        Texture2D blockTex;
        Texture2D skateBlockTex;
        Texture2D spikeBlockTex;
        Texture2D iceWall;
        Texture2D snowmenTex;
        Texture2D snowBallTex;
        Texture2D TreeTex;

        Texture2D explosionTex;

        Texture2D LifeTex;
        Texture2D HPTex;

        Texture2D checkPointTexture;
        Texture2D exitTexture;

        Texture2D ammoTexture;
        Texture2D healthTexture;
        Texture2D KeyTexture;

        Texture2D titleCard;

        Texture2D StartBtn;
        Texture2D OptionsBtn;
        Texture2D realOptionBtn;
        Texture2D ExitBtn;
        Texture2D Optionsbackground;

        Texture2D yesBtn;
        Texture2D noBtn;

        Texture2D Instructibles;
        Texture2D controllerInstructibles;

        Rectangle StartRect;
        Rectangle OptionRect;
        Rectangle RealOptionRect;
        Rectangle ExitRect;

        Rectangle enableSoundRect;
        Rectangle enableControllerRect;

        Rectangle mouse;

        SpriteFont sF;

        bool playMode;
        bool devMode;

        bool gameFinished;

        bool isPlaying;
        bool isLobby;

        bool isMenu;
        bool isOption;
        bool isRealOption;

        bool isStage1;
        bool isStage2;
        bool isStage3;
        bool isStage4;

        bool checkPress;

        bool Win;

        //blocks player has
        bool levelEditor_IsRegBlock;
        bool levelEditor_IsSlipBlock;
        bool levelEditor_IsWall;
        bool levelEditor_IsSpikeBlock;
        bool levelEditor_Issnowman;

        bool levelEditor_IsKey;
        bool levelEditor_IsAmmo;
        bool levelEditor_IsHealth;

        bool levelEditor_IsTree;

        bool levelEditor_IsCheckPoint;
        bool levelEditor_IsExit;
        //end blocktypes

        //key inputs
        bool swappingModes;
        bool swappingBlocks;

        bool isPlacingBlock;
        bool isRemovingBlock;

        bool isLoading;

        int score;
        string plName;
        //Input end

        SoundEffect mainMenuTheme;
        SoundEffect Stage1Sound;
        SoundEffect Stage2Sound;
        SoundEffect JumpSound;
        SoundEffect DeathSound;
        SoundEffect ShootSound;
        SoundEffect enemyShootSound;

        SoundEffect pickUpItem;

        SoundEffect gameOver;

        SoundEffectInstance JumpControl;
        SoundEffectInstance ShootControl;
        SoundEffectInstance DeathSoundControl;

        SoundEffectInstance enemyShootControl;

        SoundEffectInstance Stage1SoundControl;
        SoundEffectInstance Stage2SoundControl;
        SoundEffectInstance MainMenuSoundControl;

        SoundEffectInstance gameOverControl;

        SoundEffectInstance pickUpItemControl;

        bool enabledStuff;
        bool HasEnabledControllers;
        //key points in the map
        Point spawnPoint;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            devMode = false;
            playMode = true;

            levelEditor_IsRegBlock = true;

            HasEnabledControllers = false;

            plName = "";
        }

        protected override void Initialize()
        {
            scores = new List<ScoreTracker>();

            ingameBackground = Content.Load<Texture2D>(FilePath.BACKGROUND_NIGHTBACKGROUND);

            playerTex = Content.Load<Texture2D>(@"Elf_shotty_sheet");
            blockTex = Content.Load<Texture2D>(@"BlockTest");
            skateBlockTex = Content.Load<Texture2D>(@"skateBlocks");
            spikeBlockTex = Content.Load<Texture2D>(@"icicleDown");
            iceWall = Content.Load<Texture2D>(@"icewall");

            shotgunPellet = Content.Load<Texture2D>(@"shotgunPellet");

            snowmenTex = Content.Load<Texture2D>(@"snowman_sheet");
            snowBallTex = Content.Load<Texture2D>(@"snowBall");

            exitTexture = Content.Load<Texture2D>(@"exitTex");
            checkPointTexture = Content.Load<Texture2D>(@"spawnPoint");

            LifeTex = Content.Load<Texture2D>(@"LIFE");
            HPTex = Content.Load<Texture2D>(@"HP");

            titleCard = Content.Load<Texture2D>(@"TitleCard");

            StartBtn = Content.Load<Texture2D>(@"StartBtn");
            OptionsBtn = Content.Load<Texture2D>(@"OptionsBtn");
            realOptionBtn = Content.Load<Texture2D>(@"realOptionsBtn");
            ExitBtn = Content.Load<Texture2D>(@"ExitBtn");

            Optionsbackground = Content.Load<Texture2D>(@"OptionsAndStuff");

            noBtn = Content.Load<Texture2D>(@"NoBtn");
            yesBtn = Content.Load<Texture2D>(@"YesBtn"); 

            Instructibles = Content.Load<Texture2D>(@"Instructibles");
            controllerInstructibles = Content.Load<Texture2D>(@"ControllerInstructibles");

            ammoTexture = Content.Load<Texture2D>(@"AmmoPickup");
            healthTexture = Content.Load<Texture2D>(@"HealthPickup");
            KeyTexture = Content.Load<Texture2D>(@"Key");

            TreeTex = Content.Load<Texture2D>(@"tree");

            explosionTex = Content.Load<Texture2D>(@"IceExplosion");

            mainMenuTheme = Content.Load<SoundEffect>(@"MainMenuSound");
            Stage1Sound = Content.Load<SoundEffect>(@"Stage1Music");
            Stage2Sound = Content.Load<SoundEffect>(@"Stage2Music");

            pickUpItem = Content.Load<SoundEffect>(@"PickUpPower");

            gameOver = Content.Load<SoundEffect>(@"GameOver");

            JumpSound = Content.Load<SoundEffect>(@"Jump");
            ShootSound = Content.Load<SoundEffect>(@"Shoot");
            enemyShootSound = Content.Load<SoundEffect>(@"ShootingSound");

            DeathSound = Content.Load<SoundEffect>(@"DeathSound");

            ShootControl = ShootSound.CreateInstance();
            enemyShootControl = enemyShootSound.CreateInstance();
            JumpControl = JumpSound.CreateInstance();

            DeathSoundControl = DeathSound.CreateInstance();

            pickUpItemControl = pickUpItem.CreateInstance();
    
            MainMenuSoundControl = mainMenuTheme.CreateInstance();
            Stage1SoundControl = Stage1Sound.CreateInstance();
            Stage2SoundControl = Stage2Sound.CreateInstance();

            gameOverControl = gameOver.CreateInstance();

            MainMenuSoundControl.IsLooped = true;
            Stage1SoundControl.IsLooped = true;
            Stage2SoundControl.IsLooped = true;

            sF = Content.Load<SpriteFont>(@"spriteFont");

            _player = new Player(new Rectangle(0,0,64,64),playerTex,Color.White,new Point(Window.ClientBounds.Width,Window.ClientBounds.Height),HasEnabledControllers);
            cam = new Camera(GraphicsDevice.Viewport);

            LevelEditor_RegularBlock = new Blocks(new Rectangle(0,0,200,50),blockTex);
            LevelEditor_SlipBlock = new Blocks(new Rectangle(0,0,200,50),skateBlockTex);
            LevelEditor_iceWall = new Blocks(new Rectangle(0,0,50,100),iceWall);

            LevelEditor_checkPoint = new Blocks(new Rectangle(0,0,96,96),checkPointTexture);
            LevelEditor_exit = new Blocks(new Rectangle(0,0,96,96),exitTexture);

            LevelEditor_Ammo = new PickUp(new Rectangle(0,0,64,48),ammoTexture);
            LevelEditor_Health = new PickUp(new Rectangle(0,0,64,48),healthTexture);
            LevelEditor_Key = new PickUp(new Rectangle(0, 0, 48, 64), KeyTexture);

            LevelEditor_Tree = new Blocks(new Rectangle(0,0,96,96),TreeTex);

            LevelEditor_spikeBlock = new Blocks(new Rectangle(0,0,16,16),spikeBlockTex);

            LevelEditor_snowmen = new Enemy((new Rectangle(0,0,64,64)),snowmenTex);

            RegularBlockList = new List<Blocks>();
            SkateBlockList = new List<Blocks>();
            spikeBlockList = new List<Blocks>();
            iceWallList = new List<Blocks>();

            TreeList = new List<Blocks>();

            checkPointList = new List<Blocks>();
            exitList = new List<Blocks>();

            snowmenList = new List<Enemy>();
            penguinList = new List<Enemy>();
            snowBalls = new List<Projectile>();

            playerHP = new Blocks[3];
            Buttons = new Blocks[3];

            projectiles = new List<Projectile>();
            playerList = new List<Player>();

            KeyList = new List<PickUp>();
            ammoPickUp = new List<PickUp>();
            healthPickUp = new List<PickUp>();

            explosion = new List<Explosion>();

            Blocks _playerHP = new Blocks((new Rectangle(0,0, 48, 48)),HPTex);
            Blocks _playerLife = new Blocks(new Rectangle(0,0,48,64),LifeTex);


            loadPlayer("MENU.SWAG",3,3,12);

            for (int i = 0; i < 3; i++)
            {
                playerHP[i]=(_playerHP);

            }

            Blocks start = new Blocks(new Rectangle(0,0,200,100),StartBtn);
            mouse.Location = new Point(playerList[0].playerRect.X, Mouse.GetState().Y);

            StartRect = new Rectangle(playerList[0].playerRect.X - playerList[0].playerRect.Width / 2, 100, 150, 50);
            OptionRect = new Rectangle(playerList[0].playerRect.X - playerList[0].playerRect.Width / 2, 170, 150, 50);
            RealOptionRect = new Rectangle(playerList[0].playerRect.X - playerList[0].playerRect.Width / 2, 240, 150, 50);
            ExitRect = new Rectangle(playerList[0].playerRect.X - playerList[0].playerRect.Width / 2, 240, 150, 50);

            enableControllerRect = new Rectangle(playerList[0].playerRect.X - playerList[0].playerRect.Width / 2,130,150,50);
            enableSoundRect = new Rectangle(playerList[0].playerRect.X - playerList[0].playerRect.Width / 2, 240, 150, 50);

            Buttons[0] = start;

            isMenu = true;

            spawnPoint = new Point(checkPointList[0].blockPos.X, checkPointList[0].blockPos.Y);


            loadScore();
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
          //  Console.WriteLine("WAT:"+playerList[0].usingController);
            LevelEditor();

            //player related methods
          //  Console.WriteLine("Shooting:"+GamePad.GetState(PlayerIndex.One).Buttons.LeftShoulder);
        //    Console.WriteLine("LEFT:"+GamePad.GetState(PlayerIndex.One).ThumbSticks.Left+"RIGHT:"+ GamePad.GetState(PlayerIndex.One).ThumbSticks.Right);
            foreach (Player pl in playerList)
            {
                if (pl.PlayerLife != 0)
                {
                    if (isPlaying && !gameFinished)
                    {
                        if (!pl.usingController)
                        {
                            if (Keyboard.GetState().IsKeyDown(Keys.W) && !pl.pressingJump)
                            {
                                JumpControl = JumpSound.CreateInstance();
                                JumpControl.Volume = 0.2f;
                                JumpControl.Play();
                            }
                        }else
                            if(GamePad.GetState(PlayerIndex.One).Buttons.RightShoulder == ButtonState.Pressed && !pl.pressingJump)
                            {
                                JumpControl = JumpSound.CreateInstance();
                                JumpControl.Volume = 0.2f;
                                JumpControl.Play();
                            }

                        if (pl.spawnDelay==0)
                            pl.move(devMode);


                        if (!devMode)
                        {
                            if (!pl.usingController)
                            {
                                if (Keyboard.GetState().IsKeyDown(Keys.Space) && !pl.shooting)
                                {
                                    if (pl.ammoCounter > 0 && pl.spawnDelay == 0)
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

                                        ShootControl = ShootSound.CreateInstance();
                                        ShootControl.Volume = 0.3f;
                                        ShootControl.Play();

                                        pl.ammoCounter--;
                                    }
                                }
                                else
                                    if (Keyboard.GetState().IsKeyUp(Keys.Space))
                                {
                                    pl.shooting = false;
                                }
                            }else
                                if (pl.usingController)
                                {
                                    if(GamePad.GetState(PlayerIndex.One).Buttons.LeftShoulder == ButtonState.Pressed && !pl.shooting)
                                    {
                                        if(pl.ammoCounter > 0 && pl.spawnDelay == 0)
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

                                            ShootControl = ShootSound.CreateInstance();
                                            ShootControl.Volume = 0.3f;
                                            ShootControl.Play();

                                            pl.ammoCounter--;
                                        }
                                    }
                                else
                                    if (GamePad.GetState(PlayerIndex.One).Buttons.LeftShoulder == ButtonState.Released)
                                    {
                                        pl.shooting = false;
                                    }
                            }
                            pl.DoPhysics();

                            foreach (Blocks b in RegularBlockList)
                            {
                                pl.Colliders(b);
                            }
                            foreach (Blocks b in iceWallList)
                            {
                                pl.Colliders(b);
                            }

                            foreach (Blocks sb in SkateBlockList)
                            {
                                pl.Colliders(sb);
                            }

                            foreach (Blocks sb in spikeBlockList)
                            {
                                pl.Colliders(sb);
                            }

                            foreach (PickUp p in KeyList)
                            {
                                if (pl.playerRect.Intersects(p.pickUpRect))
                                {
                                    pl.hasKey = true;
                                    score += 500;
                                    KeyList.Remove(p);

                                    break;
                                }
                            }

                            foreach (PickUp p in ammoPickUp)
                            {
                                if (pl.playerRect.Intersects(p.pickUpRect))
                                {
                                    pl.ammoCounter += 4;
                                    score += 500;
                                    ammoPickUp.Remove(p);
                                    break;
                                }
                            }

                            foreach (PickUp p in healthPickUp)
                            {
                                if (pl.playerRect.Intersects(p.pickUpRect))
                                {
                                    pl.PlayerHP++;

                                    playerHP = new Blocks[pl.PlayerHP];

                                    Blocks _playerHP = new Blocks((new Rectangle(0, 0, 48, 48)), HPTex);

                                    for (int i = 0; i < pl.PlayerHP; i++)
                                    {
                                        playerHP[i] = (_playerHP);
                                    }

                                    pl.PlayerLife = 3;
                                    score += 500;

                                    healthPickUp.Remove(p);
                                    break;
                                }
                            }
                        }
                    }
                    pl.Animation();

                    if (pl.playerPos.Y > Window.ClientBounds.Height)
                    {
                        if (pl.spawnDelay == 0)
                        {
                            DeathSoundControl = DeathSound.CreateInstance();
                            DeathSoundControl.Volume = 0.2f;
                            DeathSoundControl.Play();
                        }
                        pl.spawnDelay++;

                        if (pl.spawnDelay >= 120)
                        {
                            pl.velocity = new Point(0, 0);
                            pl.PlayerHP--;
                            pl.PlayerLife = 3;

                            pl.playerPos = spawnPoint;

                            pl.spawnDelay = 0;
                        }
                    }
                    //camera stuff
                    cam.setToCenter(pl.playerRect, new Point(Window.ClientBounds.Width, Window.ClientBounds.Height));
                }//else player dies

                //save and load inputs
                if (Keyboard.GetState().IsKeyDown(Keys.F))
                {
                    savePlayer();
                }

                if (Keyboard.GetState().IsKeyDown(Keys.L) && !isLoading)
                {
                    loadPlayer("Stage4.SWAG", 3, 3, 8);
                    isLoading = true;
                    break;
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

                    foreach (Player pl in playerList)
                    {
                        if (p.projectileRect.Intersects(pl.playerRect) && pl.PlayerLife != 0)
                        {
                            pl.hitMove(p.projectileRect);

                            snowBalls.Remove(p);
                            pl.PlayerLife--;
                            goto there;
                        }
                    }
                }

                there:
            //end cleaning
            //end projectiles

            
            //enemy related stuff

            if (!devMode)
            {
                foreach (Enemy e in snowmenList)
                {
                    
                    e.snowmanMovement(playerList[0]);
                    e.snowmanAnimation();

                    if (e.spawnSnowBall)
                    {
                        if (e.attackingRight)
                        {
                            Projectile p = new Projectile(new Rectangle(e.enemyRect.X + e.enemyRect.Width, e.enemyRect.Y + 16, 16, 16), snowBallTex, Color.White, 5, 0, 0.5f);
                            snowBalls.Add(p);
                            e.spawnSnowBall = false;
                        }
                        if (e.attackingLeft)
                        {
                            Projectile p = new Projectile(new Rectangle(e.enemyRect.X, e.enemyRect.Y + 16, 16, 16), snowBallTex, Color.White, -5, 0, 0.5f);
                            snowBalls.Add(p);
                            e.spawnSnowBall = false;
                        }

                        enemyShootControl = enemyShootSound.CreateInstance();
                        enemyShootControl.Volume = 0.3f;
                        enemyShootControl.Play();
                    }
                }
            }

            foreach(Enemy e in snowmenList)
            {
                foreach(Projectile p in projectiles)
                {
                    if (e.enemyRect.Intersects(p.projectileRect))
                    {
                        Explosion exp = new Explosion(new Rectangle(e.enemyRect.X,e.enemyRect.Y,100,100),explosionTex,Color.White);
                        explosion.Add(exp);

                        projectiles.Remove(p);
                        snowmenList.Remove(e);

                        score += 100;
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
            treeSize = TreeList.Count - 1;
            //end size tracker

            //stage swapping and key-related functions
            if (playerList[0].playerRect.Intersects(exitList[0].blockRect))
            {
                if (playerList[0].hasKey)
                {
                    if (isStage1)
                    {
                        isStage1 = false;
                        isStage2 = true;
                        isStage3 = false;

                        loadPlayer("Stage2.SWAG", playerList[0].PlayerLife, playerList[0].PlayerHP, playerList[0].ammoCounter);
                    } else
                    if (isStage2)
                    {
    
                        isStage1 = false;
                        isStage2 = false;
                        isStage3 = true;

                        loadPlayer("Stage3.SWAG", playerList[0].PlayerLife, playerList[0].PlayerHP, playerList[0].ammoCounter);
                       // Win = true;

                      //  gameOverControl.Play();
                    } else
                        if (isStage3)
                    {
                        isStage1 = false;
                        isStage2 = false;
                        isStage3 = false;
                        isStage4 = true;

                        loadPlayer("Stage3.SWAG", playerList[0].PlayerLife, playerList[0].PlayerHP, playerList[0].ammoCounter);
                    }
                    else
                        if (isStage4)
                    {
                        isPlaying = false;

                        isStage1 = false;
                        isStage2 = false;
                        isStage3 = false;
                        isStage4 = false;

                        Win = true;
                        gameOverControl.Play();
                        //loadPlayer("Menu.SWAG", playerList[0].PlayerLife, playerList[0].PlayerHP, playerList[0].ammoCounter);
                    }
                    playerList[0].hasKey = false;
                }
            }


            //end keys

            if(playerList[0].PlayerHP == 0 && !gameFinished) 
            {
                Stage1SoundControl.Stop();
                Stage2SoundControl.Stop();
                MainMenuSoundControl.Stop();


                gameOverControl.Play();

                gameFinished = true;

            }

            if (gameOverControl.State == SoundState.Stopped && isPlaying && !Win)
            {
                if (playerList[0].PlayerHP == 0)
                {
                    isPlaying = false;
                    isStage1 = false;
                    isStage2 = false;
                    isStage3 = false;
                    isStage4 = false;

                    saveScore();

                    isMenu = true;
                    score = 0;
                    loadPlayer("MENU.SWAG",3,3,12);
                }
            }

            if (Win)
            {

             //   playerList[0].spawnDelay++;

                if (gameOverControl.State == SoundState.Stopped)
                {
                    playerList[0].playerPos = spawnPoint;
                    playerList[0].PlayerLife = 3;
                    playerList[0].PlayerHP--;

                    //   playerList[0].spawnDelay = 0;
                    saveScore();

                    isMenu = true;
                    Win = false;
                    score = 0;
                    loadPlayer("MENU.SWAG", 3, 3, 8);
                }
            }


            mouse.Location = new Point(playerList[0].playerRect.X, Mouse.GetState().Y);

            //Console.WriteLine("Mouse State and player:"+ playerList[0].playerRect.X + Mouse.GetState().X +" Player Rect:"+ playerList[0].playerRect.X+" mouse state:"+Mouse.GetState().X);
            if (isLobby)
            {
                IsMouseVisible = true;
                StartRect = new Rectangle(playerList[0].playerRect.X - playerList[0].playerRect.Width / 2, 410, 150, 50);

                Keys[] d = Keyboard.GetState().GetPressedKeys();
                foreach (Keys k in d)
                {
                    if (checkPress)
                    {
                        try
                        {
                            char charname = char.Parse(k.ToString());

                            if (char.IsLetter(charname) && plName.Length <= 10)
                            {
                                plName += k.ToString();
                            }
                        }
                        catch { }

                        if (k == Keys.Back && plName.Length > 0)
                        {
                            plName = plName.Substring(0, plName.Length - 1);
                        }

                        if (k == Keys.Space)
                        {
                            plName += " ";
                        }

                        if (k == Keys.OemMinus)
                        {
                            plName += "_";
                        }

                    }

                }

                if (d.Length > 0)
                {
                    checkPress = false;
                }
                else
                    checkPress = true;

                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    if (StartRect.Intersects(mouse) && plName!="")
                    {
                        isStage1 = true;
                        gameFinished = false;
                        isPlaying = true;
                        isMenu = false;
                        isLobby = false;

                        loadPlayer("Stage1.SWAG", 3, 3, 12);
                    }
                }
            }
            if (isMenu)
            {
                IsMouseVisible = true;

                StartRect = new Rectangle(playerList[0].playerRect.X - playerList[0].playerRect.Width/2,100,150,50);
                OptionRect = new Rectangle(playerList[0].playerRect.X - playerList[0].playerRect.Width / 2, 170, 150, 50);
                RealOptionRect = new Rectangle(playerList[0].playerRect.X - playerList[0].playerRect.Width / 2, 240, 150, 50);
                ExitRect = new Rectangle(playerList[0].playerRect.X - playerList[0].playerRect.Width / 2, 310, 150, 50);
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    if (OptionRect.Intersects(mouse))
                    {
                        isMenu = false;

                        isOption = true;

                        Console.WriteLine("Eks dee");
                    }

                    if (StartRect.Intersects(mouse))
                    {
                        isMenu = false;

                        isStage1 = true;
                        gameFinished = false;
                        isPlaying = true;
                        isMenu = false;
                        isLobby = false;

                        loadPlayer("Stage1.SWAG", 3, 3, 12);
                    }
                    if (RealOptionRect.Intersects(mouse))
                    {
                        isMenu = false;

                        isRealOption = true;
                    }
                    if (ExitRect.Intersects(mouse))
                    {
                        this.Exit();
                    }
                }
            }

            if (isRealOption)
            {
                IsMouseVisible = true;
                ExitRect = new Rectangle(playerList[0].playerRect.X - playerList[0].playerRect.Width / 2, Window.ClientBounds.Height - 50, 150, 50);

                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    if (ExitRect.Intersects(mouse))
                    {
                        isMenu = true;

                        isRealOption = false;

                        Console.WriteLine("Eks dee");
                    }
                    if (enableControllerRect.Intersects(mouse) && !enabledStuff)
                    {
                        if (HasEnabledControllers)
                        {
                            HasEnabledControllers = false;
                        }
                        else
                            HasEnabledControllers = true;

                        enabledStuff = true;
                    }
                }else
                    if(Mouse.GetState().LeftButton == ButtonState.Released)
                {
                    enabledStuff = false;
                }
            }

            if (isOption)
            {
                IsMouseVisible = true;

                ExitRect = new Rectangle(playerList[0].playerRect.X - playerList[0].playerRect.Width / 2, Window.ClientBounds.Height - 50, 150, 50);

                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    if (ExitRect.Intersects(mouse))
                    {
                        isMenu = true;

                        isOption = false;

                        Console.WriteLine("Eks dee");
                    }
                }
            }

            if (isMenu || isOption||isLobby|| isRealOption)
            {
                if(MainMenuSoundControl.State != SoundState.Playing)
                {
                    MainMenuSoundControl.Play();
                    MainMenuSoundControl.Volume = 0.5f;
                }
            }else
            {
                MainMenuSoundControl.Stop();
            }

            if (isStage1 && !gameFinished)
            {
                if (Stage1SoundControl.State != SoundState.Playing)
                {
                    Stage1SoundControl.Play();
                    Stage1SoundControl.Volume = 0.5f;
               
                }
               

            }else
            if(!isStage1 && gameFinished)
            {
                Stage1SoundControl.Stop();
                
            }

            if(isStage2 && !gameFinished)
            {
                if(Stage2SoundControl.State != SoundState.Playing)
                {
                    Stage2SoundControl.Play();
                    Stage2SoundControl.Volume = 0.5f;
                }
            }else
            {
                Stage2SoundControl.Stop();
            }

            if (isStage3 && !gameFinished)
            {
                if (Stage1SoundControl.State != SoundState.Playing)
                {
                    Stage1SoundControl.Play();
                    Stage1SoundControl.Volume = 0.5f;
                }
            }
            else
            {
                Stage1SoundControl.Stop();
            }


            if (isStage4 && !gameFinished)
            {
                if (Stage2SoundControl.State != SoundState.Playing)
                {
                    Stage2SoundControl.Play();
                    Stage2SoundControl.Volume = 0.5f;
                }
            }
            else
            {
                Stage2SoundControl.Stop();
            }
            // Console.WriteLine("HP Elements:"+playerList[0].PlayerHP+ "player size"+playerList.Count);
           // Console.WriteLine(isStage1 + "game finished" + gameFinished);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,null,null,null,null,cam.Transform);

            spriteBatch.Draw(ingameBackground,new Rectangle(playerList[0].playerRect.X - (cam.myView.Bounds.Right/2 - playerList[0].playerRect.Width/2),cam.myView.Bounds.Center.Y - cam.myView.Bounds.Center.Y, 800,600),Color.White);
            //spriteBatch.Draw(_player.playerTexture, _player.playerRect, new Rectangle(_player.spriteSheetX, _player.spriteSheetY, 192, 192), _player.playerColor);

            foreach (Blocks b in TreeList)
            {
                spriteBatch.Draw(b.blockTexture, b.blockRect, Color.White);
            }
                foreach (Player p in playerList)
                {
                    if (p.PlayerLife != 0)
                    {
                        spriteBatch.Draw(p.playerTexture, p.playerRect, new Rectangle(p.spriteSheetX, p.spriteSheetY, 192, 192), p.playerColor);
                    }
                    if (devMode)
                    {
                        spriteBatch.DrawString(sF, "Player X:" + p.playerRect.X + " Y:" + p.playerRect.Y, new Vector2(p.playerRect.X - (cam.myView.Bounds.Right / 2 - p.playerRect.Width / 2), 0), Color.White);
                        spriteBatch.DrawString(sF, "Editorblock X:" + LevelEditor_RegularBlock.blockRect.X + " Y:" + LevelEditor_RegularBlock.blockRect.Y, new Vector2(p.playerRect.X - (cam.myView.Bounds.Right / 2 - p.playerRect.Width / 2), 20), Color.White);
                        spriteBatch.DrawString(sF, "[F]Save [L]Load [T]Level Editor",new Vector2(p.playerRect.X - (cam.myView.Bounds.Right / 2 - p.playerRect.Width / 2), 40),Color.White);
                    
                        spriteBatch.DrawString(sF, "[1] Regular block [2] sliding blocks [3] spikes [4] ammo [5] medkit [6] snowmen [7] keys [8] check point [9] exit", new Vector2(p.playerRect.X - (cam.myView.Bounds.Right / 2 - p.playerRect.Width / 2), 60), Color.White);
                        spriteBatch.DrawString(sF, "[Space]Place Block [B]Remove Block", new Vector2(p.playerRect.X - (cam.myView.Bounds.Right / 2 - p.playerRect.Width / 2), 100), Color.White);
                     }
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
                foreach(Blocks b in exitList)
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
                    spriteBatch.Draw(e.explosionTexture, e.explosionRect, new Rectangle(e.SpriteSheetX, e.SpriteSheetY, 96, 96), e.explosionColor);
                }

                foreach(PickUp e in ammoPickUp)
                {
                    spriteBatch.Draw(e.pickUpTexture,e.pickUpRect,Color.White);
                }

                foreach (PickUp e in healthPickUp)
                {
                    spriteBatch.Draw(e.pickUpTexture, e.pickUpRect, Color.White);
                }

                foreach (PickUp e in KeyList)
                {
                    spriteBatch.Draw(e.pickUpTexture, e.pickUpRect, Color.White);
                }

            if (isPlaying) {

                foreach (Player p in playerList)
                {

                    for (int i = 0; i < p.PlayerHP; i++)
                    {
                        if (i == p.PlayerHP - 1)
                        {
                            if (p.PlayerLife == 3)
                            {
                                spriteBatch.Draw(playerHP[i].blockTexture, new Rectangle(p.playerRect.X - (cam.myView.Bounds.Right / 2 - playerHP[i].blockRect.Width) + (playerHP[i].blockRect.Width * i), 0, playerHP[i].blockRect.Width, playerHP[i].blockRect.Height), Color.White);
                            }
                            if (p.PlayerLife == 2)
                            {
                                spriteBatch.Draw(playerHP[i].blockTexture, new Rectangle(p.playerRect.X - (cam.myView.Bounds.Right / 2 - playerHP[i].blockRect.Width) + (playerHP[i].blockRect.Width * i), 0, playerHP[i].blockRect.Width, playerHP[i].blockRect.Height), Color.Red);
                            }
                            if (p.PlayerLife == 1)
                            {
                                spriteBatch.Draw(playerHP[i].blockTexture, new Rectangle(p.playerRect.X - (cam.myView.Bounds.Right / 2 - playerHP[i].blockRect.Width) + (playerHP[i].blockRect.Width * i), 0, playerHP[i].blockRect.Width, playerHP[i].blockRect.Height), Color.Blue);
                            }
                            else
                            if (p.PlayerLife == 0)
                            {
                                if (p.spawnDelay == 0)
                                {
                                    Explosion e = new Explosion(new Rectangle(p.playerRect.X, p.playerRect.Y, 100, 100), explosionTex, Color.Red);

                                    DeathSoundControl = DeathSound.CreateInstance();
                                    DeathSoundControl.Volume = 0.2f;
                                    DeathSoundControl.Play();

                                    explosion.Add(e);
                                }

                                p.spawnDelay++;

                                if (p.spawnDelay >= 120)
                                {
                                    p.playerPos = spawnPoint;
                                    p.PlayerLife = 3;
                                    p.PlayerHP--;

                                    p.spawnDelay = 0;
                                    break;
                                }
                            }
                        }
                        else
                            if (i != (p.PlayerHP - 1))
                        {
                            spriteBatch.Draw(playerHP[i].blockTexture, new Rectangle(p.playerRect.X - (cam.myView.Bounds.Right / 2 - playerHP[i].blockRect.Width) + (playerHP[i].blockRect.Width * i), 0, playerHP[i].blockRect.Width, playerHP[i].blockRect.Height), Color.White);
                        }
                    }

                    if (p.hasKey)
                    {
                        spriteBatch.Draw(KeyTexture, new Rectangle(p.playerRect.X - (cam.myView.Bounds.Right / 2 - 48), 128, 48, 64), Color.White);
                    }
                    else
                      if (p.playerRect.Intersects(exitList[0].blockRect) && !p.hasKey)
                    {
                        spriteBatch.DrawString(sF, "You must have a key to move on!", new Vector2(p.playerRect.X - 150, 100), Color.White);

                    }
                    spriteBatch.Draw(ammoTexture, new Rectangle(p.playerRect.X - (cam.myView.Bounds.Right / 2 - 48), 64, 64, 48), Color.White);
                    if (gameFinished)
                    {
                        spriteBatch.DrawString(sF, "Game Over!", new Vector2(p.playerRect.X - 50, Window.ClientBounds.Height/2 - 100), Color.Red);
                    }

                    spriteBatch.DrawString(sF, playerList[0].ammoCounter.ToString(), new Vector2(p.playerRect.X - ((cam.myView.Bounds.Right / 2 - p.playerRect.Width / 2) - 96), 86), Color.White);
                //    spriteBatch.DrawString(sF, "Score:" + score, new Vector2(playerList[0].playerRect.X - ((cam.myView.Bounds.Right / 2 - playerList[0].playerRect.Width / 2) - 612),20), Color.Red);
                //    spriteBatch.DrawString(sF, "Name:" + plName, new Vector2(playerList[0].playerRect.X - ((cam.myView.Bounds.Right / 2 - playerList[0].playerRect.Width / 2) - 612), 40), Color.LightGreen);
                }
            }


            if (Win)
            {
                spriteBatch.DrawString(sF, "You Win!", new Vector2(playerList[0].playerRect.X - 50, Window.ClientBounds.Height / 2 - 50), Color.Red);
            }
            if (isMenu)
            {
             //   StartRect = new Rectangle(playerList[0].playerRect.X - playerList[0].playerRect.Width / 2, 100, 150, 50);
                spriteBatch.Draw(titleCard, new Rectangle(playerList[0].playerRect.X - playerList[0].playerRect.Width / 2 - 60,10,250, 100), Color.White);
                spriteBatch.Draw(StartBtn,StartRect,Color.White);
                spriteBatch.Draw(OptionsBtn, OptionRect, Color.White);
                spriteBatch.Draw(realOptionBtn, RealOptionRect, Color.White);
                spriteBatch.Draw(ExitBtn,ExitRect,Color.White);
            }

            if (isOption)
            {
                spriteBatch.Draw(ingameBackground, new Rectangle(playerList[0].playerRect.X - (cam.myView.Bounds.Right / 2 - playerList[0].playerRect.Width / 2), cam.myView.Bounds.Center.Y - cam.myView.Bounds.Center.Y, 800, 600), Color.White);
                if (!HasEnabledControllers)
                    spriteBatch.Draw(Instructibles, new Rectangle(playerList[0].playerRect.X - (cam.myView.Bounds.Right / 2 - playerList[0].playerRect.Width / 2), cam.myView.Bounds.Center.Y - cam.myView.Bounds.Center.Y, 800, 600), Color.White);
                else
                    spriteBatch.Draw(controllerInstructibles, new Rectangle(playerList[0].playerRect.X - (cam.myView.Bounds.Right / 2 - playerList[0].playerRect.Width / 2), cam.myView.Bounds.Center.Y - cam.myView.Bounds.Center.Y, 800, 600), Color.White);

                spriteBatch.Draw(ExitBtn, ExitRect, Color.White);
            }

            if (isRealOption)
            {
                spriteBatch.Draw(ingameBackground, new Rectangle(playerList[0].playerRect.X - (cam.myView.Bounds.Right / 2 - playerList[0].playerRect.Width / 2), cam.myView.Bounds.Center.Y - cam.myView.Bounds.Center.Y, 800, 600), Color.White);
                spriteBatch.Draw(Optionsbackground, new Rectangle(playerList[0].playerRect.X - (cam.myView.Bounds.Right / 2 - playerList[0].playerRect.Width / 2), cam.myView.Bounds.Center.Y - cam.myView.Bounds.Center.Y, 800, 600), Color.White);
                if(HasEnabledControllers)
                spriteBatch.Draw(yesBtn,enableControllerRect,Color.White);
                else
                    spriteBatch.Draw(noBtn, enableControllerRect, Color.White);
                spriteBatch.Draw(ExitBtn, ExitRect, Color.White);
            }

            if (isLobby)
            {
                spriteBatch.Draw(ingameBackground, new Rectangle(playerList[0].playerRect.X - (cam.myView.Bounds.Right / 2 - playerList[0].playerRect.Width / 2), cam.myView.Bounds.Center.Y - cam.myView.Bounds.Center.Y, 800, 600), Color.White);
                spriteBatch.DrawString(sF, "High Scores", new Vector2(playerList[0].playerRect.X - ((cam.myView.Bounds.Right / 2 - playerList[0].playerRect.Width / 2) - 350), 10), Color.Red);
                spriteBatch.DrawString(sF, "Name", new Vector2(playerList[0].playerRect.X - ((cam.myView.Bounds.Right / 2 - playerList[0].playerRect.Width / 2) - 96), 20), Color.White);
                spriteBatch.DrawString(sF, "Scores", new Vector2(playerList[0].playerRect.X - ((cam.myView.Bounds.Right / 2 - playerList[0].playerRect.Width / 2) - 612), 20), Color.White);

                spriteBatch.DrawString(sF, "Your Name:", new Vector2(playerList[0].playerRect.X - ((cam.myView.Bounds.Right / 2 - playerList[0].playerRect.Width / 2) - 100), 312), Color.White);
                spriteBatch.DrawString(sF, plName, new Vector2(playerList[0].playerRect.X - ((cam.myView.Bounds.Right / 2 - playerList[0].playerRect.Width / 2) - 300), 312), Color.White);

                for(int i = 0; i<scores.Count;i++)
                {
                    spriteBatch.DrawString(sF, (i + 1).ToString(), new Vector2(playerList[0].playerRect.X - ((cam.myView.Bounds.Right / 2 - playerList[0].playerRect.Width / 2) - 50), 25+(25*(i+1))), Color.Red);
                    spriteBatch.DrawString(sF, scores[i].Name, new Vector2(playerList[0].playerRect.X - ((cam.myView.Bounds.Right / 2 - playerList[0].playerRect.Width / 2) - 96), 25 + (25 * (i + 1))), Color.Red);
                    spriteBatch.DrawString(sF, scores[i].Score.ToString(), new Vector2(playerList[0].playerRect.X - ((cam.myView.Bounds.Right / 2 - playerList[0].playerRect.Width / 2) - 612), 25 + (25 * (i + 1))), Color.White);
                }
                spriteBatch.Draw(StartBtn, StartRect, Color.White);
               
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

                if (levelEditor_IsAmmo)
                {
                    spriteBatch.Draw(LevelEditor_Ammo.pickUpTexture, LevelEditor_Ammo.pickUpRect, new Rectangle(0,0,96,75),Color.White);
                }

                if (levelEditor_IsHealth)
                {
                    spriteBatch.Draw(LevelEditor_Health.pickUpTexture, LevelEditor_Health.pickUpRect, new Rectangle(0, 0, 96, 75), Color.White);
                }

                if (levelEditor_IsKey)
                {
                    spriteBatch.Draw(LevelEditor_Key.pickUpTexture, LevelEditor_Key.pickUpRect, new Rectangle(0,0,78,75), Color.White);
                }

                if (levelEditor_IsTree)
                    spriteBatch.Draw(LevelEditor_Tree.blockTexture, LevelEditor_Tree.blockRect, new Rectangle(0,0,96,96), Color.White);

                if (levelEditor_IsCheckPoint)
                    spriteBatch.Draw(LevelEditor_checkPoint.blockTexture, LevelEditor_checkPoint.blockRect, new Rectangle(0,0,96,96), Color.White);
                if (levelEditor_IsExit)
                    spriteBatch.Draw(LevelEditor_exit.blockTexture,LevelEditor_exit.blockRect,new Rectangle(0,0,70,100),Color.White);
                
            }
            //end
            spriteBatch.End();

            base.Draw(gameTime);
        }


        //saving and loading methods

        void LevelSave()
        {
            StreamWriter sw = new StreamWriter("Stage4.SWAG");

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

            sw.WriteLine(checkPointList[0].blockRect.X+","+checkPointList[0].blockRect.Y);
            sw.WriteLine("");

            sw.WriteLine(exitList[0].blockRect.X + "," + exitList[0].blockRect.Y);
            sw.WriteLine("");

            sw.WriteLine(ammoPickUp[0].pickUpRect.X+","+ammoPickUp[0].pickUpRect.Y);
            sw.WriteLine("");

            sw.WriteLine(healthPickUp[0].pickUpRect.X+","+healthPickUp[0].pickUpRect.Y);
            sw.WriteLine("");

            sw.WriteLine(KeyList[0].pickUpRect.X+","+KeyList[0].pickUpRect.Y);
            sw.WriteLine("");

            foreach (Blocks b in TreeList)
            {
                sw.WriteLine(b.blockRect.X + "," + b.blockRect.Y);
            }

            sw.WriteLine("");
            sw.WriteLine(treeSize);
            sw.Close();

        }

        void savePlayer()
        {
            LevelSave();
        }

        void loadPlayer(String fileName, int Life, int HP, int ammo)
        {
            StreamReader sr = new StreamReader(fileName);
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

            for(int i = treeSize; i > 0; i--)
            {
                TreeList.Remove(TreeList[i]);
            }

            if (snowmanSize != 0)
                snowmenList.Remove(snowmenList[0]);

            for(int i = IceWallSize; i>0; i--)
            {
                iceWallList.Remove(iceWallList[i]);
            }

            if (IceWallSize != 0)
                iceWallList.Remove(iceWallList[0]);

            if(playerList.Count!=0)
            playerList.Remove(playerList[0]);

            if (exitList.Count != 0)
            {
                exitList.Remove(exitList[0]);
                checkPointList.Remove(checkPointList[0]);
            }

            if (ammoPickUp.Count != 0)
            {
                ammoPickUp.Remove(ammoPickUp[0]);
            }

            if(healthPickUp.Count != 0)
            {
                healthPickUp.Remove(healthPickUp[0]);
            }

            if (KeyList.Count != 0)
            {
                KeyList.Remove(KeyList[0]);
            }

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

               
                Player P = new Player(new Rectangle(posX, posY, 64, 64), playerTex, Color.White, new Point(Window.ClientBounds.Width, Window.ClientBounds.Height),HasEnabledControllers);
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

            spaceEater = fileData;

            while((fileData = sr.ReadLine()) != null)
            {
                string[] PosData = fileData.Split(',');

                if (fileData == "")
                    break;

                int posX = int.Parse(PosData[0]);
                int posY = int.Parse(PosData[1]);

                Blocks b = new Blocks(new Rectangle(posX,posY,96,96),checkPointTexture);
                checkPointList.Add(b);
            }

            spaceEater = fileData;

            while ((fileData = sr.ReadLine()) != null)
            {
                string[] PosData = fileData.Split(',');

                if (fileData == "")
                    break;

                int posX = int.Parse(PosData[0]);
                int posY = int.Parse(PosData[1]);

                Blocks b = new Blocks(new Rectangle(posX, posY, 96, 96), exitTexture);
                exitList.Add(b);
            }

            spaceEater = fileData;

            while ((fileData = sr.ReadLine()) != null)
            {
                string[] PosData = fileData.Split(',');

                if (fileData == "")
                    break;

                int posX = int.Parse(PosData[0]);
                int posY = int.Parse(PosData[1]);

                PickUp b = new PickUp(new Rectangle(posX,posY,64,48),ammoTexture);
                ammoPickUp.Add(b);
            }

            spaceEater = fileData;

            while ((fileData = sr.ReadLine()) != null)
            {
                string[] PosData = fileData.Split(',');

                if (fileData == "")
                    break;

                int posX = int.Parse(PosData[0]);
                int posY = int.Parse(PosData[1]);

                PickUp b = new PickUp(new Rectangle(posX, posY, 64, 48), healthTexture);
                healthPickUp.Add(b);
            }

            spaceEater = fileData;

            while ((fileData = sr.ReadLine()) != null)
            {
                string[] PosData = fileData.Split(',');

                if (fileData == "")
                    break;

                int posX = int.Parse(PosData[0]);
                int posY = int.Parse(PosData[1]);

                PickUp b = new PickUp(new Rectangle(posX, posY, 48, 64), KeyTexture);
                KeyList.Add(b);
            }

            spaceEater = fileData;


            while ((fileData = sr.ReadLine()) != null)
            {
                string[] PosData = fileData.Split(',');

                if (fileData == "")
                    break;

                int posX = int.Parse(PosData[0]);
                int posY = int.Parse(PosData[1]);

                Blocks b = new Blocks(new Rectangle(posX,posY,96,96),TreeTex);
                TreeList.Add(b);
            }

            spaceEater = fileData;

            while ((fileData = sr.ReadLine()) != null)
            {
                if (fileData == "")
                    break;
                treeSize = int.Parse(fileData);
            }

            //key pickup
            sr.Close();

            playerList[0].PlayerLife = Life;
            playerList[0].PlayerHP = HP;
            playerList[0].ammoCounter = ammo;

            spawnPoint = new Point(playerList[0].playerRect.X,playerList[0].playerRect.Y);

        }
        //save and load finish

            //level editor section

        void LevelEditor()
        {
             
            if (devMode)
            {
                if(Keyboard.GetState().IsKeyDown(Keys.G) && !swappingBlocks)
                {
                    levelEditor_IsRegBlock = false;
                    levelEditor_IsSlipBlock = false;
                    levelEditor_IsWall = false;

                    levelEditor_IsSpikeBlock = false;

                    levelEditor_Issnowman = false;

                    levelEditor_IsAmmo = false;
                    levelEditor_IsHealth = false;
                    levelEditor_IsKey = false;

                    levelEditor_IsCheckPoint = false;
                    levelEditor_IsExit = false;

                    levelEditor_IsTree = true;

                    swappingBlocks = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D1) && !swappingBlocks)
                {

                    levelEditor_IsRegBlock = true;
                    levelEditor_IsSlipBlock = false;
                    levelEditor_IsWall = false;

                    levelEditor_IsSpikeBlock = false;

                    levelEditor_Issnowman = false;

                    levelEditor_IsAmmo = false;
                    levelEditor_IsHealth = false;
                    levelEditor_IsKey = false;

                    levelEditor_IsCheckPoint = false;
                    levelEditor_IsExit = false;

                    levelEditor_IsTree = false;

                    swappingBlocks = true;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D2) && !swappingBlocks)
                {
                    levelEditor_IsRegBlock = false;
                    levelEditor_IsSlipBlock = true;
                    levelEditor_IsWall = false;

                    levelEditor_IsSpikeBlock = false;

                    levelEditor_Issnowman = false;

                    levelEditor_IsAmmo = false;
                    levelEditor_IsHealth = false;
                    levelEditor_IsKey = false;

                    levelEditor_IsCheckPoint = false;
                    levelEditor_IsExit = false;

                    levelEditor_IsTree = false;

                    swappingBlocks = true;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D3) && !swappingBlocks)
                {
                    levelEditor_IsRegBlock = false;
                    levelEditor_IsSlipBlock = false;
                    levelEditor_IsWall = false;

                    levelEditor_IsSpikeBlock = true;

                    levelEditor_Issnowman = false;

                    levelEditor_IsAmmo = false;
                    levelEditor_IsHealth = false;
                    levelEditor_IsKey = false;

                    levelEditor_IsCheckPoint = false;
                    levelEditor_IsExit = false;

                    levelEditor_IsTree = false;

                    swappingBlocks = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D4) && !swappingBlocks)
                {
                    levelEditor_IsRegBlock = false;
                    levelEditor_IsSlipBlock = false;
                    levelEditor_IsWall = false;

                    levelEditor_IsSpikeBlock = false;

                    levelEditor_Issnowman = false;

                    levelEditor_IsAmmo = true;
                    levelEditor_IsHealth = false;
                    levelEditor_IsKey = false;

                    levelEditor_IsCheckPoint = false;
                    levelEditor_IsExit = false;

                    levelEditor_IsTree = false;

                    swappingBlocks = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D5) && !swappingBlocks)
                {
                    levelEditor_IsRegBlock = false;
                    levelEditor_IsSlipBlock = false;
                    levelEditor_IsWall = false;

                    levelEditor_IsSpikeBlock = false;

                    levelEditor_Issnowman = false;

                    levelEditor_IsAmmo = false;
                    levelEditor_IsHealth = true;
                    levelEditor_IsKey = false;

                    levelEditor_IsCheckPoint = false;
                    levelEditor_IsExit = false;

                    levelEditor_IsTree = false;

                    swappingBlocks = true;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D0) && !swappingBlocks)
                {
                    levelEditor_IsRegBlock = false;
                    levelEditor_IsSlipBlock = false;
                    levelEditor_IsWall = true;

                    levelEditor_IsSpikeBlock = false;

                    levelEditor_Issnowman = false;

                    levelEditor_IsAmmo = false;
                    levelEditor_IsHealth = false;
                    levelEditor_IsKey = false;

                    levelEditor_IsCheckPoint = false;
                    levelEditor_IsExit = false;

                    levelEditor_IsTree = false;

                    swappingBlocks = true;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D6) && !swappingBlocks)
                {
                    levelEditor_IsRegBlock = false;
                    levelEditor_IsSlipBlock = false;
                    levelEditor_IsWall = false;

                    levelEditor_IsSpikeBlock = false;

                    levelEditor_Issnowman = true;

                    levelEditor_IsAmmo = false;
                    levelEditor_IsHealth = false;
                    levelEditor_IsKey = false;

                    levelEditor_IsCheckPoint = false;
                    levelEditor_IsExit = false;

                    levelEditor_IsTree = false;

                    swappingBlocks = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D7) && !swappingBlocks)
                {
                    levelEditor_IsRegBlock = false;
                    levelEditor_IsSlipBlock = false;
                    levelEditor_IsWall = false;

                    levelEditor_IsSpikeBlock = false;

                    levelEditor_Issnowman = false;

                    levelEditor_IsAmmo = false;
                    levelEditor_IsHealth = false;
                    levelEditor_IsKey = true;

                    levelEditor_IsCheckPoint = false;
                    levelEditor_IsExit = false;

                    levelEditor_IsTree = false;

                    swappingBlocks = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D1) && !swappingBlocks)
                {
                    levelEditor_IsRegBlock = false;
                    levelEditor_IsSlipBlock = false;
                    levelEditor_IsWall = false;

                    levelEditor_IsSpikeBlock = false;

                    levelEditor_Issnowman = false;

                    levelEditor_IsAmmo = false;
                    levelEditor_IsHealth = false;
                    levelEditor_IsKey = false;

                    levelEditor_IsCheckPoint = false;
                    levelEditor_IsExit = false;

                    levelEditor_IsTree = false;

                    swappingBlocks = true;
                }


                if (Keyboard.GetState().IsKeyDown(Keys.D8) && !swappingBlocks) //check point
                {
                    levelEditor_IsRegBlock = false;
                    levelEditor_IsSlipBlock = false;
                    levelEditor_IsWall = false;

                    levelEditor_IsSpikeBlock = false;

                    levelEditor_Issnowman = false;

                    levelEditor_IsAmmo = false;
                    levelEditor_IsHealth = false;
                    levelEditor_IsKey = false;

                    levelEditor_IsCheckPoint = true;
                    levelEditor_IsExit = false;

                    levelEditor_IsTree = false;

                    swappingBlocks = true;
                }

                if(Keyboard.GetState().IsKeyDown(Keys.D9) && !swappingBlocks) //exit
                {
                    levelEditor_IsRegBlock = false;
                    levelEditor_IsSlipBlock = false;
                    levelEditor_IsWall = false;

                    levelEditor_IsSpikeBlock = false;

                    levelEditor_Issnowman = false;

                    levelEditor_IsAmmo = false;
                    levelEditor_IsHealth = false;
                    levelEditor_IsKey = false;

                    levelEditor_IsCheckPoint = false;
                    levelEditor_IsExit = true;

                    levelEditor_IsTree = false;

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
                }else
                    if (levelEditor_IsExit)
                {
                    //movement
                    if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    {
                        LevelEditor_exit.Move( LevelEditor_exit.blockRect.X - 3,  LevelEditor_exit.blockRect.Y);

                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    {
                         LevelEditor_exit.Move( LevelEditor_exit.blockRect.X + 3,  LevelEditor_exit.blockRect.Y);
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    {
                         LevelEditor_exit.Move( LevelEditor_exit.blockRect.X,  LevelEditor_exit.blockRect.Y - 3);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    {
                         LevelEditor_exit.Move( LevelEditor_exit.blockRect.X,  LevelEditor_exit.blockRect.Y + 3);
                    }
                    //move end

                    //input place
                    if (Keyboard.GetState().IsKeyDown(Keys.Space) && !isPlacingBlock)
                    {
                        // Blocks b = new Blocks(new Rectangle(LevelEditor_spikeBlock.blockRect.Location.X, LevelEditor_spikeBlock.blockRect.Location.Y, 16, 16), spikeBlockTex);
                        Blocks e = new Blocks(new Rectangle( LevelEditor_exit.blockRect.X,  LevelEditor_exit.blockRect.Y, 96, 96),exitTexture);
                        exitList.Add(e);

                        isPlacingBlock = true;
                    }

                    foreach (Blocks e in exitList)
                    {
                        if (LevelEditor_exit.blockRect.Intersects(e.blockRect))
                        {
                            if (Keyboard.GetState().IsKeyDown(Keys.B) && !isRemovingBlock)
                            {
                                exitList.Remove(e);
                                isRemovingBlock = true;
                                break;
                            }

                        }
                    }
                    if (Keyboard.GetState().IsKeyUp(Keys.B))
                    {
                        isRemovingBlock = false;
                    }
                }
                else
                    if (levelEditor_IsAmmo)
                {
                    
                    //movement
                    if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    {
                        LevelEditor_Ammo.Move(LevelEditor_Ammo.pickUpRect.X - 3, LevelEditor_Ammo.pickUpRect.Y);

                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    {
                        LevelEditor_Ammo.Move(LevelEditor_Ammo.pickUpRect.X + 3, LevelEditor_Ammo.pickUpRect.Y);
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    {
                        LevelEditor_Ammo.Move(LevelEditor_Ammo.pickUpRect.X, LevelEditor_Ammo.pickUpRect.Y - 3);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    {
                        LevelEditor_Ammo.Move(LevelEditor_Ammo.pickUpRect.X, LevelEditor_Ammo.pickUpRect.Y + 3);
                    }
                    //move end

                    //input place
                    if (Keyboard.GetState().IsKeyDown(Keys.Space) && !isPlacingBlock)
                    {
                        // Blocks b = new Blocks(new Rectangle(LevelEditor_spikeBlock.blockRect.Location.X, LevelEditor_spikeBlock.blockRect.Location.Y, 16, 16), spikeBlockTex);
                        PickUp e = new PickUp(new Rectangle(LevelEditor_Ammo.pickUpRect.X, LevelEditor_Ammo.pickUpRect.Y, 64, 48), ammoTexture);
                        ammoPickUp.Add(e);

                        isPlacingBlock = true;
                    }

                    foreach (PickUp e in ammoPickUp)
                    {
                        if (LevelEditor_Ammo.pickUpRect.Intersects(e.pickUpRect))
                        {
                            if (Keyboard.GetState().IsKeyDown(Keys.B) && !isRemovingBlock)
                            {
                                ammoPickUp.Remove(e);
                                isRemovingBlock = true;
                                break;
                            }

                        }
                    }
                    if (Keyboard.GetState().IsKeyUp(Keys.B))
                    {
                        isRemovingBlock = false;
                    }
                }
                else
                    if (levelEditor_IsHealth)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Left))
                        {
                        
                            LevelEditor_Health.Move(LevelEditor_Health.pickUpRect.X - 3, LevelEditor_Health.pickUpRect.Y);

                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Right))
                        {
                            LevelEditor_Health.Move(LevelEditor_Health.pickUpRect.X + 3, LevelEditor_Health.pickUpRect.Y);
                        }

                        if (Keyboard.GetState().IsKeyDown(Keys.Up))
                        {
                            LevelEditor_Health.Move(LevelEditor_Health.pickUpRect.X, LevelEditor_Health.pickUpRect.Y - 3);
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Down))
                        {
                            LevelEditor_Health.Move(LevelEditor_Health.pickUpRect.X, LevelEditor_Health.pickUpRect.Y + 3);
                        }
                        //input place
                        if (Keyboard.GetState().IsKeyDown(Keys.Space) && !isPlacingBlock)
                        {
                            // Blocks b = new Blocks(new Rectangle(LevelEditor_spikeBlock.blockRect.Location.X, LevelEditor_spikeBlock.blockRect.Location.Y, 16, 16), spikeBlockTex);
                            PickUp e = new PickUp(new Rectangle( LevelEditor_Health.pickUpRect.X,  LevelEditor_Health.pickUpRect.Y, 64, 48),healthTexture);
                            healthPickUp.Add(e);

                            isPlacingBlock = true;
                        }

                        foreach (PickUp e in healthPickUp)
                        {
                            if ( LevelEditor_Health.pickUpRect.Intersects(e.pickUpRect))
                            {
                                if (Keyboard.GetState().IsKeyDown(Keys.B) && !isRemovingBlock)
                                {
                                    healthPickUp.Remove(e);
                                    isRemovingBlock = true;
                                    break;
                                }

                            }
                        }
                        if (Keyboard.GetState().IsKeyUp(Keys.B))
                        {
                            isRemovingBlock = false;
                        }
                    }else
                        if (levelEditor_IsKey)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Left))
                        {

                            LevelEditor_Key.Move(LevelEditor_Key.pickUpRect.X - 3, LevelEditor_Key.pickUpRect.Y);

                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Right))
                        {
                            LevelEditor_Key.Move(LevelEditor_Key.pickUpRect.X + 3, LevelEditor_Key.pickUpRect.Y);
                        }

                        if (Keyboard.GetState().IsKeyDown(Keys.Up))
                        {
                            LevelEditor_Key.Move(LevelEditor_Key.pickUpRect.X, LevelEditor_Key.pickUpRect.Y - 3);
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Down))
                        {
                            LevelEditor_Key.Move(LevelEditor_Key.pickUpRect.X, LevelEditor_Key.pickUpRect.Y + 3);
                        }
                        //input place
                        if (Keyboard.GetState().IsKeyDown(Keys.Space) && !isPlacingBlock)
                        {
                            // Blocks b = new Blocks(new Rectangle(LevelEditor_spikeBlock.blockRect.Location.X, LevelEditor_spikeBlock.blockRect.Location.Y, 16, 16), spikeBlockTex);
                            PickUp e = new PickUp(new Rectangle(LevelEditor_Key.pickUpRect.X, LevelEditor_Key.pickUpRect.Y, 48, 64), KeyTexture);
                            KeyList.Add(e);

                            isPlacingBlock = true;
                        }

                        foreach (PickUp e in KeyList)
                        {
                            if (LevelEditor_Key.pickUpRect.Intersects(e.pickUpRect))
                            {
                                if (Keyboard.GetState().IsKeyDown(Keys.B) && !isRemovingBlock)
                                {
                                    KeyList.Remove(e);
                                    isRemovingBlock = true;
                                    break;
                                }

                            }
                        }
                        if (Keyboard.GetState().IsKeyUp(Keys.B))
                        {
                            isRemovingBlock = false;
                        }
                }else
                    if (levelEditor_IsTree)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    {
                        LevelEditor_Tree.Move(LevelEditor_Tree.blockRect.X - 3, LevelEditor_Tree.blockRect.Y);

                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    {
                        LevelEditor_Tree.Move(LevelEditor_Tree.blockRect.X + 3, LevelEditor_Tree.blockRect.Y);
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    {
                        LevelEditor_Tree.Move(LevelEditor_Tree.blockRect.X, LevelEditor_Tree.blockRect.Y - 3);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    {
                        LevelEditor_Tree.Move(LevelEditor_Tree.blockRect.X, LevelEditor_Tree.blockRect.Y + 3);
                    }
                    //input place
                    if (Keyboard.GetState().IsKeyDown(Keys.Space) && !isPlacingBlock)
                    {
                        // Blocks b = new Blocks(new Rectangle(LevelEditor_spikeBlock.blockRect.Location.X, LevelEditor_spikeBlock.blockRect.Location.Y, 16, 16), spikeBlockTex);
                        Blocks e = new Blocks(new Rectangle(LevelEditor_Tree.blockRect.X, LevelEditor_Tree.blockRect.Y, 96, 96), TreeTex);
                        TreeList.Add(e);

                        isPlacingBlock = true;
                    }

                    foreach (Blocks e in TreeList)
                    {
                        if (LevelEditor_Tree.blockRect.Intersects(e.blockRect))
                        {
                            if (Keyboard.GetState().IsKeyDown(Keys.B) && !isRemovingBlock)
                            {
                                TreeList.Remove(e);
                                isRemovingBlock = true;
                                break;
                            }

                        }
                    }
                    if (Keyboard.GetState().IsKeyUp(Keys.B))
                    {
                        isRemovingBlock = false;
                    }
                }

                if (Keyboard.GetState().IsKeyUp(Keys.Space))
                {
                    isPlacingBlock = false;
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

                    LevelEditor_Ammo.Move(p.playerRect.Location.X, p.playerRect.Location.Y);
                    LevelEditor_Health.Move(p.playerRect.Location.X, p.playerRect.Location.Y);
                    LevelEditor_Key.Move(p.playerRect.Location.X, p.playerRect.Location.Y);
                    LevelEditor_Tree.Move(p.playerRect.Location.X, p.playerRect.Location.Y);
                }
             //   LevelEditor_RegularBlock.Move(Mouse.GetState().X - (LevelEditor_RegularBlock.blockRect.Width * 2 + LevelEditor_RegularBlock.blockRect.Width / 3), Mouse.GetState().Y - LevelEditor_RegularBlock.blockRect.Height / 2);
                IsMouseVisible = false;
            }
        } //level editor end

        void saveScore()
        {
            StreamWriter sw = new StreamWriter("HS");

            ScoreTracker temp = new ScoreTracker(plName, score);
            scores.Add(temp);

            scores.Sort((a, b) => -1 * a.Score.CompareTo(b.Score));

            foreach (ScoreTracker s in scores)
            {
                sw.WriteLine(s.Name + "," + s.Score);
            }

            sw.Close();
        }

        void loadScore()
        {
            StreamReader sr = new StreamReader("HS");

            string fileData = "";

            while ((fileData = sr.ReadLine()) != null)
            {
                string[] data = fileData.Split(',');

                string prevName = data[0];
                int previousScore = int.Parse(data[1]);

                ScoreTracker temp = new ScoreTracker(prevName, previousScore);
                scores.Add(temp);
            }
            scores.Sort((a, b) => -1 * a.Score.CompareTo(b.Score));

            sr.Close();
        }
    }
}
