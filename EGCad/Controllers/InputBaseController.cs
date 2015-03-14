using System.Collections.Generic;
using EGCad.Common.Infrastructure;
using EGCad.Common.Model.Data;

namespace EGCad.Controllers
{
	/// <summary>
	/// to provide user state
	/// </summary>
	public abstract class InputBaseController : ApiController
	{
		protected GeoData InputModel
		{
			get { return (GeoData)Session["InputModel"]; }
			set { Session["InputModel"] = value; }
		}

		protected Map Map
		{
			get { return (Map)Session["Map"]; }
			set { Session["Map"] = value; }
		}

		protected double XMin
		{
			get { return Map != null ? Map.StartT.X : 0; }
		}

		protected double XMax
		{
			get { return Map != null ? Map.EndT.X : 0; }
		}

		protected List<Parameter> Parameters
		{
			get { return (List<Parameter>)Session["Parameters"] ?? new List<Parameter>(); }
			set { Session["Parameters"] = value; }
		}

		protected List<ParameterTableEntry> Points
		{
			get { return (List<ParameterTableEntry>)Session["Points"] ?? new List<ParameterTableEntry>(); }
			set { Session["Points"] = value; }
		}

		protected int AdditionalPointCount
		{
			get { return (int?)Session["AdditionalPointCount"] ?? 0; }
			set { Session["AdditionalPointCount"] = value; }
		}

		protected int ClusterCount
		{
			get { return (int?)Session["ClusterCount"] ?? 0; }
			set { Session["ClusterCount"] = value; }
		}

		protected NormalizeType Normilize
		{
			get { return (NormalizeType?)Session["Normilize"] ?? NormalizeType.EuklideanAveraged; }
			set { Session["Normilize"] = value; }
		}

		protected StatCalculationType StatCalculation
		{
			get { return (StatCalculationType?)Session["StatCalculationType"] ?? StatCalculationType.Euclead; }
			set { Session["StatCalculationType"] = value; }
		}

		protected void Save()
		{
			InputModel = new GeoData(Map, AdditionalPointCount, Points, Parameters, Normilize, StatCalculation, ClusterCount);
		}

		protected void Load()
		{
			Map = InputModel.Map;
			AdditionalPointCount = InputModel.AdditionalPointCount;
			Points = InputModel.Points;
			Parameters = InputModel.Parameters;
			Normilize = InputModel.Normilize;
			StatCalculation = InputModel.StatCalculation;
			ClusterCount = InputModel.ClusterCount;
		}
	}
}