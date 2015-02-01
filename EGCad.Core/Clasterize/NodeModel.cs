using System.Collections.Generic;

namespace EGCad.Core.Clasterize
{
	public class NodeModel
	{
		public string Name { get; set; }
		public double Y { get; set; }
		public List<NodeModel> Children { get; set; }

		public NodeModel(string name, double y, List<NodeModel> children)
			: this()
		{
			Name = name;
			Y = y;
			Children = children;
		}

		public NodeModel()
		{
			Children = new List<NodeModel>();
		}
	}
}
