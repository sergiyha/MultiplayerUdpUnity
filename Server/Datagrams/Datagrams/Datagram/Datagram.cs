using System;
using Datagrams.AbstractTypes;
using Datagrams.CustomTypes;

namespace Datagrams.Datagram
{
	[Serializable]
	public class TestTransformRequest : AbstractNumberedDatagram
	{
		public string Message;
		public Vector3 Position;
		public Vector4 Rotation;

		public override string GetDatagramId()
		{
			return "TestTransformRequest";
		}
	}
}
