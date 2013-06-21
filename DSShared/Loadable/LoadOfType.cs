using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using DSShared.Interfaces;

namespace DSShared.Loadable
{
	/// <summary>
	/// This class will scan an assembly for a specific type and manage a singleton list of those objects.
	/// Originally designed for user-created save/load plugins.
	/// </summary>
	/// <typeparam name="T">Objects of this type are stored in this class</typeparam>
	public class LoadOfType<T> where T : IAssemblyLoadable,IOpenSave,new()
	{
		/// <summary>
		/// Delegate for use with the OnLoad event
		/// </summary>
		/// <param name="sender">The LoadOfType object that fired the event</param>
		/// <param name="e">Args for the event</param>
		public delegate void TypeLoadDelegate(object sender, LoadOfType<T>.TypeLoadArgs e);

		/// <summary>
		/// Event that gets called when a type has been loaded from an assembly and has returned true for registration
		/// </summary>
		public event TypeLoadDelegate OnLoad;

		//private Dictionary<int, T> filterDictionary;
		private List<T> allLoaded;
		//private string openFileFilter = "";

		/// <summary>
		/// Default constructor
		/// </summary>
		public LoadOfType()
		{
			//filterDictionary = new Dictionary<int, T>();
			allLoaded = new List<T>();
		}

		/// <summary>
		/// returns a list of objects that meet the filter requirements
		/// </summary>
		/// <param name="filterObj"></param>
		/// <returns></returns>
		//public List<T> FilterBy(IFilter<T> filterObj)
		//{
		//    List<T> filterList = new List<T>();

		//    foreach (T obj in filterList)
		//        if (filterObj.FilterObj(obj))
		//            filterList.Add(obj);

		//    return filterList;
		//}

		/// <summary>
		/// A List of all the types that have been found so far
		/// </summary>
		public List<T> AllLoaded
		{
			get { return allLoaded; }
		}

		/// <summary>
		/// A string to use for an OpenFileDialog. The string will only be created once and cached for later use
		/// If the file list changes, use CreateFilter() to build a new string
		/// </summary>
		//public string OpenFileFilter
		//{
		//    get 
		//    {
		//        if (openFileFilter == "")
		//            CreateFilter();

		//        return openFileFilter; 
		//    }
		//}

		/// <summary>
		/// Returns the object at a specific filter index
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		//public T GetFromFilter(int index)
		//{
		//    return filterDictionary[index];
		//}

		//public string CreateFilter()
		//{
		//    return "";
		//}

		/// <summary>
		/// Forces a recreation of the filter string
		/// </summary>
		/// <returns></returns>
		//public string CreateFilter()
		//{
		//    openFileFilter = "";
		//    bool two = true;
		//    int filterIdx = 1; //filter index starts at 1

		//    foreach (T fc in allLoaded)
		//    {
		//        if (fc.FilterIndex != -1)
		//        {
		//            if (!two)
		//                openFileFilter += "|";
		//            else
		//                two = false;

		//            openFileFilter += fc.FileFilter;
		//            fc.FilterIndex = filterIdx++;
		//            filterDictionary[fc.FilterIndex] = fc;
		//        }
		//    }

		//    return openFileFilter;
		//}

		public string CreateFilter(IFilter<T> filter, Dictionary<int, T> filterDictionary)
		{
			string fileFilter = "";
			bool two = true;
			int filterIdx = 1; //filter index starts at 1

			filterDictionary.Clear();

			foreach (T fc in allLoaded)
			{
				if (filter.FilterObj(fc))
				{
					if (!two)
						fileFilter += "|";
					else
						two = false;

					fileFilter += fc.FileFilter;
					filterDictionary.Add(filterIdx++, fc);
				}
			}

			return fileFilter;
		}

		//public string CreateFilter(IFilter<T> filter)
		//{
		//    string fileFilter = "";
		//    bool two = false;
		//    int filterIdx = 1; //filter index starts at 1

		//    List<T> filterList = allLoaded;

		//    if (filter != null)
		//    {
		//        filterList = new List<T>();
		//        foreach(T obj in allLoaded)
		//            if(filter.FilterObj(obj))
		//    }

		//    foreach (T fc in filterList)
		//    {
		//    }
		//    return fileFilter;
		//}

		/// <summary>
		/// Adds an object to this list and recreates the filter string
		/// </summary>
		/// <param name="fc"></param>
		public void Add(T fc)
		{
			//Console.WriteLine("Adding file: " + fc.ExplorerDescription);
			allLoaded.Add(fc);
			//CreateFilter();
		}
		
		/// <summary>
		/// Scans an assembly for matching types. When a type is found, it is created using the default constructor
		/// and stored in a list. Objects are only added to the internal list if they return true for registration
		/// </summary>
		/// <param name="a"></param>
		/// <returns>A list of objects of type T. The list contains all registered and unregistered objects</returns>
		public List<T> LoadFrom(Assembly a)
		{
			//Get creatable objects from the assembly
			List<T> objList = new List<T>();
			foreach (Type t in a.GetTypes())
			{
				if (t.IsClass && !t.IsAbstract && typeof(T).IsAssignableFrom(t))
				{
					//if a class has no default constructor, it will fail this
					//this is why the new() constraint is placed on the LoadOfType definition
					ConstructorInfo ci = t.GetConstructor(new Type[] { });
					if (ci == null)
					{
						Console.Error.WriteLine("Error loading type: {0} -> No default constructor specified", t);
						continue;
					}

					try
					{
						T fc = (T)ci.Invoke(new object[] { });
						objList.Add(fc);

						bool register = fc.RegisterFile();
						if (register)
						{
							allLoaded.Add(fc);
							if (OnLoad != null)
								OnLoad(this, new TypeLoadArgs(fc));
						}
					}
					catch(Exception ex)
					{
						Console.Error.WriteLine("Error loading type: {0} -> {1}:{2}", t, ex.Message,ex.InnerException.Message);
					}
				}
			}

			//CreateFilter();
			return objList;
		}

		/// <summary>
		/// Args class to pass on to a load event signifying that this object was successfully
		/// created from an assembly and registered properly
		/// </summary>
		public class TypeLoadArgs : EventArgs
		{
			private T justLoaded;
			/// <summary>
			/// Constructor
			/// </summary>
			/// <param name="obj">Object that has just been created and registered</param>
			public TypeLoadArgs(T obj) : base() { this.justLoaded = obj; }

			/// <summary>
			/// Object that has just been created and registered
			/// </summary>
			public T LoadedObj { get { return justLoaded; } }
		}
	}
}
