using System.Collections.Generic;
using System.Linq;

namespace ChipSecuritySystem
{
    public class ChipGraph
    {
        private readonly IReadOnlyList<ColorChip> colorChips;
        private readonly IReadOnlyList<Node<int>> graph;

        public ChipGraph(IReadOnlyList<ColorChip> colorChips)
        {
            this.colorChips = colorChips;
            this.graph = GenerateGraph(colorChips);
        }

        private IReadOnlyList<Node<int>> GenerateGraph(IReadOnlyList<ColorChip> colorChips)
        {
            var graph = BuildUnlinkedGraph(colorChips.Count);
            LinkGraph(graph, colorChips);

            return graph;
        }

        private List<Node<int>> BuildUnlinkedGraph(int colorChipCount)
        {
            var graph = new List<Node<int>>();

            for (int i = 0; i < colorChipCount; i++)
            {
                graph.Add(new Node<int>(i));
            }

            return graph;
        }

        private void LinkGraph(IList<Node<int>> graph, IReadOnlyList<ColorChip> colorChips)
        {
            for (int currentChip = 0; currentChip < colorChips.Count; currentChip++)
            {
                for (int otherChip = 0; otherChip < colorChips.Count; otherChip++)
                {
                    if (IsValidEdge(colorChips[currentChip], currentChip, colorChips[otherChip], otherChip))
                    {
                        graph[currentChip].AddEdge(graph[otherChip]);
                    }
                }

            }
        }

        private bool IsValidEdge(ColorChip orginChip, int originChipIndex, ColorChip edgeChip, int edgeChipIndex)
        {
            return (orginChip.EndColor == edgeChip.StartColor) && (originChipIndex != edgeChipIndex);
        }

        public IList<ColorChip> FindLongestChainBetween(Color startColor, Color endColor)
        {
            var currentLongest = new List<ColorChip>();

            foreach (var startingNode in TakeNodeWithChipStartingWith(startColor))
            {
                ProcessStartingNode(startingNode, currentLongest, endColor);
            }

            return currentLongest;
        }

        private void ProcessStartingNode(Node<int> startingNode, List<ColorChip> currentLongest, Color endColor)
        {
            bool[] visitedNodes = new bool[graph.Count];
            Stack<Node<int>> nodesToTraverse = new Stack<Node<int>>();
            List<Node<int>> sequence = new List<Node<int>>
                {
                    startingNode
                };

            foreach (var edge in startingNode.Edges)
            {
                nodesToTraverse.Push(edge);
            }

            visitedNodes[startingNode.Value] = true;

            while (nodesToTraverse.Count > 0)
            {
                ProcessNode(nodesToTraverse, visitedNodes, currentLongest, sequence, endColor);
            }
        }

        private void ProcessNode(Stack<Node<int>> nodesToTraverse, bool[] visitedNodes, List<ColorChip> currentLongest, List<Node<int>> sequence, Color endColor)
        {
            if (!visitedNodes[nodesToTraverse.Peek().Value])
            {
                var node = nodesToTraverse.Pop();
                sequence.Add(node);
                visitedNodes[node.Value] = true;

                if (colorChips[node.Value].EndColor == endColor && sequence.Count > currentLongest.Count)
                {
                    currentLongest.Clear();

                    foreach (var step in sequence)
                    {
                        currentLongest.Add(colorChips[step.Value]);
                    }

                    sequence.Remove(node);
                }

                foreach (var edge in node.Edges)
                {
                    nodesToTraverse.Push(edge);
                }

            }
            else
            {
                nodesToTraverse.Pop();
            }
        }

        private IEnumerable<Node<int>> TakeNodeWithChipStartingWith(Color startColor)
        {
            for (int i = 0; i < colorChips.Count; i++)
            {
                if (colorChips[i].StartColor == startColor)
                {
                    yield return graph[i];
                }
                else
                {
                    yield break;
                }
            }
        }
    }
}
