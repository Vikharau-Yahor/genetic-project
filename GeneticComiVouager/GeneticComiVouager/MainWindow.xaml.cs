using GeneticComiVouager.Models;
using GeneticLine.Core;
using GeneticLine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GeneticLine
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private const int Radius = 20;
		private Evolution _evolution;
		private IList<CityVm> _cities = new List<CityVm>();

		public MainWindow()
		{
			InitializeComponent();
		}

		private void CanvasInitial_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ButtonState == MouseButtonState.Pressed)
			{
				var cityWithHighNumber = _cities.OrderByDescending(x => x.Number).FirstOrDefault();
				var newNumber = (cityWithHighNumber?.Number ?? -1) + 1;
				var mousePos = e.GetPosition(this);
				mousePos.Y -= TopPanel.ActualHeight;

				var newCity = new CityVm
				{
					Number = newNumber,
					Position = new Point { X = mousePos.X - Radius, Y = mousePos.Y - Radius }
				};

				var circle = new Ellipse();
				circle.Stroke = Brushes.Black;
				circle.Width = Radius * 2;
				circle.Height = Radius * 2;
				circle.StrokeThickness = 2;
				circle.Margin = new Thickness(mousePos.X - Radius, mousePos.Y - Radius, 0, 0);
				CanvasInitial.Children.Add(circle);
				newCity.Circle = circle;

				TextBlock textBlock = new TextBlock();
				textBlock.Text = newCity.Name;
				textBlock.Foreground = Brushes.Black;
				textBlock.Margin = new Thickness(mousePos.X - 5, mousePos.Y - 8, 0, 0);
				CanvasInitial.Children.Add(textBlock);
				newCity.Text = textBlock;
				_cities.Add(newCity);
			}
		}

		private void Generate_Click(object sender, RoutedEventArgs e)
		{

		}

		private void BClear_Click(object sender, RoutedEventArgs e)
		{
			CanvasInitial.Children.Clear();
			_cities.Clear();
		}
	}
}
