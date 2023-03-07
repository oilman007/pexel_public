using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;





// corners
/*
             Y
            /
           /
          /
         2----3
        /|   /|
       / |  / |
      /  6-/- 7
     /  / /  /
    0----1------------------------ X
    | /  | /
    |/   |/
    4----5
    |
    |
    |
    |
    |
    Z
*/



namespace Pexel
{
    public enum CoordPlane { XY, XZ, YZ }
    public enum FaceDistance { Near, Middle, Far }

    [Serializable]
    public class Cell
    {
        Cell()
        {
            Corners = new Point3D[8];
            Corners[0] = new Point3D();
            Corners[1] = new Point3D();
            Corners[2] = new Point3D();
            Corners[3] = new Point3D();
            Corners[4] = new Point3D();
            Corners[5] = new Point3D();
            Corners[6] = new Point3D();
            Corners[7] = new Point3D();
            Act = true;
            // generate
            ///GenerateParam();
        }
        
        public Cell(Point3D c0, Point3D c1, Point3D c2, Point3D c3, Point3D c4, Point3D c5, Point3D c6, Point3D c7, bool act)
        {
            Corners = new Point3D[8];
            Corners[0] = c0;
            Corners[1] = c1;
            Corners[2] = c2;
            Corners[3] = c3;
            Corners[4] = c4;
            Corners[5] = c5;
            Corners[6] = c6;
            Corners[7] = c7;
            Act = act;
            // generate
            ///GenerateParam();
        }

        void GenerateParam()
        { 
            // 1
            Center = _Center();
            MinX = _MinX();
            MinY = _MinY();
            MinZ = _MinZ();
            MaxX = _MaxX();
            MaxY = _MaxY();
            MaxZ = _MaxZ();
            // 2
            TopFace = _TopFace();
            BottomFace = _BottomFace();
            LeftFace = _LeftFace();
            RightFace = _RightFace();
            NearFace = _NearFace();
            FarFace = _FarFace();
            // 3
            MiddleFrontFace = _MiddleFrontFace();
            MiddleTopFace = _MiddleTopFace();
            MiddleLeftFace = _MiddleLeftFace();
            // 4
            Thickness = _Thickness();
            Length = _Length();
            Width = _Width();
            Volume = _Volume();
        }




        public Point3D[] Corners { private set; get; }
        public bool Act { set; get; }

        public Point3D Center { private set { _center = value; } get { if (_center == null) _center = _Center();  return _center; } }
        public double MinX { private set { _minX = value; } get {  if (_minX == undef_values) _minX = _MinX(); return _minX; } }
        public double MinY { private set { _minY = value; } get { if (_minY == undef_values) _minY = _MinY(); return _minY; } }
        public double MinZ { private set { _minZ = value; } get { if (_minZ == undef_values) _minZ = _MinZ(); return _minZ; } }
        public double MaxX { private set { _maxX = value; } get { if (_maxX == undef_values) _maxX = _MaxX(); return _maxX; } }
        public double MaxY { private set { _maxY = value; } get { if (_maxY == undef_values) _maxY = _MaxY(); return _maxY; } }
        public double MaxZ { private set { _maxZ = value; } get { if (_maxZ == undef_values) _maxZ = _MaxZ(); return _maxZ; } }
        public CellFace TopFace { private set { _topFace = value; } get { if (_topFace == null) _topFace = _TopFace(); return _topFace; } }
        public CellFace BottomFace { private set { _bottomFace = value; } get { if (_bottomFace == null) _bottomFace = _BottomFace(); return _bottomFace; } }
        public CellFace LeftFace { private set { _leftFace = value; } get { if (_leftFace == null) _leftFace = _LeftFace(); return _leftFace; } }
        public CellFace RightFace { private set { _rightFace = value; } get { if (_rightFace == null) _rightFace = _RightFace(); return _rightFace; } }
        public CellFace NearFace { private set { _nearFace = value; } get { if (_nearFace == null) _nearFace = _NearFace(); return _nearFace; } }
        public CellFace FarFace { private set { _farFace = value; } get { if (_farFace == null) _farFace = _FarFace(); return _farFace; } }
        public CellFace MiddleFrontFace { private set { _middleFrontFace = value; } get { if (_middleFrontFace == null) _middleFrontFace = _MiddleFrontFace(); return _middleFrontFace; } }
        public CellFace MiddleTopFace { private set { _middleTopFace = value; } get { if (_middleTopFace == null) _middleTopFace = _MiddleTopFace(); return _middleTopFace; } }
        public CellFace MiddleLeftFace { private set { _middleLeftFace = value; } get { if (_middleLeftFace == null) _middleLeftFace = _MiddleLeftFace(); return _middleLeftFace; } }
        public double Thickness { private set { _thickness = value; } get { if (_thickness == undef_values) _thickness = _Thickness(); return _thickness; } }
        public double Length { private set { _length = value; } get { if (_length == undef_values) _length = _Length(); return _length; } }
        public double Width { private set { _width = value; } get { if (_width == undef_values) _width = _Width(); return _width; } }
        public double Volume { private set { _volume = value; } get { if (_volume == undef_values) _volume = _Volume(); return _volume; } }


