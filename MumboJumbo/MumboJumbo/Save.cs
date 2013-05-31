using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MumboJumbo
{
    public class Save
    {
        public Vector2 cameraPosition;
        public Vector2 worldPosition;

        public int level;
        public int[] tilemap;

        public string facing;
        public string state;
        public bool jump;
        public float jumpSpeed;
        public float gravity;

        public float speed;

        public string prevstate;
        public Point frameSize;

        public Point currentFrame;
        public Color pcolor;
        public int mapX;
        public int mapW;
        public Texture2D text;
        public bool astral;

        public Rectangle rectangle;
        public Rectangle source;

        public bool[] lstate;

        public Vector2[] Enemies;
        public Vector2[] Elements;

        public bool[] ene_state;
        public bool[] ele_state;

    }
}
