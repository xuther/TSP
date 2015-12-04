using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSP
{
    class Ant
    {
        private CityMatrixAnt matrix;

        private Random rand;

        public int startCity {
            get; set; }

        private int interationsWithoutMove;

        private int curCity;

        private readonly int THRESHOLD = 10;

        private readonly int alpha = 1;
        private readonly int beta = 5;

        private bool abort;

        //In the format of [CityNum,(0=source City, 1= Dest City)]
        private int[,] Path;

        //this is needed to prevent premature loops;
        private int[] eligibleCities;

        //Moves taken
        private int movesTaken;

        //If a path was found. 
        public Boolean pathFound {
            get; set; }

        //0 if the city is eligible to visit 1 if it's been visited.;
        private int problemSize;

        //running total of the path cost.
        private double runningPathCost;

        public Ant(CityMatrixAnt readOnlyMatrix, int startCity)
        {
            rand = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

            matrix = readOnlyMatrix;
            problemSize = readOnlyMatrix.getCityNumber();

            initAnt(startCity);
        }

        private void initAnt(int startCity)
        {
            interationsWithoutMove = 0;
            runningPathCost = 0;
            pathFound = false;

            movesTaken = 0;
            Path = new int[problemSize, 2];

            for (int i = 0; i < problemSize; i++)
            {
                Path[i, 0] = -1;
                Path[i, 1] = -1;
            }

            curCity = startCity;
            this.startCity = startCity;

            eligibleCities = new int[problemSize];
            eligibleCities[startCity] = 1;

        }


        public double Move(double runningSum)
        {
            double randValue = rand.NextDouble();
            double curBest = double.NegativeInfinity;
            int current = -1;
            int i;

            double[] cumProbs = new double[problemSize];
            double cumProb = 0.0;

            //we were getting stuck sometimes, because we never met the requirements, so implemneting the running sum as reccomended by Ant Colony Optimization (mitpress.mit.edu)
            this.interationsWithoutMove++;
            for (i = 0; i < problemSize; i++)
            {
                //If we're already there, or we've been there. 
                if (i == curCity || eligibleCities[i] == 1 || matrix.getCost(curCity, i) == double.PositiveInfinity)
                {
                    cumProbs[i] = cumProb;
                    continue;
                }

                double chance = calcChanceToMove(i);
                cumProb += chance;

                cumProbs[i] = cumProb;

                if (chance > curBest)
                {
                    curBest = calcChanceToMove(i);
                    current = i;
                }
            }

            

            if (rand.NextDouble() <= .5)
            {
                i = current;
                
            }
            else
            {
                double value = rand.NextDouble();

                for (i = 0; i < problemSize; i++)
                {
                    if (value < cumProbs[i])
                        break;
                }

            }
            if (i == -1 || curBest == double.NegativeInfinity)
            {
                abort = true;
                return -1;
            }


            interationsWithoutMove = 0;
            Path[curCity, 1] = i;
            Path[i, 0] = curCity;
            eligibleCities[i] = 1;

            runningPathCost += matrix.getCost(curCity, i);
            curCity = i;
            movesTaken++;
            runningSum = 0;
            return runningSum;

            
        }


        private double calcChanceToMove(int nextCity)
        {
            if (nextCity == curCity)
                return 0.0;

            double sum = 0;
            //sum the pheromoneByConstant for every path from this city to other cities not visited by ant (that is still eligible).
            for (int i = 0; i < problemSize; i++)
            {
                if (eligibleCities[i] == 1 || matrix.getCost(curCity, i) == double.PositiveInfinity)
                    continue;

                sum += matrix.getPheremoneByConstant(curCity,nextCity);

            }
            double toReturn = matrix.getPheremoneByConstant(curCity,nextCity)/sum;

            return toReturn;
        }


        public void run(int startCity)
        {
            initAnt(startCity);
            abort = false;
            double runningSum = 0;
            while (movesTaken < problemSize - 1 && abort == false)
            {
                Move(runningSum);
            }

            //The ant followed a bad path. No pheromone will be deposited. 
            if (matrix.getCost(curCity,startCity) == double.PositiveInfinity || movesTaken < problemSize-1)
            {
                pathFound = false;
                return;
            }

            pathFound = true;

            //Close the loop.
            Path[curCity, 1] = startCity;
            Path[startCity, 0] = curCity;
            movesTaken++;

            runningPathCost += matrix.getCost(curCity, startCity);

            return;
        }

        public int[,] getPath()
        { return this.Path; }

        public double getTourCost()
        { return runningPathCost; }


    }
}
