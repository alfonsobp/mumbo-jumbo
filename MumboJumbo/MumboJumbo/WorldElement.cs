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

      
        private Vector2 position;
        private Texture2D spriteObject;
        private Vector2 spriteOrigin;
        private int type;

        /*Para las colisiones*/
        private Rectangle block;
        private Rectangle blocksTop;
        private Rectangle blocksLeft;
        private Rectangle blocksBottom;
        private Rectangle blocksRight;

        /*Para los estados de los objetos*/
        private Boolean state = true;
        private Boolean scalable = false;
        private Boolean astralObject = false;
        private Boolean movible = false;
        private Boolean activable = false;
        private Boolean destroyable = false;
        private Boolean hurts = false;
        
        /*Para la posicion en el mundo*/
        private int x;
        private int y;

        public WorldElement()
        {
        }

        public WorldElement(Rectangle rec, int type, int x, int y, Boolean astralObject)
        {

            this.block = rec;
            this.astralObject = astralObject;
            if (astralObject) this.state = false;

            this.type = type;
            this.x = x;
            this.y = y;

            if (type == 2) scalable = true;
            if (type == 3) movible = true;
            if (type == 4) activable = true;
            if (type == 5) destroyable = true;
            if (type == 6) hurts = true;

        }


        public Boolean Scalable
        {
            get { return scalable; }
            set { scalable = value; }
        }
        

        public Boolean AstralObject
        {
            get { return astralObject; }
            set { astralObject = value; }
        }
        

        public Boolean Movible
        {
            get { return movible; }
            set { movible = value; }
        }
        

        public Boolean Activable
        {
            get { return activable; }
            set { activable = value; }
        }
        

        public Boolean Destroyable
        {
            get { return destroyable; }
            set { destroyable = value; }
        }


        public Boolean State
        {
            get { return state; }
            set { state = value; }
        }

        public Rectangle Block
        {
            get { return block; }
            set { block = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Texture2D SpriteObject
        {
            get { return spriteObject; }
            set { spriteObject = value; }
        }
        

        public Vector2 SpriteOrigin
        {
            get { return spriteOrigin; }
            set { spriteOrigin = value; }
        }

        public int Type
        {
            get { return type; }
            set { type = value; }
        }
        
        public Rectangle BlocksTop
        {
            get { return blocksTop; }
            set { blocksTop = value; }
        }
        
        public Rectangle BlocksLeft
        {
            get { return blocksLeft; }
            set { blocksLeft = value; }
        }

        public Rectangle BlocksRight
        {
            get { return blocksRight; }
            set { blocksRight = value; }
        }

        public Rectangle BlocksBottom
        {
            get { return blocksBottom; }
            set { blocksBottom = value; }
        }


        public int X
        {
            get { return x; }
            set { x = value; }
        }
        
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
    }

}
