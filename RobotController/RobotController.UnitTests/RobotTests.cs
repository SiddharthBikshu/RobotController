using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotController.Tests
{
    [TestClass()]
    public class RobotTests
    {
        private IRobot robot;
        private Grid grid;

        [TestInitialize]
        public void Initialize()
        {
            grid = new Grid(10, 10);
            Controller.PlaceObstacle(grid, 3, 3, new RockConfiguration("Rock"));
            Controller.PlaceObstacle(grid, 3, 5, new RockConfiguration("Rock"));
            Controller.PlaceObstacle(grid, 5, 6, new RockConfiguration("Rock"));

            Controller.PlaceObstacle(grid, 2, 2, new SpinnerConfiguration("Spinner", 90));
            Controller.PlaceObstacle(grid, 5, 2, new SpinnerConfiguration("Spinner", 180));

            Controller.PlaceObstacle(grid, 1, 4, new HoleConfiguration("Hole", 2, 2));
            Controller.PlaceObstacle(grid, 5, 4, new HoleConfiguration("Hole", 1, 4));
        }

        [TestMethod()]
        public void ExecuteMoveSequenceTest()
        {
            //Arrange
            robot = Controller.CreateRobot(2, 5, Direction.Top, grid);

            //Act 
            robot.ExecuteMoveSequence("R");
            //Assert
            Assert.AreEqual("2, 5 facing Top", robot.ToString());

            //Act 
            robot.ExecuteMoveSequence("FL");
            //Assert
            Assert.AreEqual("2, 2 facing Right", robot.ToString());

            //Act 
            robot.ExecuteMoveSequence("FR");
            //Assert
            Assert.AreEqual("3, 2 facing Right", robot.ToString());

            //Act 
            robot.ExecuteMoveSequence("FF");
            //Assert
            Assert.AreEqual("5, 2 facing Left", robot.ToString());

            //Act 
            robot.ExecuteMoveSequence("LL");
            //Assert
            Assert.AreEqual("2, 2 facing Top", robot.ToString());

            //Act 
            robot.ExecuteMoveSequence("FFF");
            //Assert
            Assert.AreEqual("2, 0 facing Top", robot.ToString());
        }
    }
}