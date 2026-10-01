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
    public partial class FormJeu : Form
    {
        public int taille_echequier = 8;

        private PictureBox[,] grille = new PictureBox[8, 8];
        
        private Image cavalierSombre;
        private Image cavalierClair;
        private Image cavalierRouge;
        private Image cav_actuel;

        private string theme = "sombre";

        private int posL = -1, posC = -1;

        private int num_coup = 0;

        private bool partieEnCours = false;
        private bool premierCoupEnAttente = false;
        private bool[,] visite = new bool[8, 8];

        //jtableua pour mémo les 5coups
        private (int, int)[] histo = new (int, int)[64];
        private int idx_histo =0;


        public FormJeu()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //pions
            cavalierSombre = Image.FromFile("Images\\cav1_1.png");
            cavalierClair = Image.FromFile("Images\\cav1_1.png");
            cavalierRouge = Image.FromFile("Images\\cav2_1.png");

            //thème par defaut
            cav_actuel = cavalierSombre;

            ConstruireEchiquier();


        }

        private void clairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            theme = "clair";
            cav_actuel = cavalierClair;

            label1.ForeColor = Color.Black;
            label1.BackColor = Color.White;

            label2.ForeColor = Color.Black;
            label2.BackColor = Color.White;

            label6.ForeColor = Color.Black;
            label6.BackColor = Color.White;

            label7.ForeColor = Color.Black;
            label7.BackColor = Color.White;

            label5.ForeColor = Color.Black;
            label5.BackColor = Color.White;

            button1.BackColor = Color.LightGreen;
            button1.ForeColor = Color.Black;

            button2.BackColor = Color.LightGreen;
            button2.ForeColor = Color.Black;

            button3.BackColor = Color.LightGreen;
            button3.ForeColor = Color.Black;

            button4.BackColor = Color.LightGreen;
            button4.ForeColor = Color.Black;

            button5.BackColor = Color.LightGreen;
            button5.ForeColor = Color.Black;


            /*for (int i = 0; i < taille_echequier; i++)
            {
                for (int j = 0; j < taille_echequier; j++)
                {
                    PictureBox pb = (PictureBox)tableLayoutPanel1.GetControlFromPosition(j, i);
                    


                    //alternons les colueurs
                    if ((i + j) % 2 == 0)
                    {
                        pb.BackColor = Color.Black;
                    }
                    else
                    {
                        pb.BackColor = Color.LightGreen;
                    }

                }
            }*/

            ResetColors(); //recoloration

            if(posL>=0 && posC>=0) //replacer le bon cav
            {
                grille[posL, posC].Image = cav_actuel;
            }

            possib_mouv();//surbrillance
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("PROPRIETE DE MBODE JOSEPH v.2025");
        }

        private void RougeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.OrangeRed;
            theme = "rouge";               
            cav_actuel = cavalierRouge;

            label1.ForeColor = Color.Red;
            label1.BackColor = Color.Black;

            label2.ForeColor = Color.Red;
            label2.BackColor = Color.Black;

            label6.ForeColor = Color.Red;
            label6.BackColor = Color.Black;

            label7.ForeColor = Color.Red;
            label7.BackColor = Color.Black;

            label5.ForeColor = Color.Red;
            label5.BackColor = Color.Black;

            button1.BackColor = Color.Black;
            button1.ForeColor = Color.Red;

            button2.BackColor = Color.Black;
            button2.ForeColor = Color.Red;

            button3.BackColor = Color.Black;
            button3.ForeColor = Color.Red;

            button4.BackColor = Color.Black;
            button4.ForeColor = Color.Red;

            button5.BackColor = Color.Black;
            button5.ForeColor = Color.Red;
            /*/ for (int i = 0; i < taille_echequier; i++)
             {
                 for (int j = 0; j < taille_echequier; j++)
                 {
                     PictureBox pb = (PictureBox)tableLayoutPanel1.GetControlFromPosition(j, i);



                     //alternons les colueurs
                     if ((i + j) % 2 == 0)
                     {
                         pb.BackColor = Color.DarkRed;
                     }
                     else
                     {
                         pb.BackColor = Color.Red;
                     }

                 }
             }*/

            ResetColors();

            
            if (posL >= 0 && posC >= 0)
                grille[posL, posC].Image = cav_actuel;

            
            possib_mouv();

        }

        private void sombreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Black;
            theme = "sombre";
            cav_actuel = cavalierSombre;


            label1.ForeColor = Color.White;
            label1.BackColor = Color.Black;

            label2.ForeColor = Color.White;
            label2.BackColor = Color.Black;

            label6.ForeColor = Color.White;
            label6.BackColor = Color.Black;

            label7.ForeColor = Color.White;
            label7.BackColor = Color.Black;

            label5.ForeColor = Color.White;
            label5.BackColor = Color.Black;

            button1.BackColor = Color.LightGreen;
            button1.ForeColor = Color.Black;

            button2.BackColor = Color.LightGreen;
            button2.ForeColor = Color.Black;

            button3.BackColor = Color.LightGreen;
            button3.ForeColor = Color.Black;

            button4.BackColor = Color.LightGreen;
            button4.ForeColor = Color.Black;

            button5.BackColor = Color.LightGreen;
            button5.ForeColor = Color.Black;

            /*for (int i = 0; i < taille_echequier; i++)
            {
                for (int j = 0; j < taille_echequier; j++)
                {
                    PictureBox pb = (PictureBox)tableLayoutPanel1.GetControlFromPosition(j, i);
                    


                    //alternons les colueurs
                    if ((i + j) % 2 == 0)
                    {
                        pb.BackColor = Color.Black;
                    }
                    else
                    {
                        pb.BackColor = Color.LightGreen;
                    }

                }
            }*/
            ResetColors();

            if (posL >= 0 && posC >= 0)
                grille[posL, posC].Image = cav_actuel;

            
            possib_mouv();
        }

        private void ConstruireEchiquier()
        {
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.RowCount = taille_echequier;
            tableLayoutPanel1.ColumnCount = taille_echequier;

            for (int i = 0; i < taille_echequier; i++)
            {
                for (int j = 0; j < taille_echequier; j++)
                {
                    PictureBox pb = new PictureBox();
                    pb.Dock = DockStyle.Fill;
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;

                    //thème par defaut
                    pb.BackColor = ((i+j) % 2 == 0)? Color.Black : Color.LightGreen;

                    pb.Tag = (i, j);
                    pb.Click += Case_Click;

                    tableLayoutPanel1.Controls.Add(pb, j,i);
                    grille[i, j] = pb;
                }
            }
        }

        private void Case_Click(object sender, EventArgs e)
        {
            if(!partieEnCours)
            {
                MessageBox.Show("Cliquez sur <<Demarrer>> ou <<Aléatoire>> pour commencer.");
                return;
            }

            PictureBox pb = (PictureBox)sender;
            var(l,c) = ((int, int))pb.Tag;

            //1er coup
            if (premierCoupEnAttente)
            {
                PlacerCav(l, c);
                possib_mouv();

                premierCoupEnAttente = false;
                button3.Enabled = true; //Rejouer s'active

                label1.Text = $"Étape: {num_coup}/64";
                return;
            }

            //impossib de comeback sur une case
            if (visite[l, c]) return;

            //coup non valide
            if (!Coup_val(l, c)) return;

            //coup valide alors on déplace
            PlacerCav(l, c);
            possib_mouv();

            label1.Text = $"Étape: {num_coup}/64";
            if(num_coup ==64)
            {
                MessageBox.Show("Vous avez le MEILLEUR SCORE possible👏 avec 64 cases visitées");
                partieEnCours = false;
            }
            if(AucunCoupPossible())
            {
                MessageBox.Show($"Partie terminée! Vous avez {num_coup} cases visitées.", // Le message à afficher
                                "Fin de Partie",                          
                                MessageBoxButtons.OK,                     
                                MessageBoxIcon.Stop);                     
                partieEnCours = false;
                return;
            }
        }

        private void PlacerCav(int l, int c)
        {
            // Effacer ancienne position
            for (int i = 0; i < taille_echequier; i++)
                for (int j = 0; j < taille_echequier; j++)
                    grille[i, j].Image = null;

            // Nouvelle position
            grille[l, c].Image = cav_actuel;

            posL = l;
            posC = c;
            visite[l,c] = true;

            //mémor de la pos
            histo[idx_histo] = (l, c);
            idx_histo++;

            num_coup ++;
        }

        private bool Coup_val(int l, int c)
        {
            int dx = Math.Abs(l - posL);
            int dy = Math.Abs(c - posC);

            bool enL = (dx ==2 && dy ==1)||(dx==1 &&dy==2);
            if (!enL) return false;
            
            if (grille[l,c].Image != null)
                return false;
            return true;
        }

        private void possib_mouv()
        {
            //coulurs nbormales
            ResetColors();

            int[] dx = { 2, 2, 1, 1, -1, -1, -2, -2 };
            int[] dy = { 1, -1, 2, -2, 2, -2, 1, -1 };

            for (int k = 0; k < 8; k++)
            {
                int nl = posL + dx[k];
                int nc = posC + dy[k];

                if (nl >= 0 && nl < 8 && nc >= 0 && nc<8)
                {
                    if (grille[nl, nc].Image == null)
                        grille[nl, nc].BackColor = Color.Yellow;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            partieEnCours = true;
            premierCoupEnAttente = true;
            num_coup = 0;

            //reset des cases
            for(int i=0; i<8; i++)
            {
                for(int j=0; j<8; j++)
                {
                    visite[i,j] = false;
                }
            }

            ResetColors();

            label1.Text = "Étape: 0/64";
            button1.Enabled = false; //Demarrer désactivé
            button3.Enabled = false; //Rejouer désactié tnt quer pas jouer 1 case

        }

        private void button2_Click(object sender, EventArgs e)
        {
            partieEnCours = true;
            premierCoupEnAttente = false;
            num_coup = 0;

            ResetColors();

            for(int i=0; i<8;i++)
            {
                for( int j=0; j<8;j++)
                {
                    visite[i, j] = false;
                }
            }

            Random rnd = new Random();
            int l = rnd.Next(0,8);
            int c = rnd.Next(0,8);

            PlacerCav(l,c);
            possib_mouv();

            label1.Text = "Étape: 1/64";
            button1.Enabled = false;//Démarrer désactivé
            button3 .Enabled = true;//Rejouer activé
        }

        private void button3_Click(object sender, EventArgs e)
        {
            partieEnCours = false;
            premierCoupEnAttente = false; 
            num_coup = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++) 
                {
                    grille[i,j].Image = null;
                    visite[i,j] = false;
                }
            }
            ResetColors();

            label1.Text = "Étape: 0/64";

            button1.Enabled = true; //Démarrer activé
            button3.Enabled = false; //Rejouer désactivé

        }
        private bool AucunCoupPossible()
        {
            int[] dx = { 2, 2, 1, 1, -1, -1, -2, -2 };
            int[] dy = { 1, -1, 2, -2, 2, -2, 1, -1 };

            for (int k = 0; k < 8; k++)
            {
                int nl = posL + dx[k];
                int nc = posC + dy[k];

                if (nl >= 0 && nl < 8 && nc >= 0 && nc < 8)
                {
                    if (!visite[nl, nc])   // case non visitée = possible
                        return false;
                }
            }

            return true; // aucun mouvement possible

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!partieEnCours || posL<0 || posC<0)
            {
                MessageBox.Show("Commencer avant de lancer une simulation");
                return;
            }

            //ouvre la simulation sur la même pos
            FormSimulation sim = new FormSimulation(posL, posC,cav_actuel, theme);
            sim.Show();

            //désactive Formjeu d'abord
            this.Enabled = false;

            //formjeu fermé , activation forsim
            sim.FormClosed += (s, args) => this.Enabled = true;

        }

        private void règlesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRules regles = new FormRules();
            regles.Show();

            //désactive Formjeu d'abord
            this.Enabled = false;

            //formjeu fermé , activation forsim
            regles.FormClosed += (s, args) => this.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (idx_histo<6)
            {
                MessageBox.Show("Impossible: Moins de 5 coups joués!");
                return;
            }

            // Effacer l’image du cavalier partout
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    grille[i, j].Image = null;

            // Effacer les 5 dernières cases visitées
            for (int i = 0; i < 5; i++)
            {
                var (l, c) = histo[idx_histo - 1];
                visite[l, c] = false;
                idx_histo--;
                num_coup--;
            }

            // Nouvelle position = case juste avant les 5 coups annulés
            var (nl, nc) = histo[idx_histo - 1];

            posL = nl;
            posC = nc;

            // Remettre l'image du cavalier
            grille[nl, nc].Image = cav_actuel;

            // Mettre le compteur à jour
            label1.Text = $"Étape: {num_coup}/64";

            // Recalculer les cases jaunes
            possib_mouv();
        }

        private void ResetColors()
        {
            Color c1 = Color.Black;
            Color c2 = Color.LightGreen;

            if (theme == "clair")
            {
                c1 = Color.DarkGreen;
                c2 = Color.LightGreen;
            }
            else if (theme == "rouge")
            {
                c1 = Color.DarkRed;
                c2 = Color.Red;
            }

            for (int i = 0; i < taille_echequier; i++)
            {
                for (int j = 0; j < taille_echequier; j++)
                {
                    grille[i, j].BackColor = ((i + j) % 2 == 0) ? c1 : c2;
                }
            }
            
        }
    }
}
