using System.ComponentModel.DataAnnotations;

namespace EGCad.Common.Infrastructure
{
	public enum StatCalculationType : byte
	{
        [Display(Name = "не выбрано")]
		None = 0,
        [Display(Name = "1")]
		StatCalc1 = 1,
        [Display(Name = "2")]
		StatCalc2 = 2,
        [Display(Name = "3")]
		StatCalc3 = 3,
        [Display(Name = "4")]
		StatCalc4 = 4

	}
}
