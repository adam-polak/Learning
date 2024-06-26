using System.ComponentModel.DataAnnotations;

namespace WaterDrinkingLog.Models
{
	public class DrinkingWaterModel
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		[Range(0, Int32.MaxValue, ErrorMessage = "Value must be positive.")]
		public int Quantity { get; set; }
	}
}

