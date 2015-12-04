using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace TSP
{

    class ProblemAndSolver
    {

        private class TSPSolution
        {
            /// <summary>
            /// we use the representation [cityB,cityA,cityC] 
            /// to mean that cityB is the first city in the solution, cityA is the second, cityC is the third 
            /// and the edge from cityC to cityB is the final edge in the path.  
            /// You are, of course, free to use a different representation if it would be more convenient or efficient 
            /// for your node data structure and search algorithm. 
            /// </summary>
            public ArrayList
                Route;

            public TSPSolution(ArrayList iroute)
            {
                Route = new ArrayList(iroute);
            }


            /// <summary>
            /// Compute the cost of the current route.  
            /// Note: This does not check that the route is complete.
            /// It assumes that the route passes from the last city back to the first city. 
            /// </summary>
            /// <returns></returns>
            public double costOfRoute()
            {
                // go through each edge in the route and add up the cost. 
                int x;
                City here;
                double cost = 0D;

                for (x = 0; x < Route.Count - 1; x++)
                {
                    here = Route[x] as City;
                    cost += here.costToGetTo(Route[x + 1] as City);
                }

                // go from the last city to the first. 
                here = Route[Route.Count - 1] as City;
                cost += here.costToGetTo(Route[0] as City);
                return cost;
            }
        }

        #region Private members 

        /// <summary>
        /// Default number of cities (unused -- to set defaults, change the values in the GUI form)
        /// </summary>
        // (This is no longer used -- to set default values, edit the form directly.  Open Form1.cs,
        // click on the Problem Size text box, go to the Properties window (lower right corner), 
        // and change the "Text" value.)
        private const int DEFAULT_SIZE = 25;

        private const int CITY_ICON_SIZE = 5;

        // For normal and hard modes:
        // hard mode only
        private const double FRACTION_OF_PATHS_TO_REMOVE = 0.20;

        /// <summary>
        /// the cities in the current problem.
        /// </summary>
        private City[] Cities;
        /// <summary>
        /// a route through the current problem, useful as a temporary variable. 
        /// </summary>
        private ArrayList Route;
        /// <summary>
        /// best solution so far. 
        /// </summary>
        private TSPSolution bssf; 

        /// <summary>
        /// how to color various things. 
        /// </summary>
        private Brush cityBrushStartStyle;
        private Brush cityBrushStyle;
        private Pen routePenStyle;


        /// <summary>
        /// keep track of the seed value so that the same sequence of problems can be 
        /// regenerated next time the generator is run. 
        /// </summary>
        private int _seed;
        /// <summary>
        /// number of cities to include in a problem. 
        /// </summary>
        private int _size;

        /// <summary>
        /// Difficulty level
        /// </summary>
        private HardMode.Modes _mode;

        /// <summary>
        /// random number generator. 
        /// </summary>
        private Random rnd;
        #endregion

        #region Public members
        public int Size
        {
            get { return _size; }
        }

        public int Seed
        {
            get { return _seed; }
        }
        #endregion

        #region Constructors
        public ProblemAndSolver()
        {
            this._seed = 1; 
            rnd = new Random(1);
            this._size = DEFAULT_SIZE;

            this.resetData();
        }

        public ProblemAndSolver(int seed)
        {
            this._seed = seed;
            rnd = new Random(seed);
            this._size = DEFAULT_SIZE;

            this.resetData();
        }

        public ProblemAndSolver(int seed, int size)
        {
            this._seed = seed;
            this._size = size;
            rnd = new Random(seed); 
            this.resetData();
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Reset the problem instance.
        /// </summary>
        private void resetData()
        {

            Cities = new City[_size];
            Route = new ArrayList(_size);
            bssf = null;

            if (_mode == HardMode.Modes.Easy)
            {
                for (int i = 0; i < _size; i++)
                    Cities[i] = new City(rnd.NextDouble(), rnd.NextDouble());
            }
            else // Medium and hard
            {
                for (int i = 0; i < _size; i++)
                    Cities[i] = new City(rnd.NextDouble(), rnd.NextDouble(), rnd.NextDouble() * City.MAX_ELEVATION);
            }

            HardMode mm = new HardMode(this._mode, this.rnd, Cities);
            if (_mode == HardMode.Modes.Hard)
            {
                int edgesToRemove = (int)(_size * FRACTION_OF_PATHS_TO_REMOVE);
                mm.removePaths(edgesToRemove);
            }
            City.setModeManager(mm);

            cityBrushStyle = new SolidBrush(Color.Black);
            cityBrushStartStyle = new SolidBrush(Color.Red);
            routePenStyle = new Pen(Color.Blue,1);
            routePenStyle.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// make a new problem with the given size.
        /// </summary>
        /// <param name="size">number of cities</param>
        //public void GenerateProblem(int size) // unused
        //{
        //   this.GenerateProblem(size, Modes.Normal);
        //}

        /// <summary>
        /// make a new problem with the given size.
        /// </summary>
        /// <param name="size">number of cities</param>
        public void GenerateProblem(int size, HardMode.Modes mode)
        {
            this._size = size;
            this._mode = mode;
            resetData();
        }

        /// <summary>
        /// return a copy of the cities in this problem. 
        /// </summary>
        /// <returns>array of cities</returns>
        public City[] GetCities()
        {
            City[] retCities = new City[Cities.Length];
            Array.Copy(Cities, retCities, Cities.Length);
            return retCities;
        }

        /// <summary>
        /// draw the cities in the problem.  if the bssf member is defined, then
        /// draw that too. 
        /// </summary>
        /// <param name="g">where to draw the stuff</param>
        public void Draw(Graphics g)
        {
            float width  = g.VisibleClipBounds.Width-45F;
            float height = g.VisibleClipBounds.Height-45F;
            Font labelFont = new Font("Arial", 10);

            // Draw lines
            if (bssf != null)
            {
                // make a list of points. 
                Point[] ps = new Point[bssf.Route.Count];
                int index = 0;
                foreach (City c in bssf.Route)
                {
                    if (index < bssf.Route.Count -1)
                        g.DrawString(" " + index +"("+c.costToGetTo(bssf.Route[index+1]as City)+")", labelFont, cityBrushStartStyle, new PointF((float)c.X * width + 3F, (float)c.Y * height));
                    else 
                        g.DrawString(" " + index +"("+c.costToGetTo(bssf.Route[0]as City)+")", labelFont, cityBrushStartStyle, new PointF((float)c.X * width + 3F, (float)c.Y * height));
                    ps[index++] = new Point((int)(c.X * width) + CITY_ICON_SIZE / 2, (int)(c.Y * height) + CITY_ICON_SIZE / 2);
                }

                if (ps.Length > 0)
                {
                    g.DrawLines(routePenStyle, ps);
                    g.FillEllipse(cityBrushStartStyle, (float)Cities[0].X * width - 1, (float)Cities[0].Y * height - 1, CITY_ICON_SIZE + 2, CITY_ICON_SIZE + 2);
                }

                // draw the last line. 
                g.DrawLine(routePenStyle, ps[0], ps[ps.Length - 1]);
            }

            // Draw city dots
            for (int i = 0; i < Cities.Length; i++)
            {
                if (bssf == null)
                    g.DrawString(" " + i, labelFont, cityBrushStyle, new PointF((float)Cities[i].X * width + 3F, (float)Cities[i].Y * height));

                g.FillEllipse(cityBrushStyle, (float)Cities[i].X * width, (float)Cities[i].Y * height, CITY_ICON_SIZE, CITY_ICON_SIZE);
            }

        }

        /// <summary>
        ///  return the cost of the best solution so far. 
        /// </summary>
        /// <returns></returns>
        public double costOfBssf ()
        {
            if (bssf != null)
                return (bssf.costOfRoute());
            else
                return -1D; 
        }
        private ArrayList buildSolution(SortedDictionary<int,int> path)
        {
            ArrayList toReturn = new ArrayList();
            List<int> debug = new List<int>();
            int current = 0;
            int next;
            if (!path.TryGetValue(current, out next))
                return null;
            toReturn.Add(Cities[current]);
            debug.Add(current);
            current = next;
            if (!path.TryGetValue(current, out next))
                return null;

            while(current != 0)
            {
                toReturn.Add(Cities[current]);
                debug.Add(current);
                current = next;
                if (!path.TryGetValue(current, out next))
                    return null;
            }

            return toReturn;
        }

        private SortedDictionary<int, int> findUpperBound()
        {
            HashSet<int> visited = new HashSet<int>();

            SortedDictionary<int, int> toReturn = new SortedDictionary<int, int>();

            int current = 0;
            int currentIndex = -1;
            visited.Add(0);

            while(visited.Count < Cities.Length)
            {
                double curBest = double.PositiveInfinity;
                double temp;

                for (int i = 0; i < Cities.Length; i++ )
                {
                    if (i == current || visited.Contains(i))
                        continue;

                    if (curBest > (temp = Math.Min(curBest, Cities[current].costToGetTo(Cities[i]))))
                    {
                        curBest = temp;
                        currentIndex = i;
                    }
                }

                toReturn.Add(current, currentIndex);
                current = currentIndex;
                visited.Add(current);
            }
            toReturn.Add(current, 0);
            return toReturn;
        }

        public void StartAnt()
        {
            AntColony colony = new AntColony(Cities);

            return;
        }

        public void greedy() {
            double bestSoFar;
            bssf = new TSPSolution(buildSolution(findUpperBound()));
            bestSoFar = bssf.costOfRoute();
            // update the cost of the tour. 
            Program.MainForm.tbCostOfTour.Text = " " + bestSoFar;
            // do a refresh. 
            Program.MainForm.Invalidate();
        }



        /// <summary>
        ///  solve the problem.  This is the entry point for the solver when the run button is clicked
        /// right now it just picks a simple solution. 
        /// </summary>
        public void solveProblem()
        {
            Stopwatch sw = new Stopwatch();

            List<TSPSolution> solutions = new List<TSPSolution>();
            List<string> forDisplay = new List<string>();

            int x;
            double bestSoFar = double.PositiveInfinity;
            Route = new ArrayList();

            //find a basic upper bound this is a O(n^2) greedy nearest neighbor algorithm. (Actually it's O(n(n+1)/2) - 
            //since we're eliminating options - but O(n^2) is close enough, and still technically true.) See the function for more details. 
            bssf = new TSPSolution(buildSolution(findUpperBound()));
            bestSoFar = bssf.costOfRoute();


            forDisplay.Add("Greedy");

            // update the cost of the tour. 
            Program.MainForm.tbCostOfTour.Text = " " + bestSoFar;
            // do a refresh. 
            Program.MainForm.Invalidate();

            //Initialize our priority queue. This is the same priority queue with worst case o(log(n)) operations. The only
            //difference is that this is set to automatically resize, as we don't know the size of the problem in advance. 
            PriorityQueue queue = new PriorityQueue(Cities.Length*2);

            //Create our first cost matrix. Constant time 
            CostMatrix init = new CostMatrix();
            init.initializeMatrix(Cities);
            init.reduceMatrix();
            queue.enqueue(init);

            //Run the algorithm
            sw.Start();

            CostMatrix cur;

            //So we're going to run as long as we have elements, or we're below our 30 second time limit. 
            //This piece is O(n^n) worst case. In reality it'll be significantly less than that - since we can prune
            //branches as we find better solutions than their best cases, but worst case we're looking at O(n^n) - if 
            //if we couldn't do any purging and our soltion was the last one tried. 
            //The O(n^n) arises from the fact that we're basically branching the tree each time we iterate through - doubling 
            //The number of problems at each level.
            //
            //Space is about as bad. We're storying n^2 information in each node, in the form of the reduced cost matrix
            //and we're storing up to n^n nodes (in a worst case senario) so we're at O(n^n+2). I'm not sure if we can get 
            //rid of the +2 in this case, but I kind of think we could. 
            while (queue.hasElements())// && sw.Elapsed < TimeSpan.FromSeconds(30))
            {
                
                //pop the top element from the array - constant operation.
                cur = (CostMatrix)queue.pop();

                //unsolvable - time to return.
                if (cur.cost == double.PositiveInfinity)
                    return;

                //this is where the bulk of the algorithm logic is - it O(n^4). 
                Tuple<CostMatrix, CostMatrix> split = cur.findSplit();
                
                //We've found A solution We need to update of bssf and prune!
                if (split.Item1.visitedCities.Count >= Cities.Length)
                {
                    //Pruning is complicated. Since we can have as many as n^n elements in our queue at any given time, we 
                    //run into at least an exponential operation here. I've trimmed it down to O(log(n^n)) - or O(nlog(n)) 
                    //since the queue is sorted, but it's still expensive. 
                    queue.prune(split.Item1.cost);

                    //All the rest is constant time. 
                    bssf = new TSPSolution(buildSolution(split.Item1.path));
                    solutions.Add(bssf);
                    forDisplay.Add(" " + solutions.Count);
                    bestSoFar = bssf.costOfRoute();
                    // update the cost of the tour. 
                    Program.MainForm.tbCostOfTour.Text = " " + bssf.costOfRoute();
                    // do a refresh. 
                    Program.MainForm.Invalidate();
                }
                // we don't find a solution, so branch the tree and go again. This is the piece that makes the algorithm
                // O(n^n) time and space - since each time we iterate we double the number of sub-problems, and we're storing 
                //the reduced cost matrix each time, which is n^2 elements. 
                else 
                {
                    if (split.Item1.cost < bestSoFar)
                        queue.enqueue(split.Item1);
                    if (split.Item2.cost < bestSoFar)
                        queue.enqueue(split.Item2);
                }

            }
            sw.Stop();

            if (queue.hasElements())
                Program.MainForm.tbOptimal.Text = "No";
            else
                Program.MainForm.tbOptimal.Text = "Yes!";
            Program.MainForm.tbBSSFUpdates.Text = " " + solutions.Count;
            Program.MainForm.tbStatesCreated.Text = " " + queue.everNodes;
            Program.MainForm.tbPrunedStates.Text = " " + queue.prunedStates;
            Program.MainForm.tbStoredStates.Text = " " + queue.storedStates;
            Program.MainForm.Solutions.Text = " " + solutions.Count;
            Program.MainForm.tbTimeToSolve.Text = " " + sw.Elapsed.TotalSeconds;
        }

        #endregion
    }

}
