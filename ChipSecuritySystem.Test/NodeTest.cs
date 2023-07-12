using NUnit.Framework;

namespace ChipSecuritySystem.Test
{
    public class NodeTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddEdge_ValidNode_AddsEdge()
        {
            var originalNode = new Node<int>(1);
            var edgeNode = new Node<int>(2);

            originalNode.AddEdge(edgeNode);

            Assert.That(originalNode.Edges, Has.One.EqualTo(edgeNode));
        }
    }
}
