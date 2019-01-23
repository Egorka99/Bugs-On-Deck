using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using System.Windows.Forms;
using ClassLibraryBugs; 

namespace Bugs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Pen pen;
        Graphics g;
        SolidBrush color;

         
        BugsOnDeck bugs;

        private void pictureBox_Click(object sender, EventArgs e)
        {
            //Выбираем перо "myPen" черного цвета Black
            //толщиной в 1 пиксель:
             pen = new Pen(Color.Black, 2); 
             g = pictureBox.CreateGraphics();  
             color = new SolidBrush(Color.Black);
   
             bugs = new BugsOnDeck(g,pen,color);

             bugs.Spawn();

            foreach (var item in bugs.Info())
            {
                listBox1.Items.Add(item);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bugs.FirstStep();

            listBox1.Items.Clear();

            foreach (var item in bugs.Info() )
            {

                listBox1.Items.Add(item);
            }
           
        }

        private void buttonMove_Click(object sender, EventArgs e)
        {
            bugs.Move();

            listBox1.Items.Clear();

            foreach (var item in bugs.Info())
            {

                listBox1.Items.Add(item);
            }
        }
    }   
     
   
}
