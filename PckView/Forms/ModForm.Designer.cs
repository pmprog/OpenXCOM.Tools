namespace PckView
{
	public partial class ModForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.modList = new System.Windows.Forms.ListView();
			this.colExt = new System.Windows.Forms.ColumnHeader();
			this.colAuth = new System.Windows.Forms.ColumnHeader();
			this.colDesc = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// modList
			// 
			this.modList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colExt,
            this.colAuth,
            this.colDesc});
			this.modList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.modList.FullRowSelect = true;
			this.modList.GridLines = true;
			this.modList.HideSelection = false;
			this.modList.Location = new System.Drawing.Point(0, 0);
			this.modList.Name = "modList";
			this.modList.Size = new System.Drawing.Size(520, 273);
			this.modList.TabIndex = 0;
			this.modList.UseCompatibleStateImageBehavior = false;
			this.modList.View = System.Windows.Forms.View.Details;
			// 
			// colExt
			// 
			this.colExt.Text = "Ext";
			this.colExt.Width = 59;
			// 
			// colAuth
			// 
			this.colAuth.Text = "Author";
			this.colAuth.Width = 100;
			// 
			// colDesc
			// 
			this.colDesc.Text = "Description";
			this.colDesc.Width = 346;
			// 
			// ModForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(520, 273);
			this.Controls.Add(this.modList);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "ModForm";
			this.ShowInTaskbar = false;
			this.Text = "Loaded Modules";
			this.ResumeLayout(false);

		}
		#endregion
		private System.Windows.Forms.ColumnHeader colAuth;
		private System.Windows.Forms.ColumnHeader colDesc;
		private System.Windows.Forms.ColumnHeader colExt;
		private System.Windows.Forms.ListView modList;
	}
}