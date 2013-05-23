using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


enum moving { RIGHT, LEFT, JUMP, JUMP_DOWN };

namespace MumboJumbo
{


    class Enemy
    {
        public Vector2 worldPosition;
        private Texture2D spriteObject;
        private Rectangle block;
        static Random rand = new Random();

        Rectangle blocksBottom;

        public Rectangle BlocksBottom
        {
            get { return blocksBottom; }
            set { blocksBottom = value; }
        }
        Rectangle blocksTop;

        public Rectangle BlocksTop
        {
            get { return blocksTop; }
            set { blocksTop = value; }
        }
        Rectangle blocksRight;

        public Rectangle BlocksRight
        {
            get { return blocksRight; }
            set { blocksRight = value; }
        }
        Rectangle blocksLeft;

        public Rectangle BlocksLeft
        {
            get { return blocksLeft; }
            set { blocksLeft = value; }
        }

     
        int current_num_move = 0;
        int NumberMoves = 50;
        int move_face;
        bool isAlive = true;
        int size;
        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }

        public Enemy(Vector2 position, Texture2D text,int size)
        {

            worldPosition = position;
            this.size = size;
            block = new Rectangle((int)worldPosition.X, (int)worldPosition.Y, size, size);
            blocksTop = new Rectangle(block.Left + 2, block.Y, block.Width - 2, 6);
            blocksBottom = new Rectangle(block.Left + 2, block.Bottom, block.Width - 2, block.Height / 2);
            blocksLeft = new Rectangle(block.Left, block.Y, block.Width / 2, block.Height);
            blocksRight = new Rectangle(block.Right, block.Y, 6, block.Height);
            spriteObject = text;

            move_face = rand.Next(0, 4);

        }

        public void Update()
        {

            /*Cambio la direccion de movimientos*/

            if (NumberMoves == current_num_move)
            {
                current_num_move = 0;

                switch (move_face)
                {

                    case (int)moving.LEFT: move_face = (int)moving.RIGHT;
                        break;
                    case (int)moving.RIGHT: move_face = (int)moving.LEFT;
                        break;
                    case (int)moving.JUMP: move_face = (int)moving.JUMP_DOWN;
                        break;
                    case (int)moving.JUMP_DOWN: move_face = (int)moving.JUMP;
                        break;


                }

            }



            if (move_face == (int)moving.LEFT)
            {

                worldPosition -= new Vector2(5f, 0);
            }

            if (move_face == (int)moving.RIGHT)
            {
                worldPosition += new Vector2(5f, 0);

            }
            if (move_face == (int)moving.JUMP)
            {

                worldPosition -= new Vector2(0, 5f);
            }

            if (move_face == (int)moving.JUMP_DOWN)
            {
                worldPosition += new Vector2(0, 5f);

            }


            block = new Rectangle((int)worldPosition.X, (int)worldPosition.Y, 35, 30);
            blocksTop = new Rectangle(block.Left + 2, block.Y, block.Width - 2, 6);
            blocksBottom = new Rectangle(block.Left + 2, block.Bottom, block.Width - 2, block.Height / 2);
            blocksLeft = new Rectangle(block.Left, block.Y, block.Width / 2, block.Height);
            blocksRight = new Rectangle(block.Right, block.Y, 6, block.Height);

            current_num_move++;



            // for (int i = 0; i < 500; i++) ;



        }

        public void Draw(SpriteBatch batch, int move)
        {

            batch.Begin();
            batch.Draw(spriteObject, new Rectangle((int)worldPosition.X - move, (int)worldPosition.Y, size, size), Color.White);
            batch.End();

        }

        public void Collide(List<WorldElement> elements)
        {

            foreach (WorldElement elem in elements)
                if(elem.State)
                    {

                        if (elem.BlocksLeft.Intersects(this.blocksRight))
                        {
                            worldPosition -= new Vector2(5f, 0f);

                        }

                        if (elem.BlocksRight.Intersects(this.blocksLeft))
                        {
                            worldPosition += new Vector2(5f, 0f);

                        }

                        if (elem.BlocksTop.Intersects(this.blocksBottom))
                        {
                            worldPosition -= new Vector2(0f, 5f);
                        }

                        if (elem.BlocksBottom.Intersects(this.blocksTop))
                        {
                            worldPosition += new Vector2(0f, 5f);
                        }




                    }

        }

        public void collide(Player player){
        
             {
                        if (BlocksTop.Intersects(player.footBounds))
                        {
                            IsAlive = false;
                        }

                        if (BlocksRight.Intersects(player.leftRec))
                        {
                            player.Life = false;
                            player.Lives -= 1;
                            
                            worldPosition.X += 15f;
                            player.worldPosition.X -= 5f;
                            player.cameraPosition.X -= 5f;
                            
                            
                        }

                        if (BlocksLeft.Intersects(player.rightRec))
                        {
                            player.Life = false;
                            player.Lives -= 1;
      
                            worldPosition.X -= 15f;
                            player.worldPosition.X += 5f;
                            player.cameraPosition.X += 5f;
                           
                        }



                    }
                
        
        }
    }

}

