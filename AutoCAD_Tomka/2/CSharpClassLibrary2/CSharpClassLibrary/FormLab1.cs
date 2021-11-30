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

namespace CSharpClassLibrary2
{
    public partial class FormLab1 : Form
    {
        private int squareWidth { get; set; }
        private int squareHight { get; set; }
        private int upperWidthSlot { get; set; }
        private int lowerWidthSlot { get; set; }
        private int roundingRadiusSlot { get; set; }
        private int hightSlot { get; set; }
        private int hightToCenterCircle { get; set; }
        private int circleDiameter { get; set; }

        private Class1 class1 { get; set; }

        public FormLab1()
        {
            InitializeComponent();
            myValInitialize();
        }
        public FormLab1(Class1 class1)
        {
            InitializeComponent();
            myValInitialize();
            this.class1 = class1;
        }

        private void myValInitialize()
        {
            squareWidth = 72;
            squareHight = 60;
            upperWidthSlot = 29;
            lowerWidthSlot = 42;
            roundingRadiusSlot = 9;
            hightSlot = 17;
            hightToCenterCircle = 17;
            circleDiameter = 20;
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
            if (checkBox1.Checked)
            {
                this.textBox1.Text = squareWidth.ToString();
                this.textBox2.Text = squareHight.ToString();
                this.textBox3.Text = upperWidthSlot.ToString();
                this.textBox4.Text = lowerWidthSlot.ToString();
                this.textBox5.Text = roundingRadiusSlot.ToString();
                this.textBox6.Text = hightSlot.ToString();
                this.textBox7.Text = hightToCenterCircle.ToString();
                this.textBox8.Text = circleDiameter.ToString();
            }
            else
            {
                squareWidth = int.Parse(this.textBox1.Text);
                squareHight = int.Parse(this.textBox2.Text);
                upperWidthSlot = int.Parse(this.textBox3.Text);
                lowerWidthSlot = int.Parse(this.textBox4.Text);
                roundingRadiusSlot = int.Parse(this.textBox5.Text);
                hightSlot = int.Parse(this.textBox6.Text);
                hightToCenterCircle = int.Parse(this.textBox7.Text);
                circleDiameter = int.Parse(this.textBox8.Text);
            }

            class1.onButtonClick(new List<int>
            {
                squareWidth, squareHight, upperWidthSlot, lowerWidthSlot,
                roundingRadiusSlot, hightSlot, hightToCenterCircle, circleDiameter,
            });

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int whatDraw = 0;
            if (checkBox2.Checked && checkBox3.Checked)
            {
                whatDraw = 3;
            }
            else if (checkBox2.Checked)
            {
                whatDraw = 1;
            }
            else if (checkBox3.Checked)
            {
                whatDraw = 2;
            }

            class1.onButtonEditClick(whatDraw, textBox9.Text, comboBox1.Text, textBox10.Text, comboBox2.Text);
        }
    }
}
