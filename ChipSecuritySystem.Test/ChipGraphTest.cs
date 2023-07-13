using NUnit.Framework;
using System.Collections.Generic;

namespace ChipSecuritySystem.Test
{
    public class ChipGraphTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Constructor_ValidInput_ReturnsChipGraph()
        {
            var colorChips = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Green),
                new ColorChip(Color.Orange, Color.Purple),
                new ColorChip(Color.Green, Color.Orange)
            };
            var chipGraph = new ChipGraph(colorChips);

            Assert.That(chipGraph, Is.Not.Null);
        }

        [Test]
        public void FindLongestChainBetween_AssementInput_ReturnsLongestChain()
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

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result[0], Is.EqualTo(colorChips[0]));
            Assert.That(result[1], Is.EqualTo(colorChips[2]));
            Assert.That(result[2], Is.EqualTo(colorChips[1]));
        }

        [Test]
        public void FindLongestChainBetween_DoubleSidedChip_ReturnsLongestChain()
        {
            var colorChips = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Green),
                new ColorChip(Color.Orange, Color.Purple),
                new ColorChip(Color.Green, Color.Orange),
                new ColorChip(Color.Orange, Color.Green),
                new ColorChip(Color.Purple, Color.Green),
                new ColorChip(Color.Green, Color.Green)
            };
            var chipGraph = new ChipGraph(colorChips);

            var result = chipGraph.FindLongestChainBetween(Color.Blue, Color.Green);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(5));
            Assert.That(result[0], Is.EqualTo(colorChips[0]));
            Assert.That(result[1], Is.EqualTo(colorChips[2]));
            Assert.That(result[2], Is.EqualTo(colorChips[1]));
            Assert.That(result[3], Is.EqualTo(colorChips[4]));
            Assert.That(result[4], Is.EqualTo(colorChips[5]));
        }

        [Test]
        public void FindLongestChainBetween_OneValidChip_ReturnsLongestChain()
        {
            var colorChips = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Green)
            };
            var chipGraph = new ChipGraph(colorChips);

            var result = chipGraph.FindLongestChainBetween(Color.Blue, Color.Green);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0], Is.EqualTo(colorChips[0]));
        }

        [Test]
        public void FindLongestChainBetween_CyclicalChips_ReturnsLongestChain()
        {
            var colorChips = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Red),
                new ColorChip(Color.Red, Color.Blue),
                new ColorChip(Color.Blue, Color.Green),
                new ColorChip(Color.Green, Color.Blue)
            };
            var chipGraph = new ChipGraph(colorChips);

            var result = chipGraph.FindLongestChainBetween(Color.Blue, Color.Green);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result[0], Is.EqualTo(colorChips[0]));
            Assert.That(result[1], Is.EqualTo(colorChips[1]));
            Assert.That(result[2], Is.EqualTo(colorChips[2]));
        }

        [Test]
        public void FindLongestChainBetween_NoStartChip_ReturnsEmpty()
        {
            var colorChips = new List<ColorChip>
            {
                new ColorChip(Color.Red, Color.Orange),
                new ColorChip(Color.Orange, Color.Green),
                new ColorChip(Color.Orange, Color.Blue)
            };
            var chipGraph = new ChipGraph(colorChips);

            var result = chipGraph.FindLongestChainBetween(Color.Blue, Color.Green);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void FindLongestChainBetween_NoEndChip_ReturnsEmpty()
        {
            var colorChips = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Orange),
                new ColorChip(Color.Orange, Color.Orange),
                new ColorChip(Color.Orange, Color.Blue)
            };
            var chipGraph = new ChipGraph(colorChips);

            var result = chipGraph.FindLongestChainBetween(Color.Blue, Color.Green);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }
    }
}