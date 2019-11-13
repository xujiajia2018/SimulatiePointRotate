using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicApp
{
    public partial class MainForm : Form
    {
        Image the_back;
        Graphics graphic;

        Para para;

        public MainForm()
        {
            InitializeComponent();

            para = new Para(this.pictureBox1.Width / 2, this.pictureBox1.Height / 2);
            btnIni_Click(null, null);
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            double deltaX=para.MyPoint.X-para.Center.X;
            double deltaY=para.MyPoint.Y-para.Center.Y;
            double radius = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));


            Point point = para.Center;
            TransPoint(ref point);
            graphic.DrawEllipse(new Pen(Color.Blue, 3), (int)(point.X - radius), (int)(point.Y - radius), (int)(2 * radius), (int)(2 * radius));


            MyLocation myLocation = new MyLocation();
            myLocation.X = para.MyPoint.X;
            myLocation.Y = para.MyPoint.Y;
            var l= myLocation.GetLocation(myLocation, para.Center.X, para.Center.Y, para.Angle,para.XDirection,para.YDirection);

            Point pointResult = new Point(Convert.ToInt32(l.X), Convert.ToInt32(l.Y));

            TransPoint(ref pointResult);

            graphic.FillEllipse(Brushes.Green, pointResult.X - 5, pointResult.Y - 5, 10, 10);

            graphic.DrawString(string.Format("({0}º,{1},{2})", para.Angle.ToString("0"), l.X.ToString("0"), l.Y.ToString("0")), new Font("宋体", 10, FontStyle.Italic), Brushes.Black, pointResult);

            Point myPoint = para.MyPoint;

            TransPoint(ref myPoint);

            Point center = para.Center;

            TransPoint(ref center);

            graphic.DrawLine(new Pen(Brushes.Cyan,2), myPoint, center);

            graphic.DrawLine(new Pen(Brushes.Cyan,2), pointResult, center);



            //旋转180度参考点
            var point180 = myLocation.GetLocation(myLocation, para.Center.X, para.Center.Y, 180, para.XDirection, para.YDirection);

            Point pointResult180 = new Point(Convert.ToInt32(point180.X), Convert.ToInt32(point180.Y));

            TransPoint(ref pointResult180);

            graphic.FillEllipse(Brushes.Black, pointResult180.X - 5, pointResult180.Y - 5, 10, 10);

            graphic.DrawString(string.Format("(旋转180度)", point180.X.ToString("0"), point180.Y.ToString("0")), new Font("宋体", 10, FontStyle.Italic), Brushes.Black, pointResult180);

            this.pictureBox1.Image = the_back;
        }


        private void TransPoint(ref Point point)
        {
            // (-this.pictureBox1.Width/2,this.pictureBox1.Height / 2)=>(0,0)
            // (0，0）=>(this.pictureBox1.Width/2,this.pictureBox1.Height / 2)
            //(x1,y1)=>? 
            //Y0 is width

            double x;
            double y;
            if (para.XDirection == true)
                x = para.Origion.X + point.X;
            else
                x = para.Origion.X - point.X;
            if (para.YDirection == true)
                y = para.Origion.Y - point.Y;
            else
                y = para.Origion.Y + point.Y;
            
            point = new Point((int)x, (int)y);
           
        }

        private void btnDrawRandomPoint_Click(object sender, EventArgs e)
        {
            Point point = para.MyPoint;

            TransPoint(ref point);

            graphic.FillEllipse(Brushes.Red,point.X-5,point.Y-5, 10, 10);

            graphic.DrawString(string.Format("({0},{1})", para.MyPoint.X, para.MyPoint.Y), new Font("宋体", 10, FontStyle.Italic), Brushes.Black, point);

            this.pictureBox1.Image = the_back;
        }

        private void DrawAxis()
        {
            graphic.DrawLine(new Pen(Color.Blue, 3), 0, para.Origion.Y, this.pictureBox1.Width, para.Origion.Y);
            graphic.DrawLine(new Pen(Color.Blue, 3), para.Origion.X, 0, para.Origion.X, this.pictureBox1.Height);

            if (para.XDirection == true)
            {
                graphic.DrawString("→X", new Font("宋体", 20, FontStyle.Regular), Brushes.Black, new PointF(this.pictureBox1.Width - 50, para.Origion.Y));
            }
            else
            {
                graphic.DrawString("←X", new Font("宋体", 20, FontStyle.Regular), Brushes.Black, new PointF(10, para.Origion.Y));
            }
            if (para.YDirection == true)
            {
                graphic.DrawString("↑Y", new Font("宋体", 20, FontStyle.Regular), Brushes.Black, new PointF(para.Origion.X, 10));
            }
            else
            {
                graphic.DrawString("↓Y", new Font("宋体", 20, FontStyle.Regular), Brushes.Black, new PointF(para.Origion.X, this.pictureBox1.Height - 30));
            }
            graphic.DrawString("O(0,0)", new Font("宋体", 20, FontStyle.Italic), Brushes.Black, new PointF(para.Origion.X, para.Origion.Y));


            graphic.DrawString("说明：角度为正，点顺时针旋转，否则反之！", new Font("宋体", 10, FontStyle.Regular), Brushes.Red, new PointF(0, 0));
            this.pictureBox1.Image = the_back;
        }

        private void Clean()
        {
            this.pictureBox1.Image = null;
        }

        private void btnIni_Click(object sender, EventArgs e)
        {
            this.propertyGrid1.SelectedObject = para;

            if (graphic != null)
            {
                graphic.Dispose();
                the_back.Dispose();
            }


            the_back = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            graphic = Graphics.FromImage(the_back);

            DrawAxis();
            btnDrawRandomPoint_Click(null, null);
            btnDrawRotationCenter_Click(null, null);
            btnRun_Click(null, null);
        }

        private void btnDrawRotationCenter_Click(object sender, EventArgs e)
        {
            Point point = para.Center;

            TransPoint(ref point);

            graphic.FillEllipse(Brushes.Black, point.X - 5, point.Y - 5, 10, 10);

            graphic.DrawString(string.Format("({0},{1})", para.Center.X, para.Center.Y), new Font("宋体", 10, FontStyle.Italic), Brushes.Black, point);

            this.pictureBox1.Image = the_back;
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            Point point = new Point((int)this.pictureBox1.Width / 2, (int)this.pictureBox1.Height / 2);
            para.Origion = point;
            btnIni_Click(null, null);
        }
    }
}
