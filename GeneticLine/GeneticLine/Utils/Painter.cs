using GeneticLine.Core;
using GeneticLine.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GeneticLine.Utils
{
	public static class Painter
	{
		private static IList<UIElementsGroup> GroupedElements = new List<UIElementsGroup>();
		private static Canvas _workingCanvas;

		public static void Init(Canvas workingCanvas)
		{
			_workingCanvas = workingCanvas;
		}

		public static void Render(Population population)
		{	
			var deadIndividualsPoints = population.Individuals.Where(x => x.LifeStatus == LifeStatus.Dead).Select(x => x.ToLine(Colors.Red));
			var adultCommonIndividualsPoints = population.Individuals.Where(x => x.LifeStatus == LifeStatus.Dead && x != population.BestIndividual).Select(x => x.ToLine(Colors.Black));
			var bestIndividualPoint = population.BestIndividual.ToLine(Colors.Green);

			var newLinesList = deadIndividualsPoints.Cast<UIElement>().ToList();
			newLinesList.AddRange(adultCommonIndividualsPoints);
			newLinesList.Add(bestIndividualPoint);

			var elementsGroup = GroupedElements.FirstOrDefault(x => x.Id == population.Id);
			if(elementsGroup == null)
			{
				elementsGroup = new UIElementsGroup(population.Id);
				GroupedElements.Add(elementsGroup);
			}

			foreach(var oldElement in elementsGroup.UIElements)
			{
				_workingCanvas.Children.Remove(oldElement);
			}

			elementsGroup.UIElements = newLinesList;

			foreach (var uiElement in newLinesList)
			{
				_workingCanvas.Children.Add(uiElement);
			}

		}
	}
}
