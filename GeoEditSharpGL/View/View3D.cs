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


namespace Pexel
{
    public class View3D : UserControl
    {

        public View3D()
        {
            InitializeComponent();
        }

        //private OpenGLControl openGLControl;
        private SharpGL.SceneControl SceneControl;
        public SharpGL.SceneGraph.Effects.ArcBallEffect arcBallEffect;

        private void InitializeComponent()
        {
            this.SceneControl = new SceneControl();
            ((System.ComponentModel.ISupportInitialize)(this.SceneControl)).BeginInit();
            this.SuspendLayout();
            // 
            // openGLControl
            // 
            this.SceneControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SceneControl.DrawFPS = true;
            this.SceneControl.Location = new System.Drawing.Point(0, 0);
            this.SceneControl.Margin = new System.Windows.Forms.Padding(4);
            this.SceneControl.Name = "openGLControl";
            this.SceneControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.SceneControl.RenderContextType = SharpGL.RenderContextType.FBO;
            this.SceneControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.SceneControl.Size = new System.Drawing.Size(560, 470);
            this.SceneControl.TabIndex = 6;
            // 
            // View3D
            // 
            this.Controls.Add(this.SceneControl);
            this.Name = "View3D";
            this.Size = new System.Drawing.Size(560, 470);
            ((System.ComponentModel.ISupportInitialize)(this.SceneControl)).EndInit();
            this.ResumeLayout(false);


            ///

            // my
            //SceneControl.Resized += openGLControl_Resized;
            //SceneControl.OpenGLInitialized += openGLControl_OpenGLInitialized;
            //SceneControl.OpenGLDraw += openGLControl_OpenGLDraw;

            //
            SceneControl.MouseDown += new MouseEventHandler(FormSceneSample_MouseDown);
            SceneControl.MouseMove += new MouseEventHandler(FormSceneSample_MouseMove);
            SceneControl.MouseUp += new MouseEventHandler(openGLControl_MouseUp);

            //  Add some design-time primitives.
            //SceneControl.Scene.SceneContainer.AddChild(new SharpGL.SceneGraph.Primitives.Grid());
            //SceneControl.Scene.SceneContainer.AddChild(new SharpGL.SceneGraph.Primitives.Axies());

            //  Create a light.
            SharpGL.SceneGraph.Lighting.Light light = new SharpGL.SceneGraph.Lighting.Light()
            {
                On = true,
                Position = new Vertex(3, 10, 3),
                GLCode = OpenGL.GL_LIGHT0
            };

            //  Add the light.
            //SceneControl.Scene.SceneContainer.AddChild(light);

            //  Create a sphere.
            Cube cube = new Cube();
            arcBallEffect = new SharpGL.SceneGraph.Effects.ArcBallEffect();
            cube.AddEffect(arcBallEffect);

            //  Add it.
            SceneControl.Scene.SceneContainer.AddChild(cube);
        }




        /*
        const int wheelDelta = 120;
        private void mainOpenGLControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                ZoomInOut(true, Math.Abs(e.Delta) / wheelDelta, e.Location);
            else
                ZoomInOut(false, Math.Abs(e.Delta) / wheelDelta, e.Location);
        }


        private void ZoomInOut(bool zoom, int times, Point refPoint)
        {
            const double zoomRatio = 1.05f;
            double mult = 1f;
            if (zoom) for (int i = 0; i < times; ++i) mult *= zoomRatio;
            else      for (int i = 0; i < times; ++i) mult /= zoomRatio;
            CurrModel.Position.Z *= mult;
            //const double minZoomFactor = 0.000001f;
            //zoomFactor = Math.Max(zoomFactor, minZoomFactor);
            Point2D projPoint = WinToProjCoord(refPoint.X, refPoint.Y);
            double scale = (1 - mult) / mult * CurrModel.Position.Z;
            CurrModel.Position.X += projPoint.X * scale;
            CurrModel.Position.Y += projPoint.Y * scale;
            ////UpdateMainOpenGLControl();
            ////mainOpenGLControl_OpenGLDraw(null, null);
        }
        */



        public Grid Grid { set; get; }






        // https://stackoverflow.com/questions/39824098/add-arcball-effect-to-3d-quads


        /*
        //  Create a sphere.
        Cube cube = new Cube();
        cube.AddEffect(arcBallEffect);

        //  Add it.
        sceneControl1.Scene.SceneContainer.AddChild(cube);
        */

        private double max_dimension = 100;

