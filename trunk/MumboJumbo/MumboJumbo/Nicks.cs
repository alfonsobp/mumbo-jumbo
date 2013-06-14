using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MumboJumbo
{
    public partial class Nicks : Form
    {
        int i = 0;
        public Nicks()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            
                int id = Game1.client.AddPlayer(textBox1.Text, 0);
                Game1.idPlayer = id;
               // MessageBox.Show("" + id);

                this.Close();

            

        }
    }
}
