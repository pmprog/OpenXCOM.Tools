using System;
using System.Windows.Forms;

namespace MapView.Forms.Error
{
    public partial class ErrorWindow : Form
    {
        private readonly Exception _exception;
        public ErrorWindow(Exception exception)
        {
            _exception = exception;
            InitializeComponent();
        }

        private void ErrorDetailsPanel_Click(object sender, EventArgs e)
        {
            ErrorDetailsPanel.Dock = DockStyle.Fill;
        }
        
        private void ErrorWindow_Load(object sender, EventArgs e)
        {
            ErrorDetailsPanel.Click += ErrorDetailsPanel_Click;
            DetailsLabel.Text = _exception.ToString();
        }
    }
}
