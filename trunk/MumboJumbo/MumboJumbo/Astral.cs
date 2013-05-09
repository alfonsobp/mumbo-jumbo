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

       
        public static Vector2 cameraPosition;
        public static Vector2 worldPosition;
        
       
        public  string facing;
        public  string state;
        public  bool jump;
        public  float jumpSpeed;    
        public  float gravity;
       
        public float speed;
      
        public  string prevstate;
        public  Point frameSize;
      
        public  Point currentFrame;
        public  Color pcolor;
        public int mapX;
        public int mapW;
        public Texture2D text;
        private bool astral = false;

        private Rectangle rectangle;
        private Rectangle source;

        public void load(Player p,TileMap m) {


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
            p.pcolor = pcolor;

            m.mapX = mapX;
            m.MapW = mapW;
            astral = false;

        
        }

        public void save(Player p,TileMap m) {

        text = p.Texture;
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
        pcolor = p.pcolor;

        mapX = m.mapX;
        mapW = m.MapW;
       

        source = p.source;
        rectangle = p.rectangle;
        astral = true;

        }

        public void draw(SpriteBatch sp , int m) {

            if (astral)
            {
                sp.Begin();
                sp.Draw(text, worldPosition - new Vector2(m, 0), source, Color.White, 0f, new Vector2(rectangle.Width / 2, rectangle.Height / 2), 1.0f, SpriteEffects.None, 0);
                sp.End();
            }
          }

    }
}
