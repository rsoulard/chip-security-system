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
            var result = new List<ColorChip>();
            var sortedGraph = TopologySortGraph();
            var startingIndex = sortedGraph.FindIndex(0, sortedGraph.Count, node => node.StartColor == startColor);

            result.Add(sortedGraph[startingIndex]);

            if (sortedGraph[startingIndex].EndColor == endColor)
            {
                currentLongest = new List<ColorChip>
                {
                    sortedGraph[startingIndex]
                };
            }

            for (int i = startingIndex + 1; i < sortedGraph.Count; i++)
            {
                while (result.Last().EndColor != sortedGraph[i].StartColor)
                {
                    result.Remove(result.Last());
                }

                if (result.Last().EndColor == sortedGraph[i].StartColor)
                {
                    result.Add(sortedGraph[i]);

                    if (sortedGraph[i].EndColor == endColor && result.Count > currentLongest.Count)
                    {
                        currentLongest = result;
                    }
                }
            }

            return currentLongest;

        }

        private List<ColorChip> TopologySortGraph()
        {
            var visited = new bool[graph.Count];
            var depList = new LinkedList<Node<int>>();
            foreach (var node in graph)
            {
                if (!visited[node.Value])
                {
                    DfsVisit(node, depList, visited);
                }
            }

            var ret = new List<ColorChip>();
            foreach (var dep in depList)
            {
                ret.Add(colorChips[dep.Value]);
            }

            return ret;
        }

        private void DfsVisit(Node<int> node, LinkedList<Node<int>> depList, bool[] visited)
        {
            visited[node.Value] = true;
            foreach (var edge in node.Edges)
            {
                if (!visited[edge.Value])
                {
                    DfsVisit(edge, depList, visited);
                }
            }

            depList.AddFirst(node);
        }
    }
}
