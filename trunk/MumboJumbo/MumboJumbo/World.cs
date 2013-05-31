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
    class World
    {
        public int[,] tilemap;
        public int[,] astralObjects;
        public int[,] activableElems;

        int tilesize;
        int mapSizeX;
        int mapSizeY;

        
        List<Texture2D> texlis;
        public List<WorldElement> elements;
        public List<Enemy> enemies;
        public int mapX;
        public int MapW;
        public bool astralMode = false;
        double timeElap;
        public KeyboardState keyPrevious;
        public KeyboardState key;
        static public double TimeInAstral=0;
        

        public List<Texture2D> Texlis
        {
            get { return texlis; }
            set { texlis = value; }
        }

        public World(int[,] a, int[,] b,int [,] c, List<Texture2D> texlis)
        {
            tilemap = a;
            astralObjects = b;
            activableElems = c;

            this.texlis = texlis;     
            mapSizeX = tilemap.GetLength(1);
            mapSizeY = tilemap.GetLength(0);
            elements = new List<WorldElement>();
            enemies = new List<Enemy>();
            

            tilesize =Game1.graphics.PreferredBackBufferHeight/mapSizeY;
            mapX = 0;
         

            for (int x = 0; x < mapSizeX; x++)
            {
                for (int y = 0; y < mapSizeY; y++)
                {
                    if (tilemap[y, x] > 0 && tilemap[y, x] < 7 )
                    {
                        elements.Add(new WorldElement(new Vector2(x * tilesize, y * tilesize),tilesize, tilemap[y, x], false,texlis[tilemap[y,x]]));
                    }
                    
                    /*Matriz de configuracion deobjetos astrales*/
                    if (astralObjects[y, x] > 0)
                    {
                        elements.Add(new WorldElement(new Vector2(x * tilesize, y * tilesize), tilesize, astralObjects[y, x], true, texlis[astralObjects[y, x]]));
                    }

                    if (tilemap[y, x] == 7) { 
                    enemies.Add(new Enemy(new Vector2(x * tilesize - mapX, y * tilesize) , texlis[7],tilesize) );
                    }

                    

                }
            }

            List<WorldElement> auxActivableElem = new List<WorldElement>() ;

            foreach (WorldElement elem in elements)
                if (elem.Type == 4)
                    auxActivableElem.Add(elem);
            
            mapSizeX = activableElems.GetLength(1);
            mapSizeY = activableElems.GetLength(0);
            if (auxActivableElem.Count() > 0)
            {
                for (int x = 0; x < mapSizeX; x++)
                    for (int y = 0; y < mapSizeY; y++)
                    {
                        if (activableElems[y, x] > 0)
                        {

                            foreach (WorldElement k in elements)
                            {
                                if ((k.position.X == x * tilesize) && (k.position.Y == y * tilesize))
                                {
                                    auxActivableElem[activableElems[y, x] - 1].activableElemPos.Add(k);
                                }

                            }
                        }
                    }
            }
        

          
            MapW = tilesize * mapSizeX;

        }


        public void Update(GameTime gameTime,Player player)
        {

            keyPrevious = key;
            key = Keyboard.GetState();

            
            
            if (key.IsKeyDown(Keys.A))
            {
                if (!player.astralMode&&(player.state== "walk"||player.state =="stand")&&!player.jump)
                {
                    OnAstralMode(player);
                    timeElap = gameTime.TotalGameTime.TotalSeconds;
                }

            }
            TimeInAstral = gameTime.TotalGameTime.TotalSeconds - timeElap;
            if (astralMode && TimeInAstral >= 5)
            {
                OffAstralMode(player);
            }

            /*colision de los elementos*/
            foreach (WorldElement elem in elements)               
                if (elem.State)
                {
                    if (player.astralMode) {
                        player.astralCorp.collision(elem);
                        player.astralCorp.Update();
                    
                    }
                    
                    if (elem.Scalable)
                    {

                        if (elem.BlocksTop.Intersects(player.footBounds))
                        {
                            player.startY = player.worldPosition.Y;
                            player.gravity = 0f;
                            player.state = "stand";
                        }
                        if (elem.Block.Intersects(player.rectangle) && Keyboard.GetState().IsKeyDown(Keys.Up))
                        {
                            player.gravity = 0f;
                            player.worldPosition.Y -= 2f;
                            player.cameraPosition.Y -= 2f;
                            player.state = "upOrDown";
                        }
                        if (elem.Block.Intersects(player.rectangle) && Keyboard.GetState().IsKeyDown(Keys.Down))
                        {
                            if (elem.BlocksTop.Intersects(player.footBounds))
                            {
                                player.gravity = 0f;
                                player.worldPosition.Y += 2f;
                                player.cameraPosition.Y += 2f;
                                player.state = "upOrDown";
                            }
                        }

                        
                    }

                    /*Interseccion de la parte de arriba del player parte de abajo de un elemento*/

                    if (!elem.Scalable && !elem.Activable)
                    {
                        if (elem.BlocksBottom.Intersects(player.topBounds))
                        {


                            if ((player.topBounds.Y >= elem.BlocksBottom.Y))
                            {
                                player.gravity = 5f;
                                player.jump = false;
                                player.JumpSpeed = 0f;
                                //player.startY=;
                              
                            }
                        }
                        //
                        if (elem.Move && elem.Block.Intersects(player.rectangle))
                        {
                            //elem.Move = false;
                            player.gravity = 0f;
                            player.startY = player.worldPosition.Y;
                            player.cameraPosition.Y = elem.BlocksTop.Y - player.rectangle.Height+2;
                            player.worldPosition.Y = elem.BlocksTop.Y - player.rectangle.Height+2;
                            player.state = "stand";

                                                  
                        }

                        /*Parte de abajo de player con parte de arriba de element*/
                        if (elem.BlocksTop.Intersects(player.footBounds))
                        {
                            
                            player.gravity = 0f;
                            player.state = "stand";
                            player.startY = player.worldPosition.Y;
                       
                            if (elem.Hurts)
                            {
                                if (!player.astralMode)
                                {
                                    player.Life = false;
                                    player.Lives -= 1;
                                }
                                else
                                    OffAstralMode(player);
                            }

                            
                        }

                        if (elem.BlocksLeft.Intersects(player.rightRec))
                        {
                            if (player.footBounds.Y >= elem.BlocksLeft.Y)
                            {
                                player.worldPosition.X -= 5f;
                                player.cameraPosition.X -= 5f;
                            }
                           
                        }

                        if (elem.BlocksRight.Intersects(player.leftRec))
                        {
                            if (player.footBounds.Y >= elem.BlocksRight.Y)
                            {
                                player.worldPosition.X += 5f;
                                player.cameraPosition.X += 5f;
                            }
                          
                        }
                    }

                    if (elem.Block.Intersects(player.rectangle))
                    {
                        if (elem.Activable)
                        {
                            if (key.IsKeyDown(Keys.X)&&!keyPrevious.IsKeyDown(Keys.X) )
                            {

                              //  elem.MoveAnimation(gameTime);
                              
                                elem.AnimationMove = true;
                                foreach (WorldElement obje in elem.activableElemPos)
                                {
                                    
                                    obje.Move = true;
                                }

                            }

                        }
                    }

                    elem.Update(gameTime);

                }
            /*colision de los enemigos*/
            foreach (Enemy e in enemies)
            {
                if (e.IsAlive)
                {
                    collide(e, player);           
                    Collide(e, elements);
                    e.Update();
                }
            }
             
              
        }

        public void collide(Enemy e, Player player)
        {

            {
                if (e.BlocksTop.Intersects(player.footBounds))
                {
                    e.IsAlive = false;
                }

                if (e.BlocksRight.Intersects(player.leftRec))
                {
                    if (player.footBounds.Y >= e.BlocksRight.Y)
                    {

                        if (!astralMode)
                        {
                            player.Life = false;
                            player.Lives -= 1;
                        }
                        else
                            OffAstralMode(player);


                        e.worldPosition.X += 15f;
                        player.worldPosition.X -= 5f;
                        player.cameraPosition.X -= 5f;
                        

                    }

                }

                if (e.BlocksLeft.Intersects(player.rightRec))
                {
                    if (player.footBounds.Y >= e.BlocksLeft.Y)
                    {

                        if (!astralMode)
                        {
                            player.Life = false;
                            player.Lives -= 1;
                        }
                        else
                            OffAstralMode(player);

                        e.worldPosition.X -= 15f;
                        player.worldPosition.X += 5f;
                        player.cameraPosition.X += 5f;
                        

                    }
                }
            }
        }

        public void Collide(Enemy e,List<WorldElement> elements)
        {

            foreach (WorldElement elem in elements)
                if (elem.State)
                {

                    if (elem.BlocksLeft.Intersects(e.BlocksRight))
                    {
                        e.worldPosition -= new Vector2(5f, 0f);
                        e.current_num_move = e.NumberMoves;

                    }

                    if (elem.BlocksRight.Intersects(e.BlocksLeft))
                    {
                        e.worldPosition += new Vector2(5f, 0f);
                        e.current_num_move = e.NumberMoves;

                    }

                    if (elem.BlocksTop.Intersects(e.BlocksBottom))
                    {
                        e.worldPosition -= new Vector2(0f, 5f);
                        e.current_num_move = e.NumberMoves;
                    }

                    if (elem.BlocksBottom.Intersects(e.BlocksTop))
                    {
                        e.worldPosition += new Vector2(0f, 5f);
                        e.current_num_move = e.NumberMoves;
                    }




                }

        }

        public void OffAstralMode(Player player)
        {
            astralMode = false;
            player.astralMode = false;

            /*Devolver los valores que cambiaste en astralMode*/

            player.astralCorp.load(player, this);

            /*Cambiar de estado a todos los elementos*/
            foreach (WorldElement e in elements)
            {
                if (e.AstralObject == true)
                {
                    e.State = false;
                }

            }
        }

        public void OnAstralMode(Player player)
        {
            astralMode = true;

            player.astralMode = true;

            /*Guardar en Current las posiciones en el momento de hacer astralMode*/

           player.astralCorp.save(player, this);

            /*Cambiar el estado a todos los elementos */
            foreach (WorldElement e in elements)
            {
                if (e.AstralObject == true)
                {
                    e.State = true;

                }

            }


        }


        public int CountEnemies()
        {

            int count = 0;

            foreach (Enemy e in enemies)
                if (e.IsAlive) count++;

            return count;


        }

        public void Draw(SpriteBatch sp)
        {

            

            foreach (WorldElement we in elements) 
            {
                if (we.State)
                {
                    we.Draw(sp, mapX, astralMode);
                }
            }

            foreach (Enemy e in enemies)
                if (e.IsAlive)
                {
                    e.Draw(sp, mapX);
                }
        }

    }
}
