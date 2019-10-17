using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace tools
{
	public class BinarySerializer
	{
		public static byte[] Serialize(Object serializable)
		{
			byte[] buffer = null;
			using (MemoryStream str = new MemoryStream())
			{
				BinaryFormatter format = new BinaryFormatter();
				format.Serialize(str, serializable);
				buffer = str.ToArray();
			}
			return buffer;
		}

		public static T Deserialize<T>(byte[] buffer) where T : class
		{
			T outObj = null;
			using (MemoryStream str = new MemoryStream(buffer))
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				var deserialized = binaryFormatter.Deserialize(str);
				if (deserialized is T typedOut)
				{
					outObj = typedOut;
				}
			}

			return outObj;

		}
	}
}

