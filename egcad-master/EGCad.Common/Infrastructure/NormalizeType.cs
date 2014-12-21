using System.ComponentModel.DataAnnotations;

namespace EGCad.Common.Infrastructure
{
	public enum NormalizeType:byte
	{
        [Display(Name = "не выбрано")]
		None = 0,
        [Display(Name = "1")]
		NormType1 = 1,
        [Display(Name = "2")]
		NormType2 = 2,
        [Display(Name = "3")]
		NormType3 = 3,
        [Display(Name = "4")]
		NormType4 = 4
	}
}
