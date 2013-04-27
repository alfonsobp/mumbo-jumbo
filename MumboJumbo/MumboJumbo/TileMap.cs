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
    class TileMap
    {
        public int[,] tilemap;
        int tilesize;
        int mapSizeX;
        int mapSizeY;
        int tileindex;
        Texture2D tex;
        List<Texture2D> texlis;

        public List<Texture2D> Texlis
        {
            get { return texlis; }
            set { texlis = value; }
        }
        public List<WorldElement> elements;
        public List<Rectangle> tile;
        public int mapX;
        public int MapW;




        public TileMap()
        {
            tilemap = new int[,]
            {
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,1,1,1,0,0,0,1,0,0,0,0,0,4,0,0,5,0,0,0,0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,1,0,1,0,0,0,1,0,0,4,4,4,4,0,0,1,0,0,1,0,0,0,0,0,0,0,1,1,1,0,0,1,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,2,1,1,0,0,0,0,0,0,3,0,0,0,0,0,1,0,0,1,0,0,0,5,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0},
                {0,0,1,2,0,0,0,0,0,0,0,0,3,0,0,0,0,1,1,0,1,1,1,1,1,1,1,1,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,2,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {1,1,1,1,1,1,3,3,3,3,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,4,4,4,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,4,0,0,0,0,4,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {1,0,5,0,0,0,0,0,0,0,0,0,0,0,2,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,1,1,0,0,0,5,0,0,0,0,5,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,1,1,1,1,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,1,1,1,1,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            };

            tilesize = 40;
            texlis = new List<Texture2D>();
            mapSizeX = tilemap.GetLength(1);
            mapSizeY = tilemap.GetLength(0);
            elements = new List<WorldElement>();

            tile = new List<Rectangle>();
            mapX = 0;

            for (int x = 0; x < mapSizeX; x++)
            {
                for (int y = 0; y < mapSizeY; y++)
                {
                    if (tilemap[y, x] > 0)
                    {
                        elements.Add(new WorldElement(new Rectangle(x * tilesize - mapX, y * tilesize, 35, 30), tilemap[y, x],x,y));
                        
                    }
                }
            }

            foreach (WorldElement elem in elements)
            {

                elem.BlocksTop = new Rectangle(elem.Block.Center.X, elem.Block.Top, elem.Block.Width, elem.Block.Height);
                elem.BlocksBottom = new Rectangle(elem.Block.Center.X, elem.Block.Bottom, elem.Block.Width, elem.Block.Height);
                elem.BlocksLeft = new Rectangle(elem.Block.Left, elem.Block.Y, 1, elem.Block.Height);
                elem.BlocksRight = new Rectangle(elem.Block.Right, elem.Block.Y, elem.Block.Width / 2, elem.Block.Height);
            }

            for (int x = 0; x < mapSizeX; x++)
            {
                for (int y = 0; y < mapSizeY; y++)
                {
                    tile.Add(new Rectangle(x * tilesize, y * tilesize, tilesize, tilesize));
                }
            }

            MapW = tilesize * mapSizeX;

        }
        double timeElap;
        Boolean astralMode = false;


        public void Update(GameTime gameTime)
        {
            KeyboardState key = Keyboard.GetState();
            if (key.IsKeyDown(Keys.S))
            {
                astralMode = true;
                timeElap = gameTime.TotalGameTime.TotalSeconds;
            }

            if (key.IsKeyDown(Keys.T))
            {
                astralMode = false;
                
            }

            if (astralMode == true)
            {
                foreach (WorldElement e in elements)
                {
                    if (e.Type == 4)
                    {
                        e.state = true;
                        
                        this.tilemap[e.Y, e.X] = e.Type;
                    }
                }
            }
            else {
                foreach (WorldElement e in elements)
                {
                    if (e.Type == 4)
                    {
                        e.state = false;
                        this.tilemap[e.Y, e.X] = 0;
                    }
                }
            }

            if (astralMode && gameTime.TotalGameTime.TotalSeconds - timeElap >= 5) astralMode = false;
            

        }


        public void Draw(SpriteBatch sp)
        {
            sp.Begin();
            
            for (int i = 0; i < mapSizeX; i++)
            {
                for (int j = 0; j < mapSizeY; j++)
                {
                    tileindex = tilemap[j, i];
                    tex = texlis[tileindex];

                    if (tileindex == 4) {
                            sp.Draw(tex, new Rectangle(i * tilesize - mapX, j * tilesize, tilesize, tilesize), Color.Blue);
                    }
                    else 
                        sp.Draw(tex, new Rectangle(i * tilesize - mapX, j * tilesize, tilesize, tilesize), Color.White);
                    
                }

            }
            sp.End();
        }
    }
}
