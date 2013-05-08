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
        TileMap map;
        Song music;
        bool started;
        StartMenuScreen StartScreen;
        MultiBackground spaceBackground;
         


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //graphics.IsFullScreen = true;
            graphics.PreferredBackBufferHeight=600 ;
            graphics.PreferredBackBufferWidth = 800;
            started = false;
            StartScreen = new StartMenuScreen();
            this.IsMouseVisible = true;
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new Player();
            Texture2D tex = Content.Load<Texture2D>("Mumbo_SpSheets");
            player.LoadContent(tex);
            map = new TileMap();
            Texture2D tex1 = Content.Load<Texture2D>("in");
            Texture2D tex2 = Content.Load<Texture2D>("marioblock[1]");
            Texture2D tex3 = Content.Load<Texture2D>("ladder2");
            Texture2D tex4 = Content.Load<Texture2D>("Ice_Block");
            Texture2D tex5 = Content.Load<Texture2D>("Ice_Block");
            Texture2D tex6 = Content.Load<Texture2D>("EnemySprite");
            map.Texlis = new List<Texture2D>();
            map.Texlis.Add(tex1);
            map.Texlis.Add(tex2);
            map.Texlis.Add(tex3);
            map.Texlis.Add(tex4);
            map.Texlis.Add(tex5);
            map.Texlis.Add(tex6);

            //background inicializacion
            spaceBackground = new MultiBackground(graphics);
            Texture2D spaceTexture = Content.Load<Texture2D>("game1");

            spaceBackground.AddLayer(spaceTexture, 0, 60);
            
            
            music = Content.Load<Song>("Mumbo_Jumbo");
            MediaPlayer.IsRepeating = true;

            StartScreen.LoadContent(graphics, Content);
            

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        
        protected override void Update(GameTime gameTime)
        {
            
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            KeyboardState key = Keyboard.GetState();
            if (key.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            

            map.Update(gameTime);
            StartScreen.Update();

            if (StartScreen.Play.clicked)
            {

                if (!started)
                {
                    MediaPlayer.Play(music);
                    started = true;
                }
                
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
      

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
               
            // TODO: Add your drawing code here
            if (StartScreen.Play.clicked)
            {
                spaceBackground.Draw();
                map.Draw(spriteBatch);
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
                map.mapX += (int)cameraSpeed;
                player.cameraPosition.X -= 5f;
            }

            if (player.worldPosition.X <= 10)
            {
                player.worldPosition.X = 10;
                player.cameraPosition.X = 10;
            }
            if (map.mapX < 10)
            {
                map.mapX = 10;
                player.cameraPosition.X -= 5f;
            }

            if (player.cameraPosition.X < graphics.GraphicsDevice.Viewport.Width / 2 && player.facing == "left")
            {
                player.cameraPosition.X += 5f;
                map.mapX -= (int)cameraSpeed;
            }
            if (map.mapX > map.MapW - graphics.GraphicsDevice.Viewport.Width)
            {
                map.mapX = map.MapW - graphics.GraphicsDevice.Viewport.Width;
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
      

            foreach (WorldElement elem in map.elements)
            {
                if (elem.BlocksBottom.Intersects(player.topBounds) && elem.state != false )
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
                        map.tilemap[elem.Y, elem.X] = 0;
                    }

                    player.gravity = 0f;
                    player.state = "stand";
                    player.startY = player.worldPosition.Y;
                }

                if (elem.Type != 2 && elem.BlocksLeft.Intersects(player.rightRec) && elem.state!=false)
                {
                    if (player.footBounds.Y >= elem.BlocksLeft.Y)
                    {
                        player.worldPosition.X -= 5f;
                        player.cameraPosition.X -= 5f;
                    }
                }

                if (elem.Type != 2 && elem.BlocksRight.Intersects(player.leftRec) && elem.state != false )
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
