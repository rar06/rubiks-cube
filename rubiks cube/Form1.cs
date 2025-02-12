using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rubiks_cube
{
    public partial class MainFrame : Form
    {
        public MainFrame()
        {
            InitializeComponent();
        }

        private void LoadSolver(object sender, EventArgs e)
        {
            // Create a new instance of the Game_Screen form.
            Game_Screen cubewindow = new Game_Screen();
            // Show the Game_Screen form to the user.
            cubewindow.Show();
            
        }

        private void LoadHelper(object sender, EventArgs e)
        {
            // Create a new instance of the Helper_Cube form.
            Helper_Cube helperwindow = new Helper_Cube();
            // Show the Helper_Cube form to the user.
            helperwindow.Show();
        }

        private void LoadCubePlay(object sender, EventArgs e)
        {
            // Create a new instance of the Cube_Play form.
            Cube_Play cubeplaywindow = new Cube_Play();
            // Show the Cube_Play form to the user.
            cubeplaywindow.Show();
        }
    }
}
