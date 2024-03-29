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
using microsoft.servicemodel.samples;
using System.Windows.Forms;


namespace MumboJumbo
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        string containerName = "MyGamesStorage";
        string filename = "mysave.sav";
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Nicks nick = null;
        ScoresTable sc = null;
        

        Song music;
        bool started;

        MultiBackground spaceBackground;
        MultiBackground cloudBackground;
        
        WorldManager WorldManager1 = new WorldManager();
        ScreenManager ScreenManager1 = new ScreenManager();
        StorageDevice device;
        static public double TimeInAstral=0;
        static public ServiceClient client = new ServiceClient();
        static public int idPlayer=-1;
        static public GameTime gametime;


        public Game1()
        {
            
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;           
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            started = false;
            this.IsMouseVisible = true;        
  

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
            cloudBackground = new MultiBackground(graphics);

            Texture2D spaceTexture = Content.Load<Texture2D>("game1");
            Texture2D cloudTexture = Content.Load<Texture2D>("bg_clouds");

            spaceBackground.AddLayer(spaceTexture, 0, 60);
            cloudBackground.AddLayer(cloudTexture, 0, 60);

            spaceBackground.StartMoving();
            cloudBackground.StartMoving();

            spaceBackground.SetMoveRightLeft();

            music = Content.Load<Song>("Mumbo_Jumbo");
            MediaPlayer.IsRepeating = false;

           

            WorldManager1.Start(Content);
            player.PlayerIni.save(player, WorldManager1.getCurrentWorld());


        }

        
        protected override void UnloadContent()
        {
            
        }

        public KeyboardState keyPrevious;
        public KeyboardState key;

        protected override void Update(GameTime gameTime)
        
        {
           gametime = gameTime;
           
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                this.Exit();
            keyPrevious = key;
            key = Keyboard.GetState();


            if (keyPrevious.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Escape) && key.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
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

            if (ScreenManager1.getStartScreen().Nick.clicked)
            {
                ScreenManager1.getStartScreen().Nick.clicked = false;
                if (nick == null)
                {
                    nick = new Nicks();
                    nick.Show();
                }

                if (nick.IsDisposed)
                {
                    nick = null;
                }

            }


            if (ScreenManager1.getStartScreen().Score.clicked) {
                ScreenManager1.getStartScreen().Score.clicked = false;

                if (sc == null)
                {
                    sc = new ScoresTable();
                    sc.Show();
                }

                if (sc.IsDisposed) {
                    sc = null;
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

            if (ScreenManager1.getGameOver().Play.clicked)
            {

                player.Lives = 5;
                player.cameraPosition = new Vector2 (0,0);
                player.worldPosition = new Vector2(0, 0);
                WorldManager1.level = 0;
                WorldManager1.Start(Content);

                ScreenManager1.getStartScreen().Enable = true;
                ScreenManager1.getGameOver().Enable = false;
                ScreenManager1.getGameOver().Play.clicked = false;
            }

         
            WorldManager1.getCurrentWorld().Update(gameTime, player);

          
            ScreenManager1.update(gameTime);

            int lvlAnt = WorldManager1.level;
            WorldManager1.FinishLevel(player);

            if (WorldManager1.level > lvlAnt)
            {
                player.PlayerIni.load(player, WorldManager1.getCurrentWorld());
            }

            if (!ScreenManager1.getStartScreen().Enable && !ScreenManager1.getIntermediateScreen().Enable)
            {

                if (!started)
                {
                    MediaPlayer.Play(music);
                    started = true;

                }

                spaceBackground.Update(gameTime);
                cloudBackground.Update(gametime);
                player.Update(gameTime);
  
                Camera();
            }


            if (ScreenManager1.getStartScreen().Play.clicked)
            {
                if (idPlayer == -1)
                {
                    try
                    {
                        idPlayer = client.AddPlayer("Anonimus", 0);
                    }
                    catch (Exception e) { }
                }
                player.scoreTime = gameTime.TotalGameTime.TotalMinutes;
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
                player.PlayerIni.load(player, WorldManager1.getCurrentWorld());
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
                cloudBackground.Draw();
                WorldManager1.getCurrentWorld().Draw(spriteBatch);
                player.Draw(spriteBatch);

                if (player.astralMode) {
                    player.astralCorp.draw(spriteBatch,WorldManager1.getCurrentWorld().mapX);
                }
                
                
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


                Vector2[] Elements0 = new Vector2[WorldManager1.getCurrentWorld().elements.Count];
                bool[] elembool = new bool[WorldManager1.getCurrentWorld().elements.Count];
            
                int i = 0;
                foreach (WorldElement e in WorldManager1.getCurrentWorld().elements) {
                    Elements0[i] = e.position;
                    elembool[i] = e.State;
                    i++;
                
                }


                

                Vector2[] Enemies0 = new Vector2[WorldManager1.getCurrentWorld().enemies.Count];
                bool[] enebool = new bool[WorldManager1.getCurrentWorld().enemies.Count];
                i = 0;
                foreach (Enemy e in WorldManager1.getCurrentWorld().enemies) {
                    Enemies0[i] = e.worldPosition;
                    enebool[i] = e.IsAlive;
                    i++;
                
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
                    mapX = WorldManager1.getCurrentWorld().mapX,
                    mapW = WorldManager1.getCurrentWorld().MapW,
                    source = player.source,
                    rectangle = player.rectangle,
                    astral = player.astralMode,
                    ene_state = enebool,
                    ele_state=elembool,
                    Enemies=Enemies0,
                    Elements=Elements0          

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
                int i = 0 ;
                foreach (WorldElement e in WorldManager1.getCurrentWorld().elements) {

                    e.position = save.Elements[i];
                    e.State = save.ele_state[i];
                    i++;
                
                }

                i = 0;
                foreach (Enemy en in WorldManager1.getCurrentWorld().enemies) {
                    en.worldPosition = save.Enemies[i];
                    en.isAlive = save.ene_state[i];
                    i++;
                
                
                }




            }
        }

    }
}
