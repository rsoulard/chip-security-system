using System.Collections.Generic;

namespace ChipSecuritySystem
{
    public class Node<T>
    {
        public T Value { get; private set; }
        public IReadOnlyList<Node<T>> Edges => edges;

        private readonly List<Node<T>> edges = new List<Node<T>>();

        public Node(T value)
        {
            Value = value;
        }

        public void AddEdge(Node<T> otherNode)
        {
            edges.Add(otherNode);
        }
    }
}
