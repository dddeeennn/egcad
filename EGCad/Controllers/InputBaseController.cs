using System.Collections.Generic;
using EGCad.Common.Infrastructure;
using EGCad.Core.InputData;

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

        //protected List<Parameter> Parameters
        //{
        //    get { return (List<Parameter>)Session["Parameters"] ?? new List<Parameter>(); }
        //    set { Session["Parameters"] = value; }
        //}

        protected List<Parameter> Parameters
        {
            get
            {
                return new List<Parameter>(new[]
                {
                    new Parameter(0,">0.25 mm", "%"),
                    new Parameter(1,"0.25-0.1mm","%"),
                    new Parameter(2,"0.1-0.05mm","%"),
                    new Parameter(3, "0.05-0.01mm","%"),
                    new Parameter(4,"0.01-0.005mm","%"), 
                    new Parameter(5,"<0.005mm","%")
                });
            }
            set { Session["Parameters"] = value; }
        }

        //protected List<ParameterTableEntry> Points
        //{
        //    get { return (List<ParameterTableEntry>)Session["Points"] ?? new List<ParameterTableEntry>(); }
        //    set { Session["Points"] = value; }
        //}

        protected List<ParameterTableEntry> Points
        {
            get
            {
                return new List<ParameterTableEntry>(new[]
                {
                    new ParameterTableEntry(0,0,0,GetParameters(1.12,41.21,15.67,30.79,4.02,7.19)),
                    new ParameterTableEntry(0,0,0,GetParameters(0.79,30.97,14.78,34.13,9.01,10.32)),
                    new ParameterTableEntry(0,0,0,GetParameters(0.64,29.79,15.67,30.79,4.02,7.19)),
                    new ParameterTableEntry(0,0,0,GetParameters(0.54,20.11,14.78,34.13,9.01,10.32)),
                    new ParameterTableEntry(0,0,0,GetParameters(0.4,15.7,15.67,30.79,4.02,7.19)),
                    new ParameterTableEntry(0,0,0,GetParameters(0.32,11.8,14.78,34.13,9.01,10.32)),
                    new ParameterTableEntry(0,0,0,GetParameters(0.25,9.81,15.67,30.79,4.02,7.19)),
                    new ParameterTableEntry(0,0,0,GetParameters(0.22,7.34,14.78,34.13,9.01,10.32)),
                    new ParameterTableEntry(0,0,0,GetParameters(0.12,5.27,15.67,30.79,4.02,7.19)),
                    new ParameterTableEntry(0,0,0,GetParameters(0.02,0.89,14.78,34.13,9.01,10.32)),
                    new ParameterTableEntry(0,0,0,GetParameters(0,0.65,15.67,30.79,4.02,7.19)),
                    new ParameterTableEntry(0,0,0,GetParameters(0,30.97,14.78,34.13,9.01,10.32)),
                    new ParameterTableEntry(0,0,0,GetParameters(0.12,41.21,15.67,30.79,4.02,7.19)),
                    new ParameterTableEntry(0,0,0,GetParameters(0,30.97,14.78,34.13,9.01,10.32)),
                    new ParameterTableEntry(0,0,0,GetParameters(0,41.21,15.67,30.79,4.02,7.19)),
                    new ParameterTableEntry(0,0,0,GetParameters(0,30.97,14.78,34.13,9.01,10.32))
                });
            }
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
            InputModel = new GeoData(Map, AdditionalPointCount, Points, Parameters, Normilize, StatCalculation);
        }

        private static List<Parameter> GetParameters(params  double[] values)
        {
            return new List<Parameter>(new[]
            {
                new Parameter(0, ">0.25 mm", "%",values[0]),
                new Parameter(1, "0.25-0.1mm", "%",values[1]),
                new Parameter(2, "0.1-0.05mm", "%",values[2]),
                new Parameter(3, "0.05-0.01mm", "%",values[3]),
                new Parameter(4, "0.01-0.005mm", "%",values[4]),
                new Parameter(5, "<0.005mm", "%",values[5])
            });
        }
    }
}