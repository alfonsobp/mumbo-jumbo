using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MumboJumbo
{
    class ScreenManager
    {
        StartMenuScreen StScreen=new StartMenuScreen();
        IntermediateScreen Iscreen= new IntermediateScreen();
        GameOverScreen Oscreen = new GameOverScreen();

        public GameOverScreen getGameOver()
        {
            return Oscreen;
        }

        public StartMenuScreen getStartScreen() {
            return StScreen;
        }
        public IntermediateScreen getIntermediateScreen() {

            return Iscreen;
        }

        public void CreateScreens(GraphicsDeviceManager device, ContentManager ct)
        {

            StScreen.LoadContent(device, ct);
            Iscreen.LoadContent(device, ct);
            Oscreen.LoadContent(device, ct);
        
        }

        public void update(GameTime gt) {

            if (StScreen.Enable)
            {

                StScreen.Update();
            }

            if (Iscreen.Enable)
            {
                Iscreen.Update();

            }
        
        
        }







    }
}
