using System.ComponentModel.DataAnnotations;

namespace EGCad.Common.Infrastructure
{
	public enum NormalizeType:byte
	{
        [Display(Name = "Усредненное Евклидово расстояние")]
		EuklideanAveraged = 0,
        [Display(Name = "Модульное расстояние")]
		Modular = 1,
        [Display(Name = "Модульное центрированное расстояние")]
		ModularCentered = 2,
        [Display(Name = "Приведенное по максимуму расстояние")]
		CastToMax = 3
	}
}
