using System;
using System.Collections.Generic;
using System.Text;

namespace DSShared
{
	public interface IFilter<T>
	{
		bool FilterObj(T o);
	}
}