        const double undef_values = -999;
        Point3D _center = null;
        double _minX = undef_values;
        double _minY = undef_values;
        double _minZ = undef_values;
        double _maxX = undef_values;
        double _maxY = undef_values;
        double _maxZ = undef_values;
        CellFace _topFace = null;
        CellFace _bottomFace = null;
        CellFace _leftFace = null;
        CellFace _rightFace = null;
        CellFace _nearFace = null;
        CellFace _farFace = null;
        CellFace _middleFrontFace = null;
        CellFace _middleTopFace = null;
        CellFace _middleLeftFace = null;
        double _thickness = undef_values;
        double _length = undef_values;
        double _width = undef_values;
        double _volume = undef_values;





        Point3D _Center()
        {
            double x = (Corners[0].X + Corners[1].X + Corners[2].X + Corners[3].X + Corners[4].X + Corners[5].X + Corners[6].X + Corners[7].X) / 8;
            double y = (Corners[0].Y + Corners[1].Y + Corners[2].Y + Corners[3].Y + Corners[4].Y + Corners[5].Y + Corners[6].Y + Corners[7].Y) / 8;
            double z = (Corners[0].Z + Corners[1].Z + Corners[2].Z + Corners[3].Z + Corners[4].Z + Corners[5].Z + Corners[6].Z + Corners[7].Z) / 8;
            return new Point3D(x, y, z);
        }


        double _MinX()
        {
            double min = Corners[0].X;
            for (int c = 1; c < 8; ++c)
                if (min > Corners[c].X)
                    min = Corners[c].X;
            return min;
        }


        double _MinY()
        {
            double min = Corners[0].Y;
            for (int c = 1; c < 8; ++c)
                if (min > Corners[c].Y)
                    min = Corners[c].Y;
            return min;
        }


        double _MinZ()
        {
            double min = Corners[0].Z;
            for (int c = 1; c < 8; ++c)
                if (min > Corners[c].Z)
                    min = Corners[c].Z;
            return min;
        }


        double _MaxX()
        {
            double max = Corners[0].X;
            for (int c = 1; c < 8; ++c)
                if (max < Corners[c].X)
                    max = Corners[c].X;
            return max;
        }


        double _MaxY()
        {
            double max = Corners[0].Y;
            for (int c = 1; c < 8; ++c)
                if (max < Corners[c].Y)
                    max = Corners[c].Y;
            return max;
        }


        double _MaxZ()
        {
            double max = Corners[0].Z;
            for (int c = 1; c < 8; ++c)
                if (max < Corners[c].Z)
                    max = Corners[c].Z;
            return max;
        }



        CellFace _TopFace()
        {
            return new CellFace(Corners[0], Corners[1], Corners[2], Corners[3]);
        }



