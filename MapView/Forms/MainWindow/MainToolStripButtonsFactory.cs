using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace MapView.Forms.MainWindow
{
    public class MainToolStripButtonsFactory
    {
        /// <summary>
        /// Adds buttons for Up,Down,Cut,Copy and Paste to a toolstrip as well as sets some properties for the toolstrip
        /// </summary>
        /// <param name="toolStrip"></param>
        public void MakeToolstrip(ToolStrip toolStrip)
        {
            ToolStripButton btnUp = new ToolStripButton();
            ToolStripButton btnDown = new ToolStripButton();
            ToolStripButton btnCut = new ToolStripButton();
            ToolStripButton btnCopy = new ToolStripButton();
            ToolStripButton btnPaste = new ToolStripButton();
            // 
            // toolStrip1
            // 
            //toolStrip.Dock = DockStyle.None;
            //toolStrip.GripMargin = new Padding(0);
            //toolStrip.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip.Items.AddRange(new ToolStripItem[] {
            btnUp,
            btnDown,
            btnCut,
            btnCopy,
            btnPaste});
            //toolStrip1.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
            toolStrip.Padding = new Padding(0);
            toolStrip.RenderMode = ToolStripRenderMode.System;
            toolStrip.TabIndex = 1;
            // 
            // btnUp
            // 
            btnUp.AutoSize = false;
            btnUp.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnUp.ImageScaling = ToolStripItemImageScaling.None;
            btnUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            btnUp.Name = "btnUp";
            btnUp.Size = new System.Drawing.Size(25, 25);
            btnUp.Text = "toolStripButton1";
            btnUp.ToolTipText = "Level Up";
            btnUp.Click += btnUp_Click;
            // 
            // btnDown
            // 
            btnDown.AutoSize = false;
            btnDown.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnDown.ImageScaling = ToolStripItemImageScaling.None;
            btnDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            btnDown.Name = "btnDown";
            btnDown.Size = new System.Drawing.Size(25, 25);
            btnDown.Text = "toolStripButton2";
            btnDown.ToolTipText = "Level Down";
            btnDown.Click += btnDown_Click;
            // 
            // btnCut
            // 
            btnCut.AutoSize = false;
            btnCut.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnCut.ImageScaling = ToolStripItemImageScaling.None;
            btnCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            btnCut.Name = "btnCut";
            btnCut.Size = new System.Drawing.Size(25, 25);
            btnCut.Text = "toolStripButton3";
            btnCut.ToolTipText = "Cut";
            btnCut.Click += new EventHandler(MapViewPanel.Instance.Cut_click);
            // 
            // btnCopy
            // 
            btnCopy.AutoSize = false;
            btnCopy.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnCopy.ImageScaling = ToolStripItemImageScaling.None;
            btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new System.Drawing.Size(25, 25);
            btnCopy.Text = "toolStripButton4";
            btnCopy.ToolTipText = "Copy";
            btnCopy.Click += new EventHandler(MapViewPanel.Instance.Copy_click);
            // 
            // btnPaste
            // 
            btnPaste.AutoSize = false;
            btnPaste.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnPaste.ImageScaling = ToolStripItemImageScaling.None;
            btnPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            btnPaste.Name = "btnPaste";
            btnPaste.Size = new System.Drawing.Size(25, 25);
            btnPaste.Text = "toolStripButton5";
            btnPaste.ToolTipText = "Paste";
            btnPaste.Click += new EventHandler(MapViewPanel.Instance.Paste_click);

            Assembly a = Assembly.GetExecutingAssembly();
            btnCut.Image = Bitmap.FromStream(a.GetManifestResourceStream("MapView._Embedded.cut.gif"));
            btnPaste.Image = Bitmap.FromStream(a.GetManifestResourceStream("MapView._Embedded.paste.gif"));
            btnCopy.Image = Bitmap.FromStream(a.GetManifestResourceStream("MapView._Embedded.copy.gif"));
            btnUp.Image = Bitmap.FromStream(a.GetManifestResourceStream("MapView._Embedded.up.gif"));
            btnDown.Image = Bitmap.FromStream(a.GetManifestResourceStream("MapView._Embedded.down.gif"));
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (MapViewPanel.Instance.View.Map != null)
            {
                MapViewPanel.Instance.View.Map.Down();
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (MapViewPanel.Instance.View.Map != null)
            {
                MapViewPanel.Instance.View.Map.Up();
            }
        }
    }
}
