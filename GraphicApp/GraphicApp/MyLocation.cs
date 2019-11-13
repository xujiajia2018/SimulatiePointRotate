#region 此类为徐佳佳所有，未经允许不得使用
//严重声明！！！！！
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GraphicApp
{
    class MyLocation
    {
        private double x;
        [CategoryAttribute("MyLocation"), DisplayNameAttribute("X坐标值："), DescriptionAttribute("X坐标值"), ReadOnlyAttribute(false)]
        public double X
        {
            get { return x; }
            set { x = value; }
        }

        private double y;
        [CategoryAttribute("MyLocation"), DisplayNameAttribute("Y坐标值："), DescriptionAttribute("Y坐标值"), ReadOnlyAttribute(false)]
        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        private double z;
        [CategoryAttribute("MyLocation"), DisplayNameAttribute("Z坐标值："), DescriptionAttribute("Z坐标值"), ReadOnlyAttribute(false)]
        public double Z
        {
            get { return z; }
            set { z = value; }
        }

        private double rad;
        [CategoryAttribute("MyLocation"), DisplayNameAttribute("Rad："), DescriptionAttribute("弧度"), ReadOnlyAttribute(false)]
        public double Rad
        {
            get { return rad; }
            set { rad = value; }
        }

        private double degree;
        [CategoryAttribute("MyLocation"), DisplayNameAttribute("Degree："), DescriptionAttribute("角度"), ReadOnlyAttribute(false)]
        public double Degree
        {
            get { return degree; }
            set { degree = value; }
        }

        /// <summary>
        /// MyLocation类的+运算符重载，必须是Static
        /// </summary>
        /// <param name="loc">new Loaction</param>
        /// <param name="loc1">new Loaction</param>
        /// <returns></returns>
        public static MyLocation operator +(MyLocation loc, MyLocation loc1)
        {
            MyLocation MyLocation = new MyLocation();

            foreach (PropertyInfo item in MyLocation.GetType().GetProperties())
            {
                Type t = item.PropertyType;
                item.SetValue(MyLocation, (double)loc.GetType().GetProperty(item.Name).GetValue(loc) + (double)loc1.GetType().GetProperty(item.Name).GetValue(loc1));
            }
            return MyLocation;
        }

        /// <summary>
        /// 返回旋转后的MyLocation
        /// </summary>
        /// <param name="MyLocation">旋转之前MyLocation</param>
        /// <param name="X0">旋转中心X0</param>
        /// <param name="Y0">旋转中心Y0</param>
        /// <param name="degree">旋转的角度</param>
        /// <param name="X_Positive">用于判断相限，水平方向沿→递增为真</param>
        /// <param name="Y_Positive">用于判断相限，垂直方向向↑递增为真</param>
        /// <returns></returns>
        public MyLocation GetLocation(MyLocation myLocation, double X0, double Y0, double degree, bool X_Positive = true, bool Y_Positive = true)
        {
            /*绕原点旋转矩阵表达式    https://blog.csdn.net/u012138730/article/details/80320162
           [cos(θ)，sin(θ)]    [x]    [x1]
           [-sin(θ)，cos(θ)]   [y]    [y1]
             */

            MyLocation loc = new MyLocation();

            double rad = (degree / 180) * Math.PI;

            if (X_Positive == true && Y_Positive == true)
            {
                //角度为正 顺时针旋转
                loc.X = (myLocation.X - X0) * Math.Cos(rad) + (myLocation.Y - Y0) * Math.Sin(rad) + X0;
                loc.Y = -(myLocation.X - X0) * Math.Sin(rad) + (myLocation.Y - Y0) * Math.Cos(rad) + Y0;
            }
            else if (X_Positive == false && Y_Positive == true)
            {
                //角度为正 逆时针旋转
                rad = -rad;
                loc.X = (myLocation.X - X0) * Math.Cos(rad) + (myLocation.Y - Y0) * Math.Sin(rad) + X0;
                loc.Y = -(myLocation.X - X0) * Math.Sin(rad) + (myLocation.Y - Y0) * Math.Cos(rad) + Y0;
            }
            else if (X_Positive == true && Y_Positive == false)
            {
                //角度为正 逆时针旋转
                rad = -rad;
                loc.X = (myLocation.X - X0) * Math.Cos(rad) + (myLocation.Y - Y0) * Math.Sin(rad) + X0;
                loc.Y = -(myLocation.X - X0) * Math.Sin(rad) + (myLocation.Y - Y0) * Math.Cos(rad) + Y0;
            }
            else
            {
                //角度为正 顺时针旋转
                loc.X = (myLocation.X - X0) * Math.Cos(rad) + (myLocation.Y - Y0) * Math.Sin(rad) + X0;
                loc.Y = -(myLocation.X - X0) * Math.Sin(rad) + (myLocation.Y - Y0) * Math.Cos(rad) + Y0;
            }

            return loc;
        }
    }
}
