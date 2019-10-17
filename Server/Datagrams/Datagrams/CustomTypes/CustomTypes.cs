using System;

namespace Datagrams.CustomTypes
{
	[Serializable]
	public class Vector3
	{
		public float x;
		public float y;
		public float z;

		public Vector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
	}

	[Serializable]
	public class Vector4
	{
		public float x;
		public float y;
		public float z;
		public float w;


		public Vector4(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}
	}
}
