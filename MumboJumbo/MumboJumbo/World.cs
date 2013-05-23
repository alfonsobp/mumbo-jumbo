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
        int tilesize;
        int mapSizeX;
        int mapSizeY;
        
        
        List<Texture2D> texlis;
        public List<WorldElement> elements;
        public List<Enemy> enemies;
        public int mapX;
        public int MapW;
        public bool astralMode = false;
        

        public List<Texture2D> Texlis
        {
            get { return texlis; }
            set { texlis = value; }
        }

        public World(int[,] a, int[,] b, List<Texture2D> texlis)
        {
            tilemap = a;
            astralObjects = b;
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
                    if (tilemap[y, x] > 0 && tilemap[y, x] < 7)
                    {
                        elements.Add(new WorldElement(new Vector2(x * tilesize, y * tilesize),tilesize, tilemap[y, x], false,texlis[tilemap[y,x]]));
                    }
                    if (astralObjects[y, x] > 0)
                    {
                        elements.Add(new WorldElement(new Vector2(x * tilesize, y * tilesize), tilesize, astralObjects[y, x], true, texlis[astralObjects[y, x]]));
                    }

                    if (tilemap[y, x] == 7) { 
                    enemies.Add(new Enemy(new Vector2(x * tilesize - mapX, y * tilesize) , texlis[7],tilesize) );
                    }
                }
            }

        

          
            MapW = tilesize * mapSizeX;

        }


        public void Update(GameTime gameTime)
        {
            foreach (Enemy e in enemies)
            {
                if (e.IsAlive)
                {
                    e.Collide(elements);
                    e.Update();
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
