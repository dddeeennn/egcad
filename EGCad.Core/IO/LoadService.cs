using System.IO;
using System.Xml.Serialization;
using EGCad.Common.Model.Data;

namespace EGCad.Core.IO
{
	public class LoadService
	{
		public static GeoData Load(Stream file)
		{
			var formatter = new XmlSerializer(typeof(GeoData));

			return (GeoData)formatter.Deserialize(file);

		}
	}
}
