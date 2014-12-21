﻿using System.Collections.Generic;
using EGCad.Common.Infrastructure;
using EGCad.Models.InputData;

namespace EGCad.Controllers
{
	/// <summary>
	/// provide user state
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


		protected NormalizeType Normilize
		{
			get { return (NormalizeType?)Session["Normilize"] ?? NormalizeType.None; }
            set { Session["Normilize"] = value; }
		}

		protected StatCalculationType StatCalculation
		{
			get { return (StatCalculationType?)Session["StatCalculationType"] ?? StatCalculationType.None; }
			set { Session["StatCalculationType"] = value; }
		}

		protected void Save()
		{
			InputModel = new GeoData(Map, AdditionalPointCount, Parameters, Normilize, StatCalculation);
		}
	}
}