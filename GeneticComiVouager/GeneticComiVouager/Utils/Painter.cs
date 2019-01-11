using GeneticComiVouager.Core;
using GeneticComiVouager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GeneticComiVouager.Utils
{
	public static class Painter
	{
		private static List<UIElement> _elements = new List<UIElement>();
		private static IList<CityVm> _cities;
		private static Canvas _workingCanvas;

		public static void Init(Canvas workingCanvas, IList<CityVm> citiesPath)
		{
			_workingCanvas = workingCanvas;
			_cities = citiesPath;
		}

		public static void ClearBuffer()
		{
			foreach (var element in _elements)
				_workingCanvas.Children.Remove(element);

			_elements.Clear();
		}

		public static void Render(int[] citiesPath, Color color, double opacity, bool isTheBestPath = false)
		{
			//foreach (var line in _lines)
			//	_workingCanvas.Children.Remove(line);
			SolidColorBrush strokeBrush = new SolidColorBrush(color);
			strokeBrush.Opacity = opacity / 100;
			var citiesPathWithStartAndEndPoints = new List<int> { 0 };
			citiesPathWithStartAndEndPoints.AddRange(citiesPath);
			citiesPathWithStartAndEndPoints.Add(0);

			for (int i = 0; i < citiesPathWithStartAndEndPoints.Count - 1; i++)
			{
				var cityNumberFrom = _cities.Single(x => x.Number == citiesPathWithStartAndEndPoints[i]);
				var cityNumberTo = _cities.Single(x => x.Number == citiesPathWithStartAndEndPoints[i + 1]);

				var line = new Line();
				line.X1 = cityNumberFrom.Position.X;
				line.Y1 = cityNumberFrom.Position.Y;
				line.X2 = cityNumberTo.Position.X;
				line.Y2 = cityNumberTo.Position.Y;
				line.Stroke = strokeBrush;
				line.StrokeThickness = 3;
				_workingCanvas.Children.Add(line);
				_elements.Add(line);

				if (isTheBestPath)
				{
					TextBlock textNumberBlock = new TextBlock();
					textNumberBlock.Text = $"({(i + 1)})";
					textNumberBlock.FontSize = 13;
					textNumberBlock.FontStyle = FontStyles.Oblique;
					textNumberBlock.FontWeight = FontWeights.Medium;
					textNumberBlock.Foreground = Brushes.Green;
					textNumberBlock.Margin = new Thickness(cityNumberFrom.Position.X - 8, cityNumberFrom.Position.Y - 23, 0, 0);
					Panel.SetZIndex(textNumberBlock, 3);
					_elements.Add(textNumberBlock);
					_workingCanvas.Children.Add(textNumberBlock);
				}
			}
		}

		public static void Render(Population population)
		{
			foreach (var element in _elements)
				_workingCanvas.Children.Remove(element);

			var bestPath = population.BestIndividual.Gene.Value;
			var commonPath = population.Individuals
				.FirstOrDefault(x => x != population.BestIndividual && x.LifeStatus == LifeStatus.AdultLife)
				.Gene.Value;
			var deadPath = population.Individuals
				.FirstOrDefault(x => x.LifeStatus == LifeStatus.Dead)
				.Gene.Value;

			Render(bestPath, Colors.Green, 100, true);
			Render(commonPath, Colors.Black, 10);
			Render(deadPath, Colors.Red, 10);		
		}
	}
}
