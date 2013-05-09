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
        public static StartMenuScreen screen;
        public static int level = 0;
        public static TileMap[] ListMap;

        public static void Start(ContentManager ct,StartMenuScreen s)
        {
            screen = s;

            ListMap = new TileMap[2];

            Texture2D tex1 = ct.Load<Texture2D>("in");
            Texture2D tex2 = ct.Load<Texture2D>("marioblock[1]");
            Texture2D tex3 = ct.Load<Texture2D>("ladder2");
            Texture2D tex4 = ct.Load<Texture2D>("Ice_Block");
            Texture2D tex5 = ct.Load<Texture2D>("Ice_Block");
            Texture2D tex6 = ct.Load<Texture2D>("EnemySprite");
           
          

            /*Level  0 */

            int [,] MapWorld = new int [,] {
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,1,1,1,0,0,0,1,0,0,0,0,0,4,0,0,5,0,0,0,0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,1,0,1,0,0,0,1,0,0,4,4,4,4,0,0,1,0,0,1,0,0,0,0,0,0,0,1,1,1,0,0,1,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,2,1,1,0,0,0,0,0,0,3,0,0,0,0,0,1,0,0,1,0,0,0,5,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0},
                {0,0,1,2,0,0,0,0,0,0,0,0,3,0,0,0,0,1,1,0,1,1,1,1,1,1,1,1,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,2,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {1,1,1,1,1,1,3,3,3,3,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,4,4,4,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0},
                {0,0,4,0,0,0,0,4,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,0,0,0,0,0},
                {1,0,5,0,0,0,0,0,0,0,0,0,0,0,2,1,1,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,4,4,0,0,0,0,0,0},
                {0,1,1,0,0,0,5,0,0,0,0,5,0,0,2,0,0,0,0,0,0,0,0,0,0,1,1,0,0,5,0,0,0,0,0,0,4,4,0,0,0,0,0,0,0},
                {0,0,0,1,1,1,1,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,4,4,0,0,0,0,0,0,0,0},
                {0,0,0,1,1,1,1,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0},
                {0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            };

            TileMap map = new TileMap(MapWorld);
            map.Texlis = new List<Texture2D>();
            map.Texlis.Add(tex1);
            map.Texlis.Add(tex2);
            map.Texlis.Add(tex3);
            map.Texlis.Add(tex4);
            map.Texlis.Add(tex5);
            map.Texlis.Add(tex6);
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

            TileMap map1 = new TileMap(MapWorld_1);
            map1.Texlis = new List<Texture2D>();
            map1.Texlis.Add(tex1);
            map1.Texlis.Add(tex2);
            map1.Texlis.Add(tex3);
            map1.Texlis.Add(tex4);
            map1.Texlis.Add(tex5);
            map1.Texlis.Add(tex6);
            ListMap[1] = map1;

        
        
        }

        public static TileMap getCurrentWorld() {

            return ListMap[level];
        }

        public static void FinishLevel(Player p) {

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
                      screen.Play.clicked = false;
                      
                  }
 
            
            
            }

            
        
        }

        public static void resetPlayer(Player p){

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
