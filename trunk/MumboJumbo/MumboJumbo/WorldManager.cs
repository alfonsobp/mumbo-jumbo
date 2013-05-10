﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MumboJumbo
{
    class WorldManager
    {
       
        public  int level = 0;
        public  TileMap[] ListMap;

        public     void Start(ContentManager ct)
        {
           /*Elementos de los mundos */

            ListMap = new TileMap[2];

            Texture2D tex1 = ct.Load<Texture2D>("in");
            Texture2D tex2 = ct.Load<Texture2D>("marioblock[1]");
            Texture2D tex3 = ct.Load<Texture2D>("ladder2");
            Texture2D tex4 = ct.Load<Texture2D>("Ice_Block");
            Texture2D tex5 = ct.Load<Texture2D>("Ice_Block");
            Texture2D tex6 = ct.Load<Texture2D>("Ardilla");


            List<Texture2D> ListaElem = new List<Texture2D>();

            ListaElem.Add(tex1);
            ListaElem.Add(tex2);
            ListaElem.Add(tex3);
            ListaElem.Add(tex4);
            ListaElem.Add(tex5);
            ListaElem.Add(tex6);




            /*Level  0 */

            int [,] MapWorld = new int [,] {
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,1,1,0,0,0,1,0,0,0,0,0,4,0,0,5,0,0,0,0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,1,0,0,0,1,0,0,4,4,4,4,0,0,1,0,0,1,0,0,0,0,0,0,0,1,1,1,0,0,1,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,1,1,0,0,0,0,0,0,3,0,0,0,0,0,1,0,0,1,0,0,0,5,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0},
                {0,0,1,0,0,0,0,0,0,0,0,0,3,0,0,0,0,1,1,0,1,1,1,1,1,1,1,1,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {1,1,1,1,1,1,3,3,3,3,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,4,4,4,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0},
                {0,0,4,0,0,0,0,4,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,0,0,0,0,0},
                {1,0,5,0,0,0,0,0,0,0,0,0,0,0,2,1,1,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,4,4,0,0,0,0,0,0},
                {0,1,1,0,0,0,5,0,0,0,0,5,0,0,2,0,0,0,0,0,0,0,0,0,0,1,1,0,0,5,0,0,0,0,0,0,4,4,0,0,0,0,0,0,0},
                {0,0,0,1,1,1,1,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,4,4,0,0,0,0,0,0,0,0},
                {0,0,0,1,1,1,1,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0},
                {0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            };

            TileMap map = new TileMap(MapWorld,ListaElem);  
            ListMap[0] = map;

           

            /*Level 1*/
            int[,] MapWorld_1 = new int[,] {
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,1,1,1,0,0,0,1,0,0,0,0,0,4,0,0,5,0,0,0,0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,1,0,1,0,0,0,1,0,0,4,4,4,4,0,0,1,0,0,1,0,0,0,0,0,0,0,1,1,1,0,0,1,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,2,1,1,0,0,0,0,0,0,3,0,0,0,0,4,1,0,0,1,0,0,0,5,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0},
                {0,0,1,2,0,0,0,0,0,0,0,0,3,0,0,4,4,1,1,0,1,1,1,1,1,1,1,1,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,2,0,0,0,0,0,0,0,3,3,0,4,4,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {1,1,1,1,1,1,3,3,3,3,1,0,0,4,4,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,4,4,4,4,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,4,0,0,0,0,4,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {1,0,5,0,0,0,0,0,0,0,0,0,0,0,2,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,1,1,0,0,0,5,0,0,0,0,5,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,1,1,1,1,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,1,1,1,1,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            };

            
            TileMap map1 = new TileMap(MapWorld_1,ListaElem);
            
            ListMap[1] = map1;

       
        
        }

        public  TileMap getCurrentWorld() {

            return ListMap[level];
        }

        public  void FinishLevel(Player p) {

            /*level 0 */

            if (level == 0) {


                if (ListMap[0].CountEnemies() == 0) {
                    resetPlayer(p);
                    level++;                             
                }


            
            }

            if (level == 1)
            { 
            
            
                  if (ListMap[1].CountEnemies() == 0) {
                    
                      resetPlayer(p);
                     
                      
                  }
 
            
            
            }

            
        
        }

        public  void resetPlayer(Player p){

            p.cameraPosition = new Vector2(0, 0);
            p.worldPosition = new Vector2(0, 0);
            p.startY = p.worldPosition.Y;
            p.facing = "right";
            p.prevstate = p.state;
            p.state = "stand";
            p.jump = false;
            p.gravity = 0f;
            p.speed = 5f;
            p.frameSize = new Point(30, 30);
            p.sheetSize = new Point(6, 7);
            p.currentFrame = new Point(0, 0);
            p.time = 0f;
            
        
        }

     
       

    }
}
