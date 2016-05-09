using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MemoryGame
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FirstLevel form = new FirstLevel();
            form.Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SecondLevel form = new SecondLevel();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ThirdLevel form = new ThirdLevel();
            form.Show();

        }
    }
}
