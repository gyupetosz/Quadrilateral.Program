using NUnit.Framework;

namespace Library.NUnit
{
    internal sealed class PositionFixture
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test()
        {
            var pos = new Position(1, 2, 3);
            Assert.That(pos, Is.Not.Null);    
        }

    }
}