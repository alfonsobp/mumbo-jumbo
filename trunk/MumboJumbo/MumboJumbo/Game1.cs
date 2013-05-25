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
using Microsoft.Xna.Framework.Storage;
using System.Xml.Serialization;
using System.IO;
namespace MumboJumbo
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        string containerName = "MyGamesStorage";
        string filename = "mysave.sav";
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        bool AstralMode = false;

        Song music;
        bool started;

        MultiBackground spaceBackground;
        double timeElap;
        Astral current;
        Astral playerIni;
        WorldManager WorldManager1 = new WorldManager();
        ScreenManager ScreenManager1 = new ScreenManager();
        StorageDevice device;
        static public double TimeInAstral=0;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferHeight = 900;
            graphics.PreferredBackBufferWidth = 1600;
            started = false;
            this.IsMouseVisible = true;
            current = new Astral();
            playerIni = new Astral();


        }


     
        protected override void Initialize()
        {
         
            base.Initialize();
        }

        
        protected override void LoadContent()
        {
           
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new Player(Content);
            
            
           
            /*Manejador de ventanas*/
            ScreenManager1.CreateScreens(graphics,Content);


            spaceBackground = new MultiBackground(graphics);
            Texture2D spaceTexture = Content.Load<Texture2D>("game1");

            spaceBackground.AddLayer(spaceTexture, 0, 60);


            music = Content.Load<Song>("Mumbo_Jumbo");
            MediaPlayer.IsRepeating = true;

           

            WorldManager1.Start(Content);
            playerIni.save(player, WorldManager1.getCurrentWorld());


        }

        
        protected override void UnloadContent()
        {
            
        }

        public KeyboardState keyPrevious;
        public KeyboardState key;

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            keyPrevious = key;
            key = Keyboard.GetState();


            if (keyPrevious.IsKeyUp(Keys.Escape) && key.IsKeyDown(Keys.Escape))
            {
                if (!ScreenManager1.getIntermediateScreen().Enable)
                {
                    ScreenManager1.getIntermediateScreen().Enable = true;
                }
                else
                {
                    ScreenManager1.getIntermediateScreen().Enable = false;

                }

            }

            if (ScreenManager1.getIntermediateScreen().Save.clicked)
            {
                if (!player.astralMode)
                {
                    InitiateSave();

                }
                ScreenManager1.getIntermediateScreen().Save.clicked = false;
            }

            if (ScreenManager1.getStartScreen().Load.clicked || ScreenManager1.getIntermediateScreen().Load.clicked)
            {

                InitiateLoad();

                if (ScreenManager1.getStartScreen().Load.clicked)
                {
                    ScreenManager1.getStartScreen().Load.clicked = false;
                    ScreenManager1.getStartScreen().Play.clicked = true;
                }

                if (ScreenManager1.getIntermediateScreen().Load.clicked)
                {
                    ScreenManager1.getIntermediateScreen().Load.clicked = false;
                    ScreenManager1.getIntermediateScreen().Resume.clicked = true;

                }

            }

            WorldManager1.getCurrentWorld().Update(gameTime, player);
            ScreenManager1.update(gameTime);

            int lvlAnt = WorldManager1.level;
            WorldManager1.FinishLevel(player);

            if (WorldManager1.level > lvlAnt)
            {
                playerIni.load(player, WorldManager1.getCurrentWorld());
            }

            if (!ScreenManager1.getStartScreen().Enable && !ScreenManager1.getIntermediateScreen().Enable)
            {

                if (!started)
                {
                    MediaPlayer.Play(music);
                    started = true;

                }

                spaceBackground.Update(gameTime);
                player.Update(gameTime);
                Camera();
            }


            if (ScreenManager1.getStartScreen().Play.clicked)
            {
                ScreenManager1.getStartScreen().Enable = false;
                ScreenManager1.getStartScreen().Play.clicked = false;
            }

            if (ScreenManager1.getIntermediateScreen().Resume.clicked)
            {

                ScreenManager1.getIntermediateScreen().Enable = false;
                ScreenManager1.getIntermediateScreen().Resume.clicked = false;
            }

            if (player.Lives == 0)
                ScreenManager1.getGameOver().Enable = true;

            if (!player.Life)
            {
                playerIni.load(player, WorldManager1.getCurrentWorld());
            }

            if (ScreenManager1.getStartScreen().Exit.clicked || ScreenManager1.getIntermediateScreen().Exit.clicked)
            {
                this.Exit();
            }

            base.Update(gameTime);
        }

  
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

          
            if (!ScreenManager1.getStartScreen().Enable && !ScreenManager1.getIntermediateScreen().Enable)
            {
                spaceBackground.Draw();
                WorldManager1.getCurrentWorld().Draw(spriteBatch);
                player.Draw(spriteBatch);
                
            }

          

            if (ScreenManager1.getStartScreen().Enable)
            {

                ScreenManager1.getStartScreen().Draw(spriteBatch);

            }

            if (ScreenManager1.getIntermediateScreen().Enable) {

                ScreenManager1.getIntermediateScreen().Draw(spriteBatch);
            }

            if (ScreenManager1.getGameOver().Enable) {

                ScreenManager1.getGameOver().Draw(spriteBatch);

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


        private void InitiateSave()
        {
            device = null;
            StorageDevice.BeginShowSelector(PlayerIndex.One, this.SaveToDevice, null);
        }

        private void SaveToDevice(IAsyncResult result)
        {
            device = StorageDevice.EndShowSelector(result);
            if (device != null && device.IsConnected)
            {


                int[] convertion = new int[WorldManager1.getCurrentWorld().tilemap.GetLength(1) * WorldManager1.getCurrentWorld().tilemap.GetLength(0)];

                for (int i = 0; i < WorldManager1.getCurrentWorld().tilemap.GetLength(1); i++)
                {
                    for (int j = 0; j < WorldManager1.getCurrentWorld().tilemap.GetLength(0); j++)
                    {

                        convertion[i * WorldManager1.getCurrentWorld().tilemap.GetLength(0) + j] = WorldManager1.getCurrentWorld().tilemap[j, i];
                    }



                }


                bool[] lbool = new bool[WorldManager1.getCurrentWorld().elements.Count];

                for (int i = 0; i < lbool.GetLength(0); i++)
                {

                    lbool[i] = WorldManager1.getCurrentWorld().elements[i].State;
                }

                Save SaveData = new Save()
                {
                    //text = player.Texture,
                    cameraPosition = player.cameraPosition,
                    worldPosition = player.worldPosition,
                    facing = player.facing,
                    state = player.state,
                    jump = player.jump,
                    jumpSpeed = player.jumpSpeed,
                    gravity = player.gravity,
                    speed = player.speed,
                    prevstate = player.prevstate,
                    frameSize = player.frameSize,
                    currentFrame = player.currentFrame,
                  
                    level = WorldManager1.level,
                    tilemap = convertion,
                    mapX = WorldManager1.getCurrentWorld().mapX,
                    mapW = WorldManager1.getCurrentWorld().MapW,
                    source = player.source,
                    rectangle = player.rectangle,
                    astral = player.astralMode,
                    lstate = lbool



                };
                IAsyncResult r = device.BeginOpenContainer(containerName, null, null);
                result.AsyncWaitHandle.WaitOne();
                StorageContainer container = device.EndOpenContainer(r);
                if (container.FileExists(filename))
                    container.DeleteFile(filename);
                Stream stream = container.CreateFile(filename);
                XmlSerializer serializer = new XmlSerializer(typeof(Save));
                serializer.Serialize(stream, SaveData);
                stream.Close();
                container.Dispose();
                result.AsyncWaitHandle.Close();
            }
        }

        private void InitiateLoad()
        {
            device = null;
            StorageDevice.BeginShowSelector(PlayerIndex.One, this.LoadFromDevice, null);
        }

        void LoadFromDevice(IAsyncResult result)
        {
            device = StorageDevice.EndShowSelector(result);
            IAsyncResult r = device.BeginOpenContainer(containerName, null, null);
            result.AsyncWaitHandle.WaitOne();
            StorageContainer container = device.EndOpenContainer(r);
            result.AsyncWaitHandle.Close();
            if (container.FileExists(filename))
            {
                Stream stream = container.OpenFile(filename, FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(Save));
                Save save = (Save)serializer.Deserialize(stream);
                stream.Close();
                container.Dispose();
                //Update the game based on the save game file

                // player.Texture = save.text;
                player.cameraPosition = save.cameraPosition;
                player.worldPosition = save.worldPosition;
                player.facing = save.facing;
                player.state = save.state;
                player.jump = save.jump;
                player.jumpSpeed = save.jumpSpeed;
                player.gravity = save.gravity;
                player.speed = save.speed;
                player.prevstate = save.prevstate;
                player.frameSize = save.frameSize;
                player.currentFrame = save.currentFrame;
                
                WorldManager1.level = save.level;

                WorldManager1.getCurrentWorld().mapX = save.mapX;
                WorldManager1.getCurrentWorld().MapW = save.mapW;
                player.source = save.source;
                player.rectangle = save.rectangle;

                player.astralMode = save.astral;



                for (int i = 0; i < WorldManager1.getCurrentWorld().tilemap.GetLength(1); i++)
                {
                    for (int j = 0; j < WorldManager1.getCurrentWorld().tilemap.GetLength(0); j++)
                    {

                        WorldManager1.getCurrentWorld().tilemap[j, i] = save.tilemap[i * WorldManager1.getCurrentWorld().tilemap.GetLength(0) + j];

                    }

                }

                for (int i = 0; i < save.lstate.GetLength(0); i++)
                {

                    WorldManager1.getCurrentWorld().elements[i].State = save.lstate[i];
                }



            }
        }

    }
}