        CellFace _BottomFace()
        {
            return new CellFace(Corners[4], Corners[5], Corners[6], Corners[7]);
        }



        CellFace _LeftFace()
        {
            return new CellFace(Corners[0], Corners[2], Corners[4], Corners[6]);
        }



        CellFace _RightFace()
        {
            return new CellFace(Corners[1], Corners[3], Corners[5], Corners[7]);
        }



        CellFace _NearFace()
        {
            return new CellFace(Corners[0], Corners[1], Corners[4], Corners[5]);
        }



        CellFace _FarFace()
        {
            return new CellFace(Corners[2], Corners[3], Corners[6], Corners[7]);
        }



        CellFace _MiddleFrontFace()
        {
            CellFace nearFace = NearFace;
            CellFace farFace = FarFace;
            return new CellFace((nearFace.Corners[0] + farFace.Corners[0]) / 2, (nearFace.Corners[1] + farFace.Corners[1]) / 2,
                                (nearFace.Corners[2] + farFace.Corners[2]) / 2, (nearFace.Corners[3] + farFace.Corners[3]) / 2);
        }



        CellFace _MiddleTopFace()
        {
            CellFace topFace = TopFace;
            CellFace bottomFace = BottomFace;
            return new CellFace((topFace.Corners[0] + bottomFace.Corners[0]) / 2, (topFace.Corners[1] + bottomFace.Corners[1]) / 2,
                                (topFace.Corners[2] + bottomFace.Corners[2]) / 2, (topFace.Corners[3] + bottomFace.Corners[3]) / 2);
        }



        CellFace _MiddleLeftFace()
        {
            CellFace leftFace = LeftFace;
            CellFace rightFace = RightFace;
            return new CellFace((leftFace.Corners[0] + rightFace.Corners[0]) / 2, (leftFace.Corners[1] + rightFace.Corners[1]) / 2,
                                (leftFace.Corners[2] + rightFace.Corners[2]) / 2, (leftFace.Corners[3] + rightFace.Corners[3]) / 2);
        }



        CellFace Face(CoordPlane plane, FaceDistance distance)
        {
            switch (plane)
            {
                case CoordPlane.XY:
                    switch (distance)
                    {
                        case FaceDistance.Near:         return TopFace;
                        case FaceDistance.Middle:       return MiddleTopFace;
                        default: /*FaceDistance.Far:*/  return BottomFace;
                    }
                case CoordPlane.XZ:
                    switch (distance)
                    {
                        case FaceDistance.Near:         return LeftFace;
                        case FaceDistance.Middle:       return MiddleLeftFace;
                        default: /*FaceDistance.Far:*/  return RightFace;
                    }
                default: // CoordPlane.YZ:
                    switch (distance)
                    {
                        case FaceDistance.Near:         return NearFace;
                        case FaceDistance.Middle:       return MiddleFrontFace;
                        default: /*FaceDistance.Far:*/  return FarFace;
                    }
            }
        }



        double _Thickness()
        {
            return BottomFace.Center().Z - TopFace.Center().Z;
        }

        double _Length()
        {
            return NearFace.Center().Y - FarFace.Center().Y;
        }

        double _Width()
        {
            return RightFace.Center().X - LeftFace.Center().X;
        }


        double _Volume()
        {
            return Thickness * Length * Width;
        }

        
        public bool Contain(Point3D p)
        {
            return ( (TopFace.Contain(p.Point2D(CoordPlane.XY), CoordPlane.XY)   || (BottomFace.Contain(p.Point2D(CoordPlane.XY), CoordPlane.XY))) &&
                     (RightFace.Contain(p.Point2D(CoordPlane.YZ), CoordPlane.YZ) || (LeftFace.Contain(p.Point2D(CoordPlane.YZ), CoordPlane.YZ))  ) &&
                     (NearFace.Contain(p.Point2D(CoordPlane.XZ), CoordPlane.XZ)  || (FarFace.Contain(p.Point2D(CoordPlane.XZ), CoordPlane.XZ))   ) );
        }

        

    }
}
