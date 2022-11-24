using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_MPT_Lab2_GeneticAlgorithm
{
    internal sealed class ChromosomeComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            if (!(x is Chromosome) || !(y is Chromosome))
                throw new ArgumentException("Not of type Chromosome");
            if (((Chromosome)x).ChromosomeFitness > ((Chromosome)y).ChromosomeFitness)
                return 1;
            else if (((Chromosome)x).ChromosomeFitness == ((Chromosome)y).ChromosomeFitness)
                return 0;
            else
                return -1;
        }
    }
}
