using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace TSP
{
    class CostMatrix: Node
    {
        public double cost { get; private set; }
        private double[][] matrix{get;set;}
        public HashSet<int> visitedCities {get;private set;}
        public HashSet<int> fromCities { get; set; }
        private int problemSize;
        private int[] entered;

        //in format Tuple(origin City, Dest City)
        public SortedDictionary<int,int> path{get;private set;}
        public Tuple<CostMatrix, CostMatrix> childrenMatrix{get; private set;}
        public List<Tuple<int, int>> orderedPath{get;set;}

        public CostMatrix()
        {
            cost = -1;
            visitedCities = new HashSet<int>();
            path = new SortedDictionary<int, int>();
            childrenMatrix = null;
            fromCities = new HashSet<int>();
            orderedPath = new List<Tuple<int, int>>();
        }

        public void addCityToSet(int to, int from)
        {
            path.Add(from, to);
            orderedPath.Add(Tuple.Create(from, to));
            fromCities.Add(from);
            visitedCities.Add(to);
        }

        public bool hasVisitedCity(int toCheck)
        {
            return visitedCities.Contains(toCheck);
        }

        public double reduceMatrix()
        {
            if (matrix == null)
                return -1;
            
            double tempCost;
            double runningCost = 0;

            for (int i = 0; i < problemSize; i++)
            {
                if (fromCities.Contains(i))
                    continue;

                tempCost = 0;
                tempCost = matrix[i].Min();
                if (tempCost == 0 || tempCost == double.PositiveInfinity)
                {
                    runningCost += tempCost;
                    continue;
                }
                //perform the transforms on the matrix
                matrix[i] = matrix[i].Select(x => { return x - tempCost; }).ToArray();
                
                //update our cost;
                runningCost += tempCost;
            }

            //check our columns. I wish there were a prettier way to do this.... My justification is that we're not going to
            //be getting very big problem sets, so n^2 isn't THAT bad. Right? 
            for (int i = 0; i < problemSize; i++)
            {
                if (visitedCities.Contains(i))
                    continue;

                double curLowest = double.PositiveInfinity;
                //we have to go through each item in the row - we can't just run a min because it's a staggared array. we could maintain 2 arrays, but then we 
                //have to worry about keeping them in sync. 
                for (int j = 0; j < problemSize; j++)
                {
                    curLowest = Math.Min(matrix[j][i], curLowest);
                    if (curLowest == 0)
                        break;
                }

                //we need to reduce something. 
                if (curLowest != 0 && curLowest != double.PositiveInfinity)
                {
                    for(int j = 0; j < problemSize; j++)
                    {
                        matrix[j][i] = matrix[j][i] - curLowest;
                    }

                    //Update our cost;
                    runningCost += curLowest;
                }
            }

            cost += runningCost;
            if (path.Count >= (problemSize / 2))
                queuePriority = cost - (path.Count * 10);
            else
                queuePriority = cost;
            //queuePriority = cost;
            return runningCost;
        }

        public void initializeMatrix(City[] list)
        {
            matrix = new double[list.Length][];
            problemSize = list.Length;
            entered = new int[problemSize];

            for (int i = 0; i < list.Length; i++)
                {
                    entered[i] = -1;
                    matrix[i] = new double[problemSize];
                    for (int j = 0; j < list.Length; j++)
                    {
                        if (i == j)
                            matrix[i][j] = double.PositiveInfinity;
                        else
                        {
                            matrix[i][j] = list[i].costToGetTo(list[j]);
                        }
                    }
                }
        }

        //We're building our include path, based on the curent matrix and including the
        //provided x and y. This is a O(n^2) operation, as we have to copy all the data (n^2 cells in a table)
        //and we also have to examine each cell again for the reduction (we have to do this twice, but it's moot, 
        //since we're in big(O).
        public void includePath(int x, int y)
        {
            addCityToSet(x,y);
            //We can't leave the city any more.
            matrix[y] = matrix[y].Select(newCost => { return double.PositiveInfinity; }).ToArray();
            
            //se can't go back the way we came
            matrix[x][y] = double.PositiveInfinity;

            if (path.Count != problemSize)
                processPrematureCycles(x,y);

            for (int i = 0; i < problemSize; i++)
            {
                matrix[i][x] = double.PositiveInfinity;
            }
            reduceMatrix();
        }

        private void processPrematureCycles(int x, int y)
        {
            int end = y;
            int start = x; 
            int endTemp; 

            entered[x] = y;

            while (path.TryGetValue(end, out endTemp)) { end = endTemp; }
            while (entered[start] != -1)
                start = entered[start];

            if (path.Count < problemSize-1)
            {
                while (start != x)
                {
                    matrix[end][start] = double.PositiveInfinity;
                    matrix[y][start] = double.PositiveInfinity;
                    path.TryGetValue(start, out start);
                }
            }
        }

        //Build our exclude path - just like include path, but a little simpler, since we don't have to do all the hullaballo of
        //actually taking an edge. 
        public void excludePath(int x, int y)
        {
            matrix[y][x] = double.PositiveInfinity;
            reduceMatrix();
        }

        //duplicates the matrix and reduces it with the indicated cell included. 
        public CostMatrix buildInclude(int x, int y)
        {
            CostMatrix toReturn = duplicateMatrix();

            toReturn.includePath(x, y);

            return toReturn;
        }

        //duplicates the matrix and reduces it with the indicated cell excluded. 
        public CostMatrix buildExclude(int x, int y)
        {
            CostMatrix toReturn = duplicateMatrix();

            toReturn.excludePath(x, y);

            return toReturn;
        }

        //O(n^2)
        private CostMatrix duplicateMatrix()
        {
            CostMatrix toReturn = new CostMatrix();
            toReturn.matrix = new double[problemSize][];
            toReturn.problemSize = this.problemSize;
            toReturn.cost = this.cost;
            toReturn.visitedCities = duplicateSet(this.visitedCities);
            toReturn.fromCities = duplicateSet(this.fromCities);
            toReturn.path = new SortedDictionary<int, int>(this.path);
            toReturn.orderedPath = new List<Tuple<int, int>>(this.orderedPath);

            toReturn.entered = new int[problemSize];
            Array.Copy(this.entered, toReturn.entered, problemSize);

            copyMatrix(toReturn);

            return toReturn;
        }
        
        private HashSet<int> duplicateSet(HashSet<int> toCopy)
        {
            int[] temp = new int[toCopy.Count];
            toCopy.CopyTo(temp);
            return new HashSet<int>(temp);
        }

        private void copyMatrix(CostMatrix dest)
        {
            for (int i =0; i < problemSize ; i ++)
            {
                if (dest.matrix[i] == null)
                    dest.matrix[i] = new double[problemSize];

                Array.Copy(this.matrix[i], dest.matrix[i], this.matrix.Length); 
            }
        }

        //This is the entry point for most of our algorithm. Each step(iteration) is O(n^2). See explaination below. 
        public Tuple<CostMatrix, CostMatrix> findSplit()
        {
            double curBest = double.NegativeInfinity;
            double challenger;

            //go through each row and consider all the zeros. This is worst case O(n^4) - if everything was 0.
            // notice the double loop - tell tale sign. And inside we perform O(n^2) operation - copying each array and looking at all the 
            //elements. Etc. etc. so n^2 operations n^2 times is worst case O(n^4) - it won't usually be this bad, as we'll have lots of excluded edges
            //as we take paths, but worst case it's O(n^4).
            for (int i = 0; i < problemSize; i ++)
            {
                int location= 0;
                while((location = Array.IndexOf(matrix[i],0,location)) != -1)
                {
                    //Here we build our include and exclude. Again both are n^2 operations - as we're copying the data. 
                    //Not terribly efficient, but it allows us to track each individually. 
                    CostMatrix tempInc = buildInclude(location, i);
                    CostMatrix tempExc = buildExclude(location, i);

                    //We check to see if this branch is more attractve than the othres. If so, guard it. 
                    if (curBest < (challenger = tempExc.cost - tempInc.cost))
                    {
                        curBest = challenger;
                        childrenMatrix = Tuple.Create(tempInc, tempExc);
                    }
                    //IF we're at the end of the row, go to the next one. 
                    if (location >= problemSize - 1)
                        break;
                    location++;
                }
            }
            //return the best solution. (branching) 
            return childrenMatrix;
        }

    }
}
