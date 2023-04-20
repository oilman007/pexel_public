using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;


namespace Pexel
{

    public class Pillar
    {
        public Pillar()
        {
            Reset(0, 0, 0, 0, 0, 0);
        }



        public Pillar(double xt, double yt, double zt, double xb, double yb, double zb)
        {
            Reset(xt, yt, zt, xb, yb, zb);
        }


        public Pillar(Point3D top, Point3D bottom)
        {
            Reset(top, bottom);
        }

        public Pillar(double k_xz, double b_xz, double k_yz, double b_yz)
        {
            this.k_xz = k_xz;
            this.b_xz = b_xz;
            this.k_yz = k_yz;
            this.b_yz = b_yz;
        }




        public void Reset(Point3D top, Point3D bottom)
        {
            Reset(top.X, top.Y, top.Z, bottom.X, bottom.Y, bottom.Z);
        }



        public void Reset(double xt, double yt, double zt, double xb, double yb, double zb)
        {
            if (zb - zt == 0)
            {
                k_xz = 0;
                k_yz = 0;
            }
            else
            {
                k_xz = (xb - xt) / (zb - zt);
                k_yz = (yb - yt) / (zb - zt);
            } 
            b_xz = xt - k_xz * zt;
            b_yz = yt - k_yz * zt;
        }



        public Point3D Point3D(double depth)
        {
            return new Point3D(k_xz * depth + b_xz, k_yz * depth + b_yz, depth);
        }



        public Point2D Point2D(double depth)
        {
            return new Point2D(k_xz * depth + b_xz, k_yz * depth + b_yz);
        }



        double k_xz, b_xz;
        double k_yz, b_yz;



        public const double TopDepth = 0;
        public const double BottomDepth = 9999;

        public Point3D Top() { return Point3D(TopDepth); }
        public Point3D Bottom() { return Point3D(BottomDepth); }



        override public string ToString()
        {
            return Point3D(TopDepth).ToString() + " " + Point3D(BottomDepth).ToString();
        }




        public void Write(BinaryWriter writer)
        {
            writer.Write(k_xz);
            writer.Write(b_xz);
            writer.Write(k_yz);
            writer.Write(b_yz);
        }


        public static Pillar Read(BinaryReader reader)
        {
            return new Pillar(reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble());
        }
        

        Point3D CreateVector(Point3D a, Point3D b)
        //Функция создает вектор из двух точек A,B.
        {
            return new Point3D(b.X - a.X, b.Y - a.Y, b.Z - a.Z);
        }

        Point3D VectorPropduct(Point3D a, Point3D b)
        //Векторное произведение
        {
            return new Pexel.Point3D(a.Y * b.Z - b.Y * a.Z, a.Z * b.X - b.Z * a.X, a.X * b.Y - b.X * a.Y);
        }

        double DotProduct(Point3D a, Point3D b)
        //Скалярное произведение
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        void Normalize(ref Point3D a)
        //Привести длину вектора к единице
        {
            double mlr = Math.Sqrt(a.X * a.X + a.Y * a.Y + a.Z * a.Z);
            a.X = a.X / mlr;
            a.Y = a.Y / mlr;
            a.Z = a.Z / mlr;
        }

        public Point3D Intersection(Point3D a, Point3D b, Point3D c, Point3D x, Point3D y, out bool intersect)
        // Итак на входе у нас три точки плоскости A,B,C и две точки прямой X,Y
        {
            Point3D n, v, w;
            double e, d;
            n = VectorPropduct(CreateVector(a, b), CreateVector(a, c));
            Normalize(ref n);
            v = CreateVector(x, a);
            // расстояние до плоскости по нормали
            d = DotProduct(n, v);
            w = CreateVector(x, y);
            // приближение к плоскости по нормали при прохождении отрезка
            e = DotProduct(n, w);
            if (e != 0) // одна точка , 
                        //в любом другом случае(принадлежит, или параллельна плоскости)
                        //флаг сигнализирующий что единственная точка не найдена будет правдой
            {
                intersect = true;
                return new Point3D(x.X + w.X * d / e, x.Y + w.Y * d / e, x.Z + w.Z * d / e);
            }
            else
            {
                intersect = false;
                return new Pexel.Point3D();
            }
        }




        public static bool operator ==(Pillar p1, Pillar p2)
        {
            return p1.k_xz == p2.k_xz &&
                   p1.b_xz == p2.b_xz &&
                   p1.k_yz == p2.k_yz &&
                   p1.b_yz == p2.b_yz;
        }


        public static bool operator !=(Pillar p1, Pillar p2)
        {
            return p1.k_xz != p2.k_xz ||
                   p1.b_xz != p2.b_xz ||
                   p1.k_yz != p2.k_yz ||
                   p1.b_yz != p2.b_yz;
        }




    }
}
