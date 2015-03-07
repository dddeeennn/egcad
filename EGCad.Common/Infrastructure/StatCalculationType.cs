using System;
using System.ComponentModel.DataAnnotations;

namespace EGCad.Common.Infrastructure
{
	[Serializable]
	public enum StatCalculationType : byte
	{
		[Display(Name = "Евклидово расстояние")]
		Euclead = 0,
		[Display(Name = "Квадрат евклидова расстояния")]
		QuadEuclead = 2,
		[Display(Name = "Линейное расстояние")]
		Linear = 3,
		[Display(Name = "Манхэттенское расстояние")]
		Manhatten = 4,
		[Display(Name = "Расстояние Чебышева")]
		Chebishev = 5
	}
}
