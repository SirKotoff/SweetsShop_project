using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SweetsShop_project
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Priomka priomka = new Priomka();
            this.Close();
            priomka.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Prodaza prodaza = new Prodaza();
            this.Close();
            prodaza.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Create_oth create_Oth = new Create_oth();
            this.Close();
            create_Oth.Show();
           // Help.ShowHelp(this, "");
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "InformationSystem.chm");

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://vk.com/id197441719");

        }
    }
}
