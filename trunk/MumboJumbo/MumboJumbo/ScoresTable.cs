using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using microsoft.servicemodel.samples;

namespace MumboJumbo
{
    public partial class ScoresTable : Form
    {

        PlayerOnline[] listaPlayers = new PlayerOnline[100];

        public ScoresTable()
        {
            InitializeComponent();

            try
            {
                listaPlayers = Game1.client.ShowScorePlayers();
                tabla.Enabled = false;
                if (listaPlayers.Length != 0)
                {
                    for (int i = 0; i < listaPlayers.Length; i++)

                        if (listaPlayers[i].score != null)
                        {
                            int nlevel = listaPlayers[i].score.Length;
                            for (int k = 0; k < nlevel; k++)
                            {
                                tabla.Rows.Add(listaPlayers[i].name, "" + k, "" + Math.Round(listaPlayers[i].score[k], 3));
                            }
                        }
                }

            }
            catch (Exception e) { }
            tabla.AutoResizeColumns();
            tabla.AutoResizeRows();
           // C:\Users\Alfonso\Desktop\Lab4 lp2\Laboratorio4 v2.0\MumboJumbo\
            //this.BackgroundImage = Image.FromFile(@"MumboJumbo\MumboJumboContent\Start.jpg");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            

        }

        private void ScoresTable_Load(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
