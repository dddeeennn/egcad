using System.Collections.Generic;
using EGCad.Common.Infrastructure;
using EGCad.Core.Input;

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
                    new ParameterTableEntry(1,0,0,GetParameters(0.79,30.97,14.78,34.13,9.01,10.32)),
                    new ParameterTableEntry(2,0,0,GetParameters(0.64,29.79,10.81,40.89,7.02,10.85)),
                    new ParameterTableEntry(3,0,0,GetParameters(0.54,20.11,13.62,49.56,8.16,8.01)),
                    new ParameterTableEntry(4,0,0,GetParameters(0.4,15.7,11.4,54.9,9.07,8.53)),
                    new ParameterTableEntry(5,0,0,GetParameters(0.32,11.8,10.7,61.65,6.08,9.45)),
                    new ParameterTableEntry(6,0,0,GetParameters(0.25,9.81,11.01,57.78,11.8,9.35)),
                    new ParameterTableEntry(7,0,0,GetParameters(0.22,7.34,12.3,61.35,10.3,8.49)),
                    new ParameterTableEntry(8,0,0,GetParameters(0.12,5.27,4.22,68.1,11.7,10.59)),
                    new ParameterTableEntry(9,0,0,GetParameters(0.02,0.89,5.08,65.19,14.3,14.52)),
                    new ParameterTableEntry(10,0,0,GetParameters(0,0.65,2.1,32.12,42.98,22.15)),
                    new ParameterTableEntry(11,0,0,GetParameters(0,0.43,1.78,32.34,45.78,19.67)),
                    new ParameterTableEntry(12,0,0,GetParameters(0,0.21,1.59,23.78,49.87,24.55)),
                    new ParameterTableEntry(13,0,0,GetParameters(0,0.31,1.91,29.76,50.45,17.57)),
                    new ParameterTableEntry(14,0,0,GetParameters(0,0.2,1.62,21.2,49.92,27.06)),
                    new ParameterTableEntry(15,0,0,GetParameters(0,0.15,1.65,22.78,47.89,27.53)),
                    new ParameterTableEntry(16,0,0,GetParameters(0,0.1,1.6,20.3,45.24,32.76)),
                    new ParameterTableEntry(17,0,0,GetParameters(0,0.05,1.55,14.82,42.59,40.99)),
                    new ParameterTableEntry(18,0,0,GetParameters(0,0.1,1.2,12.8,41.56,44.34)),
                    new ParameterTableEntry(19,0,0,GetParameters(0,0.09,0.14,12.5,29.45,57.82)),
                    new ParameterTableEntry(20,0,0,GetParameters(0,0.05,0.34,11,32.7,55.91)),
                    new ParameterTableEntry(21,0,0,GetParameters(0,0.01,0.02,10.02,37.98,51.97)),
                    new ParameterTableEntry(22,0,0,GetParameters(0,0,0,8.68,21.81,69.51))
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