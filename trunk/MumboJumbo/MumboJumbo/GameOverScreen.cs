using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MumboJumbo
{
    class GameOverScreen
    {
        public Texture2D background;
        public bool Enable = false;


        public Button Play;

        public void LoadContent(GraphicsDeviceManager device, ContentManager ct)
        {
            Texture2D play1 = ct.Load<Texture2D>("play1");
            Texture2D play2 = ct.Load<Texture2D>("play2");
            Texture2D play3 = ct.Load<Texture2D>("play3");
            background = ct.Load<Texture2D>("Over");
            Play = new Button(play1, play2, play3, new Vector2((device.GraphicsDevice.Viewport.Width /2)-30, (device.GraphicsDevice.Viewport.Height/2)+60));
            
        }


        public void Update()
        {
            Play.Update();

        }

        public void Draw(SpriteBatch sp)
        {
            sp.Begin();
            sp.Draw(background, new Vector2(0, 0), Color.White);
            sp.End();

            Play.Draw(sp);

        }


    }
}
