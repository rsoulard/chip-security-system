using System;
using System.Collections.Generic;

namespace ChipSecuritySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var colorChips = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Red, Color.Green),
                new ColorChip(Color.Yellow, Color.Red),
                new ColorChip(Color.Orange, Color.Purple)
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
                Console.Write(colorChip.ToString());

                if (results.IndexOf(colorChip) != results.Count - 1)
                {
                    Console.Write(" -> ");
                }
            }

            Console.WriteLine();
        }
    }
}
