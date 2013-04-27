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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace MumboJumbo
{
    class AnimationSprite
    {
        public Texture2D texture;
        Point frameSize;
        Point sheetSize;
        Point currentFrame;
        public Rectangle source;
        float time;
        float interval;


        public AnimationSprite()
        {
            frameSize = new Point(30, 30);
            sheetSize = new Point(6, 7);
            currentFrame = new Point(0, 0);
            time = 0f;
            interval = 100f;

        }
        public void LoadContent(Texture2D tex)
        {
            texture = tex;
        }

        public void MoveAnimation(GameTime gt)
        {
            time += (float)gt.ElapsedGameTime.TotalMilliseconds;
            currentFrame.Y = 3;
            currentFrame.X = 1;
            frameSize = new Point(35, 35);
            if (time > interval)
            {
                currentFrame.X++;
                if (currentFrame.X == 4)
                {
                    currentFrame.X = 1;
                }
                source = new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * 25, frameSize.X, frameSize.Y);
                time = 0f;
            }
        }

        public void JumpAnimation()
        {

            currentFrame.Y = 5;
            currentFrame.X = 1;
            frameSize = new Point(29, 29);
            source = new Rectangle(currentFrame.X * 29, currentFrame.Y * 19, frameSize.X, frameSize.Y);

        }

        public void FallAnimation()
        {
            currentFrame.X = 5;
            currentFrame.X = 2;
            frameSize = new Point(29, 29);
            source = new Rectangle(currentFrame.X * 29, currentFrame.Y * 19, frameSize.X, frameSize.Y);
        }
        public void StandAnimation()
        {
            currentFrame.X = 1;
            currentFrame.Y = 2;
            frameSize = new Point(35, 35);
            source = new Rectangle(currentFrame.X * 29, currentFrame.Y * 19, frameSize.X, frameSize.Y);

        }




    }
}
