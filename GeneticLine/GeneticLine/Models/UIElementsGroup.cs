using System;
using System.Collections.Generic;
using System.Windows;

namespace GeneticLine.Models
{
	public class UIElementsGroup
	{
		public UIElementsGroup(int id)
		{
			Id = id;
			UIElements = new List<UIElement>();
		}

		public int Id { get; set; }
		public IList<UIElement> UIElements {get;set;}
	}
}
