using System.Collections.Generic;
using System.Linq;

namespace ChipSecuritySystem
{
    public class ChipGraph
    {
        private readonly IReadOnlyList<Node<ColorChip>> graph;

        public ChipGraph(IReadOnlyList<ColorChip> colorChips)
        {
            graph = GenerateGraph(colorChips);
        }

        private IReadOnlyList<Node<ColorChip>> GenerateGraph(IReadOnlyList<ColorChip> colorChips)
        {
            var graph = BuildUnlinkedGraph(colorChips);
            LinkGraph(graph);

            return graph;
        }

        private List<Node<ColorChip>> BuildUnlinkedGraph(IReadOnlyList<ColorChip> colorChips)
        {
            var graph = new List<Node<ColorChip>>();

            foreach(var colorChip in colorChips)
            {
                graph.Add(new Node<ColorChip>(colorChip));
            }

            return graph;
        }

        private void LinkGraph(IList<Node<ColorChip>> graph)
        {
            foreach (var currentChipNode in graph)
            {
                foreach (var otherChipNode in graph)
                {
                    if (IsValidEdge(currentChipNode.Value, otherChipNode.Value))
                    {
                        currentChipNode.AddEdge(otherChipNode);
                    }
                }

            }
        }

        private bool IsValidEdge(ColorChip orginChip, ColorChip edgeChip)
        {
            return (orginChip.EndColor == edgeChip.StartColor) && (orginChip != edgeChip);
        }

        public IList<ColorChip> FindLongestChainBetween(Color startColor, Color endColor)
        {
            var sortedGraph = TopologySortGraph();
            var startingIndex = sortedGraph.FindIndex(node => node.StartColor == startColor);
            var endingIndex = sortedGraph.FindLastIndex(node => node.EndColor == endColor);

            if (startingIndex == -1 || endingIndex == -1)
            {
                return new List<ColorChip>();
            }

            return FilterGraph(sortedGraph, startingIndex, endingIndex, endColor);
        }

        private List<ColorChip> TopologySortGraph()
        {
            var visited = new HashSet<Node<ColorChip>>();
            var dependencyList = new LinkedList<Node<ColorChip>>();

            foreach (var node in graph)
            {
                if (!visited.Contains(node))
                {
                    DepthFirstDependencySearch(node, dependencyList, visited);
                }
            }

            var sortedGraph = new List<ColorChip>();
            foreach (var dependency in dependencyList)
            {
                sortedGraph.Add(dependency.Value);
            }

            return sortedGraph;
        }

        private void DepthFirstDependencySearch(Node<ColorChip> node, LinkedList<Node<ColorChip>> dependencyList, HashSet<Node<ColorChip>> visited)
        {
            visited.Add(node);

            foreach (var edge in node.Edges)
            {
                if (!visited.Contains(edge))
                {
                    DepthFirstDependencySearch(edge, dependencyList, visited);
                }
            }

            dependencyList.AddFirst(node);
        }

        private IList<ColorChip> FilterGraph(List<ColorChip> graph, int startingIndex, int endingIndex, Color endColor)
        {
            var currentSequence = new List<ColorChip>()
            {
                graph[startingIndex]
            };
            var longestSequence = new List<ColorChip>();

            if (graph[startingIndex].EndColor == endColor)
            {
                longestSequence.Add(graph[startingIndex]);
            }

            for (int i = startingIndex + 1; i < graph.Count; i++)
            {
                if (i > endingIndex)
                {
                    break;
                }

                while (currentSequence.Last().EndColor != graph[i].StartColor)
                {
                    currentSequence.Remove(currentSequence.Last());
                }

                if (currentSequence.Last().EndColor == graph[i].StartColor)
                {
                    currentSequence.Add(graph[i]);

                    if (graph[i].EndColor == endColor && currentSequence.Count > longestSequence.Count)
                    {
                        longestSequence = currentSequence;
                    }
                }
            }

            return longestSequence;
        }
    }
}
