using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicApp
{
    class Para
    {
        public Para(int x,int y)
        {
           this.origion=new Point(x,y);
        }

        private Point myPoint = new Point(50, 50);

        public Point MyPoint
        {
            get { return myPoint; }
            set { myPoint = value; }
        }


        private Point center = new Point(100, 100);

        public Point Center
        {
            get { return center; }
            set { center = value; }
        }

        private Point origion;

        public Point Origion
        {
            get { return origion; }
            set { origion = value; }
        }

        private double angle=30;

        public double Angle
        {
            get { return angle; }
            set { angle = value; }
        }


        private bool xDirection = true;

        public bool XDirection
        {
            get { return xDirection; }
            set { xDirection = value; }
        }

        private bool yDirection = true;

        public bool YDirection
        {
            get { return yDirection; }
            set { yDirection = value; }
        }
    }
}
