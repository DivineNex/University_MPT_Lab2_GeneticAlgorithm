using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static University_MPT_Lab2_GeneticAlgorithm.Chromosome;

namespace University_MPT_Lab2_GeneticAlgorithm
{
    internal class GeneticAlgorithm
    {
        private static Random rand = new Random();
        public delegate double GAFunction(double[] values);
        private double _mutationRate;
        private double _crossoverRate;
        private int _chromosomeLength;
        private int _populationSize;
        private int _generationSize;
        private double _totalFitness;
        public bool Elitism;
        private ArrayList _currentGenerationList;
        private ArrayList _nextGenerationList;
        private ArrayList _fitnessList;
        static private GAFunction getFitness;
        public GAFunction FitnessFunction
        {
            get { return getFitness; }
            set { getFitness = value; }
        }

        //Constructor with user specified crossover rate,
        //mutation rate, population size, generation size
        //and chromosome length.
        public GeneticAlgorithm(double XoverRate, double mutRate, int popSize, int genSize, int ChromLength)
        {
            Elitism = false;
            _mutationRate = mutRate;
            _crossoverRate = XoverRate;
            _populationSize = popSize;
            _generationSize = genSize;
            _chromosomeLength = ChromLength;
        }

        // Method which starts the GA executing.
        public void Launch()
        {
            //Create the arrays to hold the fitness, 
            //current and next generation lists
            _fitnessList = new ArrayList();
            _currentGenerationList = new ArrayList(_generationSize);
            _nextGenerationList = new ArrayList(_generationSize);
            //and initilize the mutation rate.
            Chromosome.ChromosomeMutationRate = _mutationRate;

            //Create the initial chromosome population by repeatedly 
            //calling the user supplied fitness function
            for (int i = 0; i < _populationSize; i++)
            {
                Chromosome g = new Chromosome(_chromosomeLength, true);
                _currentGenerationList.Add(g);
            }

            //Rank the initial chromosome population
            RankPopulation();

            //Loop through the entire generation size creating
            //and evaluating generations of new chromosomes.
            for (int i = 0; i < _generationSize; i++)
            {
                CreateNextGeneration();
                RankPopulation();
            }
        }

        //After ranking all the chromosomes by fitness, use a
        //"roulette wheel" selection method that allocates a large
        //probability of being selected to those chromosomes with the
        //highest fitness. That is, preference in the selection process 
        //is biased towards those chromosomes exhibiting highest fitness.
        private int RouletteSelection()
        {
            double randomFitness = rand.NextDouble() * _totalFitness;
            int idx = -1;
            int mid;
            int first = 0;
            int last = _populationSize - 1;
            mid = (last - first) / 2;
            while (idx == -1 && first <= last)
            {
                if (randomFitness < (double)_fitnessList[mid])
                { last = mid; }
                else if (randomFitness > (double)_fitnessList[mid])
                { first = mid; }
                mid = (first + last) / 2;
                if ((last - first) == 1) idx = last;
            }
            return idx;
        }

        // Rank population and then sort it in order of fitness.
        private void RankPopulation()
        {
            _totalFitness = 0;
            for (int i = 0; i < _populationSize; i++)
            {
                Chromosome g = ((Chromosome)_currentGenerationList[i]);
                g.ChromosomeFitness = FitnessFunction(g.ChromosomeGenes);
                _totalFitness += g.ChromosomeFitness;
            }
            _currentGenerationList.Sort(new ChromosomeComparer());
            double fitness = 0.0;
            _fitnessList.Clear();
            for (int i = 0; i < _populationSize; i++)
            {
                fitness += ((Chromosome)_currentGenerationList[i]).ChromosomeFitness;
                _fitnessList.Add((double)fitness);
            }
        }

        //Create a new generation of chromosomes. There are many 
        //different ways to do this. The basic idea used here is
        //to first check to see if the elitist flag has been set.
        //If so, then copy the chromosomes from this generation 
        //to the next before looping through the entire chromosome
        //population spawning and mutating children. Finally, if the
        //elitism flag has been set, then copy the best chromosomes 
        //to the new population.
        private void CreateNextGeneration()
        {
            _nextGenerationList.Clear();
            Chromosome g = null;
            if (Elitism)
                g = (Chromosome)_currentGenerationList[_populationSize - 1];
            for (int i = 0; i < _populationSize; i += 2)
            {
                int pidx1 = RouletteSelection();
                int pidx2 = RouletteSelection();
                Chromosome parent1, parent2, child1, child2;
                parent1 = ((Chromosome)_currentGenerationList[pidx1]);
                parent2 = ((Chromosome)_currentGenerationList[pidx2]);

                if (rand.NextDouble() < _crossoverRate)
                { parent1.Crossover(ref parent2, out child1, out child2); }
                else
                {
                    child1 = parent1;
                    child2 = parent2;
                }
                child1.Mutate();
                child2.Mutate();
                _nextGenerationList.Add(child1);
                _nextGenerationList.Add(child2);
            }
            if (Elitism && g != null) _nextGenerationList[0] = g;
            _currentGenerationList.Clear();
            for (int i = 0; i < _populationSize; i++)
                _currentGenerationList.Add(_nextGenerationList[i]);
        }

        //Extract the best values based on fitness from the current generation.
        //Since the ranking process already sorted the latest current generation 
        //list, just pluck out the best values from the current generation list.
        public void GetBestValues(out double[] values, out double fitness)
        {
            Chromosome g = ((Chromosome)_currentGenerationList[_populationSize - 1]);
            values = new double[g.ChromosomeLength];
            g.ExtractChromosomeValues(ref values);
            fitness = (double)g.ChromosomeFitness;
        }
    }
}
