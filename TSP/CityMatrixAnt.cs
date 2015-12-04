using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSP
{
    class CityMatrixAnt
    {

        // in the format[from],[to]
        private double[][] costMatrix;
        private double[][] pheremone;
        private double[][] newPheromone;
        private double[][] pheremoneByConstant;
        private int problemSize;

        private readonly double alpha = 1;
        private readonly double beta = 5;
        private readonly double EvapConst = 0.5;
        private double Q = 100.0;

        public CityMatrixAnt(City[] cities, double qConst)
        {
            //intial pheremone.
            double initPheremone = 1.0 / cities.Length;
            Q = qConst;

            problemSize = cities.Length;

            costMatrix = new double[problemSize][];
            pheremone = new double[problemSize][];
            newPheromone = new double[problemSize][];
            pheremoneByConstant = new double[problemSize][];

           
            for (int i = 0; i < cities.Length; i++)
            {
                costMatrix[i] = new double[problemSize];
                pheremone[i] = new double[problemSize];
                newPheromone[i] = new double[problemSize];
                pheremoneByConstant[i] = new double[problemSize];

                City cur = cities[i];
                for (int j = 0; j < cities.Length; j++)
                {
                    if (i == j) {
                        costMatrix[i][j] = double.PositiveInfinity;
                        pheremone[i][j] = double.NegativeInfinity;
                        pheremoneByConstant[i][j] = double.NegativeInfinity;
                        newPheromone[i][j] = 0;
                        continue;
                    }

                    costMatrix[i][j] = cur.costToGetTo(cities[j]);
                    pheremone[i][j] = initPheremone;

                    //Pij^Alpha * Nij^Beta where Nij = Q/dist(i,j)
                    pheremoneByConstant[i][j] = Math.Pow(pheremone[i][j], alpha) * Math.Pow((Q / costMatrix[i][j]), beta); ;
                }        

            }
        }

        public int getCityNumber() {
            return problemSize;
        }

        private void setPheromoneByConstant(int fromCity, int toCity)
        {
            double first = Math.Pow(pheremone[fromCity][toCity], alpha);
            double second = Math.Pow((1 / costMatrix[fromCity][toCity]), beta);
            double value = first * second;

            //Forget the fancy heruistics, see if this works. 
            //double value = (pheremone[fromCity][toCity] * 100.00) / costMatrix[fromCity][toCity];
            pheremoneByConstant[fromCity][toCity] = value;
        }


        public double getCost(int fromCity, int toCity)
        {
            return costMatrix[fromCity][toCity];
        }

        public double getPheromone(int fromCity, int toCity)
        {
            return costMatrix[fromCity][toCity];
        }

        public void setPheromone(int fromCity, int toCity, double newAmount)
        {
            newPheromone[fromCity][toCity] = newAmount;
            //Since we're only ever updating this after we evaporate, we'll only apply it post evaporation.
            //setPheromoneByConstant(fromCity, toCity);
        }

        public void changePheromone(int fromCity, int toCity, double changeAmount) {
            setPheromone(fromCity, toCity, changeAmount + getPheromone(fromCity, toCity));
        }


        public double getPheremoneByConstant(int fromCity, int toCity)
        {
            return pheremoneByConstant[fromCity][toCity];
        }

        public void Evaporate()
        {
            double toMultiply = 1 - EvapConst;

            for (int i = 0; i < problemSize; i++)
            {
                for (int j = 0; j < problemSize; j++)
                {
                    pheremone[i][j] = newPheromone[i][j] + (pheremone[i][j] * toMultiply);
                    setPheromoneByConstant(i, j);
                }

            }


        }


    }
}
