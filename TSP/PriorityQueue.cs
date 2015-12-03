using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSP
{
    class PriorityQueue
    {
        public int size { get; set; }
        private Node[] nodes;
        public long everNodes { get; set; }
        public long storedStates { get; set; }
        public long prunedStates { get; set; }

        public PriorityQueue(int possibleSize)
        {
            size = 0;
            nodes = new Node[possibleSize + 1];
            everNodes = 0;
            storedStates = 0;
            prunedStates = 0;
        }
        //----------------------------------------------------------------------------------------------------------
        //Constant time lookup - we assume that pointers to all objects in the queue are maintained in an outside 
        //list that is sorted in a more lookupable way. 
        //----------------------------------------------------------------------------------------------------------
        public bool inQueue(Node n)
        {
            if (n.queuePosition == -1)
                return false;

            return nodes[n.queuePosition] == n;
        }

        public void enqueue(Node n, double priority)
        {
            n.queuePriority = priority;
            enqueue(n);
        }
        //----------------------------------------------------------------------------------------------------------
        //O(logN) - the actualy insertion is constant, and the bubble up is worst case O(LogN)
        //----------------------------------------------------------------------------------------------------------
        public void enqueue(Node n)
        {
            size++;
            everNodes++;
            if (size > this.storedStates)
                storedStates = size;

            //We need to expand the array. 
            if (size >= nodes.Length)
            {
                Node[] newNodes = new Node[nodes.Length * 2];
                Array.Copy(nodes, newNodes,nodes.Length);
                nodes = newNodes;
            }

            nodes[size] = n;
            n.queuePosition = size;
            bubbleUp(n);
        }
        //----------------------------------------------------------------------------------------------------------
        //O(logN) - we can only do at most log(N) switches with parent nodes as we move up, after that we're at the head of the tree.
        //----------------------------------------------------------------------------------------------------------
        public void bubbleUp(Node n)
        {
            int parent;
            parent = n.queuePosition / 2;

            while (parent >= 1)
            {
                if (nodes[parent].queuePriority < n.queuePriority)
                    break;

                flip(n, nodes[parent]);

                parent = n.queuePosition / 2;
            }
        }

        public bool hasElements()
        {
            return size > 0;
        }

        //----------------------------------------------------------------------------------------------------------
        //O(LogN) - we can only do at most Log(N) switches before we're at the bottom of the tree. 
        //----------------------------------------------------------------------------------------------------------
        public void sink(Node n)
        {
            Node ProspectiveParent;
            while (true)
            {
                ProspectiveParent = n;

                int evenChild = 2 * n.queuePosition;

                //if this is true then we're already a leaf node.
                if (evenChild > size)
                {
                    break;
                }

                //is our even child closer than I currently am. 
                if (nodes[evenChild].queuePriority < ProspectiveParent.queuePriority)
                {
                    ProspectiveParent = nodes[evenChild];
                }

                int oddChild = evenChild + 1;
                if (oddChild <= size)
                {
                    if (nodes[oddChild].queuePriority < ProspectiveParent.queuePriority)
                    {
                        ProspectiveParent = nodes[oddChild];
                    }
                }

                if (ProspectiveParent != n)
                {
                    flip(n, ProspectiveParent);
                }
                else
                {
                    break;
                }
            }
        }


        //make sure to change priority on the node BEFORE you call this function, and ALWAYS call this function after updating the priority.
        //otherwise the priority queue might not be true anymore. 

        //----------------------------------------------------------------------------------------------------------
        //Worst case O(LogN), it's essentially just a bubble up or sink call. 
        //----------------------------------------------------------------------------------------------------------
        public void updated(Node n)
        {
            //Checking to see if it's in the queue is constant time, 
            if (!inQueue(n))
                return;

            int par = n.queuePosition / 2;
            //----------------------------------------------------------------------------------------------------------
            //bubble up and sink are both worst case log(n) Functions. 
            //----------------------------------------------------------------------------------------------------------
            if (par >= 1 && nodes[par].queuePriority > n.queuePriority)
            {
                bubbleUp(n);
            }
            else
            {
                sink(n);
            }
        }

        public Node pop()
        {
            Node toReturn = nodes[1];
            remove(toReturn);
            return toReturn;
        }

        public Node head()
        {
            return nodes[1];
        }

        public void flip(Node a, Node b)
        {
            nodes[a.queuePosition] = b;
            nodes[b.queuePosition] = a;

            int c = a.queuePosition;
            a.queuePosition = b.queuePosition;
            b.queuePosition = c;
        }

        public void prune(double upperBound)
        {
            for (int i = size; i > 0; i --)
            {
                if (((CostMatrix)nodes[i]).cost >= upperBound)
                {
                    remove(nodes[i]);
                    prunedStates++;
                }
                //else{break;}
            }
        }
        //----------------------------------------------------------------------------------------------------------
        //O(LogN) here - we switch the node to be removed with the last node in the list, remove it, and then call
        //sink on the previous leaf node. That part is O(logN) - everything else is constant time. 
        //----------------------------------------------------------------------------------------------------------
        public void remove(Node n)
        {
            if (!inQueue(n))
                return;

            if (size <= 1)
            {
                size = 0;
                nodes[1] = null;
                return;
            }

            //We need to figure out how to get the node out without messing things up. We could take the value below it and place it in it's place
            //(i.e. sink, but don't call swap so much as move... but that'll screw up things. So we'll just swap to the very back and 
            //then call updated on that guy (the previous leaf node). 

            Node last = nodes[size];
            if (last != n)
            {
                flip(last, n);
            }

            nodes[size] = null;
            size--;

            updated(last);
        }

    }
}
