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
        WorldManager WorldManager1 = new WorldManager();
        ScreenManager ScreenManager1 = new ScreenManager();
        StorageDevice device;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //graphics.IsFullScreen = true;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            started = false;
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
           
            /*Manejador de ventanas*/
            ScreenManager1.CreateScreens(graphics,Content);


            spaceBackground = new MultiBackground(graphics);
            Texture2D spaceTexture = Content.Load<Texture2D>("game1");

            spaceBackground.AddLayer(spaceTexture, 0, 60);


            music = Content.Load<Song>("Mumbo_Jumbo");
            MediaPlayer.IsRepeating = true;

           

            WorldManager1.Start(Content);


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



          

            if ( keyPrevious.IsKeyUp(Keys.Escape) && key.IsKeyDown(Keys.Escape))
            {
                if (!ScreenManager1.getIntermediateScreen().Enable)
                {
                    ScreenManager1.getIntermediateScreen().Enable = true;
                }
                else {
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


            if (ScreenManager1.getStartScreen().Load.clicked||ScreenManager1.getIntermediateScreen().Load.clicked)
            {

                InitiateLoad();

                if (ScreenManager1.getStartScreen().Load.clicked)
                {
                    ScreenManager1.getStartScreen().Load.clicked = false;
                    ScreenManager1.getStartScreen().Play.clicked = true;
                }

                if (ScreenManager1.getIntermediateScreen().Load.clicked) {
                    ScreenManager1.getIntermediateScreen().Load.clicked = false;
                    ScreenManager1.getIntermediateScreen().Resume.clicked = true;
                
                }

            }


            WorldManager1.getCurrentWorld().Update(gameTime);
            ScreenManager1.update(gameTime);
            ControlScreen();
            WorldManager1.FinishLevel(player);

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
                Collision();
               

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


            if (ScreenManager1.getStartScreen().Exit.clicked||ScreenManager1.getIntermediateScreen().Exit.clicked)
            {
                this.Exit();
            }
         
            base.Update(gameTime);
        }

        private void ControlScreen()
        {
            
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
                if (e.AstralObject == true)
                {
                    e.State = false;
                    //WorldManager1.getCurrentWorld().tilemap[e.Y, e.X] = e.Type;
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
                if (e.AstralObject == true)
                {

                    e.State = true;
                    // WorldManager1.getCurrentWorld().tilemap[e.Y, e.X] = e.Type;
                }

            }



        }


      
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

          
            if (!ScreenManager1.getStartScreen().Enable && !ScreenManager1.getIntermediateScreen().Enable)
            {
                spaceBackground.Draw();
                WorldManager1.getCurrentWorld().Draw(spriteBatch);
                current.draw(spriteBatch, WorldManager1.getCurrentWorld().mapX);
                player.Draw(spriteBatch);
                
            }

          

            if (ScreenManager1.getStartScreen().Enable)
            {

                ScreenManager1.getStartScreen().Draw(spriteBatch);

            }

            if (ScreenManager1.getIntermediateScreen().Enable) {

                ScreenManager1.getIntermediateScreen().Draw(spriteBatch);
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

                if (elem.Scalable)
                {
                    if (elem.BlocksTop.Intersects(player.footBounds))
                    {
                        player.startY = player.worldPosition.Y;
                        player.gravity = 0f;

                    }
                    if (elem.Block.Intersects(player.rectangle) && Keyboard.GetState().IsKeyDown(Keys.Up))
                    {
                        player.gravity = 0f;
                        player.worldPosition.Y -= 2f;
                        player.cameraPosition.Y -= 2f;
                    }
                    if (elem.Block.Intersects(player.rectangle) && Keyboard.GetState().IsKeyDown(Keys.Down))
                    {
                        if (elem.BlocksTop.Intersects(player.footBounds))
                        {
                            player.gravity = 0f;
                            player.worldPosition.Y += 2f;
                            player.cameraPosition.Y += 2f;
                        }
                    }
                }

                if (elem.BlocksBottom.Intersects(player.topBounds) && elem.State)
                {

                    if ((player.topBounds.Y >= elem.BlocksBottom.Y))
                    {
                        player.gravity = 5f;
                        player.jump = false;
                        player.JumpSpeed = 0f;
                        //player.startY=;
                    }
                }

                if (elem.BlocksTop.Intersects(player.footBounds) && elem.State)
                {
                    if (elem.Type == 5)
                    {
                        elem.State = false;
                        WorldManager1.getCurrentWorld().tilemap[elem.Y, elem.X] = 0;
                        if (elem.AstralObject) WorldManager1.getCurrentWorld().astralObjects[elem.Y, elem.X] = 0;
                    }

                    player.gravity = 0f;
                    player.state = "stand";
                    player.startY = player.worldPosition.Y;
                }

                if (!elem.Scalable && elem.BlocksLeft.Intersects(player.rightRec) && elem.State)
                {
                    if (player.footBounds.Y >= elem.BlocksLeft.Y)
                    {
                        player.worldPosition.X -= 5f;
                        player.cameraPosition.X -= 5f;
                    }
                }

                if (!elem.Scalable && elem.BlocksRight.Intersects(player.leftRec) && elem.State)
                {
                    if (player.footBounds.Y >= elem.BlocksRight.Y)
                    {
                        player.worldPosition.X += 5f;
                        player.cameraPosition.X += 5f;
                    }
                }
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
                    pcolor = player.pcolor,
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
                player.pcolor = save.pcolor;
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
