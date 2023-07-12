using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
