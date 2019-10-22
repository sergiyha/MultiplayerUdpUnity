using System;

namespace Datagrams.AbstractTypes
{
	[Serializable]
	public abstract class AbstractNumberedDatagram
	{
		public abstract string GetDatagramId();
	}
}
