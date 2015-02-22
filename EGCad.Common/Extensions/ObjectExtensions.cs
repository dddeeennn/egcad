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
	}
}
