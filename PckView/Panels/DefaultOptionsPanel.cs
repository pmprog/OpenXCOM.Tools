using System;
using System.Windows.Forms;

namespace PckView
{
	public class DefaultOptionsPanel:Panel
	{
		private Label label;

		public DefaultOptionsPanel()
		{
			label = new Label();
			label.Text="There are no options for this format";
			label.Dock=DockStyle.Fill;
			label.TextAlign=System.Drawing.ContentAlignment.MiddleCenter;
			Controls.Add(label);
		}
	}
}
