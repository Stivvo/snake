using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snake
{
    public partial class Form1 : Form
    {
        engine en = new engine();
        char direction;
        bool start;
        public Form1()
        {
            InitializeComponent();

            direction = 'd';
            newPicture(new Point(0, 0));
            start = true;
        }
        public void newPicture(Point x)
        {
            System.Windows.Forms.PictureBox nuovo = new System.Windows.Forms.PictureBox();

            nuovo.Height = nuovo.Width = en.moveStd;
            nuovo.Location = x;
            nuovo.BackColor = Color.FromArgb(en.colori[0, 0], en.colori[0, 1], en.colori[0, 2]);
            Controls.Add(nuovo);

            en.s.push(nuovo);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Point p = new Point();

            switch (direction)
            {
                case 'w':
                    p= en.getUp();
                    break;
                case 'a':
                    p = en.getLeft();
                    break;
                case 's':
                    p = en.getDown();
                    break;
                case 'd':
                    p = en.getRight();
                    break;
            }
            if (timer1.Enabled == true)
            {
                if (p.X >= 0 && p.X <= ClientSize.Width && p.Y >= 0 && p.Y <= ClientSize.Height)
                {
                    en.sposta1(p);

                    if (en.s.isLooping(en.s.getFront()))
                        timer1.Enabled = false;
                }
                else
                    timer1.Enabled = false;
            }

            if (food.Bounds.IntersectsWith(en.s.getFront().Bounds))
            {
                Random r = new Random();

                food.Location = new Point(
                r.Next(0,  (ClientSize.Width - food.Width) / en.moveStd ) * en.moveStd,
                r.Next(0,  (ClientSize.Height - food.Height) / en.moveStd ) * en.moveStd
                );

                switch (direction)
                {
                    case 'w':
                        newPicture(en.getUp1());
                        break;
                    case 'a':
                        newPicture(en.getLeft1());
                        break;
                    case 's':
                        newPicture(en.getDown1());
                        break;
                    case 'd':
                        newPicture(en.getRight1());
                        break;
                }
            }
            if (timer1.Enabled == false)
                MessageBox.Show("Game over");
        }

        private void Form1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'w' || e.KeyChar == 'a' || e.KeyChar == 's' || e.KeyChar == 'd')
            {
                if (e.KeyChar == 'w' && direction != 's' || e.KeyChar == 's' && direction != 'w'||
                    e.KeyChar == 'a' && direction != 'd' || e.KeyChar == 'd' && direction != 'a')
                direction = e.KeyChar;
            }
            else if (e.KeyChar == 'p' && start)
            {
                timer1.Enabled = true;
                start = false;
            }
        }
    }
}
