using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Collections;
using SharpGL.SceneGraph.Primitives;
//using SharpGL.Serialization;
using SharpGL.SceneGraph.Core;
using SharpGL.Enumerations;
using Pexel.HM.FR;
using System.Numerics;
//using Microsoft.Office.Interop.Excel;

namespace Pexel
{
    public partial class View2D : UserControl
    {

        public View2D()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.openGLControl = new OpenGLControl();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            this.SuspendLayout();
            // 
            // openGLControl
            // 
            this.openGLControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openGLControl.DrawFPS = true;
            this.openGLControl.Location = new System.Drawing.Point(0, 0);
            this.openGLControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl.RenderContextType = SharpGL.RenderContextType.FBO;
            this.openGLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl.Size = new System.Drawing.Size(315, 474);
            this.openGLControl.TabIndex = 5;
            this.openGLControl.OpenGLInitialized += new System.EventHandler(this.mainOpenGLControl_OpenGLInitialized);
            this.openGLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.mainOpenGLControl_OpenGLDraw);
            this.openGLControl.Resized += new System.EventHandler(this.mainOpenGLControl_Resized);
            this.openGLControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mainOpenGLControl_KeyDown);
            this.openGLControl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mainOpenGLControl_KeyPress);
            //this.openGLControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mainOpenGLControl_MouseClick);
            this.openGLControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainOpenGLControl_MouseDown);
            this.openGLControl.MouseEnter += new System.EventHandler(this.mainOpenGLControl_MouseEnter);
            this.openGLControl.MouseLeave += new System.EventHandler(this.mainOpenGLControl_MouseLeave);
            this.openGLControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mainOpenGLControl_MouseMove);
            this.openGLControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mainOpenGLControl_MouseUp);
            this.openGLControl.MouseWheel += new MouseEventHandler(mainOpenGLControl_MouseWheel);
            // 
            // 
            // View3D
            // 
            this.Controls.Add(this.openGLControl);
            this.Name = "View2D";
            this.Size = new System.Drawing.Size(560, 470);
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
            this.ResumeLayout(false);
        }







        private OpenGLControl openGLControl;
        //private SharpGL.SceneControl SceneControl;

        public GridMap Map { set; get; } = new GridMap();
        public WellsPlane WellsPlane { set; get; } = new WellsPlane();
        public WellsPlane2D WellsPlane2D { set; get; } = new WellsPlane2D();



        const double propPlaneDepth = 0;
        const double gridLinesPlaneDepth = -1;
        const double wellLinesPlaneDepth = -2;
        const double scalePlaneDepth = -3;
        const double histogramPlaneDepth = -4;
        const double circlePlaneDepth = -10;



        double X_shift = 0;
        double Y_shift = 0;
        double Z_scale = 1;

        public bool MapMode { set; get; }
        public bool ShowGridLines { set; get; }
        public bool AddModifierMode { set; get; } = false;
        public double ModifierRadius { set; get; } = 0;
        public bool WellsCreateMode { set; get; }
        public List<Point3D> WellsPoints { set; get; }
        public PropScale Scale { set; get; }
        //public int [] SelectedLayers { set; get; }
        public Point CurrCellIndex { protected set; get; }
        public Point2D CurrMouseLocationF { protected set; get; }

        public List<WellsLink> WellsLinks { set; get; } = new List<WellsLink>();
        public List<Polygon2D> Polygons { set; get; } = new List<Polygon2D>();
        public List<Polygon2D> Boundaries { set; get; } = new List<Polygon2D>();
        public List<Polygon2D> TargetAreas { set; get; } = new List<Polygon2D>();
        public List<Triangle2D> Covered { set; get; } = new List<Triangle2D>();
        public List<Quad2D> Uncovered { set; get; } = new List<Quad2D>();
        public List<Tuple<Point3D, bool>> PieChart { set; get; } = new List<Tuple<Point3D, bool>>();



        public bool ShowUncovered { set; get; } = true;
        public bool ShowCovered { set; get; } = true;


        public event EventHandler PositionChanged;
        public event EventHandler MouseLeave;



        void UpdateMainOpenGLControl()
        {
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();
            gl.Translate(X_shift, Y_shift, 0);
            gl.Scale(Z_scale, Z_scale, 1);
            /*
            if (ShowGridLines) DrawGridLines(Map);    
                        if (AddModifierMode) 
                DrawCircle(CurrMouseLocationF.X, CurrMouseLocationF.Y, ModifierRadius, 360, Color.AntiqueWhite, circlePlaneDepth);
            if (WellsCreateMode) Draw(WellsPoints);
            if (MapMode) DrawMap(Map, Scale);
            else DrawCells(Map, Scale);
            DrawScale(Scale);
            if (WellsPlane != null) Draw(WellsPlane);
            */
            DrawLinks(WellsLinks);
            DrawImpactAreas(Polygons);
            DrawBoundaries(Boundaries);
            DrawBoundaries(TargetAreas);
            DrawAreas(Covered, Uncovered);
            Draw(WellsPlane2D);
            DrawPie(PieChart);
        }



        void DrawPie(List<Tuple<Point3D, bool>> pieChart)
        {
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
            gl.Enable(OpenGL.GL_BLEND);
            foreach (Tuple<Point3D, bool> value in pieChart)
            {
                if (!value.Item2) continue;
                DrawCircle(value.Item1.X, value.Item1.Y, 
                           Math.Sqrt(Math.Abs(value.Item1.Z) / Math.PI), 
                           100,
                           value.Item1.Z > 0 ? Color.FromArgb(128, Color.Green) : Color.FromArgb(128, Color.Red),
                           circlePlaneDepth);
            }
            gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
            gl.Disable(OpenGL.GL_BLEND);
        }




        void DrawLinks(IEnumerable<WellsLink> links)
        {
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            gl.Begin(BeginMode.Lines);
            foreach (WellsLink link in links)
            {
                if (!link.Visible) continue;
                gl.Color(link.Color);
                gl.Vertex(link.W1.Point.X, link.W1.Point.Y, gridLinesPlaneDepth);
                gl.Vertex(link.W2.Point.X, link.W2.Point.Y, gridLinesPlaneDepth);
            }
            gl.End();
        }


        void DrawImpactAreas(List<Polygon2D> polygons)
        {
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            gl.Begin(BeginMode.Quads); // TODO: 
            foreach (Polygon2D poly in polygons)
            {
                if (!poly.Visible) continue;
                gl.Color(poly.Color.R, poly.Color.G, poly.Color.B, (byte)0);
                foreach (Point2D p in poly.Points) gl.Vertex(p.X, p.Y, propPlaneDepth);
            }
            gl.End();
        }



        void DrawBoundaries(IEnumerable<Polygon2D> polygons)
        {
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            foreach (Polygon2D poly in polygons)
            {
                if (!poly.Visible) continue;
                //gl.Enable( double((GL_LINE_STIPPLE);
                if (poly.Closed) gl.Begin(BeginMode.LineLoop);
                else gl.Begin(BeginMode.LineStrip);
                gl.Color(poly.Color);
                foreach (Point2D p in poly.Points) gl.Vertex(p.X, p.Y, propPlaneDepth);
                gl.End();
            }
        }

        void DrawAreas(List<Triangle2D> covered, List<Quad2D> uncovered)
        {
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;

            // Disable AutoTexture Coordinates
            //gl.Disable(OpenGL.GL_TEXTURE_GEN_S);
            //gl.Disable(OpenGL.GL_TEXTURE_GEN_T);

            //gl.Enable(OpenGL.GL_TEXTURE_2D);                    // Enable 2D Texture Mapping
            //gl.Disable(OpenGL.GL_DEPTH_TEST);                   // Disable Depth Testing
            gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);               // Set Blending Mode
            gl.Enable(OpenGL.GL_BLEND);                     // Enable Blending
            //gl.BindTexture(OpenGL.GL_TEXTURE_2D OpenGL.texture BlurTexture);			// Bind To The Blur Texture
            if (ShowCovered)
            {
                foreach (Triangle2D triangle in covered)
                {
                    if (!triangle.Visible) continue;
                    gl.Begin(BeginMode.Triangles);
                    gl.Color(triangle.Color.R, triangle.Color.G, triangle.Color.B, (byte)127);
                    foreach (Point2D p in triangle.Corners) gl.Vertex(p.X, p.Y, propPlaneDepth);
                    gl.End();
                }
            }
            if (ShowUncovered)
            {
                foreach (Quad2D quad in uncovered)
                {
                    if (!quad.Visible) continue;
                    /*
                    gl.Begin(BeginMode.Triangles);
                    gl.Color(quad.Color.R, quad.Color.G, quad.Color.B, (byte)127);
                    for (int i = 0; i < 3; ++i) gl.Vertex(quad.Corners[i].X, quad.Corners[i].Y, propPlaneDepth);
                    gl.End();
                    */
                    gl.Begin(BeginMode.Quads);
                    gl.Color(quad.Color.R, quad.Color.G, quad.Color.B, (byte)127);
                    for (int i = 0; i < 4; ++i) gl.Vertex(quad.Corners[i].X, quad.Corners[i].Y, propPlaneDepth);
                    gl.End();
                }
            }
            //gl.Enable(OpenGL.GL_DEPTH_TEST);					// Enable Depth Testing
            //gl.Disable(OpenGL.GL_TEXTURE_2D);					// Disable 2D Texture Mapping
            gl.Disable(OpenGL.GL_BLEND);						// Disable Blending
            //gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);					// Unbind The Blur Texture
        }

        void Draw(List<Point3D> points)
        {
            foreach (Point3D point in points)
                DrawCircle(point.X, point.Y, 10, 12, point.Z == 0 ? Color.Red : Color.Blue, circlePlaneDepth);
        }




        void DrawGridLines(GridMap map)
        {
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            gl.Begin(BeginMode.Lines);
            gl.Color(Color.Gray);
            for (int i = 0; i < map.NX; ++i)
                for (int j = 0; j < map.NY; ++j)
                {
                    //if (map.Act[i, j] == false) continue;
                    // 1
                    gl.Vertex(map.Nodes[i + 0, j + 0].X, map.Nodes[i + 0, j + 0].Y, gridLinesPlaneDepth);
                    gl.Vertex(map.Nodes[i + 1, j + 0].X, map.Nodes[i + 1, j + 0].Y, gridLinesPlaneDepth);
                    // 2
                    gl.Vertex(map.Nodes[i + 1, j + 0].X, map.Nodes[i + 1, j + 0].Y, gridLinesPlaneDepth);
                    gl.Vertex(map.Nodes[i + 1, j + 1].X, map.Nodes[i + 1, j + 1].Y, gridLinesPlaneDepth);
                    // 3
                    gl.Vertex(map.Nodes[i + 1, j + 1].X, map.Nodes[i + 1, j + 1].Y, gridLinesPlaneDepth);
                    gl.Vertex(map.Nodes[i + 0, j + 1].X, map.Nodes[i + 0, j + 1].Y, gridLinesPlaneDepth);
                    // 4
                    gl.Vertex(map.Nodes[i + 0, j + 1].X, map.Nodes[i + 0, j + 1].Y, gridLinesPlaneDepth);
                    gl.Vertex(map.Nodes[i + 0, j + 0].X, map.Nodes[i + 0, j + 0].Y, gridLinesPlaneDepth);
                }
            gl.End();
        }









        void DrawCells(GridMap map, PropScale scale)
        {
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            gl.Begin(BeginMode.Quads);
            for (int i = 0; i < map.NX; ++i)
                for (int j = 0; j < map.NY; ++j)
                {
                    if (map.Act[i, j] == false) continue;
                    gl.Color(Scale.Color(map.Values[i, j]));
                    gl.Vertex(map.Nodes[i + 0, j + 0].X, map.Nodes[i + 0, j + 0].Y, propPlaneDepth);
                    gl.Vertex(map.Nodes[i + 1, j + 0].X, map.Nodes[i + 1, j + 0].Y, propPlaneDepth);
                    gl.Vertex(map.Nodes[i + 1, j + 1].X, map.Nodes[i + 1, j + 1].Y, propPlaneDepth);
                    gl.Vertex(map.Nodes[i + 0, j + 1].X, map.Nodes[i + 0, j + 1].Y, propPlaneDepth);
                }
            gl.End();
        }



        void DrawMap(GridMap map, PropScale scale)
        {
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            Point2D center;
            int ii, jj;
            gl.Begin(BeginMode.Quads);
            for (int i = 1; i < map.NX; ++i)
                for (int j = 1; j < map.NY; ++j)
                {
                    if (map.Act[i, j] == false) continue;
                    //
                    ii = i - 1;
                    jj = j - 1;
                    gl.Color(Scale.Color(map.Values[ii, jj]));
                    center = map.Center(ii, jj);
                    gl.Vertex(center.X, center.Y, propPlaneDepth);
                    //
                    ii = i;
                    jj = j - 1;
                    gl.Color(Scale.Color(map.Values[ii, jj]));
                    center = map.Center(ii, jj);
                    gl.Vertex(center.X, center.Y, propPlaneDepth);
                    //
                    ii = i;
                    jj = j;
                    gl.Color(Scale.Color(map.Values[ii, jj]));
                    center = map.Center(ii, jj);
                    gl.Vertex(center.X, center.Y, propPlaneDepth);
                    //
                    ii = i - 1;
                    jj = j;
                    gl.Color(Scale.Color(map.Values[ii, jj]));
                    center = map.Center(ii, jj);
                    gl.Vertex(center.X, center.Y, propPlaneDepth);
                }
            gl.End();
        }


        public void SetWells(Color color)
        {
            wellColor = color;
        }
        Color wellColor = Color.Goldenrod;

        void Draw(WellsPlane plane)
        {
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            foreach (WellFace well in plane.Faces)
            {
                if (!well.Checked) continue;
                // title
                PointF winPoint = ProjToWinCoord(well.Trajectory[0].X, well.Trajectory[0].Y);
                gl.DrawText((int)winPoint.X + 2, (int)winPoint.Y + 2, wellColor.R, wellColor.G, wellColor.B, "", 10, well.Title);
                int i, count = well.Trajectory.Count();
                // points
                gl.Color(wellColor);
                gl.Begin(BeginMode.Points);
                for (i = 0; i < count; ++i)
                {
                    gl.Vertex(well.Trajectory[i].X, well.Trajectory[i].Y, wellLinesPlaneDepth);
                }
                gl.End();
                // lintes
                gl.Color(wellColor);
                gl.Begin(BeginMode.Lines);
                for (i = 1; i < count; ++i)
                {
                    gl.Vertex(well.Trajectory[i - 1].X, well.Trajectory[i - 1].Y, wellLinesPlaneDepth);
                    gl.Vertex(well.Trajectory[i - 0].X, well.Trajectory[i - 0].Y, wellLinesPlaneDepth);
                }
                gl.End();
            }
        }




        void DrawWells(IEnumerable<WellFace2D> wells)
        {
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            foreach (WellFace2D well in wells)
            {
                if (!well.Visible) continue;
                // title
                PointF winPoint = ProjToWinCoord(well.Point.X, well.Point.Y);

                if (well.Status == WellStatus.PROD)
                    DrawTriangle(well.Point.X, well.Point.Y, wellSize, false, Color.Brown, wellLinesPlaneDepth);
                else if (well.Status == WellStatus.INJE)
                    DrawTriangle(well.Point.X, well.Point.Y, wellSize, true, Color.Blue, wellLinesPlaneDepth);
                else if (well.Status == WellStatus.AQUI)
                    DrawDoubleTriangle(well.Point.X, well.Point.Y, wellSize, true, Color.SteelBlue, Color.SkyBlue, wellLinesPlaneDepth);
                else
                    //DrawTriangle(well.Point.X, well.Point.Y, wellSize, true, Color.Black, wellLinesPlaneDepth);
                    DrawCircle(well.Point.X, well.Point.Y, wellSize / 2, 12, Color.Black, wellLinesPlaneDepth);

                gl.DrawText((int)winPoint.X + 2, (int)winPoint.Y + 2,
                            Color.Black.R, Color.Black.G, Color.Black.B, "", 10, well.Title);
            }
        }



        double wellSize = 50;
        void Draw(WellsPlane2D plane)
        {
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            foreach (WellFace2D well in plane.Wells)
            {
                if (!well.Visible) continue;
                // title
                PointF winPoint = ProjToWinCoord(well.Point.X, well.Point.Y);

                if (well.Status == WellStatus.PROD)
                    DrawTriangle(well.Point.X, well.Point.Y, wellSize, false, Color.Brown, wellLinesPlaneDepth);
                else if (well.Status == WellStatus.INJE)
                    DrawTriangle(well.Point.X, well.Point.Y, wellSize, true, Color.Blue, wellLinesPlaneDepth);
                else if (well.Status == WellStatus.AQUI)
                    DrawDoubleTriangle(well.Point.X, well.Point.Y, wellSize, true, Color.SteelBlue, Color.SkyBlue, wellLinesPlaneDepth);
                else
                    //DrawTriangle(well.Point.X, well.Point.Y, wellSize, true, Color.Black, wellLinesPlaneDepth);
                    DrawCircle(well.Point.X, well.Point.Y, wellSize / 2, 12, Color.Black, wellLinesPlaneDepth);

                gl.DrawText((int)winPoint.X + 2, (int)winPoint.Y + 2,
                            Color.Black.R, Color.Black.G, Color.Black.B, "", 10, well.Title);
            }
        }



        void DrawCircle(double cx, double cy, double r, int num_segments, Color color, double z)
        {
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            gl.PolygonMode(FaceMode.Front, PolygonMode.Filled);
            gl.Begin(BeginMode.Polygon);
            gl.Color(color);
            for (int ii = 0; ii < num_segments; ii++)
            {
                double theta = 2.0 * 3.1415926 * ii / num_segments; //get the current angle 
                double x = r * Math.Cos(theta); //calculate the x component 
                double y = r * Math.Sin(theta); //calculate the y component 
                gl.Vertex(x + cx, y + cy, z); //output vertex 
            }
            gl.End();
        }


        void DrawTriangle(double x, double y, double a, bool rotate, Color color, double z)
        {
            double k = rotate ? -1 : 1;
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            gl.Begin(BeginMode.Triangles);
            gl.Color(color);
            gl.Vertex(x - a / 2, (y - a / 3 * k) , z);
            gl.Vertex(x, (y + 2 * a / 3 * k) , z);
            gl.Vertex(x + a / 2, (y - a / 3 * k) , z);
            gl.End();
        }

        void DrawDoubleTriangle(double x, double y, double a, bool rotate, Color color1, Color color2, double z)
        {
            double k = rotate ? -1 : 1;
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            gl.Begin(BeginMode.Triangles);
            gl.Color(color1);
            gl.Vertex(x, (y - a / 3 * k), z);
            gl.Vertex(x, (y + 2 * a / 3 * k), z);
            gl.Vertex(x + a / 2, (y - a / 3 * k), z);
            gl.Color(color2);
            gl.Vertex(x - a / 2, (y - a / 3 * k), z);
            gl.Vertex(x, (y + 2 * a / 3 * k), z);
            gl.Vertex(x, (y - a / 3 * k), z);
            gl.End();
        }


        const int scale_x_min = 10;
        const int scale_x_max = 40;
        const int scale_max_hight = 400;


        
        void DrawScale(PropScale scale)
        {
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            int scale_hight = Math.Min(gl.RenderContextProvider.Height * 80 / 100, scale_max_hight);
            int scale_y_min = gl.RenderContextProvider.Height / 2 + scale_hight / 2;
            int scale_y_max = gl.RenderContextProvider.Height / 2 - scale_hight / 2;

            int nd = 100;

            Point2D min = WinToProjCoord(scale_x_min, scale_y_min);
            Point2D max = WinToProjCoord(scale_x_max, scale_y_max);

            double y = min.Y;
            double y_step = (max.Y - min.Y) / nd;

            double v = scale.Min;
            double v_step = (scale.Max - scale.Min) / nd;

            gl.Begin(BeginMode.Quads);
            for (int i = 0; i < nd; ++i)
            {
                Color color = scale.Color(v);
                gl.Color(color.R, color.G, color.B);
                gl.Vertex(min.X, y, scalePlaneDepth);
                gl.Vertex(max.X, y, scalePlaneDepth);
                y += y_step;
                v += v_step;
                color = scale.Color(v);
                gl.Color(color.R, color.G, color.B);
                gl.Vertex(max.X, y, scalePlaneDepth);
                gl.Vertex(min.X, y, scalePlaneDepth);
            }
            gl.End();

            int nstep = 10;
            int sill = 0;
            y = min.Y;

            gl.Begin(BeginMode.Lines);
            gl.Color(Color.Gray);
            for (int i = 0; i < nd + 1; i += nstep)
            {
                gl.Vertex(min.X, y, scalePlaneDepth);
                gl.Vertex(max.X + sill, y, scalePlaneDepth);
                y += y_step * nstep;
            }
            gl.End();

            y = min.Y;
            v = scale.Min;

            for (int i = 0; i < nd + 1; i += nstep)
            {
                PointF winPoint = ProjToWinCoord(max.X, y);
                gl.DrawText((int)winPoint.X + sill, (int)winPoint.Y, Color.Gray.R, Color.Gray.G, Color.Gray.B, "", 10, Helper.ShowDouble(v));
                y += y_step * nstep;
                v += v_step * nstep;
            }
        }


        public double MinX()
        {
            return WellsPlane2D.MinX();
        }
        public double MaxX()
        {
            return WellsPlane2D.MaxX();
        }
        public double MinY()
        {
            return WellsPlane2D.MinY();
        }
        public double MaxY()
        {
            return WellsPlane2D.MaxY();
        }



        public void HomePosition()
        {
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            double viewWidth = this.openGLControl.Width;
            double viewHeight = this.openGLControl.Height;
            double gridMinX = MinX();
            double gridMaxX = MaxX();
            double gridMinY = MinY();
            double gridMaxY = MaxY();
            double gridWidth = gridMaxX - gridMinX;
            double gridHeight = gridMaxY - gridMinY;
            Z_scale = Math.Min(viewHeight / gridHeight, viewWidth / gridWidth);
            X_shift = -gridMinX * Z_scale + viewWidth / 2 - gridWidth * Z_scale / 2;
            Y_shift = -gridMinY * Z_scale + viewHeight / 2 - gridHeight * Z_scale / 2;
        }


        




        private void mainOpenGLControl_MouseWheel(object sender, MouseEventArgs e)
        {
            Zooming(e.Delta, e.Location);
        }




        const int wheelDelta = 120;
        private void Zooming(int delta, Point refPoint)
        {
            int times = delta / wheelDelta;
            const double zoomRatio = 1.05f;
            double mult = 1f;
            if (times > 0)
                for (int i = 0; i < times; ++i) mult *= zoomRatio;
            else
                for (int i = 0; i < -times; ++i) mult /= zoomRatio;
            Z_scale *= mult;
            //const double minZoomFactor = 0.000001f;
            //zoomFactor = Math.Max(zoomFactor, minZoomFactor);
            Point2D projPoint = WinToProjCoord(refPoint.X, refPoint.Y);
            double scale = (1 - mult) / mult * Z_scale;
            X_shift += projPoint.X * scale;
            Y_shift += projPoint.Y * scale;
            ////UpdateMainOpenGLControl();
            ////mainOpenGLControl_OpenGLDraw(null, null);
        }




        Point PrevMouseLocation = new Point();
        private void mainOpenGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Cursor.Current = Cursors.NoMove2D;
                PrevMouseLocation.X = e.X;
                PrevMouseLocation.Y = e.Y;
                //prevLoc = WinToProjCoord(e.X, e.Y);
            }
        }

        private void mainOpenGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Cursor.Current = Cursors.Default;
            }
        }

        Point CurrMouseLocation = new Point();
        private void mainOpenGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            CurrMouseLocation.X = e.X;
            CurrMouseLocation.Y = e.Y;
            if (e.Button == MouseButtons.Right)
            {
                X_shift += +(CurrMouseLocation.X - PrevMouseLocation.X);
                Y_shift += -(CurrMouseLocation.Y - PrevMouseLocation.Y); // в View ось у направлена вниз
                PrevMouseLocation.X = CurrMouseLocation.X;
                PrevMouseLocation.Y = CurrMouseLocation.Y;
            }
            else
            {
                CurrMouseLocationF = WinToProjCoord(CurrMouseLocation.X, CurrMouseLocation.Y);
                CurrCellIndex = Map.CellIndex(CurrMouseLocationF);
                PositionChanged?.Invoke(this, new EventArgs());
            }
        }
        
        Point2D WinToProjCoord(int xWin, int yWin)
        {
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            double[] p = gl.UnProject(xWin, (gl.RenderContextProvider.Height - yWin), 0.0);
            return new Point2D(p[0], p[1]);
        }
        
        PointF ProjToWinCoord(double xProj, double yProj)
        {
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            Vertex v = gl.Project(new Vertex((float)xProj, (float)yProj, 0));
            return new PointF(v.X, v.Y);
        }

        private void mainOpenGLControl_MouseEnter(object sender, EventArgs e)
        {

        }

        private void mainOpenGLControl_MouseLeave(object sender, EventArgs e)
        {
            MouseLeave?.Invoke(this, new EventArgs());
        }



        /*
        void ShowCurrLocation()
        {
            coordToolStripStatusLabel.Text = "X=" + CurrMouseLocationF.X.ToString() + " Y=" + CurrMouseLocationF.Y.ToString();
            if (CurrGridPlane == null)
                return;
            CurrCellIndex = CurrGridPlane.Index(CurrMouseLocationF);
            if (CurrCellIndex == GridPlane.UndefIndex)
            {
                toolStripTextBox_cellValue.Text = string.Empty;
                toolStripTextBox_index.Text = string.Empty;
            }
            else
            {
                toolStripTextBox_index.Text = "I=" + (CurrCellIndex.X + 1).ToString() + " J=" + (CurrCellIndex.Y + 1).ToString();
                toolStripTextBox_cellValue.Text = CurrGridPlane.Values[CurrCellIndex.X, CurrCellIndex.Y].ToString();
            }
        }
        */






        private void mainOpenGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            UpdateMainOpenGLControl();
        }



        Color BackGround = Color.Black;
        float BackGroundAlfa = 0f;
        public void SetBackGround(Color color, float alfa = 0f)
        {
            BackGround = color;
            BackGroundAlfa = alfa;
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            gl.ClearColor(BackGround.R, BackGround.G, BackGround.B, BackGroundAlfa);
        }

        private void mainOpenGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            gl.ClearColor(BackGround.R, BackGround.G, BackGround.B, BackGroundAlfa);
            gl.Viewport(0, 0, openGLControl.Size.Width, openGLControl.Size.Height);
        }





        const double orthoLeft = 0;
        const double orthoRight = 1000;
        const double orthoBottom = 0;
        const double orthoTop = 1000;
        const double orthoZNear = 1000;
        const double orthoZFar = -1000;
        double curOrthoLeft, curOrthoRight, curOrthoBottom, curOrthoTop, aspect;

        private void mainOpenGLControl_Resized(object sender, EventArgs e)
        {
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            aspect = (double)gl.RenderContextProvider.Width / (double)gl.RenderContextProvider.Height;
            //aspect = this.Width / this.Height;
            if (aspect > 1)
            {
                curOrthoLeft = -orthoLeft;
                curOrthoRight = orthoRight;
                curOrthoBottom = -orthoBottom / aspect;
                curOrthoTop = orthoTop / aspect;
            }
            else
            {
                curOrthoLeft = -orthoLeft * aspect;
                curOrthoRight = orthoRight * aspect;
                curOrthoBottom = -orthoBottom;
                curOrthoTop = orthoTop;
            }
            gl.Ortho(curOrthoLeft, curOrthoRight, curOrthoBottom, curOrthoTop, orthoZNear, orthoZFar);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }


        



        const double shiftFrac = 0.01f;
        private void mainOpenGLControl_KeyDown(object sender, KeyEventArgs e)
        {
            double gridMinX = MinX();
            double gridMaxX = MaxX();
            double gridMinY = MinY();
            double gridMaxY = MaxY();
            double gridHeight = gridMaxY - gridMinY;
            double gridWidth = gridMaxX - gridMinX;
            double min = Math.Min(gridWidth, gridHeight);
            switch (e.KeyCode)
            {
                case Keys.W: // Up
                    Y_shift -= -(min * shiftFrac); // у View ось у направлена вниз
                    break;
                case Keys.S: // Down
                    Y_shift += -(min * shiftFrac); // у View ось у направлена вниз
                    //NextLayer();
                    break;
                case Keys.D: // Right
                    X_shift += +(min * shiftFrac);
                    break;
                case Keys.A: // Left
                    X_shift -= +(min * shiftFrac);
                    break;
            }
        }

        




        private void mainOpenGLControl_KeyPress(object sender, KeyPressEventArgs e)
        {

        }








    }
}
