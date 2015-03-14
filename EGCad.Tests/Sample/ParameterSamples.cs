using System.Collections.Generic;
using System.Linq;
using EGCad.Common.Model.Data;

namespace EGCad.Tests.Sample
{
	static class ParameterSamples
	{
		public static List<Parameter> DefaultFilledParameters
		{
			get
			{
				return new List<Parameter>(new[]{
													new Parameter(0, ">0.25 mm", "%",6),
													new Parameter(1, "0.25-0.1mm", "%",7),
													new Parameter(2, "0.1-0.05mm", "%",90),
													new Parameter(3, "0.05-0.01mm", "%",1),
													new Parameter(4, "0.01-0.005mm", "%",2),
													new Parameter(5, "<0.005mm", "%",18)
												});
			}
		}


		//<summary>
		//params for tech scurf (domain-specific params)
		//</summary>
		public static List<Parameter> DefaultTechScurfParameters
		{
			get
			{
				return new List<Parameter>(new[]{
													new Parameter(0,"десятичный логарфим среднего размера зерна", ""),
													new Parameter(1,"плотность","г/см3"),
													new Parameter(2,"влажность","%"),
													new Parameter(3, "начальный коэффициент пористости",""),
													new Parameter(4,"угол внутреннего трения","град"),
													new Parameter(5,"сцепление","кг/см2"),
													new Parameter(6,"степень уплотнения","")
												});
			}
		}

		/// <summary>
		/// params for gran composition (domain-specific parameters)
		/// </summary>
		public static List<Parameter> DefaultGranCompositionParameters
		{
			get
			{
				return new List<Parameter>(new[]{
													new Parameter(0,">0.25 mm", "%"),
													new Parameter(1,"0.25-0.1mm","%"),
													new Parameter(2,"0.1-0.05mm","%"),
													new Parameter(3, "0.05-0.01mm","%"),
													new Parameter(4,"0.01-0.005mm","%"), 
													new Parameter(5,"<0.005mm","%")
												});
			}
		}
	}
}
