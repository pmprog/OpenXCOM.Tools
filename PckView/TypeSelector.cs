/*
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace PckView
{
	/// <summary>
	/// Summary description for TypeSelector.
	/// </summary>
	public class TypeSelector : System.Windows.Forms.Form
	{
		private System.ComponentModel.Container components = null;
		private Hashtable typeCheck;
		//private Hashtable info;
		private Hashtable typeSizes;
		private static int startX;
		private Type selected;

		private int w,by;

		public TypeSelector(Hashtable info)
		{
			InitializeComponent();

			//this.info=info;			
			typeCheck = new Hashtable();
			typeSizes=new Hashtable();

			startX = (Height-ClientSize.Height)/2;

			foreach(Type t in info.Keys)
				addCheck(t,(Size)info[t],info);

			Button b = new Button();
			b.Text="OK";
			b.Location = new Point((w-b.Width)/2,by);
			b.Click+=new EventHandler(okClick);
			Controls.Add(b);

			ClientSize = new Size(w,b.Bottom);
		}

		private void okClick(object sender, EventArgs e)
		{
			if(selected!=null)
				Close();
			else
				MessageBox.Show(this,"You must make a selection","Uh...",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
		}

		private void addCheck(Type t, Size s,Hashtable info)
		{
			CheckBox cb = new CheckBox();
			cb.Text = t.Name;

			MethodInfo mi = t.GetMethod("GetCollectionType");
			if(mi==null)
			{
				cb.Tag=t;
				typeSizes[t]=s;
			}
			else
			{
				cb.Tag=mi.Invoke(null,new object[]{});
				typeSizes[cb.Tag]=s;
				mi = ((Type)cb.Tag).GetMethod("DisplayName");
				if(mi!=null)
					cb.Text=(string)mi.Invoke(null,new object[]{});
			}

			typeCheck[cb]=cb.Tag;
			typeCheck[cb.Tag]=cb;
			Controls.Add(cb);
			cb.Location = new Point((Width-ClientSize.Width)/2,startX);
			startX+=cb.Height;
			cb.Click+=new EventHandler(checkClick);
			w = cb.Width;
			by = cb.Bottom;
		}

		private void checkClick(object sender, EventArgs e)
		{
			if(selected!=null)
				((CheckBox)typeCheck[selected]).Checked=false;

			selected = (Type)((CheckBox)sender).Tag;//(Type)typeCheck[sender];
			((CheckBox)typeCheck[selected]).Checked=true;
		}

		public Type SelectedType
		{
			get{return selected;}
		}

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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// TypeSelector
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.ControlBox = false;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "TypeSelector";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "TypeSelector";

		}
		#endregion
	}
}
*/