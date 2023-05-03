using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Threading.Tasks;
using Pexel.Eclipse;
using ZedGraph;
using Pexel.General;
using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;
using Pexel.Geometry2D;
using System.Data.SqlTypes;

namespace Pexel.HM.FR
{
    public partial class FRForm : Form
    {
        public FRForm()
        {
            InitializeComponent();
            Init();
            CreateFileMenuItems();
        }

        public const string EXT = "PXLFDM";

        View2D View2D = new View2D();
        void Init()
        {
            View2D.SetBackGround(BackGroundColor);
            View2D.SetWells(TitleColor);
            //this.splitContainer_main.Panel2.Controls.Add(this.View2D);
            this.tableLayoutPanel1.Controls.Add(View2D, 0, 1);
            this.View2D.Dock = System.Windows.Forms.DockStyle.Fill;
            this.View2D.Size = new Size(100, 100);
             //analyzer.SaveEvent += new EventHandler(SaveEvent);
            _messageHandler += new MessageHandler(ShowMessage);
        }





        FRProject Project { set; get; } = new FRProject();

        Color BackGroundColor = Color.White;
        Color TitleColor = Color.Black;
        Color ProdColor = Color.Brown;
        Color InjeColor = Color.Blue;
        Color IPLinksColor = Color.Red;
        Color PPLinksColor = Color.Green;
        Color IILinksColor = Color.Blue;
        Color BoundariesColor = Color.Black;
        Color TargetAreasColor = Color.Orange;
        Color RCoveredColor = Color.Yellow;
        Color SCoveredColor = Color.Yellow;
        Color UncoveredColor = Color.Gray;




        void UpdateDates()
        {
            //this.numericUpDown_dates.Minimum = 0;
            //this.numericUpDown_dates.Maximum = dates.Length - 1;
            //this.numericUpDown_dates.Value = 0;
            //
            this.comboBox_dates.Items.Clear();
            foreach (Index2D i in Periods)
                comboBox_dates.Items.Add(Helper.ShowDateTimeShort(Project.Dates[i.I]) + "-" + Helper.ShowDateTimeShort(Project.Dates[i.J]));
            //
            //this.trackBar_dates.Minimum = 0;
            //this.trackBar_dates.Maximum = dates.Length - 1;
            //this.trackBar_dates.Value = 0;
        }

        /*
        void UpdateBoundaries()
        {
            View2D.Boundaries = Project.Regions.Values.Select(x => x.Boundaries.Items).SelectMany(x => x).ToArray();
        }
        */


        void UpdateCases(int dt)
        {
            foreach (var periods in RegPeriods)
                foreach (var period in periods)
                    period.Visible = InsidePeriod(period.FirstDt, dt, period.LastDt);
        }



        static bool InsidePeriod(int first, int current, int last)
        {
            return first <= current && current <= last;
        }



        FRPeriod[][] RegPeriods { set; get; }
        FRLink[][] RegLinks { set; get; }





        void UpdateProject(FRProject project)
        {
            Project = project;

            View2D.WellsLinks.Clear();
            View2D.WellsPlane2D.Wells.Clear();

            RegPeriods = new FRPeriod[Project.Regions.Length][];
            RegLinks = new FRLink[Project.Regions.Length][];

            TreeModel _model = new TreeModel();
            this.treeViewAdv.Model = _model;
            this.treeViewAdv.BeginUpdate();
            _model.Nodes.Clear();
            int i = 0;
            foreach (FRRegion r in Project.Regions)
            {
                r.GetNodes(Project.Dates, out IViewable2D boundaries_node, out IViewable2D wells_node, 
                                          out IViewable2D links_node     , out RegPeriods[i]);
                //
                ColumnNode reg_node = new ColumnNode(r.Title, r._Visible, r._Used) { Tag = r };
                // bounds
                reg_node.Nodes.Add(BoundariesNode(boundaries_node));
                // wells
                WellFace2D[] wells = wells_node.Controlled.SelectMany(x => x.Controlled).Select(x => (WellFace2D)x).ToArray();
                reg_node.Nodes.Add(WellsNode(wells_node));
                // links
                FRLink[] links = links_node.Controlled.SelectMany(x => x.Controlled).Select(x => (FRLink)x).ToArray();
                RegLinks[i] = links;
                reg_node.Nodes.Add(LinksNode(links_node));
                //
                _model.Nodes.Add(reg_node);
                View2D.WellsPlane2D.Wells.AddRange(wells);
                View2D.WellsLinks.AddRange(links);
                i++;
            }
            this.treeViewAdv.EndUpdate();

            UpdatePeriods();
            UpdateCases(0);
            UpdateLinksColor();
            View2D.HomePosition();
        }