        /// <summary>
        /// The current rotation.
        /// </summary>
        private double rotation = 0.0f;
        
        


        
        void FormSceneSample_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                arcBallEffect.ArcBall.MouseMove(SceneControl.Width / 2 + currentPositionX - e.X, e.Y);
            }
        }

        int currentPositionX;
        void FormSceneSample_MouseDown(object sender, MouseEventArgs e)
        {
            arcBallEffect.ArcBall.SetBounds(SceneControl.Width, SceneControl.Height);
            arcBallEffect.ArcBall.MouseDown(e.X, e.Y);
            currentPositionX = e.X;
        }
        private void openGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            arcBallEffect.ArcBall.MouseUp(SceneControl.Width / 2 + currentPositionX - e.X, e.Y);
        }













        /*
        void Draw(Grid grid)
        {

        }




        
        void Draw(Prop prop, GridPlane plane)
        {
            int nx = plane.NX;
            int ny = plane.NY;
            SharpGL.OpenGL gl = this.SceneControl.OpenGL;
            *//*
            gl.LoadIdentity();
            gl.Translate(x_shift, y_shift, 0f);
            //gl.Translate(translate.X, translate.Y, 0f);
            gl.Scale(zoomFactor, zoomFactor, 1f);
             * *//*
            Cube cube = new Cube();
            cube.Faces[0] = new Face();
            Face f = new Face();
            gl.Begin(BeginMode.Quads);
            for (int i = 0; i < nx; ++i)
                for (int j = 0; j < ny; ++j)
                {
                    if (plane.Act[i, j] == false) continue;
                    Color color = prop.Scale.Color(plane.Values[i, j]);
                    //Color color = (curGrid.Cells[i, j, k].Act == false) ? Color.Black : curProp.Scale.ValueColor(curProp.Values[i, j, k]);
                    gl.Color(color.R, color.G, color.B);
                    gl.Vertex(plane.Faces[i, j].Corners[0].X, plane.Faces[i, j].Corners[0].Y, propPlaneDepth);
                    gl.Vertex(plane.Faces[i, j].Corners[1].X, plane.Faces[i, j].Corners[1].Y, propPlaneDepth);
                    gl.Vertex(plane.Faces[i, j].Corners[3].X, plane.Faces[i, j].Corners[3].Y, propPlaneDepth);
                    gl.Vertex(plane.Faces[i, j].Corners[2].X, plane.Faces[i, j].Corners[2].Y, propPlaneDepth);
                }
            gl.End();
        }








        void Draw(GridPlane plane)
        {
            int nx = plane.NX;
            int ny = plane.NY;
            SharpGL.OpenGL gl = this.openGLControl_main.OpenGL;
            gl.Begin(BeginMode.Lines);
            gl.Color(Color.Gray);
            for (int i = 0; i < nx; ++i)
                for (int j = 0; j < ny; ++j)
                {
                    *//*
                    if (CurrentGridPlane.Act[i, j] == false)
                        continue;
                    *//*
                    // 1
                    gl.Vertex(plane.Faces[i, j].Corners[0].X, plane.Faces[i, j].Corners[0].Y, gridLinesPlaneDepth);
                    gl.Vertex(plane.Faces[i, j].Corners[1].X, plane.Faces[i, j].Corners[1].Y, gridLinesPlaneDepth);
                    // 2
                    gl.Vertex(plane.Faces[i, j].Corners[1].X, plane.Faces[i, j].Corners[1].Y, gridLinesPlaneDepth);
                    gl.Vertex(plane.Faces[i, j].Corners[3].X, plane.Faces[i, j].Corners[3].Y, gridLinesPlaneDepth);
                    // 3
                    gl.Vertex(plane.Faces[i, j].Corners[3].X, plane.Faces[i, j].Corners[3].Y, gridLinesPlaneDepth);
                    gl.Vertex(plane.Faces[i, j].Corners[2].X, plane.Faces[i, j].Corners[2].Y, gridLinesPlaneDepth);
                    // 4
                    gl.Vertex(plane.Faces[i, j].Corners[2].X, plane.Faces[i, j].Corners[2].Y, gridLinesPlaneDepth);
                    gl.Vertex(plane.Faces[i, j].Corners[0].X, plane.Faces[i, j].Corners[0].Y, gridLinesPlaneDepth);
                }
            gl.End();
        }



        //double x_shift = 0.0f;
        //double y_shift = 0.0f;
        //PointF translate = new PointF(0f, 0f);
        //double zoomFactor = 1.0f;

        const int bufferSize = 4;
        uint[] buffer = new uint[bufferSize];



        void UpdateMainOpenGLControl()
        {
            SharpGL.OpenGL gl = this.SceneControl.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            if (Grid == null) return;
            gl.LoadIdentity();
            gl.Translate(0f, 0f, 0f);
            gl.Scale(CurrModel.Position.Z, CurrModel.Position.Z, 1f);
            Draw(Grid);
        }






        void CreateMainOpenGLControl()
        {
            SceneControl.MouseWheel += new MouseEventHandler(mainOpenGLControl_MouseWheel);
        }



        private void mainOpenGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            UpdateMainOpenGLControl();
        }




        private void mainOpenGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            SharpGL.OpenGL gl = this.SceneControl.OpenGL;
            gl.ClearColor(0, 0, 0, 0);
            gl.Viewport(0, 0, SceneControl.Size.Width, SceneControl.Size.Height);
        }





        void HomePosition()
        {
            if (CurrGridPlane == null)
                return;
            SharpGL.OpenGL gl = this.openGLControl_main.OpenGL;

            double viewWidth = this.openGLControl_main.Width;
            double viewHeight = this.openGLControl_main.Height;

            ///double viewWidth = gl.RenderContextProvider.Width;
            ///double viewHeight = gl.RenderContextProvider.Height;

            //double viewHeight = mainOpenGLControl.Size.Height;
            //double viewWidth = mainOpenGLControl.Size.Width;

            double gridMinX = CurrGridPlane.MinX();
            double gridMaxX = CurrGridPlane.MaxX();
            double gridMinY = CurrGridPlane.MinY();
            double gridMaxY = CurrGridPlane.MaxY();
            double gridWidth = gridMaxX - gridMinX;
            double gridHeight = gridMaxY - gridMinY;
            CurrModel.Position.Z = Math.Min(viewHeight / gridHeight, viewWidth / gridWidth);
            CurrModel.Position.X =
                -gridMinX * CurrModel.Position.Z + viewWidth / 2 - gridWidth * CurrModel.Position.Z / 2;
            CurrModel.Position.Y =
                -gridMinY * CurrModel.Position.Z + viewHeight / 2 - gridHeight * CurrModel.Position.Z / 2;
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





        Point CurrMouseLocation = new Point();
        Point2D CurrMouseLocationF = new Point2D();

        Point2D WinToProjCoord(int xWin, int yWin)
        {
            SharpGL.OpenGL gl = this.SceneControl.OpenGL;
            double[] p = gl.UnProject(xWin, (gl.RenderContextProvider.Height - yWin), 0.0);
            return new Point2D(p[0], p[1]);
        }
        
        PointF ProjToWinCoord(double xProj, double yProj)
        {
            SharpGL.OpenGL gl = this.SceneControl.OpenGL;
            Vertex v = gl.Project(new Vertex(xProj, yProj, 0));
            return new PointF(v.X, v.Y);
        }

        private void mainOpenGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (SelectedLayers.Count() == 0) return;
            CurrMouseLocation.X = e.X;
            CurrMouseLocation.Y = e.Y;
            if (e.Button == MouseButtons.Right)
            {
                CurrModel.Position.X += +(CurrMouseLocation.X - PrevMouseLocation.X);
                CurrModel.Position.Y += -(CurrMouseLocation.Y - PrevMouseLocation.Y); // в View ось у направлена вниз
                PrevMouseLocation.X = CurrMouseLocation.X;
                PrevMouseLocation.Y = CurrMouseLocation.Y;
            }
            else
            {
                CurrMouseLocationF = WinToProjCoord(CurrMouseLocation.X, CurrMouseLocation.Y);
                ShowCurrLocation();
                if (AddModifierMode && modifierForm.AutoTitle) UpdateModifierWellNames();
            }
        }





        const int wheelDelta = 120;
        private void mainOpenGLControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                ZoomInOut(true, Math.Abs(e.Delta) / wheelDelta, e.Location);
            else
                ZoomInOut(false, Math.Abs(e.Delta) / wheelDelta, e.Location);
        }


        private void ZoomInOut(bool zoom, int times, Point refPoint)
        {
            const double zoomRatio = 1.05f;
            double mult = 1f;
            if (zoom)
                for (int i = 0; i < times; ++i)
                    mult *= zoomRatio;
            else
                for (int i = 0; i < times; ++i)
                    mult /= zoomRatio;
            CurrModel.Position.Z *= mult;
            //const double minZoomFactor = 0.000001f;
            //zoomFactor = Math.Max(zoomFactor, minZoomFactor);
            Point2D projPoint = WinToProjCoord(refPoint.X, refPoint.Y);
            double scale = (1 - mult) / mult * CurrModel.Position.Z;
            CurrModel.Position.X += projPoint.X * scale;
            CurrModel.Position.Y += projPoint.Y * scale;
            ////UpdateMainOpenGLControl();
            ////mainOpenGLControl_OpenGLDraw(null, null);
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
            SharpGL.OpenGL gl = this.SceneControl.OpenGL;
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            aspect = gl.RenderContextProvider.Width / gl.RenderContextProvider.Height;
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
            double gridMinX = CurrGridPlane.MinX();
            double gridMaxX = CurrGridPlane.MaxX();
            double gridMinY = CurrGridPlane.MinY();
            double gridMaxY = CurrGridPlane.MaxY();
            double gridHeight = gridMaxY - gridMinY;
            double gridWidth = gridMaxX - gridMinX;
            double min = Math.Min(gridWidth, gridHeight);
            switch (e.KeyCode)
            {
                case Keys.W: // Up
                    CurrModel.Position.Y -= -(min * shiftFrac); // у View ось у направлена вниз
                    break;
                case Keys.S: // Down
                    CurrModel.Position.Y += -(min * shiftFrac); // у View ось у направлена вниз
                    //NextLayer();
                    break;
                case Keys.D: // Right
                    CurrModel.Position.X += +(min * shiftFrac);
                    break;
                case Keys.A: // Left
                    CurrModel.Position.X -= +(min * shiftFrac);
                    break;
                case Keys.Q:
                    PrevLayer();
                    ShowCurrLocation();
                    break;
                case Keys.E:
                    NextLayer();
                    ShowCurrLocation();
                    break;
            }
        }







        private void mainOpenGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Cursor.Current = Cursors.Default;
            }
        }
*/





    }
}
