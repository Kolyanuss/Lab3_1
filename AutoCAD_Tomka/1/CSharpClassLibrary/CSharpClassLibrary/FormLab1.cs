using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpClassLibrary
{
    public partial class FormLab1 : Form
    {
        public int dotx { get; set; }
        public int doty { get; set; }
        public bool hasDot { get; set; }

        public FormLab1()
        {
            InitializeComponent();
            hasDot = false;
        }

        public KeyValuePair<int,int> getDot()
        {
            return new KeyValuePair<int, int>(dotx,doty);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dotx = Int32.Parse(this.textBox1.Text);
            doty = Int32.Parse(this.textBox2.Text);
            hasDot = true;
        }
    }
}
