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

    public class WorldElement
    {

        private Rectangle block;

        public Rectangle Block
        {
            get { return block; }
            set { block = value; }
        }

        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        private Texture2D spriteObject;

        public Texture2D SpriteObject
        {
            get { return spriteObject; }
            set { spriteObject = value; }
        }
        private Vector2 spriteOrigin;

        public Vector2 SpriteOrigin
        {
            get { return spriteOrigin; }
            set { spriteOrigin = value; }
        }

        private int type;

        public int Type
        {
            get { return type; }
            set { type = value; }
        }
        Rectangle blocksTop;

        public Rectangle BlocksTop
        {
            get { return blocksTop; }
            set { blocksTop = value; }
        }
        Rectangle blocksLeft;

        public Rectangle BlocksLeft
        {
            get { return blocksLeft; }
            set { blocksLeft = value; }
        }
        Rectangle blocksRight;

        public Rectangle BlocksRight
        {
            get { return blocksRight; }
            set { blocksRight = value; }
        }
        Rectangle blocksBottom;

        public Rectangle BlocksBottom
        {
            get { return blocksBottom; }
            set { blocksBottom = value; }
        }

        

        public Boolean climbed = false;

        //atributo añadido
        public Boolean state = true;
        int x;

        public int X
        {
            get { return x; }
            set { x = value; }
        }
        int y;

        public int Y
        {
            get { return y; }
            set { y = value; }
        }


        public WorldElement(Rectangle rec, int type,int x,int y)
        {

            this.block = rec;
            if (type == 2) climbed = true;
            if (type == 4) state = false;
            this.type = type;
            this.x = x;
            this.y = y;

        }

        public void Draw(SpriteBatch batch)
        {

            batch.Draw(spriteObject, position, null, Color.White,
                0.0f, spriteOrigin, 1.0f, SpriteEffects.None, 0.9f);

        }



    }

}
