using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RobotController
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. Initialize the Grid
            Grid grid = new Grid(10, 10);

            //2. Place some obstacle to the grid
            Controller.PlaceObstacle(grid, 3, 3, new RockConfiguration("Rock"));
            Controller.PlaceObstacle(grid, 3, 5, new RockConfiguration("Rock"));
            Controller.PlaceObstacle(grid, 5, 6, new RockConfiguration("Rock"));

            Controller.PlaceObstacle(grid, 2, 2, new SpinnerConfiguration("Spinner", 90));
            Controller.PlaceObstacle(grid, 5, 2, new SpinnerConfiguration("Spinner", 180));

            Controller.PlaceObstacle(grid, 1, 4, new HoleConfiguration("Hole", 2, 2));
            Controller.PlaceObstacle(grid, 5, 4, new HoleConfiguration("Hole", 1, 4));

            //Walk to robot to explore the grid.
            IRobot robot = Controller.CreateRobot(2, 5, Direction.Top, grid);
            Console.WriteLine(string.Format("Created robot at: {0}", robot.ToString()));
            robot.ExecuteMoveSequence("RFLFRFFLLFFF");

            Console.WriteLine(string.Format("\nRobot Final position is: {0} ", robot.ToString()));
            Console.ReadLine();
        }
    }
}