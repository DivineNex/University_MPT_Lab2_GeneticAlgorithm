using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace University_MPT_Lab2_GeneticAlgorithm
{
    internal class Chromosome
    {
        public double[] ChromosomeGenes;
        public int ChromosomeLength;
        public double ChromosomeFitness;
        public static double ChromosomeMutationRate;
        private static Random rand = new Random();

        //Chromosome class constructor
        //Actual functionality is to set up an array 
        //called ChromosomeGenes and depending on the 
        //boolean flag createGenes, it may or may not
        //fill this array with random values from 0 to 1
        //up to some specified ChromosomeLength
        public Chromosome(int length, bool createGenes)
        {
            ChromosomeLength = length;
            ChromosomeGenes = new double[length];
            if (createGenes)
            {
                for (int i = 0; i < ChromosomeLength; i++)
                    ChromosomeGenes[i] = rand.NextDouble();
            }
        }

        //Creates two offspring children using a single crossover point.
        //The basic idea is to first pick a random position, create two 
        //children and then swap their genes starting from the randomly 
        //picked position point.
        public void Crossover(ref Chromosome Chromosome2, out Chromosome child1, out Chromosome child2)
        {
            int position = (int)(rand.NextDouble() * (double)ChromosomeLength);
            child1 = new Chromosome(ChromosomeLength, false);
            child2 = new Chromosome(ChromosomeLength, false);
            for (int i = 0; i < ChromosomeLength; i++)
            {
                if (i < position)
                {
                    child1.ChromosomeGenes[i] = ChromosomeGenes[i];
                    child2.ChromosomeGenes[i] = Chromosome2.ChromosomeGenes[i];
                }
                else
                {
                    child1.ChromosomeGenes[i] = Chromosome2.ChromosomeGenes[i];
                    child2.ChromosomeGenes[i] = ChromosomeGenes[i];
                }
            }
        }

        //Mutates the chromosome genes by randomly switching them around
        public void Mutate()
        {
            for (int position = 0; position < ChromosomeLength; position++)
            {
                if (rand.NextDouble() < ChromosomeMutationRate)
                    ChromosomeGenes[position] = (ChromosomeGenes[position] + rand.NextDouble()) / 2.0;
            }
        }

        //Extracts the chromosome values
        public void ExtractChromosomeValues(ref double[] values)
        {
            for (int i = 0; i < ChromosomeLength; i++)
                values[i] = ChromosomeGenes[i];
        }
    }
}
