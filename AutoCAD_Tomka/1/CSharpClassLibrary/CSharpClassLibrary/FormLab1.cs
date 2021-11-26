using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpClassLibrary
{
    public partial class FormLab1 : Form
    {
        public float dotx { get; set; }
        public float doty { get; set; }
        public bool hasDot { get; set; }
        private Class1 class1 { get; set; }

        public FormLab1()
        {
            InitializeComponent();
            hasDot = false;
        }
        public FormLab1(Class1 class1)
        {
            InitializeComponent();
            hasDot = false;
            this.class1 = class1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dotx = float.Parse(this.textBox1.Text);
            doty = float.Parse(this.textBox2.Text);
            hasDot = true;
            class1.onButtonClick(dotx, doty);
        }
    }
}
