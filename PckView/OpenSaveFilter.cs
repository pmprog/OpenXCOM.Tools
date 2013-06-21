using System;
using System.Collections.Generic;
using System.Text;
using DSShared;

namespace PckView
{
	public class OpenSaveFilter:IFilter<XCom.Interfaces.IXCImageFile>
	{
		private XCom.Interfaces.IXCImageFile.Filter filterBy;

		public OpenSaveFilter()
		{
			filterBy = XCom.Interfaces.IXCImageFile.Filter.Open;
		}

		public void SetFilter(XCom.Interfaces.IXCImageFile.Filter filter)
		{
			filterBy = filter;
		}

		public bool FilterObj(XCom.Interfaces.IXCImageFile obj)
		{
			//Console.WriteLine("Filter: {0} -> {1}", filterBy, obj.FileOptions[filterBy]);
			return obj.FileOptions[filterBy];
		}
	}
}
