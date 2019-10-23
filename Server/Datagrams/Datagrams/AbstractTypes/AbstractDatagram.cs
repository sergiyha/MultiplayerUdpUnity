using System;
using Datagrams.CustomTypes;

namespace Datagrams.AbstractTypes
{
	[Serializable]
	public abstract class AbstractDatagram
	{
		public abstract RequestIdentifiers GetDatagramId();
	}
}
