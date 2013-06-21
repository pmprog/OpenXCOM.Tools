using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Win32;

namespace DSShared.Windows
{
	/// <summary>
	/// Delegate for use in the saving and loading events raised in the RegistryInfo class
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public delegate void RegistrySaveLoadHandler(object sender, RegistrySaveLoadEventArgs e);

	/// <summary>
	/// a class to help facilitate the saving and loading of values into the registry in a central location
	/// </summary>
	public class RegistryInfo
	{
		private Dictionary<string,PropertyInfo> properties;
		private object obj;
		private string name;
		private bool saveOnClose=true;

		private static string regKey = "DSShared";

		/// <summary>
		/// Event fired when retrieving values from the registry. This happens after the values are read and set in the object
		/// </summary>
		public event RegistrySaveLoadHandler Loading;

		/// <summary>
		/// Event fired when saving values to the registry. This happens after the values are saved.
		/// </summary>
		public event RegistrySaveLoadHandler Saving;

		RegistryKey swKey = null;
		RegistryKey riKey = null;
		RegistryKey ppKey = null;

		/// <summary>
		/// Changes the global registry key everything will be saved under
		/// </summary>
		public static string RegKey
		{
			get{return regKey;}
			set{regKey=value;}
		}

		/// <summary>
		/// Constructor that uses the name parameter as the registry key to save values under
		/// </summary>
		/// <param name="obj">the object to save/load values into the registry</param>
		/// <param name="name">the name of the registry key to save/load</param>
		public RegistryInfo(object obj,string name)
		{
			this.obj = obj;
			this.name=name;

			properties = new Dictionary<string,PropertyInfo>();

			if (obj is System.Windows.Forms.Form)
			{
				System.Windows.Forms.Form f = (System.Windows.Forms.Form)obj;
				f.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
				f.Load+=new EventHandler(Load);
				f.Closing+=new System.ComponentModel.CancelEventHandler(this.Closing);
				this.AddProperty("Width","Height","Left","Top");
			}
		}

		/// <summary>
		/// Constructor that uses the ToString() value of the object as the name of the constructor parameter
		/// </summary>
		/// <param name="obj"></param>
		public RegistryInfo(object obj):this(obj,obj.GetType().ToString()){}

		/// <summary>
		/// Deletes the key located at HKEY_CURRENT_USER\Software\RegKey\
		/// RegKey is the public static parameter of this class
		/// </summary>
		/// <param name="saveOnClose">if false, a future call to Save() will have no effect</param>
		public void ClearKey(bool saveOnClose)
		{
			RegistryKey swKey = Registry.CurrentUser.CreateSubKey("Software");
			RegistryKey riKey = swKey.CreateSubKey(regKey);
			riKey.DeleteSubKey(name);
			this.saveOnClose=saveOnClose;
		}

		/// <summary>
		/// Calls ClearKey(false)
		/// </summary>
		public void ClearKey()
		{
			ClearKey(false);
		}

		/// <summary>
		/// loads the specified values from the registry. parameters match those needed for a Form.Load event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void Load(object sender, EventArgs e)
		{
			RegistryKey swKey = Registry.CurrentUser.CreateSubKey("Software");
			RegistryKey riKey = swKey.CreateSubKey(regKey);
			RegistryKey ppKey = riKey.CreateSubKey(name);

			foreach (string s in properties.Keys)
				properties[s].SetValue(obj, ppKey.GetValue(s, properties[s].GetValue(obj, null)), null);

			if(Loading!=null)
				Loading(this,new RegistrySaveLoadEventArgs(ppKey));

			ppKey.Close();
			riKey.Close();
			swKey.Close();
		}

		/// <summary>
		/// Opens the registry key and returns it for custom read/write
		/// </summary>
		/// <returns></returns>
		public RegistryKey OpenKey()
		{
			if(swKey==null)
			{
				swKey = Registry.CurrentUser.CreateSubKey("Software");
				riKey = swKey.CreateSubKey(regKey);
				ppKey = riKey.CreateSubKey(name);
			}
			return ppKey;
		}

		/// <summary>
		/// Closes the registry key previously opened by OpenKey()
		/// </summary>
		public void CloseKey()
		{
			if(ppKey!=null)
			{
				ppKey.Close();
				riKey.Close();
				swKey.Close();
			}
			ppKey=riKey=swKey=null;
		}

		/// <summary>
		/// Adds properties to be saved/loaded
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
			if (obj is System.Windows.Forms.Form)
				((System.Windows.Forms.Form)obj).WindowState = System.Windows.Forms.FormWindowState.Normal;

			if(saveOnClose)
			{
				RegistryKey swKey = Registry.CurrentUser.CreateSubKey("Software");
				RegistryKey riKey = swKey.CreateSubKey(regKey);
				RegistryKey ppKey = riKey.CreateSubKey(name);

				foreach(string s in properties.Keys)
					ppKey.SetValue(s, properties[s].GetValue(obj, null));

				if(Saving!=null)
					Saving(this,new RegistrySaveLoadEventArgs(ppKey));

				ppKey.Close();
				riKey.Close();
				swKey.Close();
			}
		}

		/// <summary>
		/// Method intended for use with Form.Closing events - directly calls Save
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Save(sender,e);
		}
	}

	/// <summary>
	/// EventArgs for saving and loading events
	/// </summary>
	public class RegistrySaveLoadEventArgs:EventArgs
	{
		private RegistryKey key;
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="openKey">Registry key that has been opened for reading and writing to</param>
		public RegistrySaveLoadEventArgs(RegistryKey openKey)
		{
			this.key = openKey;
		}

		/// <summary>
		/// The registry key that is now open for saving and loading to. Do not close the key when finished
		/// </summary>
		public RegistryKey OpenKey
		{
			get { return key; }
		}
	}
}
