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
    public class ControllerTests
    {
        [TestMethod()]
        public void Create_Robot_Test()
        {
            //Arrange //Act
            IRobot _robot = Controller.CreateRobot(2, 2, Direction.Top, new Grid(10, 10));

            //Assert
            Assert.AreEqual("2, 2 facing Top", _robot.ToString());
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Create_Robot_OutSide_Grid()
        {
            //Arrange
            Grid _grid = new Grid(10, 10);
            //Act //Assert
            IRobot _robot = Controller.CreateRobot(20, 20, Direction.Top, _grid);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Create_Robot_On_Rock()
        {
            //Arrange
            Grid grid = new Grid(10, 10);
            Controller.PlaceObstacle(grid, 2, 2, new RockConfiguration("Rock"));

            //Act //Assert
            IRobot _robot = Controller.CreateRobot(2, 2, Direction.Top, grid);
        }

        [TestMethod()]
        public void Create_Robot_On_Spinner()
        {
            //Arrange
            Grid grid = new Grid(10, 10);
            Controller.PlaceObstacle(grid, 2, 2, new SpinnerConfiguration("Spinner", 90));

            //Act 
            IRobot _robot = Controller.CreateRobot(2, 2, Direction.Top, grid);

            //Assert
            Assert.AreEqual("2, 2 facing Right", _robot.ToString());
        }

        [TestMethod()]
        public void Create_Robot_On_Hole()
        {
            //Arrange
            Grid grid = new Grid(10, 10);
            Controller.PlaceObstacle(grid, 2, 2, new HoleConfiguration("Hole", 3, 3));

            //Act 
            IRobot _robot = Controller.CreateRobot(2, 2, Direction.Top, grid);

            //Assert
            Assert.AreEqual("3, 3 facing Top", _robot.ToString());
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Create_Robot_On_Hole_Connect_To_Rock()
        {
            //Arrange
            Grid grid = new Grid(10, 10);
            Controller.PlaceObstacle(grid, 2, 2, new HoleConfiguration("Hole", 3, 3));
            Controller.PlaceObstacle(grid, 3, 3, new RockConfiguration("Rock"));

            //Act //Assert
            IRobot _robot = Controller.CreateRobot(2, 2, Direction.Top, grid);
        }

        [TestMethod()]
        public void Create_Robot_On_Hole_Connect_To_Spinner()
        {
            //Arrange
            Grid grid = new Grid(10, 10);
            Controller.PlaceObstacle(grid, 2, 2, new HoleConfiguration("Hole", 3, 3));
            Controller.PlaceObstacle(grid, 3, 3, new SpinnerConfiguration("Spinner", 90));

            //Act 
            IRobot _robot = Controller.CreateRobot(2, 2, Direction.Top, grid);

            //Assert
            Assert.AreEqual("3, 3 facing Right", _robot.ToString());
        }

        [TestMethod()]
        public void Create_Robot_On_Hole_Connect_To_Hole()
        {
            //Arrange
            Grid grid = new Grid(10, 10);
            Controller.PlaceObstacle(grid, 2, 2, new HoleConfiguration("Hole", 3, 3));
            Controller.PlaceObstacle(grid, 3, 3, new HoleConfiguration("Hole", 4, 4));

            //Act 
            IRobot _robot = Controller.CreateRobot(2, 2, Direction.Top, grid);

            //Assert
            Assert.AreEqual("4, 4 facing Top", _robot.ToString());
        }
    }
}