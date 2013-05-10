using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MumboJumbo
{
    class IntermediateScreen
    {
        public Button Resume;
        public Button Load;
        public Button Save;
        public Button Exit;

        public bool Enable = false;

        public Texture2D background;
        public void LoadContent(GraphicsDeviceManager device, ContentManager ct)
        {

            Texture2D Resume1 = ct.Load<Texture2D>("resume1");
            Texture2D Resume2 = ct.Load<Texture2D>("resume2");
            Texture2D Resume3 = ct.Load<Texture2D>("resume3");
            Texture2D Save1 = ct.Load<Texture2D>("save1");
            Texture2D Save2 = ct.Load<Texture2D>("save2");
            Texture2D Save3 = ct.Load<Texture2D>("save3");
            Texture2D Load1 = ct.Load<Texture2D>("load1");
            Texture2D Load2 = ct.Load<Texture2D>("load2");
            Texture2D Load3 = ct.Load<Texture2D>("load3");
            Texture2D exit1 = ct.Load<Texture2D>("exit1");
            Texture2D exit2 = ct.Load<Texture2D>("exit2");
            Texture2D exit3 = ct.Load<Texture2D>("exit3");

            background = ct.Load<Texture2D>("Start");
            Resume = new Button(Resume1, Resume2, Resume3, new Vector2((device.GraphicsDevice.Viewport.Width / 2) - 60, (device.GraphicsDevice.Viewport.Height / 2) - 105));
            Save = new Button(Save1, Save2, Save3, new Vector2(Resume.position.X, Resume.position.Y + Resume1.Height * 2));
            Load = new Button(Load1, Load2, Load3, new Vector2(Save.position.X, Save.position.Y + Save1.Height * 2));
            Exit = new Button(exit1, exit2, exit3, new Vector2(Load.position.X + 18, Load.position.Y + Load1.Height * 2));

        }

        public void Update()
        {

            Save.Update();
            Load.Update();
            Resume.Update();
            Exit.Update();

        }

        public void Draw(SpriteBatch sp)
        {
            sp.Begin();
            sp.Draw(background, new Vector2(0, 0), Color.White);
            sp.End();

            Save.Draw(sp);
            Load.Draw(sp);
            Resume.Draw(sp);
            Exit.Draw(sp);

        }

    }
}
