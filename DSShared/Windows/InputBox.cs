using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace DSShared.Windows
{
	/// <summary>
	/// Delegate used for pressing the Okay button on an InputBox
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="settings"></param>
	public delegate void InputOkClickDelegate(object sender, InputOkEventArgs e);
	/// <summary>
	/// A generic inputbox providing an area for a user-supplied panel, and okay+cancel buttons
	/// The default inputbox prompts the user to enter a string
	/// </summary>
	public partial class InputBox : System.Windows.Forms.Form
	{
		private Panel customContents = null;

		/// <summary>
		/// Raised when the Okay button is pressed
		/// </summary>
		public event InputOkClickDelegate OkClick;
		//private DialogResult res = DialogResult.Cancel;

		/// <summary>
		/// Default constructor, Calls this("Input value","Input Box","")
		/// </summary>
		public InputBox():this("Input value","Input Box",""){}

		/// <summary>
		/// Constructor, calls this(caption,"Input","")
		/// </summary>
		/// <param name="caption"></param>
		public InputBox(string caption):this(caption,"Input",""){}

		/// <summary>
		/// Constructor, calls this(caption, title,"")
		/// </summary>
		/// <param name="caption"></param>
		/// <param name="title"></param>
		public InputBox(string caption, string title):this(caption, title,""){}

		/// <summary>
		/// Constructor specifying all arguements
		/// </summary>
		/// <param name="caption">Text that will be shown above the text box</param>
		/// <param name="title">Title of the form</param>
		/// <param name="defaultValue">Initial value of the text box</param>
		public InputBox(string caption, string title,string defaultValue)
		{
			InitializeComponent();
			
			DialogResult=DialogResult.Cancel;

			lblCaption.Text=caption;
			this.Text=title;
			txtInput.Text=defaultValue;
			txtInput.Focus();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="title">Title of the form</param>
		/// <param name="contents">Panel containing UI elements for the user to select</param>
		public InputBox(string title, Panel contents)
		{
			InitializeComponent();
			DialogResult=DialogResult.Cancel;

			Text=title;
			CustomContents=contents;
		}

		/// <summary>
		/// get/set the contents of the panel
		/// </summary>
		public Panel CustomContents
		{
			get{return customContents;}
			set
			{
				customContents = value;
				ClientSize = new Size(customContents.Width,customContents.Height+panel1.Height);
				
				panelMid.Controls.Clear();
				panelMid.Controls.Add(customContents);
			}
		}

		/// <summary>
		/// get/set the text in the default text box
		/// </summary>
		public string InputValue
		{
			get{return txtInput.Text;}
			set { txtInput.Text = value; }
		}

		/// <summary>
		/// Gives focus to the text box and calls ShowDialog()
		/// </summary>
		/// <param name="parent"></param>
		/// <returns></returns>
		public new DialogResult Show(IWin32Window parent)
		{
			txtInput.Focus();
			return ShowDialog(parent);
		}

		private void buttonClick(object sender, EventArgs e)
		{
			DialogResult = ((Button)sender).DialogResult;
			if(OkClick!=null)
				OkClick(this,new InputOkEventArgs(customContents));
			Close();
		}

		private void txtInput_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
				btnOk.PerformClick();
		}

		/// <summary>
		/// Creates an InputBox(title,contents) and returns the value of calling ShowDialog() on it
		/// </summary>
		/// <param name="title"></param>
		/// <param name="contents"></param>
		/// <returns></returns>
		public static DialogResult ShowDialog(string title, Panel contents)
		{
			InputBox ib = new InputBox(title,contents);
			return ib.ShowDialog();
		}
	}

	/// <summary>
	/// Args class for the InputOkClickDelegate
	/// </summary>
	public class InputOkEventArgs:EventArgs
	{
		private Panel panel;

		/// <summary>
		/// Constructor 
		/// </summary>
		/// <param name="panel"></param>
		public InputOkEventArgs(Panel panel)
		{
			this.panel = panel;
		}

		/// <summary>
		/// Panel used for the display of an InputBox
		/// </summary>
		public Panel Panel
		{
			get { return panel; }
		}
	}
}
