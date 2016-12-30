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

        List<Blocks> RegularBlockList;
        List<Blocks> SkateBlockList;

        List<Player> playerList;

        List<Projectile> projectiles;

        Texture2D shotgunPellet;

        Texture2D playerTex;
        Texture2D blockTex;
        Texture2D skateBlockTex;

        SpriteFont sF;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //player related methods

            foreach (Player pl in playerList) {
                pl.move();

                if (Keyboard.GetState().IsKeyDown(Keys.Space) && !pl.shooting)
                {
                    if (pl.ammoCounter > 0)
                    {
                        if (pl.IdlingLeft || pl.MoveLeft)
                        {
                            //if player has shotgun do this
                            Projectile p1 = new Projectile(new Rectangle(_player.playerRect.X, _player.playerRect.Y + 16, 16, 16), shotgunPellet, Color.White, -10, 1);
                            Projectile p2 = new Projectile(new Rectangle(_player.playerRect.X, _player.playerRect.Y + 16, 16, 16), shotgunPellet, Color.White, -10, 0);
                            Projectile p3 = new Projectile(new Rectangle(_player.playerRect.X, _player.playerRect.Y + 16, 16, 16), shotgunPellet, Color.White, -10, -1);

                            projectiles.Add(p1);
                            projectiles.Add(p2);
                            projectiles.Add(p3);
                            //end shotgun condition
                        }
                        if (pl.IdlingRight || pl.MoveRight)
                        {
                            //if player has shotgun do this
                            Projectile p1 = new Projectile(new Rectangle(_player.playerRect.X + _player.playerRect.Width, _player.playerRect.Y + 16, 16, 16), shotgunPellet, Color.White, 10, 1);
                            Projectile p2 = new Projectile(new Rectangle(_player.playerRect.X + _player.playerRect.Width, _player.playerRect.Y + 16, 16, 16), shotgunPellet, Color.White, 10, 0);
                            Projectile p3 = new Projectile(new Rectangle(_player.playerRect.X + _player.playerRect.Width, _player.playerRect.Y + 16, 16, 16), shotgunPellet, Color.White, 10, -1);

                            projectiles.Add(p1);
                            projectiles.Add(p2);
                            projectiles.Add(p3);
                            //end shotgun condition
                        }
                        _player.shooting = true;
                    }
                }
                else
                    if (Keyboard.GetState().IsKeyUp(Keys.Space))
                {
                    _player.shooting = false;
                }

                _player.DoPhysics();

                foreach (Blocks b in RegularBlockList)
                {
                    _player.Colliders(b);
                }

                foreach (Blocks sb in SkateBlockList)
                {
                    _player.Colliders(sb);
                }

                _player.Animation();
            }
                //save and load inputs
                if (Keyboard.GetState().IsKeyDown(Keys.F))
                {
                    savePlayer();
                }

                if (Keyboard.GetState().IsKeyDown(Keys.L))
                {
                    loadPlayer();
                }
                //end sve and load

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

            cam.setToCenter(_player.playerRect,new Point(Window.ClientBounds.Width,Window.ClientBounds.Height));
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
               
                spriteBatch.DrawString(sF,"X:" + _player.playerRect.X +" Y:"+ _player.playerRect.Y, new Vector2(_player.playerRect.X -( cam.myView.Bounds.Right/2 - _player.playerRect.Width/2) , 0),Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }


        //saving and loading methods

        void gameplaySave()
        {
            StreamWriter sw = new StreamWriter("GameplaySave.SWAG");

            sw.WriteLine(_player.playerRect.X+","+_player.playerRect.Y);

            sw.Close();
        }

        void savePlayer()
        {
            gameplaySave();
        }

        void loadPlayer()
        {
            StreamReader sr = new StreamReader("GameplaySave.SWAG");

            foreach (Player p in playerList)
            {
                playerList.Remove(p);
                break;
            }

            string fileData = "";

            while ((fileData=sr.ReadLine())!=null)
            {
                string[] playerPosData = fileData.Split(',');

                if (fileData == "")
                {
                    break;
                }

                int posX = int.Parse(playerPosData[0]);
                int posY = int.Parse(playerPosData[1]);

                Player P = new Player(new Rectangle(posX, posY, 64, 64), playerTex, Color.White, new Point(Window.ClientBounds.Width, Window.ClientBounds.Height));
                playerList.Add(P);
            }
        }
        //save and load finish
    }
}
