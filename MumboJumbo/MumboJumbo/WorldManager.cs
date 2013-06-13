using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;

namespace MumboJumbo
{
    class WorldManager
    {

        public int level = 0;
        public World[] ListMap;

        public void Start(ContentManager ct)
        {
            /*Elementos de los mundos */

            ListMap = new World[2];

            Texture2D tex0 = ct.Load<Texture2D>("in");
            Texture2D tex1 = ct.Load<Texture2D>("Loco_Ground");
            Texture2D tex2 = ct.Load<Texture2D>("ladder2");
            Texture2D tex3 = ct.Load<Texture2D>("ground1");
            Texture2D tex4 = ct.Load<Texture2D>("palanca_Sheet");
            Texture2D tex5 = ct.Load<Texture2D>("Ice_Block");
            Texture2D tex6 = ct.Load<Texture2D>("Pua");
            Texture2D tex7 = ct.Load<Texture2D>("Ardilla");
            Texture2D tex8 = ct.Load<Texture2D>("Flame");


            List<Texture2D> ListaElem = new List<Texture2D>();

            ListaElem.Add(tex0);
            ListaElem.Add(tex1);
            ListaElem.Add(tex2);
            ListaElem.Add(tex3);
            ListaElem.Add(tex4);
            ListaElem.Add(tex5);
            ListaElem.Add(tex6);
            ListaElem.Add(tex7);
            ListaElem.Add(tex8);

            /*Level  0 */
            int[,] activableElemsMoves = this.LoadMap(0, "Content//activableMoves");
            int[,] activableMap = this.LoadMap(0, "Content//activable");
            int[,] MapWorld = this.LoadMap(0, "Content//map");
            int[,] astralObjects = this.LoadMap(0, "Content//astral");

            World map = new World(MapWorld, astralObjects, activableMap,activableElemsMoves, ListaElem);
            ListMap[0] = map;

            /*Level 1*/
            int[,] activableElemsMoves_1 = this.LoadMap(1, "Content//activableMoves");
            int[,] activableMap_1 = this.LoadMap(1, "Content//activable");
            int[,] MapWorld_1 = this.LoadMap(1, "Content//map");
            int[,] astralObjects_1 = this.LoadMap(1, "Content//astral");


            World map1 = new World(MapWorld_1, astralObjects_1, activableMap_1, activableElemsMoves_1, ListaElem);
            ListMap[1] = map1;
        }

        public World getCurrentWorld()
        {

            return ListMap[level];
        }

        public void FinishLevel(Player p)
        {

            /*level 0 */

            if (level == 0)
            {
                if (ListMap[0].CountEnemies() == 0)
                {
                    p.resetPlayer();
                    level++;
                }
            }

            if (level == 1)
            {
                if (ListMap[1].CountEnemies() == 0)
                {
                    p.resetPlayer();

                }
            }
        }



        public int[,] LoadMap(int index, string path)
        {

            int[,] map = new int[10, 100];
            StreamReader sr = new StreamReader(path + index + ".txt");
            String word;
            string val = "";
            int k = 0;

            for (int i = 0; i < 10; i++)
            {
                word = sr.ReadLine();

                k = 0;
                for (int j = 0; j < 100; j++)
                {

                    val = "";

                    while (word[k] != ' ')
                    {
                        val += word[k];
                        k++;

                    }
                    map[i, j] = int.Parse(val);
                    k++;

                }

            }


            sr.Close();

            return map;


        }

    }
}
