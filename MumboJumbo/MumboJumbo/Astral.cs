using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MumboJumbo
{
    class Astral
    {


            Vector2 cameraPosition; 
            Vector2 worldPosition; 
            
            string facing; 
            
            string state ;
            bool jump;
            float gravity;
            float speed;
            Point frameSize;
            Point currentFrame;
            float jumpSpeed;
            string prevstate;
            Texture2D texture;
           public int mapX;
           public int mapW;
       
        private bool astral = false;
        Rectangle source;
        Rectangle rectangle;
        Rectangle footBounds;
        Rectangle rightRec;
        Rectangle leftRec;
        Rectangle topBounds;


       

        public void load(Player p,World m) {


            p.cameraPosition = cameraPosition;
            p.worldPosition = worldPosition;
            p.facing = facing;
            p.state = state;
            p.jump = jump;
            p.jumpSpeed = jumpSpeed;
            p.gravity = gravity;
            p.speed = speed;
            p.prevstate = prevstate;
            p.frameSize = frameSize;
            p.currentFrame = currentFrame;
            
            p.Life = true;

            m.mapX = mapX;
            m.MapW = mapW;
            astral = false;

        
        }

        public void save(Player p,World m) {

        texture = p.Texture;
        cameraPosition = p.cameraPosition;
        worldPosition = p.worldPosition;
        facing=p.facing;
        state=p.state;
        jump=p.jump;
        jumpSpeed=p.jumpSpeed;    
        gravity =p.gravity;        
        speed=p.speed;        
        prevstate=p.prevstate;
        frameSize=p.frameSize;       
        currentFrame =p.currentFrame;
        

        mapX = m.mapX;
        mapW = m.MapW;
       

        source = p.source;
        rectangle = p.rectangle;
        astral = true;

        }

        public void collision(WorldElement e)
        {

            if (e.Move)
            {
                if (e.BlocksTop.Intersects(this.footBounds) && e.type_move == (int)Moves.UP)
                {
                    cameraPosition.Y -= 2;
                    worldPosition.Y -= 2;

                }

                if (e.BlocksTop.Intersects(this.footBounds) && e.type_move == (int)Moves.DOWN)
                {
                    cameraPosition.Y += 2;
                    worldPosition.Y += 2;

                
                }
            }

        }

        public void Update() 
        {


            rectangle = new Rectangle((int)worldPosition.X, (int)worldPosition.Y, 20, 25);
            footBounds = new Rectangle(rectangle.X, rectangle.Center.Y, rectangle.Width - 3, rectangle.Height / 2);
            rightRec = new Rectangle(rectangle.Right - 3, rectangle.Y, 3, rectangle.Height);
            leftRec = new Rectangle(rectangle.Left, rectangle.Y, 3, rectangle.Height);
            topBounds = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 3, 3);

            

        }

        public void draw(SpriteBatch sp , int m) {

            if (astral)
            {
                sp.Begin();
                sp.Draw(texture, worldPosition - new Vector2(m, 0), source, Color.White, 0f, new Vector2(rectangle.Width / 2, rectangle.Height / 2), 1.0f, SpriteEffects.None, 0);
                sp.End();
            }
          }

    }
}
