﻿using System;
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
            this.tableLayoutPanel1.Controls.Add(this.numericUpDown_dates, 0, 1);
            this.View2D.Dock = System.Windows.Forms.DockStyle.Fill;
            this.View2D.Size = new Size(100, 100);
             //analyzer.SaveEvent += new EventHandler(SaveEvent);
            _messageHandler += new MessageHandler(ShowMessage);
        }


        TreeNode[] CaseNodes { get; set; }


        FRProject Project = new FRProject();

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
            this.numericUpDown_dates.Minimum = 1;
            this.numericUpDown_dates.Maximum = dates.Length;
            this.numericUpDown_dates.Value = 1;
            //
            this.comboBox_dates.Items.Clear();
            foreach (DateTime date in dates)
                comboBox_dates.Items.Add(Helper.ShowDateTimeLong(date));
            //
            this.trackBar_dates.Minimum = 1;
            this.trackBar_dates.Maximum = dates.Length;
            this.trackBar_dates.Value = 1;
        }




        void UpdateProject(FRProject project)
        {
            Project = project;
            this.treeView_cases.Nodes.Clear();
            CaseNodes = new TreeNode[project.Regions.Count];
            int i = 0;
            foreach (FRRegion r in project.Regions.Values)
                treeView_cases.Nodes.Add(RegionNode(r, out CaseNodes[i++]));
            UpdateDates(project.Dates);
        }




        TreeNode RegionNode(FRRegion region, out TreeNode case_node)
        {
            TreeNode result = new TreeNode(region.Title) { Checked = true };
            result.Tag = region;
            result.Nodes.Add(BoundariesNode(region.Boundaries));
            case_node = CaseNode(region.Cases.First());
            result.Nodes.Add(case_node);
            return result;
        }


        TreeNode BoundariesNode(IEnumerable<Polygon2D> boundaries)
        {
            TreeNode result = new TreeNode("Boundaries") { Checked = true };
            result.Tag = boundaries;
            foreach (Polygon2D p in boundaries)
                result.Nodes.Add(BoundaryNode(p));
            return result;
        }

        TreeNode BoundaryNode(Polygon2D boundary)
        {
            TreeNode result = new TreeNode(boundary.Title) { Checked = boundary.Checked };
            result.Tag = boundary;
            return result;
        }




        TreeNode CaseNode(FRCase frc)
        {
            TreeNode result = new TreeNode("Case") { Checked = true };
            result.Tag = frc;
            result.Nodes.Add(WellsNode(frc.Wells));
            result.Nodes.Add(LinksNode("IPLinks", frc.IPLinks));
            result.Nodes.Add(LinksNode("IILinks", frc.IILinks));
            result.Nodes.Add(LinksNode("PPLinks", frc.PPLinks));
            return result;
        }

        TreeNode WellsNode(IEnumerable<WellFace2D> wells)
        {
            TreeNode result = new TreeNode("Wells") { Checked = true };
            result.Tag = wells;
            foreach (WellFace2D w in wells)
                result.Nodes.Add(WellNode(w));
            return result;
        }

        TreeNode WellNode(WellFace2D well)
        {
            TreeNode result = new TreeNode(well.Title) { Checked = well.Checked };
            result.Tag = well;
            return result;
        }

        TreeNode LinksNode(string title, IEnumerable<WellsLink> links)
        {
            TreeNode result = new TreeNode(title) { Checked = true };
            result.Tag = links;
            foreach (WellsLink l in links)
                result.Nodes.Add(LinkNode(l));
            return result;
        }

        TreeNode LinkNode(WellsLink link)
        {
            TreeNode result = new TreeNode(link.Title) { Checked = link.Checked };
            result.Tag = link;
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
        



        private void treeView_wells_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is WellFace2D)
            {
                WellFace2D well = (WellFace2D)e.Node.Tag;
                well.Checked = e.Node.Checked;
            }
            else
            if (e.Node.Tag is WellsLink)
            {
                WellsLink link = (WellsLink)e.Node.Tag;
                link.Checked = e.Node.Checked;
            }
            else
            if (e.Node.Tag is Polygon2D)
            {
                Polygon2D poly = (Polygon2D)e.Node.Tag;
                poly.Checked = e.Node.Checked;
            }
            else
            if (e.Node.Tag is Triangle2D)
            {
                Triangle2D triangle = (Triangle2D)e.Node.Tag;
                triangle.Checked = e.Node.Checked;
            }
            else
            if (e.Node.Tag is Quad2D)
            {
                Quad2D quad = (Quad2D)e.Node.Tag;
                quad.Checked = e.Node.Checked;
            }
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
                UpdateProject(new FRProject());
                SetCurrentFile(string.Empty);
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






        ToolStripMenuItem newMenu, openMenu, saveMenu, saveAsMenu, exitMenu;
        ToolStripSeparator separatorUpFileMenu, separatorDownFileMenu;



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
            fileToolStripMenuItem.DropDownItems.Add(separatorUpFileMenu);
            for (int i = 0; i < maxRecentFiles; ++i)
                fileToolStripMenuItem.DropDownItems.Add(recentFilesFileMenu[i]);
            fileToolStripMenuItem.DropDownItems.Add(separatorDownFileMenu);
            fileToolStripMenuItem.DropDownItems.Add(exitMenu);
        }



    }
}
