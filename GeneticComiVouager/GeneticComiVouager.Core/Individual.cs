using System;
using System.Windows;

namespace GeneticLine.Core
{
    public class Individual
    {
        public Gene XGene { get; set; }
        public Gene YGene { get; set; }

        public LifeStatus LifeStatus { get; private set; }
        public double SurvivalRate { get; set; }

        public Individual()
        {
            LifeStatus = LifeStatus.Childhood;
            XGene = new Gene();
            YGene = new Gene();
        }

        public Point GetPoint(double x, double y)
        {
            return new Point(XGene.Value, YGene.Value);
        }

		public void Rest()
		{
			// individual doesn't have to do any actions
		}

        public void Kill()
        {
            LifeStatus = LifeStatus.Dead;
        }

		public void Grow()
		{
			if (LifeStatus != LifeStatus.Childhood) throw new Exception($"Individual can't grow, because his lifeStatus is { LifeStatus }");

			LifeStatus = LifeStatus.AdultLife;
		}

		public void Reborn(Individual firstParent, Individual secondParent)
        {
          var inheritedXGene = (firstParent.XGene.Quality > secondParent.XGene.Quality)
				? firstParent.XGene 
				: secondParent.XGene;

			var inheritedYGene = (firstParent.YGene.Quality > secondParent.YGene.Quality)
				? firstParent.YGene
				: secondParent.YGene;

			XGene = new Gene(inheritedXGene);
			YGene = new Gene(inheritedYGene);
			LifeStatus = LifeStatus.Childhood;
		}
    }
}
