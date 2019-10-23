using System;
using Datagrams.AbstractTypes;
using Datagrams.CustomTypes;

namespace Datagrams.Datagram
{
	[Serializable]
	public class TestTransformRequest : AbstractDatagram
	{
		public string Message;
		public Vector3 Position;
		public Vector4 Rotation;

		public override RequestIdentifiers GetDatagramId()
		{
			throw new NotImplementedException();
		}
	}
}
