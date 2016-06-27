using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphImplementation
{
    public class Graph
    {
        public List<Node> NodeList;
        public int VisitedNodeNumber;
        public bool isTree { get; set; }
        public Graph(List<string> _list)
        {
            NodeList = new List<Node>();
            VisitedNodeNumber = 0;
            CreateGraph(_list);

            if (VisitedNodeNumber == NodeList.Count())
            {
                isTree = true;
            }
            else
            {
                isTree = false;
            }
        }
        public Node GetNode(int _id)
        {
            foreach (Node node in NodeList)
            {
                if (node.ID == _id)
                    return node;
            }
            return null;
        }
        public void CreateGraph(List<string> _list)
        {
            int first;
            int second;
            foreach (string s in _list)
            {
                first = Convert.ToInt32(s.Split('-')[0]);
                second = Convert.ToInt32(s.Split('-')[1]);

                Node node1 = GetNode(first);
                Node node2 = GetNode(second);

                if (node1 == null)
                {
                    node1 = new Node(first);
                    NodeList.Add(node1);
                }
                if (node2 == null)
                {
                    node2 = new Node(second);
                    NodeList.Add(node2);
                }
                node1.AddNeighbor(node2);
            }
            Traverse(NodeList);
        }
        public void Traverse(List<Node> _list)
        {
            foreach (Node node in _list)
            {
                if (node.Visited == false)
                {
                    VisitedNodeNumber++;
                    node.Visited = true;
                }
                else
                {
                    break;
                }
                Traverse(node.Neighbors.FindAll(a => a.Visited == false));
            }
        }
    }
    public class Node
    {
        public int ID { get; set; }
        public bool Visited;
        public List<Node> Neighbors;
        public Node(int _id)
        {
            ID = _id;
            Visited = false;
            Neighbors = new List<Node>();
        }
        public void AddNeighbor(Node node)
        {
            Neighbors.Add(node);
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            //Your code goes here
            List<string> _list = new List<string>(){
                "1-2",
                "2-1",
                "2-4",
                "4-2",
                "0-2",
                "2-0",
                "2-3",
                "3-2"
            };
            Graph g = new Graph(_list);

            if (g.isTree)
            {
                Console.WriteLine("graph is a tree");
            }
            else
            {
                Console.WriteLine("graph is NOT a tree");

            }
        }
    }
}