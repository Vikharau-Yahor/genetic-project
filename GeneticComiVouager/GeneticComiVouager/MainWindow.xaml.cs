using GeneticComiVouager.Core;
using GeneticComiVouager.Mappers;
using GeneticComiVouager.Models;
using GeneticComiVouager.Utils;
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
		private const int Radius = 24;
		private Evolution _evolution = null;
		private IList<CityVm> _cities = new List<CityVm>();

		public MainWindow()
		{
			InitializeComponent();
		}

		private void CanvasInitial_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ButtonState == MouseButtonState.Pressed)
			{
				Painter.Init(CanvasInitial, _cities);

				var cityWithHighNumber = _cities.OrderByDescending(x => x.Number).FirstOrDefault();
				var newNumber = (cityWithHighNumber?.Number ?? -1) + 1;
				var mousePos = e.GetPosition(this);
				mousePos.Y -= TopPanel.ActualHeight;

				var newCity = new CityVm
				{
					Number = newNumber,
					Position = new Point { X = mousePos.X, Y = mousePos.Y }
				};

				var circle = new Ellipse();
				circle.Stroke = Brushes.Black;
				circle.Width = Radius * 2;
				circle.Height = Radius * 2;
				circle.StrokeThickness = 2;
				circle.Fill = Brushes.White;
				circle.Margin = new Thickness(mousePos.X - Radius, mousePos.Y - Radius, 0, 0);
				CanvasInitial.Children.Add(circle);
				Panel.SetZIndex(circle, 1);
				newCity.Circle = circle;

				TextBlock textBlock = new TextBlock();
				textBlock.Text = newCity.Name;
				textBlock.Foreground = Brushes.Black;
				textBlock.Margin = new Thickness(mousePos.X - 5, mousePos.Y - 8, 0, 0);
				CanvasInitial.Children.Add(textBlock);
				Panel.SetZIndex(textBlock, 2);
				newCity.Text = textBlock;
				_cities.Add(newCity);
			}
		}

		private void Generate_Click(object sender, RoutedEventArgs e)
		{
			if(_evolution != null)
			{
				_evolution.Stop();
				Painter.ClearBuffer();
			}

			var mutations = _cities.Select(x => x.ToMutationData()).ToArray();
			if (mutations == null || mutations.Length < 3)
			{
				MessageBox.Show($"Required at least 3 cities to start calculation");
				return;
			}
			_evolution = new Evolution(mutations, Int32.Parse(TBPopulationSize.Text), Int32.Parse(TBEvolutionSpeed.Text));
			_evolution.Population.OnPopulationLifecycleEnd += Render;
			_evolution.OnMaxGenerationAchieved += showNotification;
			_evolution.RunEvolutionCycle();
		}

		private void BClear_Click(object sender, RoutedEventArgs e)
		{
			_cities.Clear();
			_evolution.Stop();
			Painter.ClearBuffer();
			CanvasInitial.Children.Clear();
		}

		private void Render(Population population)
		{
			Dispatcher.InvokeAsync(() =>
			{
				Painter.Render(population);
				var bestPathRange = population.BestIndividual.SurvivalRate * (-1);
				LBestPath.Content = $"Best range: {bestPathRange}";
				LBGenerationNumber.Content = $"Generation: {population.GenerationNumber}";
			});
		}

		private void showNotification(double bestRange)
		{
			MessageBox.Show($"The best range was found: {bestRange}");
		}
	}
}
