using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet_Cavalier
{
    public partial class FormSimulation : Form
    {
        private PictureBox[,] grilleSimu = new PictureBox[8, 8];
        private int startL, startC;
        private Image cavalierImage;
        private bool pause = false;
        private int idx = 0;
        private int vitesse = 700;
        private string theme;

        public FormSimulation(int l, int c, Image cav, string th)
        {
            InitializeComponent(); 
            startL = l;
            startC = c;
            cavalierImage = cav;
            theme = th;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pause = !pause;

            if (pause)
            {
                button1.Text = "REPRENDRE";
                MessageBox.Show($"Simulation mise en pause à l'étape {idx}/64");

            }
            else 
            {
                button1.Text = "PAUSE";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close(); //réactive RormJeu automatriquement
        }


        private async void FormSimulation_Load(object sender, EventArgs e)//j'ai enlevé Task et mis void
        {
            EchecSim();

            var chemin =  EulerTour(startL, startC) ;

            for (int i = 0; i < chemin.Count; i++)
            {
                while (pause)
                {
                    await Task.Delay(100);
                }
                var (l, c) = chemin[i];

                idx++;
                label1.Text = $"Étape: {idx}/64";


                //affichage du coup
                grilleSimu[l,c].Image = cavalierImage ;
                grilleSimu[l, c].BackColor = Color.Yellow;
                
                await Task.Delay(vitesse);

                grilleSimu[l, c].BackColor = Color.Blue;
                grilleSimu[l, c].Image = null;

            }
            MessageBox.Show("Simulation terminée  🎉","Fin de partie", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private List<(int, int)> EulerTour(int l0, int c0)
        {
            List<(int, int)> chemin = new List<(int, int)>();

            bool[,] occ = new bool[8, 8];
            int l = l0, c = c0;

            int[] dx = { 2, 2, 1, 1, -1, -1, -2, -2 };
            int[] dy = { 1, -1, 2, -2, 2, -2, 1, -1 };

            for (int step = 0; step < 64; step++)
            {
                chemin.Add((l, c));
                occ[l, c] = true;

                // Sélection du coup suivant : règle d'Euler
                int bestL = -1, bestC = -1, bestDeg = 9;

                for (int k = 0; k < 8; k++)
                {
                    int nl = l + dx[k];
                    int nc = c + dy[k];

                    if (nl < 0 || nl >= 8 || nc < 0 || nc >= 8) continue;
                    if (occ[nl, nc]) continue;

                    // Compter les fuites possibles
                    int deg = 0;
                    for (int t = 0; t < 8; t++)
                    {
                        int fl = nl + dx[t];
                        int fc = nc + dy[t];
                        if (fl >= 0 && fl < 8 && fc >= 0 && fc < 8 && !occ[fl, fc])
                            deg++;
                    }

                    if (deg < bestDeg)
                    {
                        bestDeg = deg;
                        bestL = nl;
                        bestC = nc;
                    }
                }

                if (bestL == -1) break; // impasse improbable

                l = bestL;
                c = bestC;
            }

            return chemin;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            //vitesse = trackBar1.Value;
            //vitesse = trackBar1.Maximum - trackBar1.Value + 50;
            vitesse = 700 - (int)(trackBar1.Value * 5.5);

            if (vitesse < 50) vitesse = 50; // sécurité au cas où
        }

        private void EchecSim()
        {
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.RowCount = 8;
            tableLayoutPanel1.ColumnCount = 8;
            Color c1, c2;
            if(theme=="clair")
            {
                c1 = Color.Black;
                c2 = Color.LightGreen;
            }
            else if (theme=="rouge")
            {
                c1 = Color.DarkRed;
                c2 = Color.Red;
            }
            else
            {
                c1 = Color.Black;
                c2 = Color.LightGreen;
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    PictureBox pb = new PictureBox();
                    pb.Dock = DockStyle.Fill;
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;

                    pb.BackColor = ((i + j) % 2 == 0) ? Color.Black : Color.LightGreen;

                    tableLayoutPanel1.Controls.Add(pb, j, i);
                    grilleSimu[i, j] = pb;
                }
            }
        }
    }
}
