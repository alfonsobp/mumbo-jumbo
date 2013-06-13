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



enum property {SCALABLE =2,MOVIBLE=3,ACTIVABLE=4 , DESTRUIBLE=5 , HURTS=6, FLAMA = 8}
enum Moves{UP,DOWN}


namespace MumboJumbo
{

    public class WorldElement
    {

      
        public Vector2 position;
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
        private Boolean flamabable = false;
        private Boolean destroyable = false;
        private Boolean hurts = false;
        private Texture2D texture;
        private int size;

        public List<WorldElement> activableElemList = new List<WorldElement>();

        /*Para el movimiento de los objetos*/
        public float time;
        public string StateSheet = "Stand";
        public Point frameSize;
        public Point sheetSize;
        public static Point currentFrame;
        public Rectangle source;
        public int type_move = (int)Moves.UP;
        public int nMoves;
        
        
        bool move = false;

        public bool Move
        {
            get { return move; }
            set { move = value; }
        }
        int numMove = 0;

        public bool AnimationMove = false;

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
            if (type == (int)property.FLAMA) flamabable = true;
            if (type == (int)property.DESTRUIBLE) destroyable = true;
            if (type == (int)property.HURTS) hurts = true;
           
            this.astralObject = astralObject;
            if (astralObject) this.state = false;

            frameSize = new Point(30, 40);
            sheetSize = new Point(6, 7);
            currentFrame = new Point(0, 0);
            time = 0f;
         
            StandAnimation();

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


        public Boolean Flamabable
        {
            get { return flamabable; }
            set { flamabable = value; }
        }

        /*public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }*/

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

        public int AnimateNum = 0;
        public int FlameNum = 0;

        public void Draw(SpriteBatch sp, int move,bool astral_mode) {

            if (astralObject)
            {
                if (astral_mode)
                {
                    sp.Begin();
                    if (activable == true)
                    {
                        if (AnimationMove)
                        {
                            if (AnimateNum == 10)
                            {
                                AnimateNum = 0;
                                AnimationMove = false;
                                type_move = (type_move == (int)Moves.UP) ? (int)Moves.DOWN : (int)Moves.UP;
                            }
                            else
                            {
                                if (type_move == (int)Moves.UP)
                                    MoveAnimation(AnimateNum);
                                else
                                    MoveAnimation(9 - AnimateNum);

                                AnimateNum++;

                                for (int i = 0; i < 1000; i++) ;
                            }

                        }
                        sp.Draw(texture, position + new Vector2(23 - move, 28), source, Color.IndianRed, 0f, new Vector2(block.Width / 2, block.Height / 2), 1.0f, SpriteEffects.None, 0);

                    }
                    else
                    {
                        sp.Draw(texture, new Rectangle((int)position.X - move, (int)position.Y, size, size), Color.IndianRed);
                    }
                    sp.End();
                }

            }
            else
            {
                sp.Begin();
                if (activable == true)
                {
                    if (AnimationMove)
                    {
                        if (AnimateNum == 10)
                        {
                            AnimateNum = 0;
                            AnimationMove = false;
                            type_move = (type_move == (int)Moves.UP) ? (int)Moves.DOWN : (int)Moves.UP;
                               

                        }
                        else
                        {
                            if (type_move == (int)Moves.UP)
                                MoveAnimation(AnimateNum);                          
                            else
                                MoveAnimation(9 - AnimateNum);

                            AnimateNum++;
   
                            for (int i = 0; i < 1000; i++) ;
                        }
                     
                    }
                    sp.Draw(texture, position + new Vector2(23 - move, 28), source, Color.White, 0f, new Vector2(block.Width / 2, block.Height / 2), 1.0f, SpriteEffects.None, 0);

                }
                else
                {
                    if (flamabable == true) { }
                    else
                    {

                        sp.Draw(texture, new Rectangle((int)position.X - move, (int)position.Y, size, size), Color.White);
                    }
                }
                sp.End();
            
            }

            if (flamabable == true)
            {

                if (FlameNum == 7)
                {
                    FlameNum = 0;
                }
                else
                {
                    sp.Begin();
                    FlameAnimation(FlameNum);
                    FlameNum++;
                    for (int j = 0; j < 1000; j++) ;
                    sp.Draw(texture, position + new Vector2(22-move, 20), source, Color.White, 0f, new Vector2(block.Width / 2, block.Height / 2), 1.0f, SpriteEffects.None, 0);
                    sp.End();
                }
            }


             
        }

        public void Update(GameTime gt)
        {

            if (move)
            {
                if (type_move == (int)Moves.UP)
                    position.Y = (position.Y - (nMoves/Math.Abs(nMoves)));               
                else
                    position.Y = (position.Y + (nMoves/Math.Abs(nMoves)));

                numMove = numMove + (1 * (nMoves / Math.Abs(nMoves)));

                if (numMove == nMoves)
                {
                    move = false;
                    numMove = 0;
                    type_move = (type_move==((int)Moves.UP))?(int)Moves.DOWN:(int)Moves.UP;
                }

            }

            
            this.block = new Rectangle((int)position.X, (int)position.Y, size, size);
            this.BlocksTop = new Rectangle(this.Block.Left + 2, this.Block.Y, this.Block.Width - 2, 6);
            this.BlocksBottom = new Rectangle(this.Block.Left + 2, this.Block.Bottom, this.Block.Width - 2, this.Block.Height / 2);
            this.BlocksLeft = new Rectangle(this.Block.Left, this.Block.Y, this.Block.Width / 2, this.Block.Height);
            this.BlocksRight = new Rectangle(this.Block.Right, this.Block.Y, 6, this.Block.Height);

         

        }

        public void MoveAnimation(int i)
        {

            frameSize = new Point(57, 46);
            source = new Rectangle(i * frameSize.X, currentFrame.Y, frameSize.X, frameSize.Y);
        }

        public void FlameAnimation(int i)
        {
            frameSize = new Point(59, 57);
            source = new Rectangle(i * frameSize.X, currentFrame.Y, frameSize.X, frameSize.Y);
        }

        public void StandAnimation()
        {
            currentFrame.X = 0;
            currentFrame.Y = 0;
            frameSize = new Point(57, 46);
            source = new Rectangle(currentFrame.X, currentFrame.Y, frameSize.X, frameSize.Y);

        }


       
    }

}
