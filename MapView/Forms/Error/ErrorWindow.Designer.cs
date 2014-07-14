namespace MapView.Forms.Error
{
    partial class ErrorWindow
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ErrorDetailsPanel = new System.Windows.Forms.GroupBox();
            this.DetailsLabel = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.ErrorDetailsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(465, 118);
            this.label1.TabIndex = 0;
            this.label1.Text = "Opps something went wrong.\r\nA lot of bad things can happen in this project. \r\nBut" +
    " we will fix most of the errors.";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.ErrorDetailsPanel);
            this.panel1.Location = new System.Drawing.Point(12, 130);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(465, 172);
            this.panel1.TabIndex = 1;
            // 
            // ErrorDetailsPanel
            // 
            this.ErrorDetailsPanel.Controls.Add(this.DetailsLabel);
            this.ErrorDetailsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ErrorDetailsPanel.Location = new System.Drawing.Point(0, 0);
            this.ErrorDetailsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ErrorDetailsPanel.Name = "ErrorDetailsPanel";
            this.ErrorDetailsPanel.Size = new System.Drawing.Size(465, 20);
            this.ErrorDetailsPanel.TabIndex = 0;
            this.ErrorDetailsPanel.TabStop = false;
            this.ErrorDetailsPanel.Text = "Error Details";
            // 
            // DetailsLabel
            // 
            this.DetailsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DetailsLabel.Location = new System.Drawing.Point(3, 16);
            this.DetailsLabel.Name = "DetailsLabel";
            this.DetailsLabel.Size = new System.Drawing.Size(459, 1);
            this.DetailsLabel.TabIndex = 1;
            this.DetailsLabel.Text = "Error Details go here";
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.CloseButton.Location = new System.Drawing.Point(325, 308);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(152, 43);
            this.CloseButton.TabIndex = 2;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            // 
            // ErrorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 363);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "ErrorWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Opps something went wrong!!";
            this.Load += new System.EventHandler(this.ErrorWindow_Load);
            this.panel1.ResumeLayout(false);
            this.ErrorDetailsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox ErrorDetailsPanel;
        private System.Windows.Forms.Label DetailsLabel;
        private System.Windows.Forms.Button CloseButton;
    }
}