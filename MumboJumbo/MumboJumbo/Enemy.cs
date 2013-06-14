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
        public Point frameSize;
        public Point currentFrame;
        public Rectangle source;
        static Random rand = new Random();
        public Boolean state = true;
        Rectangle blocksBottom;
        public Boolean isAstral = false;

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

     
        public int current_num_move = 0;
        public int NumberMoves = 100;
        public int move_face;
        public bool isAlive = true;
        public int size;

        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }

        public Enemy(Vector2 position, Texture2D text,int size,Boolean isAstral)
        {

            worldPosition = position;
            this.size = size;
            block = new Rectangle((int)worldPosition.X, (int)worldPosition.Y, size, size);
            blocksTop = new Rectangle(block.Left + 2, block.Y, block.Width - 2, 6);
            blocksBottom = new Rectangle(block.Left + 2, block.Bottom, block.Width - 2, block.Height / 2);
            blocksLeft = new Rectangle(block.Left, block.Y, block.Width / 2, block.Height);
            blocksRight = new Rectangle(block.Right, block.Y, 6, block.Height);
            spriteObject = text;
            this.isAstral = isAstral;

            state = (isAstral) ? false : true;

            move_face = rand.Next(0, 2);

        }

        public void Update()
        {

            /*Cambio la direccion de movimientos*/

            if (NumberMoves <= current_num_move)
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

                worldPosition -= new Vector2(2f, 0);
            }

            if (move_face == (int)moving.RIGHT)
            {
                worldPosition += new Vector2(2f, 0);

            }
            if (move_face == (int)moving.JUMP)
            {

                worldPosition -= new Vector2(0, 2f);
            }

            if (move_face == (int)moving.JUMP_DOWN)
            {
                worldPosition += new Vector2(0, 2f);

            }


            block = new Rectangle((int)worldPosition.X, (int)worldPosition.Y, 35, 30);
            blocksTop = new Rectangle(block.Left + 2, block.Y, block.Width - 2, 6);
            blocksBottom = new Rectangle(block.Left + 2, block.Bottom, block.Width - 2, block.Height / 2);
            blocksLeft = new Rectangle(block.Left, block.Y, block.Width / 2, block.Height);
            blocksRight = new Rectangle(block.Right, block.Y, 6, block.Height);

            current_num_move++;

        }


        public int EnemyMoveNum = 0;

        public void Draw(SpriteBatch batch, int move)
        {

            batch.Begin();
            Color color = (isAstral) ? Color.Orange : Color.White;

            //batch.Draw(spriteObject, new Rectangle((int)worldPosition.X - move, (int)worldPosition.Y, size, size),Color.White);

            if (true)
            {
                if (EnemyMoveNum == 6)
                {
                    EnemyMoveNum = 0;                                        
                }
                else
                {                    
                    MoveAnimation(EnemyMoveNum);                  
                    EnemyMoveNum++;
                    for (int i = 0; i < 20000; i++) ;
                }

            }

            if (move_face == (int)moving.RIGHT || move_face == (int)moving.JUMP)
                batch.Draw(spriteObject, new Rectangle((int)worldPosition.X - move, (int)worldPosition.Y, size, size),source, color, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);

            if (move_face == (int)moving.LEFT || move_face == (int)moving.JUMP_DOWN)
                batch.Draw(spriteObject, new Rectangle((int)worldPosition.X - move, (int)worldPosition.Y, size, size),source, color, 0, Vector2.Zero, SpriteEffects.None, 0);

            batch.End();

        }

        public void MoveAnimation(int i)
        {
            currentFrame.Y=1;
            frameSize = new Point(54, 50);
            source = new Rectangle(i * (frameSize.X + 10), currentFrame.Y * 80, frameSize.X, frameSize.Y);            
        }

    }

}

