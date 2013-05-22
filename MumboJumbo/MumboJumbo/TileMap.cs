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
        public int[,] astralObjects;
        int tilesize;
        int mapSizeX;
        int mapSizeY;
        int tileindex;
        Texture2D tex;
        List<Texture2D> texlis;
        public List<WorldElement> elements;
        public int mapX;
        public int MapW;
        public bool astralMode = false;
        

        public List<Texture2D> Texlis
        {
            get { return texlis; }
            set { texlis = value; }
        }

        public TileMap(int[,] a, int[,] b, List<Texture2D> texlis)
        {
            tilemap = a;
            astralObjects = b;
            this.texlis = texlis;


            tilesize = 40;
            
            mapSizeX = tilemap.GetLength(1);
            mapSizeY = tilemap.GetLength(0);
            elements = new List<WorldElement>();

            mapX = 0;

            for (int x = 0; x < mapSizeX; x++)
            {
                for (int y = 0; y < mapSizeY; y++)
                {
                    if (tilemap[y, x] > 0)
                    {
                        elements.Add(new WorldElement(new Rectangle(x * tilesize, y * tilesize, 35, 30), tilemap[y, x], x, y, false));
                    }
                    if (astralObjects[y, x] > 0)
                    {
                        elements.Add(new WorldElement(new Rectangle(x * tilesize, y * tilesize, 35, 30), astralObjects[y, x], x, y, true));
                    }
                }
            }

            foreach (WorldElement elem in elements)
            {

                elem.BlocksTop = new Rectangle(elem.Block.Left + 2, elem.Block.Y, elem.Block.Width - 2, 6);
                elem.BlocksBottom = new Rectangle(elem.Block.Left + 2, elem.Block.Bottom, elem.Block.Width - 2, elem.Block.Height / 2);
                elem.BlocksLeft = new Rectangle(elem.Block.Left, elem.Block.Y, elem.Block.Width / 2, elem.Block.Height);
                elem.BlocksRight = new Rectangle(elem.Block.Right, elem.Block.Y, 6, elem.Block.Height);   
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
                    int tileindex2 = astralObjects[j, i];
                    tex = texlis[tileindex];
                    Texture2D tex2 = texlis[tileindex2];
                   
                    sp.Draw(tex, new Rectangle(i * tilesize - mapX, j * tilesize, tilesize, tilesize), Color.White);
                    if (astralMode) sp.Draw(tex2, new Rectangle(i * tilesize - mapX, j * tilesize, tilesize, tilesize), Color.Red);

                }

            }
            sp.End();
        }
    }
}
