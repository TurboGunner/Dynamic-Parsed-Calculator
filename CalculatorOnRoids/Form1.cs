using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CalculatorOnRoids
{
    public partial class Form1 : Form
    {

        public string content;
        public static int amount; //Amount of textboxes/params
        public static List<float> values;

        public GroupBox groupBox1;
        public Parser parser;
        public Size textBoxSize;

        private void Form1_Load(object sender, EventArgs e)
        {
            Bounds = Screen.PrimaryScreen.Bounds;
        }

        public Form1()
        {
            InitializeComponent();

            Text = "Calculadora v1";

            label1.Text = "";
            label2.Text = "Equation: ";

            label3.Text = "Answer: ";
            label3.BackColor = Color.FromArgb(255, 60, 60, 60);

            BackColor = Color.FromArgb(255, 40, 40, 40);

            button1.Text = "Update";
            button2.Text = "Add";

            amount = 1;
            textBoxSize = new Size(80, 100);

            groupBox1 = new GroupBox();
            TextBox t = new TextBox();
        }

        private void BorderRemove(object sender, PaintEventArgs e)
        {
            SolidBrush brush = new SolidBrush(ActiveForm.BackColor);
            Rectangle rectangle = new Rectangle(new Point(0, 8), groupBox1.Size);
            e.Graphics.DrawRectangle(new Pen(brush), rectangle);
        }

        private void textBox1_TextChanged(object sender, EventArgs e) => content = textBox1.Text;

        private async void button1_Click(object sender, EventArgs e)
        {
            values = new List<float>();
            IEnumerator enumerator = groupBox1.Controls.GetEnumerator();

            label1.Text = "Values: \n";

            while (enumerator.MoveNext())
            {
                var temp = (TextBox) enumerator.Current;
                float.TryParse(temp.Text, out float val);

                values.Add(val);
                label1.Text += val + "\n";
            }
            parser = new Parser(textBox1.Text);
            parser.output = await parser.RunCode();
            label3.Text = "Answer: " + parser.output;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox1.Size = new Size(150, ActiveForm.Size.Height - 10);
            amount++;

            TextBox t = new TextBox
            {
                Location = new Point(30, (int)(10 * amount * 2.5)),
                Size = textBoxSize
            };

            groupBox1.FlatStyle = FlatStyle.Flat;

            groupBox1.Controls.Add(t);

            Controls.Add(groupBox1);
        }
    }
}