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
    }
}