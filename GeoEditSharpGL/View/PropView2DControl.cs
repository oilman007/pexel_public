using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Collections;
using SharpGL.SceneGraph.Primitives;
using SharpGL.SceneGraph.Core;
using SharpGL.Enumerations;
using Google.Protobuf.WellKnownTypes;
using System.Runtime.InteropServices.WindowsRuntime;
using Pexel.HM;

namespace Pexel.View
{
    public partial class PropView2DControl : UserControl
    {
        public PropView2DControl()
        {
            InitializeComponent();
            CreateMainOpenGLControl();
            // scalePropForm
            scalePropForm.ApplyEvent += ScalePropForm_ApplyEvent;
            scalePropForm.GetFromPropEvent += ScalePropForm_GetFromPropEvent;
            scalePropForm.Text = "Scale";
            modifierForm.FormClosing += ModifierForm_FormClosing;
            setModifierNameForm.ApplyEvent += SetModifierNameForm_ApplyEvent;
            setModifierNameForm.Text = "Set Modifier Name";
        }


        private OpenGLControl openGLControl = new OpenGLControl();
        private void CreateMainOpenGLControl()
        {
            this.openGLControl = new OpenGLControl();

            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            //this.SuspendLayout();
            this.openGLControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openGLControl.DrawFPS = true;
            this.openGLControl.Location = new System.Drawing.Point(0, 0);
            this.openGLControl.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl.RenderContextType = SharpGL.RenderContextType.FBO;
            this.openGLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl.Size = new System.Drawing.Size(403, 522);
            this.openGLControl.TabIndex = 5;
            this.openGLControl.OpenGLInitialized += new System.EventHandler(this.mainOpenGLControl_OpenGLInitialized);
            this.openGLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.mainOpenGLControl_OpenGLDraw);
            this.openGLControl.Resized += new System.EventHandler(this.mainOpenGLControl_Resized);
            this.openGLControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mainOpenGLControl_KeyDown);
            //this.openGLControl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mainOpenGLControl_KeyPress);
            this.openGLControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mainOpenGLControl_MouseClick);
            this.openGLControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainOpenGLControl_MouseDown);
            this.openGLControl.MouseLeave += new System.EventHandler(this.mainOpenGLControl_MouseLeave);
            this.openGLControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mainOpenGLControl_MouseMove);
            this.openGLControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mainOpenGLControl_MouseUp);
            this.openGLControl.MouseWheel += new MouseEventHandler(mainOpenGLControl_MouseWheel);

            this.splitContainer8.Panel2.Controls.Add(this.openGLControl);
            //this.splitContainer3.SplitterDistance = 82;
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
            //this.ResumeLayout(false);
        }


        public EventHandler SelectionChanged;


        public delegate void MsgHandler(string msg);
        public MsgHandler MsgEvent;



        Grid CurrGrid { set; get; } = new Grid();
        //PropsOpt CurrProp { set; get; } = new PropsOpt();
        string[] Titles { set; get; } = Array.Empty<string>();
        ActProp[] Props { set; get; } = Array.Empty<ActProp>();
        ActProp CurProp { set; get; }
        PropScale[] Scales { set; get; } = Array.Empty<PropScale>();
        PropScale CurScale { set; get; } = new PropScale();
        ModifiersGroup CurrModifiersGroup { set; get; }
        string CurTitle { set; get; } = String.Empty;


        GridPlaneOpt CurrGridPlane { set; get; } = null;
        WellsPlane CurrWellsPlane { set; get; } = null;

        public bool AddModifierMode { set; get; } = false;
        public double ModifierRadius { set; get; } = 0;



        const double propPlaneDepth = 0;
        const double gridLinesPlaneDepth = -1;
        const double wellLinesPlaneDepth = -2;
        const double scalePlaneDepth = -3;
        const double histogramPlaneDepth = -4;
        const double circlePlaneDepth = -10;



        const int scale_x_min = 10;
        const int scale_x_max = 40;
        const int scale_max_hight = 400;

        Point3D Position { set; get; } = new Point3D();




        Color wellColor = Color.Goldenrod;
        Point PrevMouseLocation = new Point();
        Point CurrMouseLocation = new Point();

        Point2D CurrMouseLocationF { set; get; } = new Point2D();
        Point CurrCellIndex { set; get; } = new System.Drawing.Point();




        /*
        public void UpdateData(Grid grid, Prop[] props)
        {
            CurrGrid = grid;
            Props = props;
            Scales = Enumerable.Repeat(new PropScale(), props.Length).ToArray();
            UpdateProps();
            UpdateLayers();
            UpdateAllPlanes();
        }
        */



        public void Clear()
        {
            CurrGrid = new Grid();
            Props = Array.Empty<ActProp>();
            Titles = Array.Empty<string>();
            Scales = Array.Empty<PropScale>();
            UpdateProps();
            UpdateLayers();
            UpdateAllPlanes();
        }





        public void UpdateData(Grid grid, params ActProp[] props)
        {
            CurrGrid = grid;
            Props = props;
            List<string> titles = new List<string>();
            foreach (ActProp item in props)
                titles.Add(item.Title);
            Titles = titles.ToArray();
            Scales = new PropScale[Titles.Length];
            for (int i = 0; i < Titles.Length; ++i)
                Scales[i] = new PropScale();
            UpdateProps();
            UpdateLayers();
            UpdateAllPlanes();
        }



        void UpdateAllPlanes()
        {
            Cursor.Current = Cursors.WaitCursor;
            UpdateScale();
            UpdateGridPlane();
            UpdateWellsPlane();
            ShowCurrLocation();
            Cursor.Current = Cursors.Default;
        }



        public int[] SelectedLayers
        {
            get
            {
                int[] result = new int[listBox_layers.SelectedIndices.Count];
                int n = 0;
                foreach (int index in listBox_layers.SelectedIndices)
                    result[n++] = index;
                return result;
            }
            set
            {
                int count = listBox_layers.Items.Count;
                listBox_layers.SelectedIndexChanged -= new EventHandler(this.layersListBox_SelectedIndexChanged);
                for (int i = 0; i < count; ++i)
                {
                    listBox_layers.SetSelected(i, false);
                }
                foreach (int i in value)
                {
                    listBox_layers.SetSelected(i, true);
                }
                listBox_layers.SelectedIndexChanged += new EventHandler(this.layersListBox_SelectedIndexChanged);
                UpdateAllPlanes();
                SelectionChanged?.Invoke(this, EventArgs.Empty);
            }
        }



        int _selected_prop = -1;
        public int SelectedProp
        {
            get
            {
                return listBox_props.SelectedIndex;
            }
            set
            {
                _selected_prop = Math.Max(0, value);
                CurTitle = Titles[_selected_prop];
                CurScale = Scales[_selected_prop];

                CurProp = Props[_selected_prop];

                listBox_props.SelectedIndexChanged -= new EventHandler(this.listBox_props_SelectedIndexChanged);
                listBox_props.SetSelected(_selected_prop, true);
                listBox_props.SelectedIndexChanged += new EventHandler(this.listBox_props_SelectedIndexChanged);

                UpdateAllPlanes();
                SelectionChanged?.Invoke(this, EventArgs.Empty);
            }
        }


        ActProp GetCurProp(int ititle)
        {
            return Props[ititle];
        }



        void UpdateScale()
        {
            if (!string.IsNullOrEmpty(CurTitle) && CurScale != null && CurScale.Auto)
            {
                CurScale.Min = CurProp.Values.Min();
                CurScale.Max = CurProp.Values.Max();
            }
        }



        void UpdateLayers()
        {
            int n = CurrGrid.NZ();
            if (listBox_layers.Items.Count != n)
            {
                listBox_layers.Items.Clear();
                for (int i = 0; i < n; ++i)
                    listBox_layers.Items.Add("Layer " + Helper.ShowInt(i + 1));
            }
        }


        void UpdateProps()
        {
            listBox_props.Items.Clear();
            foreach(string title in Titles)
                listBox_props.Items.Add(title);
        }



        bool first_init = true;
        void UpdateGridPlane()
        {
            if (CurrGrid != null &&
                !string.IsNullOrEmpty(CurTitle) &&
                //CurrLayers != null &&
                SelectedLayers.Length > 0)
            {
                // buttons
                this.toolStripButton_scale.Enabled = true;
                this.toolStripButton_focus.Enabled = true;
                this.toolStripButton_gridLines.Enabled = true;
                //
                CurrGridPlane = new GridPlaneOpt(CurrGrid, CurProp, CurTitle, SelectedLayers);
                if (first_init)
                {
                    HomePosition();
                    first_init = false;
                }
            }
            else
            {
                // buttons
                this.toolStripButton_scale.Enabled = false;
                this.toolStripButton_focus.Enabled = false;
                this.toolStripButton_gridLines.Enabled = false;
                //
                CurrGridPlane = null;
            }
        }



        void UpdateWellsPlane()
        {
            if (CurrGrid != null &&
                //CurrLayers != null &&
                SelectedLayers.Length > 0)// && wells != null && wells.Count() > 0)
            {
                CurrWellsPlane = new WellsPlane(CurrGrid, SelectedLayers);
            }
            else
            {
                CurrWellsPlane = null;
            }
        }




        void ShowCurrLocation()
        {
            this.toolStripTextBox_xy.Text = "X=" + Helper.ShowDouble(CurrMouseLocationF.X, 2) + " Y=" + Helper.ShowDouble(CurrMouseLocationF.Y, 2);
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
                toolStripTextBox_index.Text = "I=" + Helper.ShowInt(CurrCellIndex.X + 1) + " J=" + Helper.ShowInt(CurrCellIndex.Y + 1);
                toolStripTextBox_cellValue.Text = Helper.ShowDouble(CurrGridPlane.Values[CurrCellIndex.X, CurrCellIndex.Y]);
            }
        }

               


        void UpdateMainOpenGLControl()
        {
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            if (CurrGrid is null || string.IsNullOrEmpty(CurTitle) || SelectedLayers.Length == 0) return;
            gl.LoadIdentity();
            gl.Translate(Position.X, Position.Y, 0);
            gl.Scale(Position.Z, Position.Z, 1);
            if (CurrGridPlane != null)
            {
                //if (ShowGridLines) Draw(CurrGridPlane);
                if (AddModifierMode)
                    DrawCircle(CurrMouseLocationF.X, CurrMouseLocationF.Y, ModifierRadius, 360, Color.AntiqueWhite, circlePlaneDepth);
                Draw(CurrGridPlane);
                Draw(CurScale); // TODO: должен быть собственный control
            }
            if (CurrWellsPlane != null) Draw(CurrWellsPlane);
        }



        void Draw(PropScale scale)
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



        void Draw(Prop prop, GridPlaneOpt plane)
        {
            int nx = plane.NX;
            int ny = plane.NY;
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            /*
            gl.LoadIdentity();
            gl.Translate(x_shift, y_shift, 0f);
            //gl.Translate(translate.X, translate.Y, 0f);
            gl.Scale(zoomFactor, zoomFactor, 1f);
             * */
            gl.Begin(BeginMode.Quads);
            for (int i = 0; i < nx; ++i)
                for (int j = 0; j < ny; ++j)
                {
                    if (plane.Act[i, j] == false) continue;
                    Color color = CurScale.Color(plane.Values[i, j]);
                    //Color color = (curGrid.Cells[i, j, k].Act == false) ? Color.Black : curProp.Scale.ValueColor(curProp.Values[i, j, k]);
                    gl.Color(color.R, color.G, color.B);
                    gl.Vertex(plane.Faces[i, j].Corners[0].X, plane.Faces[i, j].Corners[0].Y, propPlaneDepth);
                    gl.Vertex(plane.Faces[i, j].Corners[1].X, plane.Faces[i, j].Corners[1].Y, propPlaneDepth);
                    gl.Vertex(plane.Faces[i, j].Corners[3].X, plane.Faces[i, j].Corners[3].Y, propPlaneDepth);
                    gl.Vertex(plane.Faces[i, j].Corners[2].X, plane.Faces[i, j].Corners[2].Y, propPlaneDepth);
                }
            gl.End();
        }




        void Draw(GridPlaneOpt plane)
        {
            int nx = plane.NX;
            int ny = plane.NY;
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;

            if (ShowGridLines)
            {
                gl.Begin(BeginMode.Lines);
                gl.Color(Color.Gray);
                for (int i = 0; i < nx; ++i)
                    for (int j = 0; j < ny; ++j)
                    {
                        /*
                        if (CurrentGridPlane.Act[i, j] == false)
                            continue;
                        */
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

            gl.Begin(BeginMode.Quads);
            for (int i = 0; i < nx; ++i)
                for (int j = 0; j < ny; ++j)
                {
                    if (plane.Act[i, j] == false) continue;
                    Color color = CurScale.Color(plane.Values[i, j]);
                    //Color color = (curGrid.Cells[i, j, k].Act == false) ? Color.Black : curProp.Scale.ValueColor(curProp.Values[i, j, k]);
                    gl.Color(color.R, color.G, color.B);
                    gl.Vertex(plane.Faces[i, j].Corners[0].X, plane.Faces[i, j].Corners[0].Y, propPlaneDepth);
                    gl.Vertex(plane.Faces[i, j].Corners[1].X, plane.Faces[i, j].Corners[1].Y, propPlaneDepth);
                    gl.Vertex(plane.Faces[i, j].Corners[3].X, plane.Faces[i, j].Corners[3].Y, propPlaneDepth);
                    gl.Vertex(plane.Faces[i, j].Corners[2].X, plane.Faces[i, j].Corners[2].Y, propPlaneDepth);
                }
            gl.End();
        }




        void Draw(WellsPlane plane)
        {
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            foreach (WellFace well in plane.Faces)
            {
                if (!well.Checked) continue;
                // title
                PointF winPoint = ProjToWinCoord(well.Trajectory[0].X, well.Trajectory[0].Y);
                gl.DrawText((int)winPoint.X + 2, (int)winPoint.Y + 2, wellColor.R, wellColor.G, wellColor.B, "", 10, well.Title);
                int i, count = well.Trajectory.Count;
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




        public bool HomePosition()
        {
            if (CurrGridPlane is null)
                return false;
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            double viewWidth = this.openGLControl.Width;
            double viewHeight = this.openGLControl.Height;
            double gridMinX = CurrGridPlane.MinX();
            double gridMaxX = CurrGridPlane.MaxX();
            double gridMinY = CurrGridPlane.MinY();
            double gridMaxY = CurrGridPlane.MaxY();
            double gridWidth = gridMaxX - gridMinX;
            double gridHeight = gridMaxY - gridMinY;
            Position.Z = Math.Min(viewHeight / gridHeight, viewWidth / gridWidth);
            Position.X = -gridMinX * Position.Z + viewWidth / 2 - gridWidth * Position.Z / 2;
            Position.Y = -gridMinY * Position.Z + viewHeight / 2 - gridHeight * Position.Z / 2;
            return true;
        }




        private void mainOpenGLControl_MouseLeave(object sender, EventArgs e)
        {
            toolStripTextBox_xy.Text = string.Empty;
        }
        


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
        


        private void mainOpenGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (SelectedLayers.Length == 0) return;
            CurrMouseLocation.X = e.X;
            CurrMouseLocation.Y = e.Y;
            if (e.Button == MouseButtons.Right)
            {
                Position.X += +(CurrMouseLocation.X - PrevMouseLocation.X);
                Position.Y += -(CurrMouseLocation.Y - PrevMouseLocation.Y); // в View ось у направлена вниз
                PrevMouseLocation.X = CurrMouseLocation.X;
                PrevMouseLocation.Y = CurrMouseLocation.Y;
            }
            else
            {
                CurrMouseLocationF = WinToProjCoord(CurrMouseLocation.X, CurrMouseLocation.Y);
                ShowCurrLocation();
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
            Position.Z *= mult;
            //const double minZoomFactor = 0.000001f;
            //zoomFactor = Math.Max(zoomFactor, minZoomFactor);
            Point2D projPoint = WinToProjCoord(refPoint.X, refPoint.Y);
            double scale = (1 - mult) / mult * Position.Z;
            Position.X += projPoint.X * scale;
            Position.Y += projPoint.Y * scale;
            ////UpdateMainOpenGLControl();
            ////mainOpenGLControl_OpenGLDraw(null, null);
        }

               

        private void homeToolStripButton_Click(object sender, EventArgs e)
        {
            HomePosition();
        }
                


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




        const double shiftFrac = 0.01;
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
                    Position.Y -= -(min * shiftFrac); // у View ось у направлена вниз
                    break;
                case Keys.S: // Down
                    Position.Y += -(min * shiftFrac); // у View ось у направлена вниз
                    //NextLayer();
                    break;
                case Keys.D: // Right
                    Position.X += +(min * shiftFrac);
                    break;
                case Keys.A: // Left
                    Position.X -= +(min * shiftFrac);
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



        void NextLayer()
        {
            int[] selected = SelectedLayers;
            int count = selected.Length;
            int last = count - 1;
            if (count == 0)
                return;
            listBox_layers.ClearSelected();
            listBox_layers.SelectedIndex = Math.Min(selected[last] + 1, listBox_layers.Items.Count - 1);
        }



        void PrevLayer()
        {
            int[] selected = SelectedLayers;
            int count = selected.Length;
            if (count == 0)
                return;
            listBox_layers.ClearSelected();
            listBox_layers.SelectedIndex = Math.Max(selected[0] - 1, 0);
        }

        

        public bool ShowGridLines
        {
            set
            {
                if (value)
                    toolStripButton_gridLines.CheckState = CheckState.Checked;
                else
                    toolStripButton_gridLines.CheckState = CheckState.Unchecked;
            }
            get
            {
                return toolStripButton_gridLines.CheckState == CheckState.Checked;
            }
        }

        

        private void mainOpenGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Cursor.Current = Cursors.Default;
            }
        }

        

        private void layersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SelectionChanged?.Invoke(this, EventArgs.Empty);
            UpdateAllPlanes();
        }


        private void listBox_props_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedProp = listBox_props.SelectedIndex;
            this.toolStripButton_prop_export.Enabled = listBox_props.SelectedIndex != -1;
            //SelectionChanged?.Invoke(this, EventArgs.Empty);
            UpdateAllPlanes();
        }


        PropScaleForm scalePropForm = new PropScaleForm();
        private void toolStripButton_propScale_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CurTitle)) return;
            scalePropForm.PropScale = CurScale;
            scalePropForm.ShowDialog(this);
        }



        private void ScalePropForm_ApplyEvent(object sender, EventArgs e)
        {
            CurScale = scalePropForm.PropScale;
        }



        private void ScalePropForm_GetFromPropEvent(object sender, EventArgs e)
        {
            scalePropForm.PropScale.Min = CurProp.Values.Min();
            scalePropForm.PropScale.Max = CurProp.Values.Max();
        }



        ModifierForm modifierForm = new ModifierForm();
        private void toolStripButton_mod_add_Click(object sender, EventArgs e)
        {
            modifierForm.Show(this);
            AddModifierMode = true;
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



        private void ModifierForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AddModifierMode = false;
        }





        SetNameForm setModifierNameForm = new SetNameForm();
        private void toolStripButton_modSettings_Click(object sender, EventArgs e)
        {
            setModifierNameForm.Value = CurrModifiersGroup.Title;
            setModifierNameForm.ShowDialog(this);
        }


        private void SetModifierNameForm_ApplyEvent(object sender, EventArgs e)
        {
            /*
            CurrModifiersGroup.Title = setModifierNameForm.Value;
            this.UpdateModifiersGroups(CurrProp.Groups);
            */
        }



        /*
        static int imod = 1;
        void GenerateModTitle()
        {
            this.modifierForm.Title = "mod_" + Helper.ShowInt(imod++);
        }

        void AddModifiersGroup(ModifiersGroup mg)
        {
            Cursor.Current = Cursors.WaitCursor;
            CurrProp.Groups.Add(mg);
            int i = CurrProp.Groups.Count - 1;
            TreeNode node = new TreeNode(mg.Title) { Checked = true, Tag = i };
            this.treeView_mods.SelectedNode = this.treeView_mods.Nodes[0];
            this.treeView_mods.SelectedNode.Nodes.Add(node);
            this.treeView_mods.SelectedNode.Expand();
            CurrProp.Apply(CurrGrid, i);
            UpdateGridPlane();
            Cursor.Current = Cursors.Default;
        }
        */


        private void mainOpenGLControl_MouseClick(object sender, MouseEventArgs e)
        {
            /*
            if (e.Button == MouseButtons.Left && AddModifierMode)
            {
                switch (modifierForm.Layers)
                {
                    case Pexel.SelectedLayers.All:
                        AddModifiersGroup(new ModifiersGroup(modifierForm.Title, CurrMouseLocationF,
                                                             modifierForm.Radius, modifierForm.Value,
                                                             CurrProp.NZ()));
                        break;
                    case Pexel.SelectedLayers.Selected:
                        AddModifiersGroup(new ModifiersGroup(modifierForm.Title, CurrMouseLocationF,
                                                             modifierForm.Radius, modifierForm.Value,
                                                             CurrProp.NZ(), SelectedLayers));
                        break;
                }
                GenerateModTitle();
            }
            */
        }





        const int col_value = 0;
        const int col_radius = 1;
        const int col_use = 2;

        /*
        private void dataGridView_mods_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (CurrModifiersGroup == null ||
                CurrModifiersGroup.Modifiers.Length != this.dataGridView_mods.Rows.Count ||
                e.ColumnIndex < 0 ||
                this.dataGridView_mods.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                return;

            Cursor.Current = Cursors.WaitCursor;

            string value = this.dataGridView_mods.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            Modifier modifier = CurrModifiersGroup.Modifiers[e.RowIndex];
            if (CurrModifiersGroup.Applied && modifier.Use)
                CurrProp.Apply(CurrGrid, modifier, false);

            switch (e.ColumnIndex)
            {
                case col_value: { if (IsValidValue(value, out double result)) modifier.Value = result; } break;
                case col_radius: { if (IsValidRadius(value, out double result)) modifier.Radius = result; } break;
                case col_use: { modifier.Use = bool.Parse(value); } break;
            }

            if (CurrModifiersGroup.Applied && modifier.Use)
                CurrProp.Apply(CurrGrid, modifier, true);
            if (CurrModifiersGroup.Applied)
            {
                //CurrProp.UpdateScale();
                //UpdateGridPlane();
                CurrGridPlane.UpdateValues();
            }

            Cursor.Current = Cursors.Default;
        }
        */




        /*
        private void treeView_modifiers_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (CurrProp != null)
            {
                if (e.Node.Tag == null)
                    return;
                Cursor.Current = Cursors.WaitCursor;
                int i = (int)e.Node.Tag;
                CurrProp.Apply(CurrGrid, i);
                //CurrProp.UpdateScale();
                CurrGridPlane.UpdateValues();
                //UpdateGridPlane();
                Cursor.Current = Cursors.Default;
            }
        }
        */





        //bool updateModifiersList;
        void UpdateModifiersGroups(List<ModifiersGroup> mgs)
        {
            //updateModifiersList = true;
            this.treeView_mods.Nodes.Clear();
            if (mgs != null)
                for (int i = 0; i < mgs.Count; ++i)
                {
                    TreeNode node = new TreeNode(mgs[i].Title);
                    node.Checked = mgs[i].Applied;
                    node.Tag = i;
                    this.treeView_mods.Nodes.Add(node);
                }
            ///UpdateModifiersGroup(CurrProp?.CurrModifiersGroup);
        }




        /*
        void UpdateModifiersGroup(ModifiersGroup mg)
        {
            this.dataGridView_mods.Rows.Clear();
            if (mg != null)
            {
                int nz = CurrProp.NZ();
                for (int k = 0; k < nz; ++k)
                {
                    this.dataGridView_mods.Rows.Add(mg.Modifiers[k].Value, mg.Modifiers[k].Radius, mg.Modifiers[k].Use);
                    this.dataGridView_mods.Rows[k].HeaderCell.Value = "K " + Helper.ShowInt(k + 1);
                }
                CurrModifiersGroup = mg;
            }
            //modifierToShowIndex = undef_modifierToShowIndex;
            // modofier groups
            this.toolStripButton_mod_add.Enabled = mg != null;
            this.toolStripButton_mod_remove.Enabled = mg != null;
            this.toolStripButton_mod_rename.Enabled = mg != null;
        }
        */





        static bool IsValidValue(string txtValue, out double value)
        {
            return double.TryParse(txtValue, out value);
        }



        static bool IsValidRadius(string txtValue, out double value)
        {
            return double.TryParse(txtValue, out value) && value > 0;
        }


        private void toolStripButton_prop_export_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "GRDECL File (*.*)|*.*";
            dialog.DefaultExt = HistMatchingProject.Identifier;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (string.IsNullOrEmpty(filename))
                return;
            Task.Run(() =>
            {
                try
                {
                    MsgEvent?.Invoke($"{CurTitle} saving to file {filename} started");
                    CurProp.GetProp(CurrGrid.Actnum).Write(CurTitle, filename, HistMatching.FILETYPE);
                    MsgEvent?.Invoke($"{CurTitle} saved to file {filename} successfully!");
                }
                catch (Exception ex)
                {
                    MsgEvent?.Invoke($"{CurTitle} saving to file {filename} error! Message: {ex.Message}.");
                }
            });
        }




    }
}
