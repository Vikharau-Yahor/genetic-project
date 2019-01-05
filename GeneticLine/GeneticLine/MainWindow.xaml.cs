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
		private Point currentPoint;
		private Evolution _evolution;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void CanvasInitial_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ButtonState == MouseButtonState.Pressed)
			{
				currentPoint = e.GetPosition(this);
			}
		}

		private void CanvasInitial_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				Line line = new Line();

				line.Stroke = new SolidColorBrush(Colors.Black);
				line.X1 = currentPoint.X;
				line.Y1 = currentPoint.Y - TopPanel.ActualHeight;
				line.X2 = e.GetPosition(this).X;
				line.Y2 = e.GetPosition(this).Y - TopPanel.ActualHeight;

				currentPoint = e.GetPosition(this);

				CanvasInitial.Children.Add(line);
			}
		}

		private void Generate_Click(object sender, RoutedEventArgs e)
		{
			//clearAndPaintRandom();
			var goals = buildGoals();
			//_evolution = new Evolution(goal);
		}


		private Point[] buildGoals()
		{
			var goals = new List<Point>();
			var pointsCount = Int32.Parse(TBPointsCount.Text) - 2;
			var lineElements = CanvasInitial.Children.Cast<Line>().ToArray();
			//add last and end line points
			goals.Add(lineElements[0].GetStartPoint());
			goals.Add(lineElements[lineElements.Length - 1].GetStartPoint());
			var pointsStep = (CanvasInitial.Children.Count - 2) / pointsCount;
			for (int lineIndex = 0, i = 0; i < pointsCount; i++)
			{
				lineIndex += pointsStep;
				goals.Add(lineElements[lineIndex].GetStartPoint());
			}
			renderPoints(goals.ToArray());
			return goals.ToArray();
		}

		private void clearAndPaintRandom()
		{
			CanvasGenerated.Children.Clear();
			var rootX = TopPanel.ActualHeight;
			var random = new Random();
			var pointsCount = Int32.Parse(TBPointsCount.Text);
			var points = new List<Point>();
			for (int i = 1; i <= pointsCount; i++)
			{
				var line = new Line();
				line.X1 = random.Next(200);
				line.Y1 = random.Next(200);
				line.X2 = line.X1 + 1;
				line.Y2 = line.Y1;
				line.Stroke = new SolidColorBrush(Colors.Black);
				CanvasGenerated.Children.Add(line);
			}
		}

		private void renderPoints(Point[] points)
		{
			CanvasGenerated.Children.Clear();
			foreach(var point in points)
			{
				var line = new Line();
				line.X1 = point.X;
				line.Y1 = point.Y;
				line.X2 = point.X + 1;
				line.Y2 = point.Y;
				line.Stroke = new SolidColorBrush(Colors.Black);
				CanvasGenerated.Children.Add(line);
			}
		}
	}
}
