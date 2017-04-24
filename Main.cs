using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookStoreProject
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddPeople f1 = new AddPeople();
            f1.ShowDialog(); // Shows People
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddBooks f2 = new AddBooks();
            f2.ShowDialog(); // Shows Books
            
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            Check_Out f2 = new Check_Out();
            f2.ShowDialog(); // Shows Books
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Checkin f2 = new Checkin();
            f2.ShowDialog();

            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }


    }
}
