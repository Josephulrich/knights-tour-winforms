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
    public partial class FormRules : Form
    {
        private Image img = Image.FromFile("Images\\eurler.png");
        public FormRules()
        {
            InitializeComponent();
        }

        private void FormRules_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = img;
            //this.for
            label1.Text = "Le but du jeu est de parcourir les 64 cases de l’échiquier en déplaçant un cavalier SANS JAMAIS REVISITER une case.\r\n\r\n" +
                "------------------------------------\r\n♞ #RÈGLE DE DÉPLACEMENT DU CAVALIER #:\r\nLe cavalier se déplace en forme " +
                "de 'L':\r\n- 2 cases dans une direction, puis 1 case perpendiculaire  \r\n- ou 1 case, puis 2 cases perpendiculaire\r\n\r\nExemples " +
                ":\r\n• ( +2 , +1 )\r\n• ( +2 , –1 )\r\n• ( –1 , +2 )\r\n• … etc (8 possibilités)\r\n\r\n------------------------------------\r\n🏁 #COMMENT " +
                "JOUER :#\r\n1. Cliquez sur #Démarrer#  \r\n2. Choisissez votre #première case#  \r\n3. Le jeu affiche les **coups possibles en jaune**  \r\n4. " +
                "Cliquez sur une case valide pour avancer  \r\n5. Le compteur affiche votre progression (1 à 64)\r\n\r\n------------------------------------\r\n" +
                "🚫 **RESTRICTIONS :**\r\n- Dans ce jeu , vous avez toutefois la possibilité de revenir sur mes 5 dernières cases à travers le bouton #COMEBACK# " +
                "\r\n- Si aucun déplacement n’est possible -> Partie terminée\r\n\r\n" +
                "------------------------------------\r\n🤖 #SIMULATION AUTOMATIQUE #:\r\nLe bouton #Simulation# lance un parcours automatique\r\nbasé sur une version" +
                " simplifiée de la méthode d’Euler / Warnsdorff .\r\n\r\n------------------------------------\r\n🎯 #BUT FINAL# :\r\nRéaliser un **chemin complet de 64" +
                " cases** (Tour du Cavalier)";

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
