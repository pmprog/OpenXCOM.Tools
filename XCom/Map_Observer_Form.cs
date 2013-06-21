using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using XCom.Interfaces.Base;
using XCom;

namespace MapView
{
	public class Map_Observer_Form:Form,IMap_Observer
	{
		protected IMap_Base map;
		private DSShared.Windows.RegistryInfo registryInfo;
		private MenuItem menuItem;
		private Settings

		//public void SetMap(object sender, SetMapEventArgs e)
		//{
		//    IMap_Base newMap = e.Map;
		//    if (map != null)
		//    {
		//        map.HeightChanged -= new HeightChangedDelegate(HeightChanged);
		//        map.SelectedTileChanged -= new SelectedTileChangedDelegate(SelectedTileChanged);
		//    }

		//    map = newMap;
		//    if (map == null)
		//        return;

		//    map.HeightChanged += new HeightChangedDelegate(HeightChanged);
		//    map.SelectedTileChanged += new SelectedTileChangedDelegate(SelectedTileChanged);
		//    OnSetMap(map);
		//}

		public MenuItem MenuItem
		{
			get { return menuItem; }
			set { menuItem = value; }
		}

		public virtual DSShared.Windows.RegistryInfo RegistryInfo
		{
			get { return registryInfo; }
			set
			{
				registryInfo = value;
			}
		}

		public virtual IMap_Base Map
		{
			set { map = value; Refresh(); }
		}

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (e.Delta > 0)
                map.Up();
            else
                map.Down();
        }

		public virtual void HeightChanged(IMap_Base sender, HeightChangedEventArgs e) { Refresh(); }
		public virtual void SelectedTileChanged(IMap_Base sender, SelectedTileChangedEventArgs e) { Refresh(); }
    }
}
