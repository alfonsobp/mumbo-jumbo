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
//using Microsoft.Xna.Framework.Net;
//using Microsoft.Xna.Framework.Storage;

namespace MumboJumbo
{
    class StartMenuScreen
    {

        public Button Play;
        public Button Options;
        public Button Exit;
        public Texture2D background;
        public void LoadContent(GraphicsDeviceManager device,ContentManager ct)
        { 

            Texture2D play1 = ct.Load<Texture2D>("play1");
            Texture2D play2 = ct.Load<Texture2D>("play2");
            Texture2D play3 = ct.Load<Texture2D>("play3");
            Texture2D exit1 = ct.Load<Texture2D>("exit1");
            Texture2D exit2 = ct.Load<Texture2D>("exit2");
            Texture2D exit3 = ct.Load<Texture2D>("exit3");
            Texture2D options1 = ct.Load<Texture2D>("options1");
            Texture2D options2 = ct.Load<Texture2D>("options2");
            Texture2D options3 = ct.Load<Texture2D>("options3");
            background = ct.Load<Texture2D>("Start");
            Play = new Button(play1, play2, play3, new Vector2( (device.GraphicsDevice.Viewport.Width/ 2)-70, (device.GraphicsDevice.Viewport.Height / 2)-100));
            Options = new Button(options1, options2, options3, new Vector2(Play.position.X, Play.position.Y + play1.Width));
            Exit = new Button(exit1, exit2, exit3, new Vector2(Options.position.X, Options.position.Y + exit1.Width));

        }

        public void Update()
        {
            
            Play.Update();
            Options.Update();
            Exit.Update();
        }

        public void Draw(SpriteBatch sp)
        {
            sp.Begin();
            sp.Draw(background, new Vector2(0, 0), Color.White);
            sp.End();

            Play.Draw(sp);
            Options.Draw(sp);
            Exit.Draw(sp);
            
        }
    }
}
