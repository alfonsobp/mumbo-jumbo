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
    class Player
    {
        Texture2D texture;

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }
        public Vector2 cameraPosition;
        public Vector2 worldPosition;
        public Rectangle rectangle;
        public Rectangle footBounds;
        public Rectangle leftRec;
        public Rectangle topBounds;
        public Rectangle rightRec;
        KeyboardState keystate;
        KeyboardState oldkeys;
        public string facing;
        public string state;
        public bool jump;
        public float jumpSpeed;


        public float JumpSpeed
        {
            get { return jumpSpeed; }
            set { jumpSpeed = value; }
        }
        public float gravity;
        public float startY;
        public float speed;
        public float time;
        public string prevstate;
        public Point frameSize;
        public Point sheetSize;
        public Point currentFrame;
        public Rectangle source;
        float interval;
        public Color pcolor = Color.White;
        public bool astralMode = false;
        public double timeElap;


        public Player()
        {
            cameraPosition = new Vector2(0, 0);
            worldPosition = new Vector2(0, 0);
            startY = worldPosition.Y;
            facing = "right";
            prevstate = state;
            state = "stand";
            jump = false;
            gravity = 0f;
            speed = 5f;
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

        public void Update(GameTime gt)
        {
            time += (float)gt.ElapsedGameTime.TotalSeconds;
            oldkeys = keystate;
            keystate = Keyboard.GetState();
            cameraPosition.Y += gravity;
            worldPosition.Y += gravity;





            if (keystate.IsKeyDown(Keys.Right))
            {
                cameraPosition.X += speed;
                worldPosition.X += speed;

                facing = "right";

            }
            if (keystate.IsKeyDown(Keys.Left))
            {
                cameraPosition.X -= speed;
                worldPosition.X -= speed;

                facing = "left";


            }
            if (oldkeys.IsKeyDown(Keys.Left) && !keystate.IsKeyDown(Keys.Left))
            {
                state = "stand";
            }

            if (oldkeys.IsKeyDown(Keys.Right) && !keystate.IsKeyDown(Keys.Right))
            {
                state = "stand";
            }


            if (state == "stand")
            {

                if (keystate.IsKeyDown(Keys.Left))
                {
                    state = "walk";
                    MoveAnimation(gt);
                }
                if (keystate.IsKeyDown(Keys.Right))
                {
                    state = "walk";
                    MoveAnimation(gt);
                }
            }

            if (state == "walk")
            {
                gravity = 5f;

            }
            if (state == "stand")
            {
                gravity = 5f;
                StandAnimation();
            }



            if (jump)
            {
                state = "jump";
                cameraPosition.Y += jumpSpeed;
                worldPosition.Y += jumpSpeed;
                jumpSpeed += 1f;
                if (jumpSpeed == 0)
                {
                    jumpSpeed = 0;
                    jump = false;
                    state = "fall";
                }

            }
            else
            {
                if (worldPosition.Y == startY)
                {
                    //Jump for player
                    if (keystate.IsKeyDown(Keys.Space) && !oldkeys.IsKeyDown(Keys.Space))
                    {
                        jumpSpeed -= 18f;
                        jump = true;
                        gravity = 0f;
                    }
                }
            }

            if (state == "fall")
            {
                gravity = 5f;
                JumpAnimation();
            }
            if (state == "jump")
            {
                JumpAnimation();
            }
            if (worldPosition.Y > startY)
            {
                state = "walk";
            }

            rectangle = new Rectangle((int)worldPosition.X, (int)worldPosition.Y, 20, 25);
            footBounds = new Rectangle(rectangle.Center.X, rectangle.Center.Y, rectangle.Width, rectangle.Height / 2);
            rightRec = new Rectangle(rectangle.Center.X, rectangle.Y, rectangle.Width / 2, rectangle.Height);
            leftRec = new Rectangle(rectangle.Left, rectangle.Y, 1, rectangle.Height);
            topBounds = new Rectangle(rectangle.Center.X, rectangle.Top, rectangle.Width, rectangle.Height / 2);

        }



        public void Draw(SpriteBatch sp)
        {
            sp.Begin();

            pcolor = (astralMode) ? Color.Red : Color.White;

            if (facing == "right")
            {
                sp.Draw(texture, cameraPosition, source, pcolor, 0f, new Vector2(rectangle.Width / 2, rectangle.Height / 2), 1.0f, SpriteEffects.None, 0);
            }

            if (facing == "left")
            {
                sp.Draw(texture, cameraPosition, source, pcolor, 0f, new Vector2(rectangle.Width / 2, rectangle.Height / 2), 1.0f, SpriteEffects.FlipHorizontally, 0);
            }

            sp.End();
        }



        public void MoveAnimation(GameTime gt)
        {

            time += (float)gt.ElapsedGameTime.TotalMilliseconds;
            currentFrame.Y = 3;

            frameSize = new Point(30, 33);
            if (time > interval)
            {
                currentFrame.X++;
                if (currentFrame.X > 4)
                {
                    currentFrame.X = 1;
                }
                source = new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * 17, frameSize.X, frameSize.Y);
                time = 0f;
            }
        }

        public void JumpAnimation()
        {

            currentFrame.Y = 5;
            currentFrame.X = 1;
            frameSize = new Point(26, 33);
            source = new Rectangle(currentFrame.X * 56, currentFrame.Y * 18, frameSize.X, frameSize.Y);

        }

        public void FallAnimation()
        {
            currentFrame.X = 5;
            currentFrame.X = 2;
            frameSize = new Point(33, 33);
            source = new Rectangle(currentFrame.X * 80, currentFrame.Y * 18, frameSize.X, frameSize.Y);
        }
        public void StandAnimation()
        {
            currentFrame.X = 1;
            currentFrame.Y = 2;
            frameSize = new Point(20, 35);
            source = new Rectangle(currentFrame.X * 23, currentFrame.Y * 4, frameSize.X, frameSize.Y);

        }
    }
}
