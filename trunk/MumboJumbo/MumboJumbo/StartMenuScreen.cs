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
        public Button Load;
        public Button Exit;
        public Button Nick;
        public Button Score;
        public Texture2D background;
        public bool Enable = true;

        public void LoadContent(GraphicsDeviceManager device,ContentManager ct)
        { 

            Texture2D play1 = ct.Load<Texture2D>("play1");
            Texture2D play2 = ct.Load<Texture2D>("play2");
            Texture2D play3 = ct.Load<Texture2D>("play3");
            Texture2D exit1 = ct.Load<Texture2D>("exit1");
            Texture2D exit2 = ct.Load<Texture2D>("exit2");
            Texture2D exit3 = ct.Load<Texture2D>("exit3");
            Texture2D load1 = ct.Load<Texture2D>("Load1");
            Texture2D load2 = ct.Load<Texture2D>("Load2");
            Texture2D load3 = ct.Load<Texture2D>("Load3");
            Texture2D nick1 = ct.Load<Texture2D>("Load1");
            Texture2D nick2 = ct.Load<Texture2D>("Load2");
            Texture2D nick3 = ct.Load<Texture2D>("Load3");
            Texture2D score1 = ct.Load<Texture2D>("Load1");
            Texture2D score2 = ct.Load<Texture2D>("Load2");
            Texture2D score3 = ct.Load<Texture2D>("Load3");

            background = ct.Load<Texture2D>("Start");
            Play = new Button(play1, play2, play3, new Vector2( (device.GraphicsDevice.Viewport.Width/ 2)-60, (device.GraphicsDevice.Viewport.Height / 2)-100));
            Load = new Button(load1, load2, load3, new Vector2(Play.position.X-15, Play.position.Y + play1.Height*2));
            Exit = new Button(exit1, exit2, exit3, new Vector2(Load.position.X + 20, Load.position.Y + load1.Height * 2));
            Nick = new Button(nick1, nick2, nick3, new Vector2(Exit.position.X - 20, Exit.position.Y + exit1.Height * 2));
            Score = new Button(score1, score2, score3, new Vector2(Nick.position.X , Nick.position.Y + nick1.Height * 2));

        }

        public void Update()
        {
            
                Play.Update();
                Load.Update();
                Exit.Update();
                Nick.Update();
                Score.Update();
            
        }

        public void Draw(SpriteBatch sp)
        {
            sp.Begin();
            sp.Draw(background, new Vector2(0, 0), Color.White);
            sp.End();

            Play.Draw(sp);
            Load.Draw(sp);
            Exit.Draw(sp);
            Nick.Draw(sp);
            Score.Draw(sp);
            
        }
    }
}
