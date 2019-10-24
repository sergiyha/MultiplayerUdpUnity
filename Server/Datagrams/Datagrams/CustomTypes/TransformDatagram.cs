using System;
using Datagrams.AbstractTypes;
using Datagrams.CustomTypes;

namespace Datagrams.Datagram
{
	[Serializable]
	public struct TransformDatagram 
	{
		public string Message;
		public Vector3 Position;
		public Vector4 Rotation;
	}

	[Serializable]
	public struct UsersInfo
	{
		public int UserIdentifier;
		public TransformDatagram Transform;
	}

}
