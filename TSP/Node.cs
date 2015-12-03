using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSP
{
    class Node
    {
        public Node()
        {
            queuePriority = double.MaxValue;
            queuePosition = -1;
        }

        // The priority
        public double queuePriority { get; set; }

        //the Position in the queue.
        public int queuePosition { get; set; }
    }
}
