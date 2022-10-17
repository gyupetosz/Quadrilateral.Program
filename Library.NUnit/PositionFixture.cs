using NUnit.Framework;
using System.IO;
using System;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Application;
using Program = Application.Program;
using System.Text;
using System.Security.Principal;
using System.Collections.Generic;

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

        [Test]
        public void ReadingInvalidLine()
        {
            Assert.False(Program.Parse("invalid", out double result));
        }

        [Test]
        public void ReadingOutOfRangeLatitudeValue()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("125,3");
            builder.AppendLine("15,3");
            builder.AppendLine("25,3");
            var stringReader = new StringReader(builder.ToString());
            Console.SetIn(stringReader);
            Assert.Throws<ArgumentOutOfRangeException>(() => Program.ReadPositions());

        }
        [Test]
        public void ReadingOutOfRangeLongitudeValue()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("11,3");
            builder.AppendLine("-85,3");
            builder.AppendLine("25,3");
            var stringReader = new StringReader(builder.ToString());
            Console.SetIn(stringReader);
            Assert.Throws<ArgumentOutOfRangeException>(() => Program.ReadPositions());

        }
        [Test]
        public void ReadingDoubleValues()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("11,3");
            builder.AppendLine("12,3");
            builder.AppendLine("25,3");
            builder.AppendLine("11,3");
            builder.AppendLine("12,3");
            builder.AppendLine("25,3");
            var stringReader = new StringReader(builder.ToString());
            Console.SetIn(stringReader);
            Assert.Throws<ArgumentException>(() => Program.ReadPositions());

        }

        [Test]
        public void Ordered()
        {
            List<Position> positions = new List<Position>();
            positions.Add(new Position(90.0, 180.0, 5.0));
            positions.Add(new Position(-90.0, 10.0, 5.0));
            positions.Add(new Position(0, 0, 5.0));
            positions = Program.OrderElements(positions);
            Assert.True((positions[0].Latitude > positions[1].Latitude));
        }

        [Test]
        public void Distance()
        {
            Position p1 = new Position(53.32055555555556, 1.7297222222222221, 1);
            Position p2 = new Position(53.31861111111111, 1.6997222222222223, 1);
            double result = Calculations.Distance(p1, p2);
            Assert.AreEqual(2006.6131958704568, result, 0.00000001);
        }

        [Test]
        public void ToRadians()
        {
            double radians = Calculations.ToRadians(90);
            Assert.AreEqual(1.5707963267948966, radians);
        }
    }

}