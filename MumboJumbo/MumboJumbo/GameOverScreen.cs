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

        public void LoadContent(GraphicsDeviceManager device, ContentManager ct)
        {

            background = ct.Load<Texture2D>("Over");

        }

        public void Draw(SpriteBatch sp)
        {
            sp.Begin();
            sp.Draw(background, new Vector2(0, 0), Color.White);
            sp.End();

        }


    }
}
