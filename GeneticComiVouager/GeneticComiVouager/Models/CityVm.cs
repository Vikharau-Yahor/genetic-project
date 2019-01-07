using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace GeneticComiVouager.Models
{
	public class CityVm
	{
		public static string CityNames = "ABCDEFGHIGKLMNOPQRSTUVWXYZ";

		public int Number { get; set; }
		public string Name => CityNames[Number].ToString();

		public Point Position { get; set; }

		public Ellipse Circle { get; set; }
		public TextBlock Text { get; set; }
	}
}
