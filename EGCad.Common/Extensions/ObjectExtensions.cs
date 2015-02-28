using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace EGCad.Common.Extensions
{
	public static class ObjectExtensions
	{
		public static T DeepCopy<T>(T objectToCopy)
        {
			using (var memoryStream = new MemoryStream())
			{
				var binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(memoryStream, objectToCopy);
				memoryStream.Seek(0, SeekOrigin.Begin);
				return (T)binaryFormatter.Deserialize(memoryStream);
			}
        }

		public static byte[] ReadAllBytes(this BinaryReader reader)
		{
			const int bufferSize = 4096;
			using (var ms = new MemoryStream())
			{
				byte[] buffer = new byte[bufferSize];
				int count;
				while ((count = reader.Read(buffer, 0, buffer.Length)) != 0)
					ms.Write(buffer, 0, count);
				return ms.ToArray();
			}

		}
	}
}