        ColumnNode BoundariesNode(IViewable2D boundaries)
        {
            ColumnNode result = new ColumnNode(boundaries.Title, boundaries._Visible, boundaries._Used) { Tag = boundaries };
            foreach (Polygon2D p in boundaries.Controlled)
            {
                result.Nodes.Add(BoundaryNode(p));
                View2D.Boundaries.Add(p);
            }
            return result;
        }

        ColumnNode BoundaryNode(Polygon2D boundary)
        {
            ColumnNode result = new ColumnNode("Boundary " + boundary.Title, boundary._Visible, boundary._Used) { Tag = boundary };
            return result;
        }




        ColumnNode WellsNode(IViewable2D wells_node)
        {
            ColumnNode result = new ColumnNode(wells_node.Title, wells_node._Visible, wells_node._Used) { Tag = wells_node };
            foreach(IViewable2D well in wells_node.Controlled)
                result.Nodes.Add(WellNode(well));
            return result;
        }

        ColumnNode WellNode(IViewable2D well)
        {
            ColumnNode result = new ColumnNode(well.Title, well._Visible, well._Used) { Tag = well };
            return result;
        }


        ColumnNode LinksNode(IViewable2D links_node)
        {
            ColumnNode result = new ColumnNode(links_node.Title, links_node._Visible, links_node._Used) { Tag = links_node };
            foreach (IViewable2D well_links in links_node.Controlled)
                result.Nodes.Add(WellLinksNode(well_links));
            return result;
        }


        ColumnNode WellLinksNode(IViewable2D well_links)
        {
            ColumnNode result = new ColumnNode(well_links.Title, well_links._Visible, well_links._Used) { Tag = well_links };
            foreach (FRLink link in well_links.Controlled)
                result.Nodes.Add(LinkNode(link));
            return result;
        }


        ColumnNode LinkNode(FRLink link)
        {
            ColumnNode result = new ColumnNode(link.Title, link._Visible, link._Used) { Tag = link };
            return result;
        }





        public delegate void MessageHandler(string message);
        MessageHandler _messageHandler;


        void ShowMessage(string message)
        {
            var inner = Task.Factory.StartNew(() =>  // вложенная задача
            {
                try
                {
                    toolStripStatusLabel_msg.Text = message;
                    Thread.Sleep(5000);
                    toolStripStatusLabel_msg.Text = string.Empty;
                }
                catch { }
            });
        }
        



        private void homeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            View2D.HomePosition();
        }





        void SetColor(ref List<WellsLink> links, Color color)
        {
            for (int i = 0; i < links.Count; ++i) links[i].Color = color;
        }
        void SetColor(ref List<Triangle2D> triangles, Color color)
        {
            for (int i = 0; i < triangles.Count; ++i) triangles[i].Color = color;
        }
        void SetColor(ref List<Polygon2D> poly, Color color)
        {
            for (int i = 0; i < poly.Count; ++i) poly[i].Color = color;
        }
        void SetColor(ref List<Quad2D> quads, Color color)
        {
            for (int i = 0; i < quads.Count; ++i) quads[i].Color = color;
        }



        void newMenu_Click(object sender, EventArgs e)
        {
            New();
        }

        void openMenu_Click(object sender, EventArgs e)
        {
            Open();
        }

        void saveMenu_Click(object sender, EventArgs e)
        {
            Save();
        }

        void saveAsMenu_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        void exportMenu_Click(object sender, EventArgs e)
        {
            Export();
        }

        void recentFilesMenu_Click(object sender, EventArgs e)
        {
            string filename = ((ToolStripMenuItem)sender).Name;
            OpenRecentFile(filename);
        }

        void close_Event(object sender, FormClosingEventArgs e)
        {
            if (OkToContinue())
            {
                //WriteSettings();
                e.Cancel = false;
            }
            else
                e.Cancel = true;
        }

