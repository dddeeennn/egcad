using System;
using System.IO;
using System.Xml.Serialization;
using EGCad.Common.Model.Data;

namespace EGCad.Core.IO
{
	public class SaveService
	{
		public static void Save(GeoData model, string directory, out string path)
		{
			CheckDirectory(directory);

			var now = DateTime.Now;
			path = string.Format(Path.Combine(directory,
				   string.Format("egcad{0} {1}pm {2}.{3}.{4}.egc", now.Hour, now.Minute, now.Day, now.Month, now.Year)));

			var formatter = new XmlSerializer(typeof(GeoData));

			using (var fs = new StreamWriter(path))
			{
				formatter.Serialize(fs, model);
			}
		}

		private static void CheckDirectory(string path)
		{
			if (Directory.GetFiles(path).Length > 50) CleanDirectory(path);
		}

		private static void CleanDirectory(string path)
		{
			var files = Directory.GetFiles(path);

			foreach (var file in files)
			{
				var fileInfo = new FileInfo(file);

				if (fileInfo.LastAccessTime < DateTime.Now.AddDays(-2)) fileInfo.Delete();
			}
		}

	}
}
