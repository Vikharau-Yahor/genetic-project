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
            LifeStatus = LifeStatus.Alive;
            XGene = new Gene();
            YGene = new Gene();
        }

        public Point GetPoint(double x, double y)
        {
            return new Point(XGene.Value, YGene.Value);
        }

        public void Kill()
        {
            LifeStatus = LifeStatus.Dead;
        }

        public void Reborn(Gene newXGene, Gene newYGene)
        {
            LifeStatus = LifeStatus.Alive;
            XGene = newXGene;
            YGene = newYGene;
        }
    }
}
