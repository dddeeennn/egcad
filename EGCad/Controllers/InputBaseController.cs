using System.Collections.Generic;
using EGCad.Common.Infrastructure;
using EGCad.Common.Model.Data;

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

		/// <summary>
		/// params for gran composition
		/// </summary>
		//protected List<Parameter> Parameters
		//{
		//    get
		//    {
		//        return new List<Parameter>(new[]
		//        {
		//            new Parameter(0,">0.25 mm", "%"),
		//            new Parameter(1,"0.25-0.1mm","%"),
		//            new Parameter(2,"0.1-0.05mm","%"),
		//            new Parameter(3, "0.05-0.01mm","%"),
		//            new Parameter(4,"0.01-0.005mm","%"), 
		//            new Parameter(5,"<0.005mm","%")
		//        });
		//    }
		//    set { Session["Parameters"] = value; }
		//}

		//<summary>
		//params for tech scurf
		//</summary>
		protected List<Parameter> Parameters
		{
			get
			{
				return new List<Parameter>(new[]
				{
					new Parameter(0,"десятичный логарфим среднего размера зерна", ""),
					new Parameter(1,"плотность","г/см3"),
					new Parameter(2,"влажность","%"),
					new Parameter(3, "начальный коэффициент пористости",""),
					new Parameter(4,"угол внутреннего трения","град"),
					new Parameter(5,"сцепление","кг/см2"),
					new Parameter(6,"степень уплотнения","")
				});
			}
			set { Session["Parameters"] = value; }
		}

		//protected List<Parameter> Parameters
		//{
		//	get
		//	{
		//		return new List<Parameter>(new[]
		//		{
		//			new Parameter(0,"1", "1"),
		//			new Parameter(1,"2","2"),
		//			new Parameter(2,"3","3"),
		//			new Parameter(3, "4","4")
		//		});
		//	}
		//	set { Session["Parameters"] = value; }
		//}

		//protected List<ParameterTableEntry> Points
		//{
		//    get { return (List<ParameterTableEntry>)Session["Points"] ?? new List<ParameterTableEntry>(); }
		//    set { Session["Points"] = value; }
		//}

		//protected List<ParameterTableEntry> Points
		//{
		//	get
		//	{
		//		return new List<ParameterTableEntry>(new[]
		//		{
		//			new ParameterTableEntry(0,0,0,GetParameters(1,2,3,4)),
		//			new ParameterTableEntry(1,0,0,GetParameters(5,6,7,8)),
		//			new ParameterTableEntry(2,0,0,GetParameters(9,10,11,12))
		//		});
		//	}
		//	set { Session["Points"] = value; }
		//}

		protected List<ParameterTableEntry> Points
		{
			get
			{
				return new List<ParameterTableEntry>(new[]
				{
					new ParameterTableEntry(0,0,0,GetParameters(-1.165,2.08,20.7,0.51,16.4,0.16,1)),
					new ParameterTableEntry(1,1,1,GetParameters(-1.199,2.07,22.4,0.56,16.4,0.162,1)),
					new ParameterTableEntry(2,2,2,GetParameters(-1.235,2.08,24.89,0.6,15.8,0.158,1)),
					new ParameterTableEntry(3,3,3,GetParameters(-1.45,2.08,25.5,0.62,14.9,0.153,0.98)),
					new ParameterTableEntry(4,4,4,GetParameters(-1.616,2.04,26.24,0.65,14,0.155,0.94)),
					new ParameterTableEntry(5,5,5,GetParameters(-1.814,2.7,27.2,0.78,12,0.18,0.87)),
					new ParameterTableEntry(6,6,6,GetParameters(-1.936,3.02,28.9,0.83,11.5,0.18,0.86)),
					new ParameterTableEntry(7,7,7,GetParameters(-2.145,2.94,31.2,0.93,11,0.2,0.79)),
					new ParameterTableEntry(8,8,8,GetParameters(-2.154,2.93,33.4,1.16,10.5,0.21,0.78)),
					new ParameterTableEntry(9,9,9,GetParameters(-2.173,2.95,36.6,1.13,10.3,0.23,0.78)),
					new ParameterTableEntry(10,10,10,GetParameters(-2.124,2.93,32.1,1.39,10,0.2,0.75)),
					new ParameterTableEntry(11,11,11,GetParameters(-2.169,2.86,37.4,1.47,10.5,0.205,0.78)),
					new ParameterTableEntry(12,12,12,GetParameters(-2.192,2.91,36.7,1.41,10.5,0.205,0.77)),
					new ParameterTableEntry(13,13,13,GetParameters(-2.166,2.9,36.3,1.54,10,0.2,0.75)),
					new ParameterTableEntry(14,14,14,GetParameters(-2.167,2.88,37.4,1.58,10,0.205,0.76)),
					new ParameterTableEntry(15,15,15,GetParameters(-2.187,2.85,37.9,1.57,10,0.2,0.72)),
					new ParameterTableEntry(16,16,16,GetParameters(-2.253,2.8,38.6,1.56,10.1,0.2,0.71)),
					new ParameterTableEntry(17,17,17,GetParameters(-2.314,2.73,41.3,1.54,9.5,0.205,0.74)),
					new ParameterTableEntry(18,18,18,GetParameters(-2.352,2.5,44.6,1.44,9.3,0.21,0.76)),
					new ParameterTableEntry(19,19,19,GetParameters(-2.362,2.2,43.3,1.47,9.3,0.22,0.72)),
					new ParameterTableEntry(20,20,20,GetParameters(-2.381,2,43.7,1.39,9,0.2,0.72)),
					new ParameterTableEntry(21,21,21,GetParameters(-2.386,2.02,44.7,1.34,9.2,0.22,0.72)),
					new ParameterTableEntry(22,22,22,GetParameters(-2.396,2.2,43.6,1.35,9.1,0.21,0.71)),
					new ParameterTableEntry(23,23,23,GetParameters(-2.413,1.8,44.9,1.32,8.6,0.23,0.69)),
					new ParameterTableEntry(24,24,24,GetParameters(-2.416,1.9,44.5,1.32,8.9,0.22,0.64)),
					new ParameterTableEntry(25,25,25,GetParameters(-2.486,1.7,44.3,1.33,8.6,0.22,0.66)),
					new ParameterTableEntry(26,26,26,GetParameters(-2.548,1.61,47.2,1.35,7.1,0.2,0.62)),
					new ParameterTableEntry(27,27,27,GetParameters(-2.594,1.63,45.3,1.36,7.4,0.225,0.65)),
					new ParameterTableEntry(28,28,28,GetParameters(-2.667,1.6,48.8,1.35,7,0.225,0.61))
				});
			}
			set { Session["Points"] = value; }
		}

		// gran composite params
		//protected List<ParameterTableEntry> Points
		//{
		//    get
		//    {
		//        return new List<ParameterTableEntry>(new[]
		//        {
		//            new ParameterTableEntry(0,0,0,GetParameters(1.12,41.21,15.67,30.79,4.02,7.19)),
		//            new ParameterTableEntry(1,0,0,GetParameters(0.79,30.97,14.78,34.13,9.01,10.32)),
		//            new ParameterTableEntry(2,0,0,GetParameters(0.64,29.79,10.81,40.89,7.02,10.85)),
		//            new ParameterTableEntry(3,0,0,GetParameters(0.54,20.11,13.62,49.56,8.16,8.01)),
		//            new ParameterTableEntry(4,0,0,GetParameters(0.4,15.7,11.4,54.9,9.07,8.53)),
		//            new ParameterTableEntry(5,0,0,GetParameters(0.32,11.8,10.7,61.65,6.08,9.45)),
		//            new ParameterTableEntry(6,0,0,GetParameters(0.25,9.81,11.01,57.78,11.8,9.35)),
		//            new ParameterTableEntry(7,0,0,GetParameters(0.22,7.34,12.3,61.35,10.3,8.49)),
		//            new ParameterTableEntry(8,0,0,GetParameters(0.12,5.27,4.22,68.1,11.7,10.59)),
		//            new ParameterTableEntry(9,0,0,GetParameters(0.02,0.89,5.08,65.19,14.3,14.52)),
		//            new ParameterTableEntry(10,0,0,GetParameters(0,0.65,2.1,32.12,42.98,22.15)),
		//            new ParameterTableEntry(11,0,0,GetParameters(0,0.43,1.78,32.34,45.78,19.67)),
		//            new ParameterTableEntry(12,0,0,GetParameters(0,0.21,1.59,23.78,49.87,24.55)),
		//            new ParameterTableEntry(13,0,0,GetParameters(0,0.31,1.91,29.76,50.45,17.57)),
		//            new ParameterTableEntry(14,0,0,GetParameters(0,0.2,1.62,21.2,49.92,27.06)),
		//            new ParameterTableEntry(15,0,0,GetParameters(0,0.15,1.65,22.78,47.89,27.53)),
		//            new ParameterTableEntry(16,0,0,GetParameters(0,0.1,1.6,20.3,45.24,32.76)),
		//            new ParameterTableEntry(17,0,0,GetParameters(0,0.05,1.55,14.82,42.59,40.99)),
		//            new ParameterTableEntry(18,0,0,GetParameters(0,0.1,1.2,12.8,41.56,44.34)),
		//            new ParameterTableEntry(19,0,0,GetParameters(0,0.09,0.14,12.5,29.45,57.82)),
		//            new ParameterTableEntry(20,0,0,GetParameters(0,0.05,0.34,11,32.7,55.91)),
		//            new ParameterTableEntry(21,0,0,GetParameters(0,0.01,0.02,10.02,37.98,51.97)),
		//            new ParameterTableEntry(22,0,0,GetParameters(0,0,0,8.68,21.81,69.51))
		//        });
		//    }
		//    set { Session["Points"] = value; }
		//}

		protected int AdditionalPointCount
		{
			get { return (int?)Session["AdditionalPointCount"] ?? 0; }
			set { Session["AdditionalPointCount"] = value; }
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
			InputModel = new GeoData(Map, AdditionalPointCount, Points, Parameters, Normilize, StatCalculation);
		}

		//private static List<Parameter> GetParameters(params  double[] values)
		//{
		//	return new List<Parameter>(new[]
		//	{
		//			new Parameter(0,"1", "1",values[0]),
		//			new Parameter(1,"2","2",values[1]),
		//			new Parameter(2,"3","3",values[2]),
		//			new Parameter(3, "4","4",values[3])
		//	});
		//}

		private static List<Parameter> GetParameters(params  double[] values)
		{
			return new List<Parameter>(new[]
			{
					new Parameter(0,"десятичный логарфим среднего размера зерна", "",values[0]),
					new Parameter(1,"плотность","г/см3",values[1]),
					new Parameter(2,"влажность","%",values[2]),
					new Parameter(3, "начальный коэффициент пористости","",values[3]),
					new Parameter(4,"угол внутреннего трения","град",values[4]),
					new Parameter(5,"сцепление","кг/см2",values[5]),
					new Parameter(6,"степень уплотнения","",values[6])
			});
		}

		//private static List<Parameter> GetParameters(params  double[] values)
		//{
		//    return new List<Parameter>(new[]
		//    {

		//        new Parameter(0, ">0.25 mm", "%",values[0]),
		//        new Parameter(1, "0.25-0.1mm", "%",values[1]),
		//        new Parameter(2, "0.1-0.05mm", "%",values[2]),
		//        new Parameter(3, "0.05-0.01mm", "%",values[3]),
		//        new Parameter(4, "0.01-0.005mm", "%",values[4]),
		//        new Parameter(5, "<0.005mm", "%",values[5])
		//    });
		//}
	}
}