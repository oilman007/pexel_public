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
using System.Windows.Forms.VisualStyles;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Threading.Tasks;
using Pexel.Eclipse;
using ZedGraph;
using System.ComponentModel;

namespace Pexel
{

    public partial class UcTreeView : TreeView
    {
        [DisplayName("Checkbox Spacing"), CategoryAttribute("Appearance"),
         Description("Number of pixels between the checkboxes.")]
        public int Spacing { get; set; }

        [DisplayName("Text Padding"), CategoryAttribute("Appearance"),
         Description("Left padding of text.")]
        public int LeftPadding { get; set; }



        public UcTreeView()
        {
            //InitializeComponent();
            DrawMode = TreeViewDrawMode.OwnerDrawText;
            HideSelection = false;    // I like that better
            CheckBoxes = false;       // necessary!
            FullRowSelect = false;    // necessary!
            Spacing = 4;              // default checkbox spacing
            LeftPadding = 7;          // default text padding
        }

        public TreeNode2 AddNode(string label, bool check1, bool check2)
        {
            TreeNode2 node = new TreeNode2(label, check1, check2);
            this.Nodes.Add(node);
            return node;
        }
        private Size glyph = Size.Empty;

        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            TreeNode2 n = e.Node as TreeNode2;
            if (n == null) { e.DrawDefault = true; return; }

            CheckBoxState cbsTrue = CheckBoxState.CheckedNormal;
            CheckBoxState cbsFalse = CheckBoxState.UncheckedNormal;

            Rectangle rect = new Rectangle(e.Bounds.Location,
                                 new Size(ClientSize.Width, e.Bounds.Height));
            glyph = CheckBoxRenderer.GetGlyphSize(e.Graphics, cbsTrue);
            int offset = glyph.Width * 2 + Spacing * 2 + LeftPadding;

            if (n.IsSelected)
            {
                e.Graphics.FillRectangle(SystemBrushes.MenuHighlight, rect);
                e.Graphics.DrawString(n.Label, Font, Brushes.White,
                                      e.Bounds.X + offset, e.Bounds.Y);
            }
            else
            {
                CheckBoxRenderer.DrawParentBackground(e.Graphics, e.Bounds, this);
                e.Graphics.DrawString(n.Label, Font, Brushes.Black,
                                      e.Bounds.X + offset, e.Bounds.Y);
            }

            CheckBoxState bs1 = n.Check1 ? cbsTrue : cbsFalse;
            CheckBoxState bs2 = n.Check2 ? cbsTrue : cbsFalse;

            CheckBoxRenderer.DrawCheckBox(e.Graphics, cbx(e.Bounds, 0).Location, bs1);
            CheckBoxRenderer.DrawCheckBox(e.Graphics, cbx(e.Bounds, 1).Location, bs2);
        }

        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            //Console.WriteLine(e.Location + " bounds:" + e.Node.Bounds);

            TreeNode2 n = e.Node as TreeNode2;
            if (e == null) return;

            if (cbx(n.Bounds, 0).Contains(e.Location)) n.Check1 = !n.Check1;
            else if (cbx(n.Bounds, 1).Contains(e.Location)) n.Check2 = !n.Check2;
            else
            {
                if (SelectedNode == n && Control.ModifierKeys == Keys.Control)
                    SelectedNode = SelectedNode != null ? null : n;
                else SelectedNode = n;
            }

            //Console.WriteLine(" " + n.Check1 + " " + n.Check2);

            Invalidate();

        }


        Rectangle cbx(Rectangle bounds, int check)
        {
            return new Rectangle(bounds.Left + 2 + (glyph.Width + Spacing) * check,
                                 bounds.Y + 2, glyph.Width, glyph.Height);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // UcTreeView
            // 
            this.LineColor = System.Drawing.Color.Black;
            this.ResumeLayout(false);

        }
    }
}
