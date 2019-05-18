using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace snake
{
    public class nodo
    {
        private System.Windows.Forms.PictureBox info;
        private nodo nxtp;
        private nodo prep;

        public nodo()
        {
            nxtp = prep = null;
        }

        public nodo(ref System.Windows.Forms.PictureBox info, nodo nxtp, nodo prep)
        {
            this.info = info;
            this.nxtp = nxtp;
            this.prep = prep;
        }

        public void setPoint(Point x)
        {
            this.info.Location = x;
        }

        public void setColor(Color c)
        {
            this.info.BackColor = c;
        }

        public System.Windows.Forms.PictureBox getInfo() { return info; }
        public nodo getNxtp() { return nxtp; }
        public nodo getPrep() { return prep; }
        public void setNxtp(nodo nxtp)
        {
            this.nxtp = nxtp;
        }
        public void setPrep(nodo prep)
        {
            this.prep = prep;
        }
    }

    public class coda
    {
        private nodo front;
        private nodo rear;

        public coda()
        {
            front = null;
            rear = null;
        }

        public bool mpty()
        {
            if (front == null && rear == null)
                return true;

            return false;
        }
        public System.Windows.Forms.PictureBox getFront()
        {
            return front.getInfo();
        }

        public System.Windows.Forms.PictureBox getRear()
        {
            return rear.getInfo();
        }

        public void push(System.Windows.Forms.PictureBox val)
        {
            nodo nuovo = new nodo(ref val, null, null);

            if (mpty())
                front = rear = nuovo;
            else
            {
                rear.setNxtp(nuovo);
                nuovo.setPrep(rear);
                rear = nuovo;
            }
            rear.setNxtp(front);
            front.setPrep(rear);
        }

        public void sposta(Point x, Color cFront, Color cNorm)
        {
            front.setColor(cNorm);
            rear.setPoint(x);
            rear.setColor(cFront);

            rear = rear.getPrep();
            front = front.getPrep();
        }

        public bool isLooping(System.Windows.Forms.PictureBox x)
        {
            nodo pa = front.getNxtp();

            while(pa != front)
            {
                if (pa.getInfo().Location == x.Location)
                    return true;

                pa = pa.getNxtp();
            }
            return false;
        }
    }
    public class engine
    {
        public int moveStd;
        public coda s;
        public int[,] colori = {
                { 255, 0, 0 },
                { 0, 0, 255 },
                { 0, 255, 0 }
            };
        public engine()
        {
            s = new coda();
            moveStd = 15;
        }
        public void sposta1(Point x)
        {
            s.sposta(x, Color.FromArgb(colori[1, 0], colori[1, 1], colori[1, 2]), Color.FromArgb(colori[0, 0], colori[0, 1], colori[0, 2]));
        }

        //1: rear
        public Point getUp1()
        { return new Point(s.getRear().Location.X, s.getRear().Location.Y - moveStd); }
        public Point getLeft1()
        { return new Point(s.getRear().Location.X - moveStd, s.getRear().Location.Y); }
        public Point getDown1()
        { return new Point(s.getRear().Location.X, s.getRear().Location.Y + moveStd); }
        public Point getRight1()
        { return new Point(s.getRear().Location.X + moveStd, s.getRear().Location.Y); }

        //normali: front
        public Point getUp()
        { return new Point(s.getFront().Location.X, s.getFront().Location.Y - moveStd); }
        public Point getLeft()
        { return new Point(s.getFront().Location.X - moveStd, s.getFront().Location.Y); }
        public Point getDown()
        { return new Point(s.getFront().Location.X, s.getFront().Location.Y + moveStd); }
        public Point getRight()
        { return new Point(s.getFront().Location.X + moveStd, s.getFront().Location.Y); }
    }
} //end class engine
