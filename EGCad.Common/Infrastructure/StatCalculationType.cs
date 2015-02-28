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
        [Display(Name = "3")]
		StatCalc3 = 3,
        [Display(Name = "4")]
		StatCalc4 = 4

	}
}
