using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;



namespace Pexel
{



    [Serializable]
    public class CellFace
    {


        public CellFace() 
        {
            Corners = new Point3D[4];
        }


        public CellFace(Point3D c0, Point3D c1, Point3D c2, Point3D c3)
        {
            Corners = new Point3D[4] { c0, c1, c2, c3 };
        }


        public Point3D Center()
        {
            Point3D result = new Point3D(0, 0, 0);
            for (int i = 0; i < 4; ++i)
                result += Corners[i];
            return result / 4;
        }


        public Point3D[] Corners { private set; get; }




        public double MinX()
        {
            double result = Corners[0].X;
            for (int i = 1; i < 4; ++i)
                if (result > Corners[i].X)
                    result = Corners[i].X;
            return result;
        }


        public double MaxX()
        {
            double result = Corners[0].X;
            for (int i = 1; i < 4; ++i)
                if (result < Corners[i].X)
                    result = Corners[i].X;
            return result;
        }



        public double MinY()
        {
            double result = Corners[0].Y;
            for (int i = 1; i < 4; ++i)
                if (result > Corners[i].Y)
                    result = Corners[i].Y;
            return result;
        }



        public double MaxY()
        {
            double result = Corners[0].Y;
            for (int i = 1; i < 4; ++i)
                if (result < Corners[i].Y)
                    result = Corners[i].Y;
            return result;
        }


        public double MinZ()
        {
            double result = Corners[0].Z;
            for (int i = 1; i < 4; ++i)
                if (result > Corners[i].Z)
                    result = Corners[i].Z;
            return result;
        }



        public double MaxZ()
        {
            double result = Corners[0].Z;
            for (int i = 1; i < 4; ++i)
                if (result < Corners[i].Z)
                    result = Corners[i].Z;
            return result;
        }


        
        public bool Contain(Point2D point, CoordPlane plane)
        {
            Point2D c0 = new Point2D(), c1 = new Point2D(), c2 = new Point2D(), c3 = new Point2D();
            switch (plane)
            {
                case CoordPlane.XY:
                    c0 = new Point2D(Corners[0].X, Corners[0].Y);
                    c1 = new Point2D(Corners[1].X, Corners[1].Y);
                    c2 = new Point2D(Corners[2].X, Corners[2].Y);
                    c3 = new Point2D(Corners[3].X, Corners[3].Y);
                    break;
                case CoordPlane.XZ:
                    c0 = new Point2D(Corners[0].X, Corners[0].Z);
                    c1 = new Point2D(Corners[1].X, Corners[1].Z);
                    c2 = new Point2D(Corners[2].X, Corners[2].Z);
                    c3 = new Point2D(Corners[3].X, Corners[3].Z);
                    break;
                case CoordPlane.YZ:
                    c0 = new Point2D(Corners[0].Y, Corners[0].Z);
                    c1 = new Point2D(Corners[1].Y, Corners[1].Z);
                    c2 = new Point2D(Corners[2].Y, Corners[2].Z);
                    c3 = new Point2D(Corners[3].Y, Corners[3].Z);
                    break;
            }
            return InsideQuadrangle(c0, c1, c2, c3, point);
        }

        /*
        public bool Inside(Point3D p)
        {
            bool x = MinX() <= p.X && p.X <= MaxX();
            bool y = MinY() <= p.Y && p.Y <= MaxY();
            bool z = MinZ() <= p.Z && p.Z <= MaxZ();
            return x && y && z;
        }
        */




        bool InsideQuadrangle(Point2D p0, Point2D p1, Point2D p2, Point2D p3, Point2D p)
        {
            return InsideTriangle(p0, p1, p2, p) || InsideTriangle(p1, p2, p3, p);
        }

        bool InsideTriangle(Point2D p0, Point2D p1, Point2D p2, Point2D p)
        {
            bool cp0 = CrossProduct(p0, p1, p) < 0.0;
            bool cp1 = CrossProduct(p1, p2, p) < 0.0;
            bool cp2 = CrossProduct(p2, p0, p) < 0.0;
            return cp0 == cp1 && cp1 == cp2 && cp2 == cp0;
        }

        double CrossProduct(Point2D p0, Point2D p1, Point2D p)
        {
            return (p.X - p1.X) * (p1.Y - p0.Y) - (p.Y - p1.Y) * (p1.X - p0.X);
        }


        static public CellFace operator +(CellFace cf1, CellFace cf2)
        {
            return new CellFace(cf1.Corners[0] + cf2.Corners[0], cf1.Corners[1] + cf2.Corners[1],
                                cf1.Corners[2] + cf2.Corners[2], cf1.Corners[3] + cf2.Corners[3]);
        }
        static public CellFace operator -(CellFace cf1, CellFace cf2)
        {
            return new CellFace(cf1.Corners[0] - cf2.Corners[0], cf1.Corners[1] - cf2.Corners[1],
                                cf1.Corners[2] - cf2.Corners[2], cf1.Corners[3] - cf2.Corners[3]);
        }
        static public CellFace operator /(CellFace cf, double div)
        {
            return new CellFace(cf.Corners[0] / div, cf.Corners[1] / div,
                                cf.Corners[2] / div, cf.Corners[3] / div);
        }
        static public CellFace operator *(CellFace cf, double mult)
        {
            return new CellFace(cf.Corners[0] * mult, cf.Corners[1] * mult,
                                cf.Corners[2] * mult, cf.Corners[3] * mult);
        }




