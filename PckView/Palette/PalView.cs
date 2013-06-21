using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using XCom;

namespace PckView
{
	/// <summary>
	/// Summary description for PalView.
	/// </summary>
	public class PalView : System.Windows.Forms.Form
	{
		private PalPanel palPanel;
		private System.Windows.Forms.Label status;

		public event PaletteClickDelegate PaletteIndexChanged;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PalView()
		{
			InitializeComponent();
			OnResize(null);
		}

		private void palClick(int idx)
		{
			switch(palPanel.Mode)
			{
				case SelectMode.Single:
					status.Text = string.Format("Clicked index: {0} ({1:X})",idx,idx);
					break;
				case SelectMode.Bar:
					status.Text = "Clicked range: "+idx+" - "+(idx+PalPanel.NumAcross-1);
					break;
			}

			Color c = palPanel.Palette[idx];
			status.Text+=string.Format("          r:{0} g:{1} b:{2} a:{3}",c.R,c.G,c.B,c.A);
			if(PaletteIndexChanged!=null)
				PaletteIndexChanged(idx);
		}

		public Palette Palette
		{
			get{return palPanel.Palette;}
			set{palPanel.Palette=value;}
		}

/*		protected override void OnResize(EventArgs e)
		{
			if(palPanel!=null)
			{
				palPanel.Width = ClientSize.Width;
				palPanel.Height = ClientSize.Height-status.Height;

				status.Location = new Point(palPanel.Left,palPanel.Bottom);
				status.Width = ClientSize.Width;
			}
		}
		*/

		#region Windows Form Designer generated code

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.status = new System.Windows.Forms.Label();
			this.palPanel = new PckView.PalPanel();
			this.SuspendLayout();
			// 
			// status
			// 
			this.status.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.status.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.status.Location = new System.Drawing.Point(0, 237);
			this.status.Name = "status";
			this.status.Size = new System.Drawing.Size(292, 16);
			this.status.TabIndex = 0;
			// 
			// palPanel
			// 
			this.palPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.palPanel.Name = "palPanel";
			this.palPanel.Size = new System.Drawing.Size(292, 237);
			this.palPanel.TabIndex = 0;
			this.palPanel.PaletteIndexChanged += new PckView.PaletteClickDelegate(this.palClick);
			// 
			// PalView
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 253);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.palPanel,
																		  this.status});
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PalView";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "PalView";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
