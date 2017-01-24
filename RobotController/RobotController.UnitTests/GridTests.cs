using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotController;

namespace RobotController.UnitTests
{
    [TestClass]
    public class GridTests
    {
        [TestMethod]
        public void Place_Obstacle_Inside_Grid()
        {
            //Arrange
            Grid Grid = new Grid(10, 10);
            IObstacle rock = new Rock();

            //Act
            bool result = Grid.PlaceObstacle(2, 2, rock);
            Location rockLocation = Grid.GetLocation(2, 2);

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual("2, 2 RobotController.Rock", rockLocation.ToString());
        }

        [TestMethod]
        public void Place_Obstacle_Outside_Grid()
        {
            //Arrange
            Grid Grid = new Grid(10, 10);
            IObstacle rock = new Rock();

            //Act
            bool result = Grid.PlaceObstacle(12, 20, rock);

            //Assert
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Get_Obstacle_Outside_Grid()
        {
            //Arrange
            Grid Grid = new Grid(10, 10);

            //Act //Assert
            Location rockLocation = Grid.GetLocation(12, 20);
        }
    }
}