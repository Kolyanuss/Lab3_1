using System;
using System.Collections;
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
        /*private int squareWidth { get; set; }
        private int squareHight { get; set; }
        private int upperWidthSlot { get; set; }
        private int lowerWidthSlot { get; set; }
        private int roundingRadiusSlot { get; set; }
        private int hightSlot { get; set; }
        private int hightToCenterCircle { get; set; }
        private int circleDiameter { get; set; }*/

        public bool hasInfo { get; set; }
        private Class1 class1 { get; set; }

        public FormLab1()
        {
            InitializeComponent();
            hasInfo = false;
        }
        public FormLab1(Class1 class1)
        {
            InitializeComponent();
            hasInfo = false;
            this.class1 = class1;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            #region coment
            /*squareWidth = int.Parse(this.textBox1.Text);
            squareHight = int.Parse(this.textBox2.Text);
            upperWidthSlot = int.Parse(this.textBox3.Text);
            lowerWidthSlot = int.Parse(this.textBox4.Text);
            roundingRadiusSlot = int.Parse(this.textBox5.Text);
            hightSlot = int.Parse(this.textBox6.Text);
            hightToCenterCircle = int.Parse(this.textBox7.Text);
            circleDiameter = int.Parse(this.textBox8.Text);*/
            #endregion
            hasInfo = true;
            class1.onButtonClick(new List<int> {
            int.Parse(this.textBox1.Text),
            int.Parse(this.textBox2.Text),
            int.Parse(this.textBox3.Text),
            int.Parse(this.textBox4.Text),
            int.Parse(this.textBox5.Text),
            int.Parse(this.textBox6.Text),
            int.Parse(this.textBox7.Text),
            int.Parse(this.textBox8.Text)
        });
        }
    }
}
