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

        private readonly int EPOCH_NUM = 75;

        private double[] averagePath;
        private double[] lowestPath;
        private double bestCost;
        private int[,] bestPath;

        public AntColony(City[] cities)
        {
            matrix = new CityMatrixAnt(cities, Q);
            rand = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

            averagePath = new double[EPOCH_NUM];
            lowestPath = new double[EPOCH_NUM];

            bestCost = double.PositiveInfinity;


            Ant[] ants = new Ant[50];

            //let's start with 10 epochs. 
            for(int i = 0; i < ants.Length; i++)
            {
                ants[i] = new Ant(matrix, i%matrix.getCityNumber());
            }
            for (int i = 0; i < EPOCH_NUM; i++) {

                foreach (Ant worker in ants)
                {
                    worker.run(worker.startCity);
                }
                Console.WriteLine("Iteration " + i + " average length: " + getAveragePathLength(ants));
                Console.WriteLine("Iteration " + i + " minimum Length: " + getMinLength(ants));

                averagePath[i] = getAveragePathLength(ants);
                lowestPath[i] = getMinLength(ants);

                if (lowestPath[i] < bestCost)
                {
                    bestCost = lowestPath[i];
                    bestPath = ants[i].getPath();
                }



                depositPheromone(ants);
                matrix.Evaporate();
            }

            System.IO.File.WriteAllText(@"Average", buildString(averagePath));
            System.IO.File.WriteAllText(@"Best", buildString(lowestPath));

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


        private double getMinLength(Ant[] ants)
        {
            double curWinner = double.PositiveInfinity;

            foreach (Ant a in ants)
            {
                if (a.pathFound)
                {
                    if (a.getTourCost() < curWinner)
                        curWinner = a.getTourCost();
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


        public void depositPheromone(Ant[] ants)
        {
            int[,] curPath;

            //add our pheromones from the ant's traveled paths. 
            foreach (Ant a in ants)
            {
                if (a.pathFound == false)
                    continue;

                curPath = a.getPath();
                double pheromoneChange = Q / a.getTourCost();
                for (int i = 0; i < matrix.getCityNumber(); i++)
                {
                    matrix.changePheromone(i, curPath[i, 1], pheromoneChange);
                }
            }
        }



    }
}
