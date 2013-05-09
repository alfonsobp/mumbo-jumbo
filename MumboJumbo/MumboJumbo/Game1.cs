using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using xnaExtras;
namespace MumboJumbo
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        bool AstralMode = false;

        Song music;
        bool started;
        StartMenuScreen StartScreen;
        MultiBackground spaceBackground;
        double timeElap;
        Astral current;
        WorldManager WorldManager1 = new WorldManager();
        ScreenManager ScreenManager1 = new ScreenManager();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //graphics.IsFullScreen = true;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            started = false;
            StartScreen = new StartMenuScreen();
            this.IsMouseVisible = true;
            current = new Astral();

        }


     
        protected override void Initialize()
        {
         
            base.Initialize();
        }

        
        protected override void LoadContent()
        {
           
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new Player();
            Texture2D tex = Content.Load<Texture2D>("Mumbo_SpSheets");
            player.LoadContent(tex);




           
            spaceBackground = new MultiBackground(graphics);
            Texture2D spaceTexture = Content.Load<Texture2D>("game1");

            spaceBackground.AddLayer(spaceTexture, 0, 60);


            music = Content.Load<Song>("Mumbo_Jumbo");
            MediaPlayer.IsRepeating = true;

            StartScreen.LoadContent(graphics, Content);

            WorldManager1.Start(Content);


        }

        
        protected override void UnloadContent()
        {
            
        }

      
        protected override void Update(GameTime gameTime)
        {



            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            
            KeyboardState key = Keyboard.GetState();

            if (key.IsKeyDown(Keys.A))
            {
                if (!AstralMode)
                {
                    OnAstralMode();
                    timeElap = gameTime.TotalGameTime.TotalSeconds;
                }

            }

            if (AstralMode && gameTime.TotalGameTime.TotalSeconds - timeElap >= 5)
            {
                OffAstralMode();
            }



            if (key.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }



            WorldManager1.getCurrentWorld().Update(gameTime);

            StartScreen.Update();

            if (StartScreen.Play.clicked)
            {

                if (!started)
                {
                    MediaPlayer.Play(music);
                    started = true;

                }

                WorldManager1.FinishLevel(player);
                spaceBackground.Update(gameTime);
                player.Update(gameTime);
                Camera();
                Collision();
            }

            if (StartScreen.Exit.clicked)
            {
                this.Exit();
            }
            // TODO: Add your update logic here


            base.Update(gameTime);
        }

        public void OffAstralMode()
        {
            WorldManager1.getCurrentWorld().astralMode = false;
            AstralMode = false;
            player.astralMode = false;



            /*Devolver los valores que cambiaste en astralMode*/

            current.load(player, WorldManager1.getCurrentWorld());
            
            /*Cambiar de estado a todos los elementos*/
            foreach (WorldElement e in WorldManager1.getCurrentWorld().elements)
            {
                if (e.Type == 4)
                {
                    e.state = false;

                    WorldManager1.getCurrentWorld().tilemap[e.Y, e.X] = e.Type;
                }

            }


        }

        public void OnAstralMode()
        {
            WorldManager1.getCurrentWorld().astralMode = true;
            AstralMode = true;
            player.astralMode = true;

            /*Guardar en Current las posiciones en el momento de hacer astralMode*/

            current.save(player, WorldManager1.getCurrentWorld());

            /*Cambiar el estado a todos los elementos */
            foreach (WorldElement e in WorldManager1.getCurrentWorld().elements)
            {
                if (e.Type == 4)
                {
                    e.state = true;

                    WorldManager1.getCurrentWorld().tilemap[e.Y, e.X] = e.Type;
                }

            }



        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

          
            if (StartScreen.Play.clicked)
            {
                spaceBackground.Draw();
                WorldManager1.getCurrentWorld().Draw(spriteBatch);
                current.draw(spriteBatch, WorldManager1.getCurrentWorld().mapX);
                player.Draw(spriteBatch);
            }

            if (!StartScreen.Play.clicked)
            {

                StartScreen.Draw(spriteBatch);

            }

            base.Draw(gameTime);
        }

        public void Camera()
        {
            float cameraSpeed = 5f;
            if (player.cameraPosition.X > graphics.GraphicsDevice.Viewport.Width / 2)
            {
                WorldManager1.getCurrentWorld().mapX += (int)cameraSpeed;
                player.cameraPosition.X -= 5f;
            }

            if (player.worldPosition.X <= 10)
            {
                player.worldPosition.X = 10;
                player.cameraPosition.X = 10;
            }
            if (WorldManager1.getCurrentWorld().mapX < 10)
            {
                WorldManager1.getCurrentWorld().mapX = 10;
                player.cameraPosition.X -= 5f;
            }

            if (player.cameraPosition.X < graphics.GraphicsDevice.Viewport.Width / 2)
            {
                player.cameraPosition.X += 5f;
                WorldManager1.getCurrentWorld().mapX -= (int)cameraSpeed;
            }
            if (WorldManager1.getCurrentWorld().mapX > WorldManager1.getCurrentWorld().MapW - graphics.GraphicsDevice.Viewport.Width)
            {
                WorldManager1.getCurrentWorld().mapX = WorldManager1.getCurrentWorld().MapW - graphics.GraphicsDevice.Viewport.Width;
                player.cameraPosition.X += 5f;

            }

            if (player.cameraPosition.X > graphics.GraphicsDevice.Viewport.Width)
            {
                player.cameraPosition.X -= 10f;
                player.worldPosition.X -= 10f;
            }

            if (player.worldPosition.Y >= graphics.GraphicsDevice.Viewport.Height)
            {
                player.worldPosition.Y = 0;
                player.cameraPosition.Y = 0;
            }
        }

        public void Collision()
        {


            foreach (WorldElement elem in WorldManager1.getCurrentWorld().elements)
            {
                if (elem.BlocksBottom.Intersects(player.topBounds) && elem.state != false)
                {
                    if (elem.Type == 2)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Up))
                        {

                            player.worldPosition.Y -= 3f;
                            player.cameraPosition.Y -= 3f;
                        }
                    }
                    if ((player.topBounds.Y >= elem.BlocksBottom.Y))
                    {
                        player.gravity = 5f;
                        player.jump = false;
                        player.JumpSpeed = 0f;
                        //player.startY=;
                    }
                }

                if (elem.BlocksTop.Intersects(player.footBounds) && elem.state != false)
                {
                    if (elem.Type == 5)
                    {
                        elem.state = false;
                        WorldManager1.getCurrentWorld().tilemap[elem.Y, elem.X] = 0;
                    }

                    player.gravity = 0f;
                    player.state = "stand";
                    player.startY = player.worldPosition.Y;
                }

                if (elem.Type != 2 && elem.BlocksLeft.Intersects(player.rightRec) && elem.state != false)
                {
                    if (player.footBounds.Y >= elem.BlocksLeft.Y)
                    {
                        player.worldPosition.X -= 5f;
                        player.cameraPosition.X -= 5f;
                    }
                }

                if (elem.Type != 2 && elem.BlocksRight.Intersects(player.leftRec) && elem.state != false)
                {
                    if (player.footBounds.Y >= elem.BlocksRight.Y)
                    {
                        player.worldPosition.X += 5f;
                        player.cameraPosition.X += 5f;
                    }
                }
            }

        }


    }
}
