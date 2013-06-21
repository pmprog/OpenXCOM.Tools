using System;
using System.Collections;
using System.Reflection;
using Microsoft.Win32;
using System.ComponentModel;
using System.Windows.Forms;

namespace MapView.x
{
	/// <summary>
	/// a class to help facilitate the saving and loading of values into the registry
	/// </summary>
	public class RegistryInfo
	{
		private Hashtable properties;
		private object obj;
		private string name;

		private static readonly string regKey = "ViewSuite";

		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj">the object to save/load values into the registry</param>
		/// <param name="name">the name of the registry key to save/load</param>
		public RegistryInfo(object obj,string name)
		{
			this.obj = obj;
			this.name=name;

			properties = new Hashtable();

			if(obj is Form)
			{
				Form f = (Form)obj;
				f.Load+=new EventHandler(Load);
				f.Closing+=new CancelEventHandler(this.Closing);
				this.AddProperty("Width","Height","Left","Top");
			}
		}

		/// <summary>
		/// loads the specified values from the registry
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void Load(object sender, EventArgs e)
		{
			RegistryKey swKey = Registry.CurrentUser.CreateSubKey("Software");
			RegistryKey riKey = swKey.CreateSubKey(regKey);
			RegistryKey ppKey = riKey.CreateSubKey(name);

			foreach(string s in properties.Keys)
			{
				PropertyInfo pi = (PropertyInfo)properties[s];

				pi.SetValue(obj,ppKey.GetValue(s,pi.GetValue(obj,null)),null);				
			}

			ppKey.Close();
			riKey.Close();
			swKey.Close();
		}

		/// <summary>
		/// adds properties to be saved/loaded
		/// </summary>
		/// <param name="names">the names of the properties to be saved/loaded</param>
		public void AddProperty(params string[] names)
		{
			Type t = obj.GetType();
			foreach(string s in names)
				AddProperty(t.GetProperty(s));
		}

		/// <summary>
		/// adds a property to be saved/loaded
		/// </summary>
		/// <param name="property"></param>
		public void AddProperty(PropertyInfo property)
		{
			properties[property.Name] = property;
		}

		/// <summary>
		/// Saves the specified values into the registry
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void Save(object sender, EventArgs e)
		{
			RegistryKey swKey = Registry.CurrentUser.CreateSubKey("Software");
			RegistryKey riKey = swKey.CreateSubKey(regKey);
			RegistryKey ppKey = riKey.CreateSubKey(name);

			foreach(string s in properties.Keys)
			{
				PropertyInfo pi = (PropertyInfo)properties[s];
				ppKey.SetValue(s,pi.GetValue(obj,null));
			}

			ppKey.Close();
			riKey.Close();
			swKey.Close();
		}

		/// <summary>
		/// Method intended for use with Form.Closing events - directly calls Save
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void Closing(object sender, CancelEventArgs e)
		{
			Save(sender,e);
		}
	}
}
