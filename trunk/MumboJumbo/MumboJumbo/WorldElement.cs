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



enum property {SCALABLE =2,MOVIBLE=3,ACTIVABLE=4 , DESTRUIBLE=5 , HURTS=6 }
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
        private Texture2D texture;
        private int size;
        
      

        public WorldElement()
        {
        }

        public WorldElement(Vector2 position,int size, int type, Boolean astralObject,Texture2D text)
        {
            this.position = position;
            this.block = new Rectangle((int)position.X,(int)position.Y,size,size);
            this.BlocksTop = new Rectangle(this.Block.Left + 2, this.Block.Y, this.Block.Width - 2, 6);
            this.BlocksBottom = new Rectangle(this.Block.Left + 2, this.Block.Bottom, this.Block.Width - 2, this.Block.Height / 2);
            this.BlocksLeft = new Rectangle(this.Block.Left, this.Block.Y, this.Block.Width / 2, this.Block.Height);
            this.BlocksRight = new Rectangle(this.Block.Right, this.Block.Y, 6, this.Block.Height);
            this.size = size;
            this.type = type;
            this.texture = text;

            if (type == (int)property.SCALABLE) scalable = true;
            if (type == (int)property.MOVIBLE) movible = true;
            if (type == (int)property.ACTIVABLE) activable = true;
            if (type == (int)property.DESTRUIBLE) destroyable = true;
            if (type == (int)property.HURTS) hurts = true;
            this.astralObject = astralObject;
            if (astralObject) this.state = false;

        }

        public Boolean Hurts
        {
            get { return hurts; }
            set { hurts = value; }
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

        public void Draw(SpriteBatch sp, int move,bool astral_mode) {

            if (astralObject)
            {
                if (astral_mode)
                {
                    sp.Begin();
                    sp.Draw(texture, new Rectangle((int)position.X - move, (int)position.Y, size, size), Color.Red);
                    sp.End();
                }

            }
            else
            {
                sp.Begin();
                sp.Draw(texture,new Rectangle((int)position.X-move,(int)position.Y,size,size),Color.White);
                sp.End();
            
            }
           
        
        }







        internal void collide(Player player)
        {
            if (Scalable)
            {

                if (BlocksTop.Intersects(player.footBounds))
                {
                    player.startY = player.worldPosition.Y;
                    player.gravity = 0f;
                    player.state = "stand";
                }
                if (Block.Intersects(player.rectangle) && Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    player.gravity = 0f;
                    player.worldPosition.Y -= 2f;
                    player.cameraPosition.Y -= 2f;
                    player.state = "stand";
                }
                if (Block.Intersects(player.rectangle) && Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    if (BlocksTop.Intersects(player.footBounds))
                    {
                        player.gravity = 0f;
                        player.worldPosition.Y += 2f;
                        player.cameraPosition.Y += 2f;
                        player.state = "stand";
                    }
                }
            }

            /*Interseccion de la parte de arriba del player parte de abajo de un elemento*/

            if (!Scalable)
            {
                if (BlocksBottom.Intersects(player.topBounds))
                {

                    if ((player.topBounds.Y >= BlocksBottom.Y))
                    {
                        player.gravity = 5f;
                        player.jump = false;
                        player.JumpSpeed = 0f;
                        //player.startY=;
                    }
                }

                /*Parte de abajo de player con parte de arriba de element*/
                if (BlocksTop.Intersects(player.footBounds))
                {
                    if (Type == 5)
                    {
                        
                        this.State = false;
                       
                    }

                    player.gravity = 0f;
                    player.state = "stand";
                    player.startY = player.worldPosition.Y;

                    if (Hurts)
                        player.Life = false;
                }

                if (BlocksLeft.Intersects(player.rightRec))
                {
                    if (player.footBounds.Y >= BlocksLeft.Y)
                    {
                        player.worldPosition.X -= 5f;
                        player.cameraPosition.X -= 5f;
                    }
                }

                if (BlocksRight.Intersects(player.leftRec))
                {
                    if (player.footBounds.Y >= BlocksRight.Y)
                    {
                        player.worldPosition.X += 5f;
                        player.cameraPosition.X += 5f;
                    }
                }
            }
        }



    }

}