        void exit_Click(object sender, EventArgs e)
        {
            Close();
        }





        
        void New()
        {
            if (OkToContinue())
            {
                //UpdateProject(new FRProject());
                //SetCurrentFile(string.Empty);
                //
                NewFRForm newFRForm = new NewFRForm();
                newFRForm.UpdateProjectEvent += UpdateProject;
                newFRForm.ShowDialog();
            }
        }





        void Open()
        {
            if (OkToContinue())
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = string.Format("Project Files (*.{0})|*.{0}", EXT);
                //dialog.Filter = String.Format("Project Files (*.{0})|*.{0}|All Files (*.*)|*.*", defaultExt);
                dialog.Multiselect = false;
                dialog.ShowDialog();
                string filename = dialog.FileName;
                if (!string.IsNullOrEmpty(filename))
                    LoadFile(filename);
            }
        }






        bool Save()
        {
            if (string.IsNullOrEmpty(curfile))
                return SaveAs();
            else
                return SaveFile(curfile);
        }




        bool SaveAs()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = string.Format("Project Files (*.{0})|*.{0}", EXT);
            dialog.DefaultExt = EXT;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (string.IsNullOrEmpty(filename))
                return false;
            return SaveFile(filename);
        }



        bool Export()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = string.Format("Flow Direction Model Files (*.{0})|*.{0}", EXT);
            dialog.DefaultExt = EXT;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (string.IsNullOrEmpty(filename))
                return false;
            return true; // Project.GetModel().Save(filename);
        }



        void OpenRecentFile(string filename)
        {
            if (OkToContinue())
            {
                LoadFile(filename);
            }
        }


        bool modified = false;

        bool OkToContinue()
        {
            if (modified)
            {
                string caption = "Project Closing";
                string message = "The document has been modified.\n" + "Do you want to save your changes?";
                var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                    return Save();
                else if (result == DialogResult.Cancel)
                    return false;
            }
            return true;
        }


        bool LoadFile(string filename)
        {
            if (!ReadFile(filename))
                return false;
            SetCurrentFile(filename);
            return true;
        }


        bool SaveFile(string filename)
        {
            if (!WriteFile(filename))
                return false;
            SetCurrentFile(filename);
            return true;
        }


        void SetCurrentFile(string filename)
        {
            curfile = filename;
            string shownName = "Untitled";
            if (!string.IsNullOrEmpty(curfile))
            {
                shownName = StrippedName(curfile);
                recentFiles.Remove(curfile);
                //
                recentFiles.Reverse();
                recentFiles.Add(curfile);
                recentFiles.Reverse();
                //
                UpdateRecentFile();
            }
            this.Text = shownName;
        }


        void UpdateRecentFile()
        {
            List<string> temp = new List<string>();
            foreach (string filename in recentFiles)
                if (File.Exists(filename))
                    temp.Add(filename);
            recentFiles = temp;
            for (int i = 0; i < maxRecentFiles; ++i)
            {
                if (i < recentFiles.Count())
                {
                    recentFilesFileMenu[i].Name = recentFiles[i];
                    recentFilesFileMenu[i].Text = "&" + (i + 1).ToString() + " " + StrippedName(recentFiles[i]);
                    recentFilesFileMenu[i].Visible = true;
                }
                else
                    recentFilesFileMenu[i].Visible = false;
            }
            separatorDownFileMenu.Visible = (recentFiles.Count != 0);
        }


        string StrippedName(string fullFileName)
        {
            FileInfo fileinfo = new FileInfo(fullFileName);
            return fileinfo.Name;
        }


        List<string> recentFiles = new List<string>();
        string curfile;


        bool WriteFile(string filename)
        {
            Cursor.Current = Cursors.WaitCursor;
            string msg;
            bool result = true;
            if(this.Project.Save(filename))
            {
                _messageHandler("File Saved Successfully!");
            }
            else
            {
                _messageHandler("File Saving Error! Abort operation.");
                result = false;
            }
            Cursor.Current = Cursors.Default;
            return result;
        }




        bool ReadFile(string filename)
        {
            Cursor.Current = Cursors.WaitCursor;
            bool result = true;
            if(FRProject.Load(filename, out FRProject project))
            {
                UpdateProject(project);
                _messageHandler("File Loaded Successfully!");
                View2D.HomePosition();
            }
            else
            {
                _messageHandler("File Loading Error! Abort operation.");
                result = false;
            }
            Cursor.Current = Cursors.Default;
            return result;
        }




        ToolStripSeparator separatorUpFileMenu, separatorDownFileMenu;
        ToolStripMenuItem newMenu, openMenu, saveMenu, saveAsMenu, exitMenu, exportMenu;

        const int maxRecentFiles = 5;
        ToolStripMenuItem[] recentFilesFileMenu;

        //ToolStripMenuItem wellsMenu, addWellsMenu, removeWellsMenu, renameWellsMenu;


        void CreateFileMenuItems()
        {
            //
            // File Menu Items
            //
            newMenu = new ToolStripMenuItem();
            newMenu.Name = "New_FileMenu";
            newMenu.Size = new Size(152, 22);
            newMenu.Text = "&New";
            newMenu.Click += new System.EventHandler(newMenu_Click);
            //
            openMenu = new ToolStripMenuItem();
            openMenu.Name = "Open_FileMenu";
            openMenu.Size = new Size(152, 22);
            openMenu.Text = "&Open";
            openMenu.Click += new System.EventHandler(openMenu_Click);
            //
            saveMenu = new ToolStripMenuItem();
            saveMenu.Name = "Save_FileMenu";
            saveMenu.Size = new Size(152, 22);
            saveMenu.Text = "&Save";
            saveMenu.Click += new System.EventHandler(saveMenu_Click);
            saveMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            //
            saveAsMenu = new ToolStripMenuItem();
            saveAsMenu.Name = "SaveAs_FileMenu";
            saveAsMenu.Size = new Size(152, 22);
            saveAsMenu.Text = "Save &As";
            saveAsMenu.Click += new System.EventHandler(saveAsMenu_Click);
            //
            exportMenu = new ToolStripMenuItem();
            exportMenu.Name = "Export_FileMenu";
            exportMenu.Size = new Size(152, 22);
            exportMenu.Text = "&Export";
            exportMenu.Click += new System.EventHandler(exportMenu_Click);
            //
            separatorUpFileMenu = new ToolStripSeparator();
            separatorUpFileMenu.Name = "separatorUp_FileMenu";
            separatorUpFileMenu.Size = new Size(149, 6);
            recentFilesFileMenu = new ToolStripMenuItem[maxRecentFiles];
            for (int i = 0; i < maxRecentFiles; ++i)
            {
                recentFilesFileMenu[i] = new ToolStripMenuItem();
                recentFilesFileMenu[i].Size = new Size(152, 22);
                recentFilesFileMenu[i].Click += new System.EventHandler(recentFilesMenu_Click);
            }
            separatorDownFileMenu = new ToolStripSeparator();
            separatorDownFileMenu.Name = "separator2_FileMenu";
            separatorDownFileMenu.Size = new Size(149, 6);
            UpdateRecentFile();
            //
            exitMenu = new ToolStripMenuItem();
            exitMenu.Name = "Exit_FileMenuItem";
            exitMenu.Size = new Size(152, 22);
            exitMenu.Text = "E&xit";
            exitMenu.Click += new System.EventHandler(exit_Click);
            //
            // File Menu
            //
            fileToolStripMenuItem.DropDownItems.Add(newMenu);
            fileToolStripMenuItem.DropDownItems.Add(openMenu);
            fileToolStripMenuItem.DropDownItems.Add(saveMenu);
            fileToolStripMenuItem.DropDownItems.Add(saveAsMenu);
            fileToolStripMenuItem.DropDownItems.Add(exportMenu);
            fileToolStripMenuItem.DropDownItems.Add(separatorUpFileMenu);
            for (int i = 0; i < maxRecentFiles; ++i)
                fileToolStripMenuItem.DropDownItems.Add(recentFilesFileMenu[i]);
            fileToolStripMenuItem.DropDownItems.Add(separatorDownFileMenu);
            fileToolStripMenuItem.DropDownItems.Add(exitMenu);
        }







        /*
        void UpdateWells()
        {
            WellsTreeNodes.Nodes.Clear();
            foreach (WellFace2D face in Project.Data.Wells)
                WellsTreeNodes.Nodes.Add(new TreeNode(face.Title) { Tag = face, Checked = face.Checked });

            View2D.WellsPlane2D.Wells.Clear();
            View2D.WellsPlane2D.Wells.AddRange(Project.Data.Wells);
        }




        void UpdateTargetAreas()
        {
            TargetAreasTreeNodes.Nodes.Clear();
            foreach (Polygon2D poly in Project.Data.TargetAreas)
                TargetAreasTreeNodes.Nodes.Add(new TreeNode(poly.Title) { Tag = poly, Checked = poly.Checked });

            for (int i = 0; i < Project.Data.TargetAreas.Count; ++i)
                Project.Data.TargetAreas[i].Color = TargetAreasColor;

            View2D.TargetAreas = Project.Data.TargetAreas;
            cycAnalyzerForm.SetAreas(Project.Data.TargetAreas);
        }





        void UpdateLinks(List<WellsLink> iplinks, List<WellsLink> pplinks, List<WellsLink> iilinks)
        {
            SetColor(ref iplinks, IPLinksColor);
            SetColor(ref pplinks, PPLinksColor);
            SetColor(ref iilinks, IILinksColor);
            UpdateNodes(ref IPLinksTreeNodes, iplinks);
            UpdateNodes(ref PPLinksTreeNodes, pplinks);
            UpdateNodes(ref IILinksTreeNodes, iilinks);
            View2D.WellsLinks.Clear();
            if (IPLinksTreeNodes.Checked)
                View2D.WellsLinks.AddRange(iplinks);
            if (PPLinksTreeNodes.Checked)
                View2D.WellsLinks.AddRange(pplinks);
            if (IILinksTreeNodes.Checked)
                View2D.WellsLinks.AddRange(iilinks);
        }

        void UpdateLinks()
        {
            View2D.WellsLinks.Clear();
            if (IPLinksTreeNodes.Checked)
                View2D.WellsLinks.AddRange((List<WellsLink>)IPLinksTreeNodes.Tag);
            if (PPLinksTreeNodes.Checked)
                View2D.WellsLinks.AddRange((List<WellsLink>)PPLinksTreeNodes.Tag);
            if (IILinksTreeNodes.Checked)
                View2D.WellsLinks.AddRange((List<WellsLink>)IILinksTreeNodes.Tag);
        }
        */




        /*
        TreeNode selected_node;
        private void ucTreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            selected_node = e.Node;
        }


        private void ucTreeView_Click(object sender, EventArgs e)
        {
            if (selected_node is null) return;
            //
            TreeNode node = selected_node as TreeNode;
            if (!node.Check2)
                selected_node.SelectedImageIndex = 2;
            else if (!node.Check1)
                selected_node.SelectedImageIndex = 1;
            else
                selected_node.SelectedImageIndex = 0;
            selected_node.ImageIndex = selected_node.SelectedImageIndex;
            //
            if (node.Tag is WellFace2D)
            {
                WellFace2D well = (WellFace2D)node.Tag;
                well.Visible = node.Check1;
                well.Used = node.Check2;
            }
            else if (node.Tag is WellsLink)
            {
                WellsLink link = (WellsLink)node.Tag;
                link.Visible = node.Check1;
                link.Used = node.Check2;
            }
            else if (node.Tag is Polygon2D)
            {
                Polygon2D poly = (Polygon2D)node.Tag;
                poly.Visible = node.Check1;
                poly.Used = node.Check2;
            }
            else if (node.Tag is Triangle2D)
            {
                Triangle2D triangle = (Triangle2D)node.Tag;
                triangle.Visible = node.Check1;
                triangle.Used = node.Check2;
            }
            else if (node.Tag is Quad2D)
            {
                Quad2D quad = (Quad2D)node.Tag;
                quad.Visible = node.Check1;
                quad.Used = node.Check2;
            }
            else if(node.Tag is Tuple<bool, bool, List<Polygon2D>>)
            {
                Tuple<bool, bool, List<Polygon2D>> boundaries = node.Tag as Tuple<bool, bool, List<Polygon2D>>;
                boundaries = Tuple.Create(node.Check1, node.Check2, boundaries.Item3);
                UpdateBoundaries();
            }

        }
        */






        private void treeViewAdv_MouseClick(object sender, MouseEventArgs e)
        {
            NodeControlInfo info = (e is null) ? 
                                    new NodeControlInfo(null, new Rectangle(0, 0, 0, 0), null) : 
                                    treeViewAdv.GetNodeControlInfoAt(e.Location);

            if (info.Control is NodeCheckBox)
            {
                //NodeCheckBox nodeCheckBox = (NodeCheckBox)info.Control;
                ColumnNode node = info.Node.Tag as ColumnNode;
                IViewable2D item = node.Tag as IViewable2D;
                item.Visible = node.NodeControl1;
                item.Used = node.NodeControl2;
                /*
                if (nodeCheckBox.DataPropertyName == "NodeControl1")
                {
                    ColumnNode node = info.Node.Tag as ColumnNode;
                    //node.NodeControl1 = !node.NodeControl1;
                }
                else if (nodeCheckBox.DataPropertyName == "NodeControl2")
                {
                    ColumnNode node = info.Node.Tag as ColumnNode;
                    //node.NodeControl2 = !node.NodeControl2;
                }
                */
                UpdatePeriods();
                UpdateLinksColor();
            }
        }



        void UpdatePeriods()
        {
            Index2D[] periods = GetPeriods(Project.Regions.Where(x => x.Visible).ToArray());
            if (!Periods.SequenceEqual(periods))
            {
                Periods = periods;
                this.comboBox_dates.Items.Clear();
                foreach (Index2D i in Periods)
                    comboBox_dates.Items.Add(Helper.ShowDateTimeShort(Project.Dates[i.I]) + "-" + Helper.ShowDateTimeShort(Project.Dates[i.J]));
            }
            int dt = prev_date;
            prev_date = -999;
            UpdateDate(dt);
        }


        Index2D[] Periods { set; get; } = Array.Empty<Index2D>();




        void UpdateLinksColor()
        {
            List<int> items = new List<int>();
            for (int i = 0; i < RegLinks.Length; i++)
                if (RegLinks[i].Any(x => x.Visible))
                    items.Add(i);
            
            PropScale scale = new PropScale(0, items.Count - 1);

            for (int i = 0; i < items.Count; i++)
            {
                Color color = scale.Color(i);
                foreach (FRLink link in RegLinks[items[i]])
                    link.Color = color;
            }
        }





        Index2D[] GetPeriods(FRRegion[] regions)
        {
            if (regions.Length == 0)
                return Array.Empty<Index2D>();
            List<int> targ_dt = new List<int>();
            foreach (FRRegion r in regions)
            {
                targ_dt.AddRange(r.Periods.Select(x => x.I));
                targ_dt.AddRange(r.Periods.Select(x => x.J + 1));
            }
            targ_dt = targ_dt.Distinct().OrderBy(x => x).ToList();
            Index2D[] result = new Index2D[targ_dt.Count - 1];
            for (int i = 1; i < targ_dt.Count; i++)
            {
                int f = targ_dt[i - 1];
                int l = targ_dt[i];
                result[i - 1] = new Index2D(f, l - 1);
            }
            return result;
        }


        void SetVisibleRegions(bool visible)
        {
            ColumnNode node = this.treeViewAdv.AllNodes.First().Tag as ColumnNode;
            while (node != null)
            {                
                node.NodeControl1 = visible;
                IViewable2D item = node.Tag as IViewable2D;
                item.Visible = visible;
                //
                node = node.NextNode as ColumnNode;
            }
            treeViewAdv.Refresh();
        }


        void SetUsedRegions(bool used)
        {
            ColumnNode node = this.treeViewAdv.AllNodes.First().Tag as ColumnNode;
            while (node != null)
            {
                node.NodeControl2 = used;
                IViewable2D item = node.Tag as IViewable2D;
                item.Used = used;
                //
                node = node.NextNode as ColumnNode;
            }
            treeViewAdv.Refresh();
        }


        void SetVisibleBounds(bool visible)
        {
            SetVisibleItem(visible, 0);
        }

        void SetVisibleWells(bool visible)
        {
            SetVisibleItem(visible, 1);
        }

        void SetVisibleLinks(bool visible)
        {
            SetVisibleItem(visible, 2);
        }


        void SetVisibleItem(bool visible, int item)
        {
            ColumnNode node = this.treeViewAdv.AllNodes.First().Tag as ColumnNode;
            int i = 0;
            while (node != null)
            {
                ColumnNode child = node.Parent.Nodes[i++].Nodes[item] as ColumnNode;
                child.NodeControl1 = visible;
                IViewable2D tag = child.Tag as IViewable2D;
                tag.Visible = visible;
                //
                node = node.NextNode as ColumnNode;
            }
            treeViewAdv.Refresh();
        }

        void SetUsedItem(bool used, int item)
        {
            ColumnNode node = this.treeViewAdv.AllNodes.First().Tag as ColumnNode;
            int i = 0;
            while (node != null)
            {
                ColumnNode child = node.Parent.Nodes[i++].Nodes[item] as ColumnNode;
                child.NodeControl2 = used;
                IViewable2D tag = child.Tag as IViewable2D;
                tag.Used = used;
                //
                node = node.NextNode as ColumnNode;
            }
            treeViewAdv.Refresh();
        }




        void SetUsedBounds(bool used)
        {
            SetUsedItem(used, 0);
        }

        void SetUsedWells(bool used)
        {
            SetUsedItem(used, 1);
        }

        void SetUsedLinks(bool used)
        {
            SetUsedItem(used, 2);
        }





        private void button_show_all_regions_Click(object sender, EventArgs e)
        {
            SetVisibleRegions(true);
        }

        private void button_hide_all_regions_Click(object sender, EventArgs e)
        {
            SetVisibleRegions(false);
        }

        private void button_show_all_bouns_Click(object sender, EventArgs e)
        {
            SetVisibleBounds(true);
        }

        private void button_hide_all_bouns_Click(object sender, EventArgs e)
        {
            SetVisibleBounds(false);
        }

        private void button_show_all_wells_Click(object sender, EventArgs e)
        {
            SetVisibleWells(true);
        }

        private void button_hide_all_wells_Click(object sender, EventArgs e)
        {
            SetVisibleWells(false);
        }

        private void button_show_all_links_Click(object sender, EventArgs e)
        {
            SetVisibleLinks(true);
        }

        private void button_hide_all_links_Click(object sender, EventArgs e)
        {
            SetVisibleLinks(false);
        }

        private void button_use_all_regions_Click(object sender, EventArgs e)
        {
            SetUsedRegions(true);
        }

        private void button_unuse_all_regions_Click(object sender, EventArgs e)
        {
            SetUsedRegions(false);
        }

        private void button_use_all_boundaries_Click(object sender, EventArgs e)
        {
            SetUsedBounds(true);
        }

        private void button_unuse_all_boundaries_Click(object sender, EventArgs e)
        {
            SetUsedBounds(false);
        }

        private void button_use_all_wells_Click(object sender, EventArgs e)
        {
            SetUsedWells(true);
        }

        private void button_unuse_all_wells_Click(object sender, EventArgs e)
        {
            SetUsedWells(false);
        }

        private void button_use_all_links_Click(object sender, EventArgs e)
        {
            SetUsedLinks(true);
        }

        private void button_unuse_all_links_Click(object sender, EventArgs e)
        {
            SetUsedLinks(false);
        }

        private void button_first_dt_Click(object sender, EventArgs e)
        {
            UpdateDate(Periods.First().I);
        }

        private void button_prev_dt_Click(object sender, EventArgs e)
        {
            int i = comboBox_dates.SelectedIndex - 1;
            UpdateDate(Periods[i].I);
        }

        private void button_next_dt_Click(object sender, EventArgs e)
        {
            int i = comboBox_dates.SelectedIndex + 1;
            UpdateDate(Periods[i].I);
        }

        private void button_last_dt_Click(object sender, EventArgs e)
        {
            UpdateDate(Periods.Last().I);
        }





        private void comboBox_dates_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDate(Periods[comboBox_dates.SelectedIndex].I);
        }


        int prev_date = -999;
        void UpdateDate(int date)
        {
            //if (comboBox_dates.SelectedIndex != date)
            //    comboBox_dates.SelectedIndex = date;
            //if ((int)numericUpDown_dates.Value != date)
            //    numericUpDown_dates.Value = date;
            //if (trackBar_dates.Value != date)
            //    trackBar_dates.Value = date;
            if (Periods.Length > 0)
            {
                if (prev_date != date)
                {
                    prev_date = date;
                    UpdateCases(date);
                }
            }
        }














    }
}
