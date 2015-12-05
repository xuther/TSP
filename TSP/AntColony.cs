using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSP
{
    class AntColony
    {


        /*
        /   This is the implementation of the ant colony implementation, using a simply 3 stage plan.
        /   The first phase of each epoch is each ant finding a path, based on a set of equations. 
        /   Once all ants have found a path - the pheramone is updated.
        /   After the pheramone is updated, the global evaporation occurs. 
        */

        private CityMatrixAnt matrix;

        private Random rand;

        private double Q = 100;

        private readonly int EPOCH_NUM = 200;
        private readonly int ANT_NUM = 5;

        private double[] averagePath;
        private double[] lowestPath;
        private double[] worstPath;

        private double bestCost;
        private int[,] bestPath;

        public AntColony(City[] cities)
        {
            matrix = new CityMatrixAnt(cities, Q);
            rand = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

            averagePath = new double[EPOCH_NUM];
            lowestPath = new double[EPOCH_NUM];
            worstPath = new double[EPOCH_NUM];

            bestCost = double.PositiveInfinity;


            Ant[] ants = new Ant[ANT_NUM];

            //let's start with 10 epochs. 
            for(int i = 0; i < ants.Length; i++)
            {
                ants[i] = new Ant(matrix, i%matrix.getCityNumber());
            }
            for (int i = 0; i < EPOCH_NUM; i++) {

                for (int j = 0; j < ANT_NUM; j++)
                {
                    ants[j].run(rand.Next(0,cities.Length-1));
                }
               
                int antIndex, averageAntIndex, worstAntIndex;

                Console.WriteLine("Iteration " + i + " average length: " + getAveragePathLength(ants));
                Console.WriteLine("Iteration " + i + " minimum Length: " + getMinLength(ants,out antIndex));

                
                worstPath[i] = getWorstLength(ants, out worstAntIndex);
                averagePath[i] = getAveragePathLength(ants);
                lowestPath[i] = ants[antIndex].getTourCost();

                if (lowestPath[i] < bestCost)
                {
                    bestCost = lowestPath[i];
                    bestPath = ants[antIndex].getPath();
                }



                depositPheromone(ants, worstPath[i]);
                matrix.Evaporate();
            }

            System.IO.File.WriteAllText(@"Average", buildString(averagePath));
            System.IO.File.WriteAllText(@"Best", buildString(lowestPath));
            System.IO.File.WriteAllText(@"Worst", buildString(worstPath));

            Console.WriteLine("Best cost: " + bestCost.ToString());

            Console.WriteLine("Done!");
        }

        private String buildString(double[] nums)
        {
            String toReturn = "";

            foreach (double d in nums)
            {
                toReturn += d.ToString() + "\n";
            }
            toReturn.TrimEnd(',');


            return toReturn;
        }

        private double getWorstLength(Ant[] ants, out int worstAntIndex)
        {
            double curWinner = double.NegativeInfinity;
            worstAntIndex = -1;

            for (int i = 0; i < ANT_NUM; i++)
            {
                if (ants[i].pathFound)
                {
                    if (ants[i].getTourCost() > curWinner)
                    {
                        curWinner = ants[i].getTourCost();
                        worstAntIndex = i;
                    }
                }
            }
            return curWinner;
        }

        private double getMinLength(Ant[] ants, out int antIndex)
        {
            double curWinner = double.PositiveInfinity;
            antIndex = -1;
            for (int i = 0; i < ANT_NUM; i++)
            {
                if (ants[i].pathFound)
                {
                    if (ants[i].getTourCost() < curWinner)
                    {
                        curWinner = ants[i].getTourCost();
                        antIndex = i;
                    }
                }
            }
            return curWinner;
        }

        private double getAveragePathLength(Ant[] ants)
        {
            double pathTotal = 0;
            int numFound = 0;

            foreach (Ant a in ants)
            {
                if (a.pathFound)
                {
                    pathTotal += a.getTourCost();
                    numFound++;
                }
            }
            return pathTotal / numFound;
        }


        public void depositPheromone(Ant[] ants, double worstPath)
        {
            int[,] curPath;
            //apply the pheromone to the best path so far.





            //add our pheromones from the ant's traveled paths. 
            foreach (Ant a in ants)
            {
                if (a.pathFound == false)
                    continue;

                curPath = a.getPath();

                double pheromoneChange = (worstPath - a.getTourCost()) / a.getTourCost();
                for (int i = 0; i < matrix.getCityNumber(); i++)
                {
                    matrix.setNewPheromone(i, curPath[i, 1], pheromoneChange);
                }
            }
        }



    }
}
