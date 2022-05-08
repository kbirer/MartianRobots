using MartianRobots.Movements;
using Moq;
using NUnit.Framework;
using System;

namespace MartianRobots.Tests
{
    public class Tests
    {
        [Test]
        public void GridWidthAndHeightCantBeEmpty()
        {
            //Arrange
            var mockedInputReader = new Mock<IInputReader>();
            mockedInputReader.Setup(t => t.ReadLine()).Returns<string>(null);
            var safetyHandler = new Mock<IRobotMovementSafetyHandler>();

            var manager = new RobotControlManager(mockedInputReader.Object, safetyHandler.Object);

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                manager.ReadData();
            });
        }

        [Test]
        public void GridWidthAndHeightShouldHaveTwoCoordinates()
        {
            //Arrange
            var mockedInputReader = new Mock<IInputReader>();
            mockedInputReader.Setup(t => t.ReadLine()).Returns("1");
            var safetyHandler = new Mock<IRobotMovementSafetyHandler>();

            var manager = new RobotControlManager(mockedInputReader.Object, safetyHandler.Object);

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                manager.ReadData();
            });
        }

        [Test]
        public void GridWidthAndHeightShouldHaveTwoCoordinatesWithIntegers()
        {
            //Arrange
            var mockedInputReader = new Mock<IInputReader>();
            mockedInputReader.Setup(t => t.ReadLine()).Returns("1 c");
            var safetyHandler = new Mock<IRobotMovementSafetyHandler>();

            var manager = new RobotControlManager(mockedInputReader.Object, safetyHandler.Object);

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                manager.ReadData();
            });
        }

        [Test]
        public void GridWidthAndHeightShouldHaveTwoCoordinatesWithPositiveIntegers()
        {
            //Arrange
            var mockedInputReader = new Mock<IInputReader>();
            mockedInputReader.Setup(t => t.ReadLine()).Returns("1 -2");
            var safetyHandler = new Mock<IRobotMovementSafetyHandler>();

            var manager = new RobotControlManager(mockedInputReader.Object, safetyHandler.Object);

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                manager.ReadData();
            });
        }
    }
}