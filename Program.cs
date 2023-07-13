using System;
using System.Collections.Generic;

namespace ChipSecuritySystem
{
    static class Program
    {
        static void Main()
        {
            var colorChips = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Green),
                new ColorChip(Color.Orange, Color.Purple),
                new ColorChip(Color.Green, Color.Orange),
                new ColorChip(Color.Orange, Color.Green),
                new ColorChip(Color.Purple, Color.Green),
                new ColorChip(Color.Green, Color.Green),
                new ColorChip(Color.Orange, Color.Orange),
            };
            var chipGraph = new ChipGraph(colorChips);

            var result = chipGraph.FindLongestChainBetween(Color.Blue, Color.Green);

            PrintResults(result);

            Console.WriteLine("Press [Enter] to close.");
            Console.ReadLine();
        }

        static void PrintResults(IList<ColorChip> results)
        {
            if (results is null || results.Count == 0)
            {
                Console.WriteLine(Constants.ErrorMessage);
                return;
            }

            foreach(var colorChip in results)
            {
                Console.Write($"[{colorChip}]");

                if (results.IndexOf(colorChip) != results.Count - 1)
                {
                    Console.Write(" -> ");
                }
            }

            Console.WriteLine();
        }
    }
}
