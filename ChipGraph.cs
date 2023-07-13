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
            var visited = new bool[graph.Count];
            var depList = new LinkedList<Node<int>>();

            foreach (var node in graph)
            {
                if (!visited[node.Value])
                {
                    DepthFirstDependencySearch(node, depList, visited);
                }
            }

            var ret = new List<ColorChip>();
            foreach (var dep in depList)
            {
                ret.Add(colorChips[dep.Value]);
            }

            return ret;
        }

        private void DepthFirstDependencySearch(Node<int> node, LinkedList<Node<int>> depList, bool[] visited)
        {
            visited[node.Value] = true;

            foreach (var edge in node.Edges)
            {
                if (!visited[edge.Value])
                {
                    DepthFirstDependencySearch(edge, depList, visited);
                }
            }

            depList.AddFirst(node);
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
