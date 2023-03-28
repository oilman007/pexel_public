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



        TreeNode[] CaseNodes { get; set; }


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




        void UpdateDates(DateTime[] dates)
        {
            this.numericUpDown_dates.Minimum = 0;
            this.numericUpDown_dates.Maximum = dates.Length - 1;
            this.numericUpDown_dates.Value = 0;
            //
            this.comboBox_dates.Items.Clear();
            foreach (DateTime date in dates)
                comboBox_dates.Items.Add(Helper.ShowDateTimeLong(date));
            //
            this.trackBar_dates.Minimum = 0;
            this.trackBar_dates.Maximum = dates.Length - 1;
            this.trackBar_dates.Value = 0;
        }




        void UpdateProject(FRProject project)
        {
            Project = project;
            this.treeView.Nodes.Clear();
            //CaseNodes = new TreeNode[project.Regions.Count];
            //int i = 0;
            foreach (FRRegion r in Project.Regions.Values)
                treeView.Nodes.Add(RegionNode(r));
            UpdateDates(project.Dates);
            UpdateBoundaries();
            //UpdateCases(project.Dates.First());
        }


        void UpdateBoundaries()
        {
            View2D.Boundaries =
                Project.Regions.Values.Where(x => x.Boundaries.Checked).SelectMany(x => x.Boundaries.Items).ToList();
        }



        void UpdateCases(DateTime dt)
        {
            View2D.WellsLinks.Clear();
            View2D.WellsPlane2D.Wells.Clear();
            int i = 0;
            foreach (FRRegion region in Project.Regions.Values)
            {
                FRCase frc = region.Cases.Where(v => v.FirstDt <= dt && dt <= v.LastDt).FirstOrDefault();
                if (frc is null)
                    continue;
                //
                //UpdateCaseNode(CaseNodes[i++], frc);
                // wells
                if (frc.Wells.Item1 && frc.Wells.Item2)
                    View2D.WellsPlane2D.Wells.AddRange(frc.Wells.Item3);
                // links
                if (frc.IPLinks.Item1 && frc.IPLinks.Item2)
                    View2D.WellsLinks.AddRange(frc.IPLinks.Item3);
                if (frc.PPLinks.Item1 && frc.PPLinks.Item2)
                    View2D.WellsLinks.AddRange(frc.PPLinks.Item3);
                if (frc.IILinks.Item1 && frc.IILinks.Item2)
                    View2D.WellsLinks.AddRange(frc.IILinks.Item3);
            }
        }


        /*
        void UpdateCaseNode(TreeNode case_node, FRCase frc)
        {
            //TreeNode result = new TreeNode("Case") { Checked = true, Tag = frc };
            //case_node.Checked = frc
            case_node.Tag = frc;
            case_node.Check1 = true;
            case_node.Check2 = true;
            bool expanded = case_node.IsExpanded;
            case_node.Nodes.Clear();
            case_node.Nodes.Add(WellsNode(frc.Wells.Item3));
            case_node.Nodes.Add(LinksNode("IPLinks", frc.IPLinks.Item3));
            case_node.Nodes.Add(LinksNode("IILinks", frc.IILinks.Item3));
            case_node.Nodes.Add(LinksNode("PPLinks", frc.PPLinks.Item3));

            View2D.Covered.Clear();
            foreach (FRWellsLink link in frc.IPLinks.Item3)
                View2D.Covered.AddRange(link.ImpactArea);

            if (expanded)
                case_node.Expand();
        }
        */



        TreeNode RegionNode(FRRegion region)
        {
            TreeNode result = new TreeNode("Region " + region.Title)
            {
                Checked = region.Used,
                Tag = region
            };
            result.Nodes.Add(BoundariesNode(region.Boundaries));
            foreach (FRCase frc in region.Cases)
                result.Nodes.Add(CaseNode(frc));
            return result;
        }



        TreeNode RegionNode2(FRRegion region)
        {
            TreeNode result = new TreeNode("Region " + region.Title)
            {
                Checked = region.Used,
                Tag = region
            };
            result.Nodes.Add(BoundariesNode(region.Boundaries));

            Tuple<bool, bool, List<FRWellsLink>> iplinks = 
                Tuple.Create(true, true, region.Cases.Select(x => x.IPLinks).Select(x=>x.Item3).ToArray());

            result.Nodes.Add(LinksNode("IPLinks", iplinks));
            result.Nodes.Add(LinksNode("IILinks", frc.IILinks));
            result.Nodes.Add(LinksNode("PPLinks", frc.PPLinks));

            return result;
        }



        TreeNode BoundariesNode(Tuple<bool, bool, List<Polygon2D>> boundaries)
        {
            TreeNode result = new TreeNode("Boundaries")
            {
                Checked = boundaries.Item2,
                Tag = boundaries
            };
            foreach (Polygon2D p in boundaries.Item3)
                result.Nodes.Add(BoundaryNode(p));
            return result;
        }

        TreeNode BoundaryNode(Polygon2D boundary)
        {
            TreeNode result = new TreeNode("Boundary" + boundary.Title)
            {
                Checked = boundary.Used,
                Tag = boundary
            };
            return result;
        }




        TreeNode CaseNode(FRCase frc)
        {
            TreeNode result = new TreeNode(Helper.ShowDateTimeShort(frc.FirstDt) + "-" + Helper.ShowDateTimeShort(frc.LastDt))
            {
                Checked = frc.Used,
                Tag = frc
            };
            result.Nodes.Add(WellsNode(frc.Wells));
            result.Nodes.Add(LinksNode("IPLinks", frc.IPLinks));
            result.Nodes.Add(LinksNode("IILinks", frc.IILinks));
            result.Nodes.Add(LinksNode("PPLinks", frc.PPLinks));
            return result;
        }

        TreeNode WellsNode(Tuple<bool, bool, List<WellFace2D>> wells)
        {
            TreeNode result = new TreeNode("Wells")
            {
                Checked = wells.Item2,
                Tag = wells
            };
            foreach (WellFace2D w in wells.Item3)
                result.Nodes.Add(WellNode(w));
            return result;
        }

        TreeNode WellNode(WellFace2D well)
        {
            TreeNode result = new TreeNode(well.Title)
            {
                Checked = well.Used,
                Tag = well
            };
            return result;
        }

        TreeNode LinksNode(string title, Tuple<bool, bool, List<FRWellsLink>> links)
        {
            TreeNode result = new TreeNode(title)
            {
                Checked = links.Item2,
                Tag = links
            };
            foreach (WellsLink l in links.Item3)
                result.Nodes.Add(LinkNode(l));
            return result;
        }


        TreeNode[] AllLinksNode(FRCase frc)
        {
            TreeNode result = new TreeNode(title)
            {
                Checked = links.Item2,
                Tag = links
            };
            foreach (WellsLink l in links.Item3)
                result.Nodes.Add(LinkNode(l));
            return result;
        }


        TreeNode LinkNode(WellsLink link)
        {
            TreeNode result = new TreeNode(link.Title)
            {
                Checked = link.Used,
                Tag = link
            };
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
                dialog.Filter = string.Format("Project Files (*.{0})|*.{0}", defaultExt);
                //dialog.Filter = String.Format("Project Files (*.{0})|*.{0}|All Files (*.*)|*.*", defaultExt);
                dialog.Multiselect = false;
                dialog.ShowDialog();
                string filename = dialog.FileName;
                if (!string.IsNullOrEmpty(filename))
                    LoadFile(filename);
            }
        }



        const string defaultExt = "PXLFR";



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
            dialog.Filter = string.Format("Project Files (*.{0})|*.{0}", defaultExt);
            dialog.DefaultExt = defaultExt;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (string.IsNullOrEmpty(filename))
                return false;
            return SaveFile(filename);
        }



        bool Export()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = string.Format("Flow Direction Model Files (*.{0})|*.{0}", FR.FDModel.EXT);
            dialog.DefaultExt = defaultExt;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (string.IsNullOrEmpty(filename))
                return false;
            return Project.GetModel().Save(filename);
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
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write);
                formatter.Serialize(stream, this.Project);
                stream.Close();
                msg = "File Saved Successfully!";
            }
            catch (Exception ex)
            {
                msg = "File Saving Error! Abort operation.";
                result = false;
            }
            Cursor.Current = Cursors.Default;
            _messageHandler(msg);
            //MessageBox.Show(msg);
            return result;
        }




        bool ReadFile(string filename)
        {
            Cursor.Current = Cursors.WaitCursor;
            string msg;
            bool result = true;
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                FRProject project = (FRProject)formatter.Deserialize(stream);
                UpdateProject(project);
                stream.Close();
                msg = "File Loaded Successfully!";
                View2D.HomePosition();
            }
            catch (Exception ex)
            {
                msg = "File Loading Error! Abort operation.";
                result = false;
            }
            Cursor.Current = Cursors.Default;
            _messageHandler(msg);
            //MessageBox.Show(msg);
            return result;
        }




        private void comboBox_dates_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDate(Math.Max(0, comboBox_dates.SelectedIndex));
        }

        private void numericUpDown_dates_ValueChanged(object sender, EventArgs e)
        {
            UpdateDate((int)numericUpDown_dates.Value);
        }

        private void trackBar_dates_Scroll(object sender, EventArgs e)
        {
            UpdateDate(trackBar_dates.Value);
        }



        int prev_date = -999;
        void UpdateDate(int date)
        {
            if (comboBox_dates.SelectedIndex != date)
                comboBox_dates.SelectedIndex = date;
            if ((int)numericUpDown_dates.Value != date)
                numericUpDown_dates.Value = date;
            if (trackBar_dates.Value != date)
                trackBar_dates.Value = date;

            if (prev_date != date)
            {
                prev_date = date;
                UpdateCases(Project.Dates[date]);
            }
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





        private void treeView_wells_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is WellFace2D)
            {
                WellFace2D well = (WellFace2D)e.Node.Tag;
                well.Visible = e.Node.Checked;
            }
            else
            if (e.Node.Tag is WellsLink)
            {
                WellsLink link = (WellsLink)e.Node.Tag;
                link.Visible = e.Node.Checked;
            }
            else
            if (e.Node.Tag is Polygon2D)
            {
                Polygon2D poly = (Polygon2D)e.Node.Tag;
                poly.Visible = e.Node.Checked;
            }
            else
            if (e.Node.Tag is Triangle2D)
            {
                Triangle2D triangle = (Triangle2D)e.Node.Tag;
                triangle.Visible = e.Node.Checked;
            }
            else
            if (e.Node.Tag is Quad2D)
            {
                Quad2D quad = (Quad2D)e.Node.Tag;
                quad.Visible = e.Node.Checked;
            }
        }






    }
}
