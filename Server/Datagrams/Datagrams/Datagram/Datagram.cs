using System;
using Datagrams.CustomTypes;

namespace Datagrams.Datagram
{
	[Serializable]
	public class TestTransformRequest
	{
		public string Message;
		public Vector3 Position;
		public Vector4 Rotation;
	}
}
