using System;
using System.Collections.Generic;
using System.Text;

namespace XCom.Interfaces.Base
{
	public interface IMap_Observer
	{
		//void SetMap(object sender, SetMapEventArgs e);
		IMap_Base Map { set;get;}
		void HeightChanged(IMap_Base sender, HeightChangedEventArgs e);
		void SelectedTileChanged(IMap_Base sender, SelectedTileChangedEventArgs e);
		DSShared.Windows.RegistryInfo RegistryInfo { get;set;}
		Dictionary<string, IMap_Observer> MoreObservers { get;}
	}

	/// <summary>
	/// EventArgs class that holds a IMap_Base for when a SetMap event fires
	/// </summary>
    public class SetMapEventArgs : EventArgs
    {
		private IMap_Base map;

		public SetMapEventArgs(IMap_Base map)
        {
            this.map = map;
        }

		public IMap_Base Map
        {
            get { return map; }
        }
    }


	/// <summary>
	/// EventArgs class that holds a MapLocation and MapTile for when a SelectedTileChanged event fires
	/// </summary>
	public class SelectedTileChangedEventArgs : EventArgs
	{
		private MapLocation newSelected;
		private IMapTile selectedTile;

		public SelectedTileChangedEventArgs(MapLocation newSelected, IMapTile selectedTile)
		{
			this.newSelected = newSelected;
			this.selectedTile = selectedTile;
		}

		public MapLocation MapLocation
		{
			get { return newSelected; }
		}

		public IMapTile SelectedTile
		{
			get { return selectedTile; }
		}
	}

	/// <summary>
	/// EventArgs class for when a HeightChanged event fires. 
	/// </summary>
	public class HeightChangedEventArgs : EventArgs
	{
		private int newHeight;
		private int oldHeight;

		public HeightChangedEventArgs(int oldHeight, int newHeight)
		{
			this.newHeight = newHeight;
			this.oldHeight = oldHeight;
		}

		public int NewHeight
		{
			get { return newHeight; }
		}

		public int OldHeight
		{
			get { return oldHeight; }
		}
	}
}
