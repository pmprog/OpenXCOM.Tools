using System;

namespace XCom
{
	public struct StrEnum
	{
		private string display;
		private object enumeration;

		public StrEnum(string display, object enumeration)
		{
			this.display = display;
			this.enumeration = enumeration;
		}

		public override string ToString()
		{
			return display;
		}

		public object Enum
		{
			get { return enumeration; }
		}
	}
}
