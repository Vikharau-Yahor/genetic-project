using System;
using System.Windows;

namespace GeneticComiVouager.Core
{
    public class Individual
    {
        public Gene Gene { get; set; }

        public LifeStatus LifeStatus { get; private set; }
        public double SurvivalRate { get; set; }

        public Individual()
        {
            LifeStatus = LifeStatus.Childhood;
            Gene = new Gene();
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
          var inheritedGene = (firstParent.Gene.Quality > secondParent.Gene.Quality)
				? firstParent.Gene 
				: secondParent.Gene;

			Gene = new Gene(inheritedGene);
			LifeStatus = LifeStatus.Childhood;
		}
    }
}