        /*
            2 3
            0 1
        */






        public bool Intersect(Point3D x, Point3D y, out Point3D intersection)
        {
            Point3D a = Corners[0], b = Corners[1], c = Corners[2];
            Point3D n = VectorProduct(Vector(a, b), Vector(a, c));
            Normalize(ref n);
            Point3D v = Vector(x, a);
            double d = DotProduct(n, v);
            Point3D w = Vector(x, y);
            double e = DotProduct(n, w);
            if (e == 0)
            {
                intersection = new Point3D();
                return false;
            }
            else
            {
                intersection = new Point3D(x.X + w.X * d / e, x.Y + w.Y * d / e, x.Z + w.Z * d / e);
                return InsideQuadrangle(Corners[0], Corners[1], Corners[2], Corners[3], intersection);
            }
        }


        Point3D Vector(Point3D a, Point3D b)
        {
            return new Point3D(b.X - a.X, b.Y - a.Y, b.Z - a.Z);
        }

        Point3D VectorProduct(Point3D a, Point3D b)
        {
            return new Point3D(a.Y * b.Z - b.Y * a.Z, a.Z * b.X - b.Z * a.X, a.X * b.Y - b.X * a.Y);
        }

        double DotProduct(Point3D a, Point3D b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        void Normalize(ref Point3D a)
        //Привести длину вектора к единице
        {
            double mlr = Math.Sqrt((a.X * a.X + a.Y * a.Y + a.Z * a.Z));
            a.X = a.X / mlr;
            a.Y = a.Y / mlr;
            a.Z = a.Z / mlr;
        }










        bool InsideQuadrangle(Point3D p0, Point3D p1, Point3D p2, Point3D p3, Point3D p)
        {
            return InsideTriangle(p0, p1, p2, p) || InsideTriangle(p1, p2, p3, p);
        }



        /*
            Иногда встречается необходимость определения принадлежности точки какой-нибудь геометрической фигуре. 
            Самой простейшей является треугольник ABC.
            Дано:

            Точка P(P_x,P_y,P_z), треугольник с вершинами: A(A_x,A_y,A_z), B(B_x,B_y,B_z), C(C_x,C_y,C_z).
            методика

            Формируются три треугольника: ABP, ACP, BCP. После вычисляются их площади SABP,SACP,SBCP. 
            После этого сверяется сумма этих площадей с площадью треугольника SABC. Если точка лежит на треугольнике ABC, 
            то треугольники ABP, ACP, BCP будут просто частями треугольника ABC, и сумма их площадей будет равна его площади SABC. 
            Если же точка не принадлежит треугольнику, сумма площадей SABP,SACP,SBCP превысит площадь треугольника ABC.
            Легкое лирическое отступление для тех, кто не помнит, чему равна площадь треугольника: проще всего использовать формулу Герона, 
            которая позволяет найти площадь треугольника, зная только его стороны, оч. удобно
        */

        const double EPS = 1e-6f; //1e-6f
        bool InsideTriangle(Point3D a, Point3D b, Point3D c, Point3D p)
        {
            double AB = Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y) + (a.Z - b.Z) * (a.Z - b.Z));
            double BC = Math.Sqrt((b.X - c.X) * (b.X - c.X) + (b.Y - c.Y) * (b.Y - c.Y) + (b.Z - c.Z) * (b.Z - c.Z));
            double CA = Math.Sqrt((a.X - c.X) * (a.X - c.X) + (a.Y - c.Y) * (a.Y - c.Y) + (a.Z - c.Z) * (a.Z - c.Z));

            double AP = Math.Sqrt((p.X - a.X) * (p.X - a.X) + (p.Y - a.Y) * (p.Y - a.Y) + (p.Z - a.Z) * (p.Z - a.Z));
            double BP = Math.Sqrt((p.X - b.X) * (p.X - b.X) + (p.Y - b.Y) * (p.Y - b.Y) + (p.Z - b.Z) * (p.Z - b.Z));
            double CP = Math.Sqrt((p.X - c.X) * (p.X - c.X) + (p.Y - c.Y) * (p.Y - c.Y) + (p.Z - c.Z) * (p.Z - c.Z));
            double diff = (TriangleSquare(AP, BP, AB) + TriangleSquare(AP, CP, CA) + TriangleSquare(BP, CP, BC)) - TriangleSquare(AB, BC, CA);

            return Math.Abs(diff) < EPS;
        }


        double TriangleSquare(double a, double b, double c)
        {
            double p = (a + b + c) / 2;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

    }
}
