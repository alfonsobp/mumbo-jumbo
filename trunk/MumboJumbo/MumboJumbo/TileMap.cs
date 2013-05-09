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

        public bool astralMode = false;


        public TileMap(int[,] a, List<Texture2D> texlis)
        {
            tilemap = a;
            this.texlis = texlis;


            tilesize = 40;
            
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
                        elements.Add(new WorldElement(new Rectangle(x * tilesize - mapX, y * tilesize, 35, 30), tilemap[y, x], x, y));

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


        public void Update(GameTime gameTime)
        {



        }

        public int CountEnemies()
        {

            int count = 0;

            for (int i = 0; i < mapSizeX; i++)
            {
                for (int j = 0; j < mapSizeY; j++)
                {
                    tileindex = tilemap[j, i];

                    if (tileindex >= 5)
                        count++;



                }

            }

            return count;
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

                    if (tileindex == 4)
                    {
                        if (astralMode)
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
